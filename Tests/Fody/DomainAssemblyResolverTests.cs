using Xunit;


public class DomainAssemblyResolverTests
{
    [Fact]
    public void GetAssembly()
    {
        Assert.NotNull( DomainAssemblyResolver.GetAssembly(GetType().Assembly.GetName().FullName));
    }
}