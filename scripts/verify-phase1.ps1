Write-Host "=== Algo-DDD Phase 1 Verification ==="

# Root solution file
if (Test-Path "C:\Algo-DDD\Algo-DDD.sln") {
    Write-Host "✅ Solution file exists (C:\Algo-DDD\Algo-DDD.sln)"
} else {
    Write-Host "❌ Solution file missing"
}

# CoreContexts folder
if (Test-Path "C:\Algo-DDD\CoreContexts") {
    Write-Host "✅ CoreContexts folder exists"
} else {
    Write-Host "❌ CoreContexts folder missing"
}

# Strategy project
if (Test-Path "C:\Algo-DDD\CoreContexts\Strategy\src\AlgoDDD.Strategy\AlgoDDD.Strategy.csproj") {
    Write-Host "✅ Strategy project exists (AlgoDDD.Strategy.csproj)"
} else {
    Write-Host "❌ Strategy project missing"
}

# Tests project
if (Test-Path "C:\Algo-DDD\CoreContexts\Strategy\tests\AlgoDDD.Strategy.Tests\AlgoDDD.Strategy.Tests.csproj") {
    Write-Host "✅ Strategy tests project exists (AlgoDDD.Strategy.Tests.csproj)"
} else {
    Write-Host "❌ Strategy tests project missing"
}

# SupportingContexts folder
if (Test-Path "C:\Algo-DDD\SupportingContexts") {
    Write-Host "✅ SupportingContexts folder exists"
} else {
    Write-Host "❌ SupportingContexts folder missing"
}

Write-Host "`n=== Verification Complete ==="
