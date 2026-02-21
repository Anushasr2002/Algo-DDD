# Root path of your Strategy project
$root = "C:\Algo-DDD\CoreContexts\Strategy\src\AlgoDDD.Strategy"

# Expected namespace base
$baseNamespace = "AlgoDDD.Strategy.Domain"

# Scan all .cs files under Domain
Get-ChildItem -Path "$root\Domain" -Recurse -Filter *.cs | ForEach-Object {
    $file = $_.FullName
    $content = Get-Content $file

    # Determine expected namespace based on folder
    if ($file -like "*\Entities\*") {
        $expected = "$baseNamespace.Entities"
    } elseif ($file -like "*\ValueObjects\*") {
        $expected = "$baseNamespace.ValueObjects"
    } elseif ($file -like "*\Services\*") {
        $expected = "$baseNamespace.Services"
    } else {
        $expected = $baseNamespace
    }

    # Replace namespace line
    $newContent = $content | ForEach-Object {
        if ($_ -match "namespace ") {
            "namespace $expected"
        } else {
            $_
        }
    }

    # Write back to file
    Set-Content -Path $file -Value $newContent -Encoding UTF8

    Write-Host "✅ Fixed namespace in $file → namespace $expected"
}
