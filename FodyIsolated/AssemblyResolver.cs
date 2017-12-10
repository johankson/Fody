using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Mono.Cecil;

public class AssemblyResolver : IAssemblyResolver
{
    // file name to path map
    Dictionary<string, string> referenceDictionary;
    List<string> notFoundList = new List<string>();
    ILogger logger;
    List<string> splitReferences;
    ReaderParameters readerParameters;

    // file name to AssemblyDefinition map
    Dictionary<string, AssemblyDefinition> assemblyDefinitionCache = new Dictionary<string, AssemblyDefinition>(StringComparer.InvariantCultureIgnoreCase);

    public AssemblyResolver(Dictionary<string, string> referenceDictionary, ILogger logger, List<string> splitReferences)
    {
        this.referenceDictionary = referenceDictionary;
        this.logger = logger;
        this.splitReferences = splitReferences;
        readerParameters = new ReaderParameters(ReadingMode.Deferred)
        {
            AssemblyResolver = this
        };
    }

    protected AssemblyResolver()
    {
    }

    AssemblyDefinition GetAssembly(string filePath)
    {
        var name = Path.GetFileNameWithoutExtension(filePath);
        if (assemblyDefinitionCache.TryGetValue(name, out var assembly))
        {
            return assembly;
        }
        try
        {
            return assemblyDefinitionCache[filePath] = AssemblyDefinition.ReadAssembly(filePath, readerParameters);
        }
        catch (Exception exception)
        {
            throw new Exception($"Could not read '{filePath}'.", exception);
        }
    }

    public AssemblyDefinition Resolve(AssemblyNameReference assemblyNameReference)
    {
        return Resolve(assemblyNameReference.Name);
    }

    public AssemblyDefinition Resolve(AssemblyNameReference assemblyNameReference, ReaderParameters parameters)
    {
        var assemblyName = assemblyNameReference.Name;
        return Resolve(assemblyName);
    }

    public AssemblyDefinition Resolve(string assemblyName)
    {
        if (notFoundList.Contains(assemblyName))
        {
            return null;
        }
        if (referenceDictionary.TryGetValue(assemblyName, out var fileFromDerivedReferences))
        {
            return GetAssembly(fileFromDerivedReferences);
        }

        if (TryReadFromSiblings(assemblyName, out var assemblyDefinition))
        {
            return assemblyDefinition;
        }

        var joinedReferences = string.Join(Environment.NewLine, splitReferences.OrderBy(x => x));
        logger.LogDebug(string.Format("Can not find '{0}'.{1}Tried:{1}{2}", assemblyName, Environment.NewLine, joinedReferences));
        return null;
    }

    private bool TryReadFromSiblings(string assemblyName, out AssemblyDefinition assemblyDefinition)
    {
        var filesWithMatchingName = SearchDirForMatchingName(assemblyName).ToList();
        foreach (var filePath in filesWithMatchingName.OrderByDescending(s => AssemblyName.GetAssemblyName(s).Version))
        {
            assemblyDefinition = GetAssembly(filePath);
            return true;
        }

        assemblyDefinition = null;
        return false;
    }

    IEnumerable<string> SearchDirForMatchingName(string assemblyName)
    {
        var fileName = $"{assemblyName}.dll";
        return referenceDictionary.Values
            .Select(x => Path.Combine(Path.GetDirectoryName(x), fileName))
            .Where(File.Exists);
    }

    public virtual void Dispose()
    {
        foreach (var value in assemblyDefinitionCache.Values)
        {
            value?.Dispose();
        }
    }
}