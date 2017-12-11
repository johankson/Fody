using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;
using Fody;
using Mono.Cecil;
using Mono.Cecil.Cil;

public static class PropertyDelegateBuilder
{
    public static Action<object, Func<string, TypeDefinition>> BuildSetFindType(this Type weaverType)
    {
        if (weaverType.InheritsFromBaseWeaver())
        {
            return (target, action) =>
            {
                var baseModuleWeaver = (BaseModuleWeaver)target;
                baseModuleWeaver.FindType = action;
            };
        }
        return weaverType.BuildPropertySetDelegate<Func<string, TypeDefinition>>("FindType");
    }

    public static Action<object, XElement> BuildSetConfig(this Type weaverType)
    {
        if (weaverType.InheritsFromBaseWeaver())
        {
            return (target, config) =>
            {
                var baseModuleWeaver = (BaseModuleWeaver) target;
                baseModuleWeaver.Config = config;
            };
        }

        return weaverType.BuildPropertySetDelegate<XElement>("Config");
    }

    public static Action<object, List<string>> BuildSetDefineConstants(this Type weaverType)
    {
        if (weaverType.InheritsFromBaseWeaver())
        {
            return (target, list) =>
            {
                var baseModuleWeaver = (BaseModuleWeaver)target;
                baseModuleWeaver.DefineConstants = list;
            };
        }
        return weaverType.BuildPropertySetDelegate<List<string>>("DefineConstants");
    }

    public static Action<object, string> BuildSetDocumentationFilePath(this Type weaverType)
    {
        if (weaverType.InheritsFromBaseWeaver())
        {
            return (target, path) =>
            {
                var baseModuleWeaver = (BaseModuleWeaver)target;
                baseModuleWeaver.DocumentationFilePath = path;
            };
        }
        return weaverType.BuildPropertySetDelegate<string>("DocumentationFilePath");
    }

    public static Action<object, string> BuildSetProjectDirectoryPath(this Type weaverType)
    {
        if (weaverType.InheritsFromBaseWeaver())
        {
            return (target, path) =>
            {
                var baseModuleWeaver = (BaseModuleWeaver)target;
                baseModuleWeaver.ProjectDirectoryPath = path;
            };
        }
        return weaverType.BuildPropertySetDelegate<string>("ProjectDirectoryPath");
    }

    public static Action<object, string> BuildSetSolutionDirectoryPath(this Type weaverType)
    {
        if (weaverType.InheritsFromBaseWeaver())
        {
            return (target, path) =>
            {
                var baseModuleWeaver = (BaseModuleWeaver)target;
                baseModuleWeaver.SolutionDirectoryPath = path;
            };
        }
        return weaverType.BuildPropertySetDelegate<string>("SolutionDirectoryPath");
    }

    public static Action<object, List<string>> BuildSetReferenceCopyLocalPaths(this Type weaverType)
    {
        if (weaverType.InheritsFromBaseWeaver())
        {
            return (target, path) =>
            {
                var baseModuleWeaver = (BaseModuleWeaver)target;
                baseModuleWeaver.ReferenceCopyLocalPaths = path;
            };
        }
        return weaverType.BuildPropertySetDelegate<List<string>>("ReferenceCopyLocalPaths");
    }

    public static Action<object, string> BuildSetReferences(this Type weaverType)
    {
        if (weaverType.InheritsFromBaseWeaver())
        {
            return (target, references) =>
            {
                var baseModuleWeaver = (BaseModuleWeaver)target;
                baseModuleWeaver.References = references;
            };
        }
        return weaverType.BuildPropertySetDelegate<string>("References");
    }

    public static Action<object, Action<string, SequencePoint>> BuildSetLogWarningPoint(this Type weaverType)
    {
        if (weaverType.InheritsFromBaseWeaver())
        {
            return (target, action) =>
            {
                var baseModuleWeaver = (BaseModuleWeaver)target;
                baseModuleWeaver.LogWarningPoint = action;
            };
        }
        return weaverType.BuildPropertySetDelegate<Action<string, SequencePoint>>("LogWarningPoint");
    }

    public static Action<object, Action<string>> BuildSetLogWarning(this Type weaverType)
    {
        if (weaverType.InheritsFromBaseWeaver())
        {
            return (target, action) =>
            {
                var baseModuleWeaver = (BaseModuleWeaver)target;
                baseModuleWeaver.LogWarning = action;
            };
        }
        return weaverType.BuildPropertySetDelegate<Action<string>>("LogWarning");
    }

    public static Action<object, Action<string, MessageImportance>> BuildSetLogMessage(this Type weaverType)
    {
        if (weaverType.InheritsFromBaseWeaver())
        {
            return (target, action) =>
            {
                var baseModuleWeaver = (BaseModuleWeaver)target;
                baseModuleWeaver.LogMessage = action;
            };
        }
        return weaverType.BuildPropertySetDelegate<Action<string, MessageImportance>>("LogMessage");
    }

    public static Action<object, Action<string>> BuildSetLogInfo(this Type weaverType)
    {
        if (weaverType.InheritsFromBaseWeaver())
        {
            return (target, action) =>
            {
                var baseModuleWeaver = (BaseModuleWeaver)target;
                baseModuleWeaver.LogInfo = action;
            };
        }
        return weaverType.BuildPropertySetDelegate<Action<string>>("LogInfo");
    }

    public static Action<object, Action<string>> BuildSetLogDebug(this Type weaverType)
    {
        if (weaverType.InheritsFromBaseWeaver())
        {
            return (target, action) =>
            {
                var baseModuleWeaver = (BaseModuleWeaver)target;
                baseModuleWeaver.LogDebug = action;
            };
        }
        return weaverType.BuildPropertySetDelegate<Action<string>>("LogDebug");
    }

    public static Action<object, Action<string, SequencePoint>> BuildSetLogErrorPoint(this Type weaverType)
    {
        if (weaverType.InheritsFromBaseWeaver())
        {
            return (target, action) =>
            {
                var baseModuleWeaver = (BaseModuleWeaver)target;
                baseModuleWeaver.LogErrorPoint = action;
            };
        }
        return weaverType.BuildPropertySetDelegate<Action<string, SequencePoint>>("LogErrorPoint");
    }

    public static Action<object, Action<string>> BuildSetLogError(this Type weaverType)
    {
        if (weaverType.InheritsFromBaseWeaver())
        {
            return (target, action) =>
            {
                var baseModuleWeaver = (BaseModuleWeaver)target;
                baseModuleWeaver.LogError = action;
            };
        }
        return weaverType.BuildPropertySetDelegate<Action<string>>("LogError");
    }

    public static Action<object, IAssemblyResolver> BuildSetAssemblyResolver(this Type weaverType)
    {
        return weaverType.BuildPropertySetDelegate<IAssemblyResolver>("AssemblyResolver");
    }

    public static Action<object, Func<string, AssemblyDefinition>> BuildSetResolveAssembly(this Type weaverType)
    {
        if (weaverType.InheritsFromBaseWeaver())
        {
            return (target, resolver) =>
            {
                var baseModuleWeaver = (BaseModuleWeaver)target;
                baseModuleWeaver.ResolveAssembly = resolver;
            };
        }
        return null;
    }

    public static Action<object, string> BuildSetAssemblyFilePath(this Type weaverType)
    {
        if (weaverType.InheritsFromBaseWeaver())
        {
            return (target, path) =>
            {
                var baseModuleWeaver = (BaseModuleWeaver)target;
                baseModuleWeaver.AssemblyFilePath = path;
            };
        }
        return weaverType.BuildPropertySetDelegate<string>("AssemblyFilePath");
    }

    public static Action<object, string> BuildSetAddinDirectoryPath(this Type weaverType)
    {
        if (weaverType.InheritsFromBaseWeaver())
        {
            return (target, path) =>
            {
                var baseModuleWeaver = (BaseModuleWeaver)target;
                baseModuleWeaver.AddinDirectoryPath = path;
            };
        }
        return weaverType.BuildPropertySetDelegate<string>("AddinDirectoryPath");
    }

    public static Action<object, T> BuildPropertySetDelegate<T>(this Type targetType, string propertyName)
    {
        TryBuildPropertySetDelegate(targetType, propertyName, out Action<object, T> action);
        return action;
    }

    static void EmptySetter<T>(object o, T t) { }

    public static bool TryBuildPropertySetDelegate<T>(this Type targetType, string propertyName, out Action<object, T> action)
    {
        var propertyInfo = targetType.GetProperty(propertyName, BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.Public, null, typeof(T), new Type[] { }, null);

        if (propertyInfo != null)
        {
            var target = Expression.Parameter(typeof (object));
            var value = Expression.Parameter(typeof (T));
            var property = Expression.Property(Expression.Convert(target, targetType), propertyInfo);
            var body = Expression.Assign(property, value);
            action = Expression.Lambda<Action<object, T>>(body, target, value)
                             .Compile();
            return true;
        }
        var fieldInfo = GetField<T>(targetType, propertyName);
        if (fieldInfo != null)
        {
            var target = Expression.Parameter(typeof (object), "target");
            var value = Expression.Parameter(typeof (T), "value");
            var fieldExp = Expression.Field(Expression.Convert(target, targetType), fieldInfo);
            var body = Expression.Assign(fieldExp, value);
            action = Expression.Lambda<Action<object, T>>(body, target, value)
                             .Compile();
            return true;

        }
        action = EmptySetter;
        return false;
    }

    public static FieldInfo GetField<TField>(this Type type, string propertyName)
    {
        var fieldInfo = type.GetField(propertyName);
        if (fieldInfo == null)
        {
            return null;
        }
        if (!fieldInfo.IsPublic)
        {
            return null;
        }
        if (fieldInfo.IsStatic)
        {
            return null;
        }
        if (!typeof(TField).IsAssignableFrom(fieldInfo.FieldType))
        {
            return null;
        }
        return fieldInfo;
    }
}