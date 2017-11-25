param (
    [string]$CoverallsRepoToken=$(throw "-CoverallsRepoToken is required."),
    [string]$CoverFilter=$(throw "-CoverFilter is required.")
)

Write-Output "Starting code coverage with filter: $CoverFilter"

$alwaysFilter = "-[xunit*]* -[Microsoft*]* -[dotnet*]* -[NuGet*]* -[Newtonsoft*]* -[Consul*]* -[Nancy*]* -[AngleSharp]* -[csc]* -[Anonymously*]*"
$filter = "$CoverFilter $alwaysFilter"

$packagesPath = $env:USERPROFILE + "\.nuget\packages"
$opencoverPath = $packagesPath + "\OpenCover\4.6.519\tools\OpenCover.Console.exe"
$coverallsPath = $packagesPath + "\coveralls.io\1.3.4\tools\coveralls.net.exe"
$tempPath = "c:\codecoverage"
$tempCoveragePath = $tempPath + "\coverage\"
$tempCoverageFileName = $tempCoveragePath + "coverage.xml"

# create temp path
if (-not (test-path $tempPath) ) {
    mkdir $tempPath | Out-Null
}

# run opencover
Get-ChildItem -Path $PSScriptRoot\..\test -Filter *.csproj -Recurse | ForEach-Object {
    $path = "$tempPath\$($_.Directory.BaseName)"
    if (-not (test-path $path) ) {
        mkdir $path | Out-Null
    }

    $tempBinPath = $path + "\bin\"
    $targetArgs = "`"test -o $tempBinPath $($_.FullName)`""

    if (-not (test-path $tempBinPath) ) {
        mkdir $tempBinPath | Out-Null
    }

    if (-not (test-path $tempCoveragePath) ) {
        mkdir $tempCoveragePath | Out-Null
    }

    & $opencoverPath `
        -register:user `
        -target:"dotnet.exe" `
        -targetargs:$targetArgs `
        -searchdirs:$tempBinPath `
        -output:$tempCoverageFileName `
        -mergebyhash `
        -mergeoutput `
        -skipautoprops `
        -returntargetcode `
        -filter:$filter `
        -hideskipped:Filter `
        -oldstyle 
}

# upload to coveralls.io
Write-Output "Sending code coverage results to coveralls.io"

& $coverallsPath `
    --opencover $tempCoverageFileName `
    --full-sources `
    --repo-token $CoverallsRepoToken

7z a codecoverage.zip $tempCoverageFileName
Push-AppveyorArtifact codecoverage.zip

pip install codecov
codecov -f $tempCoverageFileName -X gcov


