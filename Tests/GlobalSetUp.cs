//using System;
//using System.Reflection;
//using Xunit;

//[SetUpFixture]
//public class GlobalSetUp
//{
//    [OneTimeSetUp]
//    public void Setup()
//    {
//        AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
//    }

//    static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
//    {
//        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
//        {
//            if (string.Equals(assembly.FullName, args.Name, StringComparison.OrdinalIgnoreCase))
//            {
//                return assembly;
//            }
//        }
//        return null;
//    }
//}