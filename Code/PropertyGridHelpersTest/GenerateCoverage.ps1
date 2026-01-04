param(
    [ValidateSet("Debug", "Release")]
    [string]$Configuration = "Debug"
)

$ErrorActionPreference = "Stop"

Write-Host "Running coverage script with configuration: $Configuration"
# Only run if target framework folder is net10.0*
$projectRoot = Split-Path -Parent $MyInvocation.MyCommand.Definition
$targetFramework = "net10.0-windows10.0.17763.0"
$testDll = Join-Path $projectRoot "bin\$Configuration\$targetFramework\PropertyGridHelpersTest.dll"

if (-Not (Test-Path $testDll)) {
    Write-Host "Skipping code coverage for non-net10.0 builds."
    exit 0
}

Remove-Item -Recurse -Force coverage, CoverageReport -ErrorAction Ignore

# Proceed with coverage
dotnet test $PSScriptRoot `
  --no-build `
  --framework net10.0-windows10.0.17763.0 `
  /p:Configuration=$Configuration `
  /p:CollectCoverage=true `
  /p:CoverletOutput=coverage\ `
  /p:UseSourceLink=false `
  /p:CoverletOutputFormat=cobertura

# Full path to ReportGenerator.exe
$rgVersion = "5.5.1"
$reportGeneratorExe = "$env:USERPROFILE\.nuget\packages\reportgenerator\$rgVersion\tools\net10.0\ReportGenerator.exe"

# Paths
if (Test-Path "coverage") {
    $coverageFile = Get-ChildItem -Path "coverage" -Filter "coverage*.xml" | Select-Object -ExpandProperty FullName -First 1
    if (-not $coverageFile) {
        Write-Warning "Coverage file not found in 'coverage' folder. Skipping report generation."
        return 0
    }
} else {
    Write-Warning "Coverage folder not found. Skipping report generation."
    return 0
}
$reportDir = "CoverageReport"
$reportIndex = Join-Path $reportDir "index.htm"

# Run ReportGenerator
if (Test-Path $reportGeneratorExe) {
    & $reportGeneratorExe -reports:$coverageFile -targetdir:$reportDir -reporttypes:Html -log:report.log -verbosity:Verbose
    if (Test-Path $reportIndex) {
        Start-Process $reportIndex
    } else {
        Write-Host "Report generated, but index.htm not found."
    }
} else {
    Write-Error "ReportGenerator.exe not found at $reportGeneratorExe"
}

return 0