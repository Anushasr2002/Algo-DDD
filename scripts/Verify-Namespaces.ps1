# Root path of your Strategy project
$root = "C:\Algo-DDD\CoreContexts\Strategy\src\AlgoDDD.Strategy"

# Expected namespace base
$baseNamespace = "AlgoDDD.Strategy.Domain"

# Scan all .cs files under Domain
Get-ChildItem -Path $root\Domain -Recurse -Filter *.cs | ForEach-Object {
    $file = $_.FullName
    $content = Get-Content $file

    # Extract namespace line
    $namespaceLine = $content | Where-Object { $_ -match "namespace " }

    if ($namespaceLine) {
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

        if ($namespaceLine -notmatch $expected) {
            Write-Host "❌ Namespace mismatch in $file"
            Write-Host "   Found: $namespaceLine"
            Write-Host "   Expected: namespace $expected"
        } else {
            Write-Host "✅ $file namespace OK"
        }
    } else {
        Write-Host "⚠️ No namespace declared in $file"
    }
}
