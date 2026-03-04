# AutoFix-Preview.ps1
# Reports namespace and brace issues without modifying files

$projectPath = "C:\Algo-DDD\CoreContexts\Strategy\src\AlgoDDD.Strategy"
$logFile = "C:\Algo-DDD\FixReport.txt"

"" | Set-Content $logFile

Get-ChildItem -Path $projectPath -Recurse -Filter *.cs | ForEach-Object {
    $file = $_.FullName
    $content = Get-Content $file

    $openBraces  = ($content | Select-String -Pattern "{").Count
    $closeBraces = ($content | Select-String -Pattern "}").Count

    $issues = @()

    # Detect namespace without '{'
    foreach ($line in $content) {
        if ($line -match "^\s*namespace\s+[A-Za-z0-9\._]+$") {
            $issues += "Namespace missing '{' in: $file"
        }
    }

    # Detect unbalanced braces
    if ($openBraces -ne $closeBraces) {
        $issues += "Brace mismatch in: $file (open=$openBraces, close=$closeBraces)"
    }

    if ($issues.Count -gt 0) {
        Add-Content -Path $logFile -Value ($issues -join "`n")
    }
}
