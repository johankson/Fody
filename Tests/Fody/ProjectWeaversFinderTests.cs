using System.IO;
using ApprovalTests;
using Fody;
using Moq;
using Xunit;


public class ProjectWeaversFinderTests
{
    [Fact]
    public void NotFound()
    {
        var loggerMock = new Mock<BuildLogger>();
        loggerMock.Setup(x => x.LogDebug(It.IsAny<string>()));
        var logger = loggerMock.Object;
        var testDirectory = AssemblyLocation.CurrentDirectory;
        var searchDirectory = Path.Combine(testDirectory, "FodyWeavers.xml");

        var weavingException = Assert.Throws<WeavingException>(() => ConfigFileFinder.FindWeaverConfigs(testDirectory, testDirectory, logger));
        Approvals.Verify(weavingException.Message.Replace(searchDirectory, "SearchDirectory"));
    }
}