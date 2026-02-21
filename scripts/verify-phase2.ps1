Write-Host "=== Algo-DDD Phase 2 Verification ==="

# Helper function to check file existence anywhere under CoreContexts\Strategy
function Verify-File($fileName, $label) {
    $result = Get-ChildItem -Recurse -Path "CoreContexts\Strategy" -Filter $fileName -ErrorAction SilentlyContinue
    if ($result) {
        Write-Host "✅ $label exists ($($result.FullName))" -ForegroundColor Green
    } else {
        Write-Host "❌ $label missing" -ForegroundColor Red
    }
}

# Domain Entities
Verify-File "StrategyEntity.cs" "StrategyEntity"
Verify-File "Signal.cs" "Signal entity"
Verify-File "BacktestResult.cs" "BacktestResult entity"
Verify-File "TimeFrame.cs" "TimeFrame entity"

# Aggregates
Verify-File "StrategyAggregate.cs" "StrategyAggregate"

# CQRS Handlers
Verify-File "CreateStrategyHandler.cs" "CreateStrategyHandler"
Verify-File "UpdateSignalHandler.cs" "UpdateSignalHandler"

# Application Project
Verify-File "Application.csproj" "Application project"

# Unit Test Scaffolding
Verify-File "StrategyTests.cs" "StrategyTests"
Verify-File "SignalTests.cs" "SignalTests"

Write-Host "`n=== Verification Complete ==="
Write-Host "✅ Phase 2 verification finished." -ForegroundColor Green
