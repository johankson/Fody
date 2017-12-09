using System.Diagnostics;
using Fody;
using Mono.Cecil;
using Mono.Cecil.Cil;

public class ModuleWeaver : BaseModuleWeaver
{
    public override void Execute()
    {
        LogDebug("Debug Entry");
        LogInfo("Info Entry");
        LogWarning("Warning Entry");
        LogWarningPoint("Warning Entry", new SequencePoint(Instruction.Create(OpCodes.Nop), new Document("A")));
        Debug.Assert(LogError != null);
        Debug.Assert(LogErrorPoint != null);
        // Dont break the build
        //LogError("Error Entry");
        //LogErrorPoint("Error Entry", new SequencePoint(Instruction.Create(OpCodes.Nop), new Document("A")));
        Debug.Assert(AddinDirectoryPath != null);
        Debug.Assert(AssemblyFilePath != null);
        Debug.Assert(AssemblyResolver != null);
        Debug.Assert(DefineConstants != null);
        Debug.Assert(ProjectDirectoryPath != null);
        Debug.Assert(ReferenceCopyLocalPaths != null);
        Debug.Assert(References != null);
        Debug.Assert(SolutionDirectoryPath != null);
        //Can be null
        //Debug.Assert(Config != null);
        //Debug.Assert(DocumentationFilePath != null);
        var type = GetType();
        var typeDefinition = new TypeDefinition(
            @namespace: type.Assembly.GetName().Name,
            name: $"TypeInjectedBy{type.Name}",
            attributes: TypeAttributes.Public,
            baseType: ModuleDefinition.ImportReference(typeof(object)));
        ModuleDefinition.Types.Add(typeDefinition);
    }
}