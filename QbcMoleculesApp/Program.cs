using Microsoft.Extensions.DependencyInjection;
using QbcMoleculesBusinessLogic;
using QbcMoleculesBusinessLogic.Business.Processor;
using QbcMoleculesBusinessLogic.Data.ProcessCommands;

Console.WriteLine("Start Services");
var services = new ServiceCollection();
services.AddQbcResearch();

var processor = services.BuildServiceProvider()
    .GetService<IMoleculesProcessor>();

if ( processor != null)
{
    Console.WriteLine("Start processing");
    List<Task> currentTasks = new();
    foreach (var cmd in QbcCmdInterpreter.BuildCmd(Environment.GetCommandLineArgs()))
    {
        currentTasks.Add(processor.ProcessAsync(cmd));
    }
    Task.WhenAll(currentTasks).Wait();
    Console.WriteLine("End processing");
}
else
{
    Console.WriteLine("Failed to start services");
}

Console.ReadLine();

