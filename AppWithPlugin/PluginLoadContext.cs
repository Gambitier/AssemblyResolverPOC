using System;
using System.Reflection;
using System.Runtime.Loader;

namespace AppWithPlugin
{
    // The custom AssemblyLoadContext (PluginLoadContext) enables plugins to have their own dependencies
    class PluginLoadContext : AssemblyLoadContext
    {
        private AssemblyDependencyResolver _resolver;

        public PluginLoadContext(string pluginPath)
        {
            // AssemblyDependencyResolver: type introduced in .NET Core 3.0 to resolve assembly names to paths.
            // It resolves assemblies and native libraries to their relative paths based on the .deps.json file
            // for the class library whose path was passed to the AssemblyDependencyResolver constructor
            _resolver = new AssemblyDependencyResolver(pluginPath);
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            string assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
            if (assemblyPath != null)
            {
                return LoadFromAssemblyPath(assemblyPath);
            }

            return null;
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            string libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            if (libraryPath != null)
            {
                return LoadUnmanagedDllFromPath(libraryPath);
            }

            return IntPtr.Zero;
        }
    }
}