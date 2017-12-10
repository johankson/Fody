using System.IO;
using NUnit.Framework;

[TestFixture]
public class NugetConfigReaderTest
{
    [Test]
    public void WithNugetConfig()
    {
        var solutionDir = Path.GetFullPath(Path.Combine(AssemblyLocation.CurrentDirectory, "../../../Fody/NugetPackagePathFinder/FakeSolutionWithNugetConfig"));
        var packagesPathFromConfig = NugetConfigReader.GetPackagesPathFromConfig(solutionDir);
        Assert.IsTrue(packagesPathFromConfig.EndsWith("FromNugetConfig"));
    }

    [Test]
    public void FakeSolutionWithNestedNugetConfig()
    {
        var solutionDir = Path.GetFullPath(Path.Combine(AssemblyLocation.CurrentDirectory, "../../../Fody/NugetPackagePathFinder/FakeSolutionWithNestedNugetConfig"));
        var packagesPathFromConfig = NugetConfigReader.GetPackagesPathFromConfig(solutionDir);
        Assert.IsTrue(packagesPathFromConfig.EndsWith("FromNugetConfig"));
    }

    [Test]
    public void WithNugetConfigInTree()
    {
        var solutionDir = Path.GetFullPath(Path.Combine(AssemblyLocation.CurrentDirectory, "../../../Fody/NugetPackagePathFinder/FakeSolutionWithNugetConfig/Foo"));
        var packagesPathFromConfig = NugetConfigReader.GetPackagesPathFromConfig(solutionDir);
        Assert.IsTrue(packagesPathFromConfig.EndsWith("FromNugetConfig"));
    }

    [Test]
    public void WithNoNugetConfigInTree()
    {
        var solutionDir = Path.GetFullPath(Path.Combine(AssemblyLocation.CurrentDirectory, "../../../Fody/NugetPackagePathFinder/FakeSolutionNoNugetConfig"));
        var packagesPathFromConfig = NugetConfigReader.GetPackagesPathFromConfig(solutionDir);
        Assert.IsNull(packagesPathFromConfig);
    }

    [Test]
    public void NugetConfigWithRepoNode()
    {
        var configPath = Path.Combine(AssemblyLocation.CurrentDirectory, @"Fody\NugetConfigWithRepoNode.txt");
        var packagesPathFromConfig = NugetConfigReader.GetPackagePath(configPath);
        Assert.AreEqual(Path.Combine(AssemblyLocation.CurrentDirectory, @"Fody\repositoryPathValue"), packagesPathFromConfig);
    }

    [Test]
    public void NugetConfigWithKeyNodeEmpty()
    {
        var configPath = Path.Combine(AssemblyLocation.CurrentDirectory, @"Fody\NugetConfigWithKeyNodeEmpty.txt");
        var packagesPathFromConfig = NugetConfigReader.GetPackagePath(configPath);
        Assert.IsNull(packagesPathFromConfig);
    }

    [Test]
    public void NugetConfigWithRepoNodeEmpty()
    {
        var configPath = Path.Combine(AssemblyLocation.CurrentDirectory, "Fody/NugetConfigWithRepoNodeEmpty.txt");
        var packagesPathFromConfig = NugetConfigReader.GetPackagePath(configPath);
        Assert.IsNull(packagesPathFromConfig);
    }

    [Test]
    public void NugetConfigWithKeyNode()
    {
        var configPath = Path.Combine(AssemblyLocation.CurrentDirectory, @"Fody\NugetConfigWithKeyNode.txt");
        var packagesPathFromConfig = NugetConfigReader.GetPackagePath(configPath);
        Assert.AreEqual(Path.Combine(AssemblyLocation.CurrentDirectory, @"Fody\repositoryPathValue"), packagesPathFromConfig);
    }

    [Test]
    public void NugetConfigWithPlaceholderRemovesToken()
    {
        var configPath = Path.Combine(AssemblyLocation.CurrentDirectory, @"Fody\NugetConfigWithPlaceholder.txt");
        var packagesPathFromConfig = NugetConfigReader.GetPackagePath(configPath);
        Assert.False(packagesPathFromConfig.Contains("$"));
    }

    [Test]
    public void NugetConfigWithPlaceholderUsesDirectory()
    {
        var configPath = Path.Combine(AssemblyLocation.CurrentDirectory, @"Fody\NugetConfigWithPlaceholder.txt");
        var packagesPathFromConfig = NugetConfigReader.GetPackagePath(configPath);
        Assert.AreEqual(Path.Combine(AssemblyLocation.CurrentDirectory, @"Fody\Packages"), packagesPathFromConfig);
    }
}