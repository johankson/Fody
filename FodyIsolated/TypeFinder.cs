using System;
using System.Linq;
using System.Reflection;
using Fody;

public static class TypeFinder
{
    public static Type FindType(this Assembly assembly, string typeName)
    {
        try
        {
            return assembly
                .GetTypes()
                .FirstOrDefault(x => x.Name == typeName);
        }
        catch (ReflectionTypeLoadException exception)
        {
            var message = string.Format(
                @"Could not load '{1}' from '{0}' due to ReflectionTypeLoadException.
It is possible a the package needs to be updated.
exception.LoaderExceptions:
{2}", assembly.FullName, typeName, exception.GetLoaderMessages());
            throw new WeavingException(message);
        }
    }
}