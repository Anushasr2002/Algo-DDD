# Cleanup-Duplicates.ps1
# Removes duplicate Strategy files

# Paths to delete
$filesToDelete = @(
    "C:\Algo-DDD\CoreContexts\Strategy\src\AlgoDDD.Strategy\Domain\Repositories\StrategyRepository.cs",
    "C:\Algo-DDD\CoreContexts\Strategy\src\AlgoDDD.Strategy\Domain\Repositories\IstrategyRepository.cs",
    "C:\Algo-DDD\CoreContexts\Strategy\src\AlgoDDD.Strategy\AlgoDDD.Strategy.Application\obj\Debug\net8.0\.NETCoreApp,Version=v8.0.AssemblyAttributes.cs",
    "C:\Algo-DDD\CoreContexts\Strategy\src\AlgoDDD.Strategy\AlgoDDD.Strategy.Domain\obj\Debug\net8.0\.NETCoreApp,Version=v8.0.AssemblyAttributes.cs"
)

foreach ($file in $filesToDelete) {
    if (Test-Path $file) {
        Remove-Item $file -Force
        Write-Host "Deleted: $file"
    } else {
        Write-Host "Not found: $file"
    }
}
