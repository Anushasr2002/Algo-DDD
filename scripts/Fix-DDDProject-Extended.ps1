# Root path of your Strategy project
$root = "C:\Algo-DDD\CoreContexts\Strategy\src\AlgoDDD.Strategy"

# --- Step 1: Ensure correct folder structure ---
$domainPath = Join-Path $root "Domain"
$entitiesPath = Join-Path $domainPath "Entities"
$valueObjectsPath = Join-Path $domainPath "ValueObjects"
$servicesPath = Join-Path $domainPath "Services"
$repositoriesPath = Join-Path $domainPath "Repositories"
$aggregatesPath = Join-Path $domainPath "Aggregates"
$applicationHandlersPath = Join-Path $root "Application\Handlers"

New-Item -ItemType Directory -Force -Path $entitiesPath
New-Item -ItemType Directory -Force -Path $valueObjectsPath
New-Item -ItemType Directory -Force -Path $servicesPath
New-Item -ItemType Directory -Force -Path $repositoriesPath
New-Item -ItemType Directory -Force -Path $aggregatesPath
New-Item -ItemType Directory -Force -Path $applicationHandlersPath

# --- Step 2: Move and rename specific files ---
# StrategyAggregate
Get-ChildItem -Path $entitiesPath -Recurse -Filter "strategyAggregate.cs" | ForEach-Object {
    Move-Item $_.FullName (Join-Path $aggregatesPath "StrategyAggregate.cs") -Force
}

# TimeFrame (should be a ValueObject)
Get-ChildItem -Path $entitiesPath -Recurse -Filter "TimeFrame.cs" | ForEach-Object {
    Move-Item $_.FullName (Join-Path $valueObjectsPath "TimeFrame.cs") -Force
}

# UpdateSignalHandler (belongs in Application\Handlers)
Get-ChildItem -Path $entitiesPath -Recurse -Filter "UpdateSignalHandler.cs" | ForEach-Object {
    Move-Item $_.FullName (Join-Path $applicationHandlersPath "UpdateSignalHandler.cs") -Force
}

# IStrategyRepository (fix name and move to Repositories)
Get-ChildItem -Path $domainPath -Recurse -Filter "IstrategyRepository.cs" | ForEach-Object {
    Move-Item $_.FullName (Join-Path $repositoriesPath "IStrategyRepository.cs") -Force
}

# --- Step 3: Fix namespaces automatically ---
$baseNamespace = "AlgoDDD.Strategy.Domain"

Get-ChildItem -Path $domainPath -Recurse -Filter *.cs | ForEach-Object {
    $file = $_.FullName
    $content = Get-Content $file

    if ($file -like "*\Entities\*") {
        $expected = "$baseNamespace.Entities"
    } elseif ($file -like "*\ValueObjects\*") {
        $expected = "$baseNamespace.ValueObjects"
    } elseif ($file -like "*\Services\*") {
        $expected = "$baseNamespace.Services"
    } elseif ($file -like "*\Repositories\*") {
        $expected = "$baseNamespace.Repositories"
    } elseif ($file -like "*\Aggregates\*") {
        $expected = "$baseNamespace.Aggregates"
    } else {
        $expected = $baseNamespace
    }

    $newContent = $content | ForEach-Object {
        if ($_ -match "namespace ") {
            "namespace $expected"
        } else {
            $_
        }
    }

    Set-Content -Path $file -Value $newContent -Encoding UTF8
    Write-Host "✅ Fixed namespace in $file → namespace $expected"
}

# Fix Application Handlers namespace
Get-ChildItem -Path $applicationHandlersPath -Recurse -Filter *.cs | ForEach-Object {
    $file = $_.FullName
    $content = Get-Content $file
    $expected = "AlgoDDD.Strategy.Application.Handlers"

    $newContent = $content | ForEach-Object {
        if ($_ -match "namespace ") {
            "namespace $expected"
        } else {
            $_
        }
    }

    Set-Content -Path $file -Value $newContent -Encoding UTF8
    Write-Host "✅ Fixed namespace in $file → namespace $expected"
}

Write-Host "🎉 DDD folder structure and namespaces fixed successfully!"
