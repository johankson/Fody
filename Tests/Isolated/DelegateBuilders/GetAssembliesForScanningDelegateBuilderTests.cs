using System;
using System.Collections.Generic;
using Xunit;


public class GetAssembliesForScanningDelegateBuilderTests
{
    [Fact]
    public void Should_Not_throw_When_no_method()
    {
        typeof(NoExecuteClass).BuildGetAssembliesForScanningDelegate();
    }

    public class NoExecuteClass
    {
    }

    [Fact]
    public void Find_and_run()
    {
        var action = typeof(ValidClass).BuildGetAssembliesForScanningDelegate();
        action(new ValidClass());
    }

    [Fact]
    public void Find_and_run_from_interface()
    {
        var action = typeof(WeaverFromBase).BuildGetAssembliesForScanningDelegate();
        action(new WeaverFromBase());
    }

    public class ValidClass
    {
        public IEnumerable<string> GetAssembliesForScanning()
        {
            yield break;
        }
    }

    [Fact]
    public void Should_not_throw_When_method_not_public()
    {
        typeof(NonPublicClass).BuildGetAssembliesForScanningDelegate();
    }

    public class NonPublicClass
    {
        // ReSharper disable UnusedMember.Local
        IEnumerable<string> Execute()
// ReSharper restore UnusedMember.Local
        {
            return null;
        }
    }

    [Fact]
    public void Should_not_throw_When_method_is_static()
    {
        typeof(StaticExecuteClass).BuildGetAssembliesForScanningDelegate();
    }

    public class StaticExecuteClass
    {
        public static IEnumerable<string> GetAssembliesForScanning()
        {
            return null;
        }
    }

    [Fact]
    public void Should_thrown_inner_exception_When_delegate_is_executed()
    {
        var action = typeof (ThrowFromExecuteClass).BuildGetAssembliesForScanningDelegate();
        Assert.Throws<NullReferenceException>(() => action(new ThrowFromExecuteClass()));
    }

    public class ThrowFromExecuteClass
    {
        public IEnumerable<string> GetAssembliesForScanning()
        {
            throw new NullReferenceException();
        }
    }
}