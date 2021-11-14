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
