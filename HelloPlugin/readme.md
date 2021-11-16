Now, open the HelloPlugin.csproj file. It should look similar to the following:

```
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net5.0</TargetFramework>
    <EnableDynamicLoading>true</EnableDynamicLoading>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\PluginBase\PluginBase.csproj" />
  </ItemGroup>

</Project>
```

The <EnableDynamicLoading>true</EnableDynamicLoading> prepares the project so that it can be used as a plugin.
Among other things, this will copy all of its dependencies to the output of the project.

The EnableDynamicLoading property indicates that an assembly is a dynamically loaded component.
The component could be a COM library or a non-COM library that can be used from a native host or used as a plugin.
For more details see [EnableDynamicLoading](https://docs.microsoft.com/en-us/dotnet/core/project-sdk/msbuild-props#enabledynamicloading).


----------------

In between the <Project> tags, add the following elements:

```
<ItemGroup>
    <ProjectReference Include="..\PluginBase\PluginBase.csproj">
        <Private>false</Private>
        <ExcludeAssets>runtime</ExcludeAssets>
    </ProjectReference>
</ItemGroup>
```

The <Private>false</Private> element is important. This tells MSBuild to not copy PluginBase.dll 
to the output directory for HelloPlugin. If the PluginBase.dll assembly is present in the output 
directory, PluginLoadContext will find the assembly there and load it when it loads the 
HelloPlugin.dll assembly. At this point, the HelloPlugin.HelloCommand type will implement 
the ICommand interface from the PluginBase.dll in the output directory of the HelloPlugin project, 
not the ICommand interface that is loaded into the default load context. Since the runtime sees these 
two types as different types from different assemblies, the AppWithPlugin.Program.CreateCommands method 
won't find the commands. As a result, the <Private>false</Private> metadata is required for the reference 
to the assembly containing the plugin interfaces.

Similarly, the <ExcludeAssets>runtime</ExcludeAssets> element is also important if the PluginBase references
other packages. This setting has the same effect as <Private>false</Private> but works on package references 
that the PluginBase project or one of its dependencies may include.

Now that the HelloPlugin project is complete, you should update the AppWithPlugin project to know where
the HelloPlugin plugin can be found. After the // Paths to plugins to load comment,
add @"HelloPlugin\bin\Debug\netcoreapp3.0\HelloPlugin.dll" 
(this path could be different based on the .NET Core version you use) as an element of the pluginPaths array.

