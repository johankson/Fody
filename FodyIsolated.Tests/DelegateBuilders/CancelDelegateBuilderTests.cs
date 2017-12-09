using System;
using NUnit.Framework;

[TestFixture]
public class CancelDelegateBuilderTests
{
    [Test]
    public void Should_not_throw_When_no_cancel_method()
    {
        typeof(NoCancelClass).BuildCancelDelegate();
    }

    public class NoCancelClass
    {
    }

    [Test]
    public void Find_and_execute()
    {
        var action = typeof(ValidClass).BuildCancelDelegate();
        action(new ValidClass());
    }

    [Test]
    public void Find_and_execute_from_interface()
    {
        var action = typeof(WeaverFromBase).BuildCancelDelegate();
        action(new WeaverFromBase());
    }

    public class ValidClass
    {
        public void Cancel()
        {
        }
    }

    [Test]
    public void Should_not_throw_When_cancel_is_not_public()
    {
        typeof(NonPublicClass).BuildCancelDelegate();
    }

    public class NonPublicClass
    {
// ReSharper disable UnusedMember.Local
        void Cancel()
// ReSharper restore UnusedMember.Local
        {
        }
    }

    [Test]
    public void Should_not_throw_When_method_is_static()
    {
        typeof(StaticExecuteClass).BuildCancelDelegate();
    }

    public class StaticExecuteClass
    {
        public static void Cancel()
        {
        }
    }

    [Test]
    public void Should_thrown_inner_exception_When_delegate_is_executed()
    {
        var action = typeof (ThrowFromExecuteClass).BuildCancelDelegate();
        Assert.Throws<NullReferenceException>(() => action(new ThrowFromExecuteClass()));
    }

    public class ThrowFromExecuteClass
    {
        public void Cancel()
        {
            throw new NullReferenceException();
        }
    }
}