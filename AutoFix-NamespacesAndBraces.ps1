# AutoFix-NamespacesAndBraces.ps1
# Repairs namespaces and braces in Strategy project files

$projectPath = "C:\Algo-DDD\CoreContexts\Strategy\src\AlgoDDD.Strategy"
$logFile = "C:\Algo-DDD\FixReport.txt"

# Clear old log
"" | Set-Content $logFile

Get-ChildItem -Path $projectPath -Recurse -Filter *.cs | ForEach-Object {
    $file = $_.FullName
    $content = Get-Content $file
    $fixedContent = @()
    $changes = @()

    foreach ($line in $content) {
        # Fix duplicated namespaces
        if ($line -match "^\s*namespace\s+AlgoDDD\.Strategy\.AlgoDD\.Strategy") {
            $line = $line -replace "AlgoDDD\.Strategy\.AlgoDD\.Strategy", "AlgoDDD.Strategy"
            $changes += "Fixed duplicated namespace in: $file"
        }

        # Insert missing '{' after namespace line
        if ($line -match "^\s*namespace\s+[A-Za-z0-9\._]+$") {
            $line = $line + " {"
            $changes += "Inserted missing '{' after namespace in: $file"
        }

        $fixedContent += $line
    }

    # Count braces
    $openBraces  = ($fixedContent | Select-String -Pattern "{").Count
    $closeBraces = ($fixedContent | Select-String -Pattern "}").Count

    # Balance braces at end of file
    if ($openBraces -gt $closeBraces) {
        $diff = $openBraces - $closeBraces
        for ($i = 0; $i -lt $diff; $i++) {
            $fixedContent += "}"
        }
        $changes += "Added $diff closing brace(s) to file: $file"
    }

    # Save fixed content back to file
    Set-Content -Path $file -Value $fixedContent

    # Log changes
    if ($changes.Count -gt 0) {
        Add-Content -Path $logFile -Value ($changes -join "`n")
    }
}
