# Phase 0 Verification Script
Write-Host "=== Algo-DDD Phase 0 Verification ===" -ForegroundColor Cyan

# Check directory structure
$required = @(
    "SharedKernel\Domain\ValueObjects",
    "SharedKernel\Domain\Events",
    "SharedKernel\Domain\Exceptions",
    "SharedKernel\Utils\Validators",
    "SharedKernel\Utils\Extensions"
)

$allExist = $true
foreach ($path in $required) {
    if (Test-Path $path) {
        Write-Host "✅ $path exists" -ForegroundColor Green
    } else {
        Write-Host "❌ $path missing" -ForegroundColor Red
        $allExist = $false
    }
}

# Check for solution file
if (Test-Path "*.sln") {
    Write-Host "✅ Solution file exists" -ForegroundColor Green
} else {
    Write-Host "❌ No solution file found" -ForegroundColor Red
    $allExist = $false
}

# Check for git
if (Test-Path ".git") {
    Write-Host "✅ Git repository initialized" -ForegroundColor Green
} else {
    Write-Host "⚠️  Git repository not found" -ForegroundColor Yellow
}

# Final verdict
Write-Host "`n=== Verification Complete ===" -ForegroundColor Cyan
if ($allExist) {
    Write-Host "✅ Phase 0 appears COMPLETE!" -ForegroundColor Green
} else {
    Write-Host "⚠️  Phase 0 is INCOMPLETE. Missing components above." -ForegroundColor Yellow
    Write-Host "Run the setup commands from Phase 0 to complete." -ForegroundColor Yellow
}