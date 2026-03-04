# Diagnose-Files.ps1
# Reports deeper structural issues in .cs files

$projectPath = "C:\Algo-DDD\CoreContexts\Strategy\src\AlgoDDD.Strategy"
$logFile = "C:\Algo-DDD\FixReport.txt"

"" | Set-Content $logFile

Get-ChildItem -Path $projectPath -Recurse -Filter *.cs | ForEach-Object {
    $file = $_.FullName
    $content = Get-Content $file
    $issues = @()

    # Find first non-using line
    $firstCodeLine = $content | Where-Object { $_ -notmatch "^\s*using " } | Select-Object -First 1
    if ($firstCodeLine -notmatch "^\s*namespace\s+") {
        $issues += "First code line is not a namespace in: $file"
    }

    # Brace balance
    $openBraces  = ($content | Select-String -Pattern "{").Count
    $closeBraces = ($content | Select-String -Pattern "}").Count
    if ($openBraces -ne $closeBraces) {
        $issues += "Brace mismatch in: $file (open=$openBraces, close=$closeBraces)"
    }

    # Check last line
    $lastLine = $content[-1]
    if ($lastLine -notmatch "^\s*}\s*$") {
        $issues += "File does not end with a closing brace in: $file"
    }

    if ($issues.Count -gt 0) {
        Add-Content -Path $logFile -Value ($issues -join "`n")
    }
}
