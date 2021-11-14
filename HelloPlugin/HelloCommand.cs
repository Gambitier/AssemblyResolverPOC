using PluginBase;
using System;

namespace HelloPlugin
{
    // code refered from
    // https://docs.microsoft.com/en-us/dotnet/core/tutorials/creating-app-with-plugin-support#simple-plugin-with-no-dependencies
    public class HelloCommand : ICommand
    {
        public string Name { get => "hello"; }
        public string Description { get => "Displays hello message."; }

        public int Execute()
        {
            Console.WriteLine("Hello !!!");
            return 0;
        }
    }
}