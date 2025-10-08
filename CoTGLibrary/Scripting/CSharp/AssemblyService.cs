using CoTG.CoTGServer.Logging;
using log4net;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CoTGLibrary.Scripting.CSharp;

/// <summary>
/// Internal class that loads assemblies at server instance creation.
/// </summary>
internal class AssemblyService
{
    private static readonly ILog _logger = LoggerProvider.GetLogger();
    private static Assembly?[] Assemblies = null!;

    /// <summary>
    /// Attempts to load Assembly files that parts of the server will refrence
    /// </summary>
    /// <param name="assemblyPaths">Array of paths to assembly DLLs</param>
    public static void TryLoadAssemblies(string[] assemblyPaths)
    {
        Assemblies = new Assembly[assemblyPaths.Length];
        for (int i = 0; i < assemblyPaths.Length; i++)
        {
            Assemblies[i] = TryLoadAssembly(assemblyPaths[i]) ?? TryLoadUncompiledScripts(assemblyPaths[i]);
            if (Assemblies[i] is null)
            {
                _logger.WarnFormat("Assembly or Script directory not found! ({0})", assemblyPaths[i]);
            }
        }
    }

    public static IEnumerable<Assembly> GetAssemblies()
    {
        foreach (Assembly? assembly in Assemblies)
        {
            if (assembly is not null)
            {
                yield return assembly;
            }
        }
    }

    private static Assembly? TryLoadAssembly(string path)
    {
        if (!path.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
        {
            path += ".dll";
        }

        if (!File.Exists(path))
        {
            return null;
        }

        try
        {
            Assembly assembly = Assembly.LoadFile(Path.GetFullPath(path));
            _logger.InfoFormat("Successfully loaded assembly: {0}", path);
            return assembly;
        }
        catch (Exception e)
        {
            _logger.ErrorFormat("Failed to load assembly: {0}", path);
            _logger.Error(e);
            return null;
        }
    }

    private static Assembly? TryLoadUncompiledScripts(string path)
    {
        if (!Directory.Exists(path))
        {
            return null;
        }
        string[] files = Directory.GetFiles(path, "*.cs");
        if (files.Length <= 0)
        {
            return null;
        }

        List<SyntaxTree> syntaxTrees = new(files.Length);
        foreach (string file in files)
        {
            string text = File.ReadAllText(file);
            syntaxTrees.Add(CSharpSyntaxTree.ParseText(text));
        }

        MetadataReference[] references = [.. AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
            .Select(a => MetadataReference.CreateFromFile(a.Location))
            .Cast<MetadataReference>()];

        CSharpCompilation compilation = CSharpCompilation.Create(
            Path.GetFileName(path),
            syntaxTrees,
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        );

        using MemoryStream ms = new();
        EmitResult result = compilation.Emit(ms);

        if (!result.Success)
        {
            foreach (var diagnostic in result.Diagnostics)
            {
                _logger.Error(diagnostic.ToString());
            }
            return null;
        }
        ms.Seek(0, SeekOrigin.Begin);
        _logger.InfoFormat("Successfully loaded uncompiled script directory: {0}", path);
        return Assembly.Load(ms.ToArray());
    }
}