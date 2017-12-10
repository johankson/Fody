using System.IO;
using Fody;
using NUnit.Framework;

[TestFixture]
public class AssemblyVersionReaderTests
{
    [Test]
    public void BadImage()
    {
        var path = Path.Combine(AssemblyLocation.CurrentDirectory, "Fody/BadAssembly.dll");
        Assert.Throws<WeavingException>(() =>
        {
            AssemblyVersionReader.GetAssemblyVersion(path);
        });
    }
}