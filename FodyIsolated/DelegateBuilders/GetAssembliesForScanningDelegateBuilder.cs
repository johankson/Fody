using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Fody;

public static class GetAssembliesForScanningDelegateBuilder
{
    public static Func<object,IEnumerable<string>> BuildGetAssembliesForScanningDelegate(this Type weaverType)
    {
        if (weaverType.InheritsFromBaseWeaver())
        {
            return weaver =>
            {
                var baseModuleWeaver = (BaseModuleWeaver) weaver;
                return baseModuleWeaver.GetAssembliesForScanning();
            };
        }
        var afterWeavingMethod = weaverType.GetMethod("GetAssembliesForScanning", BindingFlags.Instance | BindingFlags.Public, null, new Type[] { }, null);
        if (afterWeavingMethod == null)
        {
            return o => Enumerable.Empty<string>();
        }

        var target = Expression.Parameter(typeof (object));
        var execute = Expression.Call(Expression.Convert(target, weaverType), afterWeavingMethod);
        return Expression.Lambda<Func<object, IEnumerable<string>>>(execute, target)
                         .Compile();
    }
}