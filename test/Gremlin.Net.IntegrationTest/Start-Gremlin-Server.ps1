$gremlinServerUrl = 'https://repository.sonatype.org/service/local/artifact/maven/redirect?r=central-proxy&g=org.apache.tinkerpop&a=gremlin-server&e=zip&c=distribution&v=LATEST'
$gremlinServerDirectory = "C:/Gremlin.Net"
$gremlinServerArchive = "$gremlinServerDirectory/gremlin-server.zip"

Write-Output "Check if directory $gremlinServerDirectory exists..."
if(!(Test-Path -Path $gremlinServerDirectory)){
    Write-Output "Directory doesn't exist, creating it now."
    New-Item -ItemType Directory -Path $gremlinServerDirectory
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
$gremlinServerDirectory = "$gremlinServerDirectory/$archiveRootDirectory"
if (!(Test-Path -Path $gremlinServerDirectory))
{
    Write-Output "No extracted version found, begin extracting..."
    [io.compression.zipfile]::ExtractToDirectory($gremlinServerArchive, $gremlinServerDirectory)
    Write-Output "Extraction finished."
}
else {
    Write-Output "Found an extracted version, nothing to do here."
}

$gremlinServerStartFile = "bin/gremlin-server.bat"
Write-Output "Starting Gremlin Server from $gremlinServerStartFile"
Set-Location -Path $gremlinServerDirectory
Start-Process $gremlinServerStartFile
Write-Output "Gremlin Server should now be running"