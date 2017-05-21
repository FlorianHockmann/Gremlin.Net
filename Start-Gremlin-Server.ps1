$gremlinServerUrl = 'https://repository.sonatype.org/service/local/artifact/maven/redirect?r=central-proxy&g=org.apache.tinkerpop&a=gremlin-server&e=zip&c=distribution&v=LATEST'
$workingDirectory = "C:/Gremlin.Net"
$gremlinServerArchive = "$workingDirectory/gremlin-server.zip"

Write-Output "Check if directory $workingDirectory exists..."
if(!(Test-Path -Path $workingDirectory)){
    Write-Output "Directory doesn't exist, creating it now."
    New-Item -ItemType Directory -Path $workingDirectory
}
else {
    Write-Output "Directory exists, nothing to do."
}
    
Write-Output "Check if archive $gremlinServerArchive exists already..."
if (!(Test-Path $gremlinServerArchive)){
    Write-Output "Archive doesn't exist, downloading it from $gremlinServerUrl"
    Invoke-WebRequest -Uri $gremlinServerUrl -OutFile $gremlinServerArchive
    Write-Output "Download finished."
}
else {
    Write-Output "Archive exists, nothing to do."
}

Add-Type -assembly “system.io.compression.filesystem”
Write-Output "Check if an extracted version exists already..."
$zipArchive = [io.compression.zipfile]::OpenRead($gremlinServerArchive)
$archiveRootDirectory = $zipArchive.Entries[0]
$gremlinServerDirectory = "$workingDirectory/$archiveRootDirectory"
if (!(Test-Path -Path $gremlinServerDirectory))
{
    Write-Output "No extracted version found, begin extracting..."
    [io.compression.zipfile]::ExtractToDirectory($gremlinServerArchive, $workingDirectory)
    Write-Output "Extraction finished."
}
else {
    Write-Output "Found an extracted version, nothing to do here."
}

$gremlinServerStartFile = "bin/gremlin-server.bat"
Write-Output "Starting Gremlin Server from $gremlinServerStartFile"
Write-Output "Switching to directory $gremlinServerDirectory"
$currentDirectory = (Get-Item -Path ".\" -Verbose).FullName
Set-Location $gremlinServerDirectory
Start-Process $gremlinServerStartFile
Write-Output "Gremlin Server should now be running"

$gremlinServerSecureConf = "conf/gremlin-server-secure.yaml"
(Get-Content $gremlinServerSecureConf).Replace("8182", "8183").Replace("enabled: true}","enabled: false}") | Set-Content $gremlinServerSecureConf
Write-Output "Starting Secure Gremlin Server on Port 8183"
Start-Process $gremlinServerStartFile $gremlinServerSecureConf
Write-Output "Secure Gremlin Server should now be running"

Set-Location $currentDirectory