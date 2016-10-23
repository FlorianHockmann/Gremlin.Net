Set-Location "C:\projects\gremlin-net\test\Gremlin.Net.UnitTest"
dotnet test
Set-Location "C:\projects\gremlin-net\test\Gremlin.Net.IntegrationTest"

iex ((Get-ChildItem ($env:USERPROFILE + '\.nuget\packages\OpenCover'))[0].FullName + '\tools\OpenCover.Console.exe' + ' -register:user -target:"C:\Program Files\dotnet\dotnet.exe" -targetargs:"test" -output:coverage.xml -skipautoprops -filter:"+[Gremlin*]*"')

iex ((Get-ChildItem ($env:USERPROFILE + '\.nuget\packages\coveralls.io'))[0].FullName + '\tools\coveralls.net.exe' + ' --opencover coverage.xml')