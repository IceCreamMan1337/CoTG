using ChildrenOfTheGrave.ChildrenOfTheGraveServer.Logging;
using ChildrenOfTheGraveLibrary.Scripting.CSharp;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ChildrenOfTheGrave.ChildrenOfTheGraveServer.Scripting.CSharp;

internal class CSharpScriptEngine
{
    private static ILog _logger = LoggerProvider.GetLogger();
    private readonly Dictionary<string, Type> _types = [];

    /// <summary>
    /// Loads scripts from a list of assemblies, to avoid project circular dependeces
    /// </summary>
    /// <param name="assemblies"></param>
    public CSharpScriptEngine(string[] assemblies)
    {
        AssemblyService.TryLoadAssemblies(assemblies);
        foreach (Assembly assembly in AssemblyService.GetAssemblies())
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.FullName != null)
                {
                    _types[type.FullName] = type;
                }
            }
        }
    }

    /// <summary>
    /// Creates a script object given a script namespace and class name.
    /// </summary>
    public T? CreateObject<T>(string scriptNamespace, string scriptClass, bool suppressWarning = false, T? sourceObject = default)
    {
        if (!AssemblyService.GetAssemblies().Any() || string.IsNullOrEmpty(scriptClass) || string.IsNullOrEmpty(scriptNamespace))
        {
            return default;
        }

        scriptNamespace = scriptNamespace.Replace(" ", "_");
        scriptClass = scriptClass.Replace(" ", "_").Replace("!", "ù");
        string fullClassName = scriptNamespace + "." + scriptClass;

        Type? classType = _types.GetValueOrDefault(fullClassName);
        if (classType != null)
        {
            return (T)Activator.CreateInstance(classType)!;
        }

        if (!suppressWarning)
        {
            _logger.Warn($"Could not find script: {fullClassName}");
        }

        return default;
    }
}