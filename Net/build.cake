var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var artifactsDir = Argument("artifactsDir", MakeAbsolute(new DirectoryPath("artifacts")));

Task("Clean")
    .Does(() =>
{
    var sourceDirectories = GetDirectories("src/*");
    foreach (var dir in sourceDirectories)
    {
        CleanDirectory(dir + "/bin");
    }
    var testDirectories = GetDirectories("test/*");
    foreach (var dir in testDirectories)
    {
        CleanDirectory(dir + "/bin");
    }
    CleanDirectory(artifactsDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    DotNetCoreRestore();
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    var settings = new DotNetCoreBuildSettings
    {
        Configuration = configuration
    };
    DotNetCoreBuild("", settings);
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    var testProjects = GetFiles("test/**/*.csproj");
    foreach(var test in testProjects)
    {
        DotNetCoreTest(test.FullPath);
    }
});

Task("Pack")
    .IsDependentOn("Test")
    .Does(() =>
{
    var srcProjects = GetFiles("src/**/*.csproj");
    var settings = new DotNetCorePackSettings
    {
        Configuration = configuration,
        OutputDirectory = artifactsDir
    };
    foreach(var proj in srcProjects)
    {
        DotNetCorePack(proj.FullPath, settings);
    }    
});

Task("Default")
    .IsDependentOn("Test");

RunTarget(target);
