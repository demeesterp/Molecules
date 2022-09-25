using QbcMoleculesBusinessLogic.Applications.Data;

namespace QbcMoleculesBusinessLogic.Applications
{
    public class DummyApplication : Application
    {
        public override string Name()
        {
            return "Dummy";
        }

        public override async Task RunAsync(ApplicationParameters parameters)
        {
            Console.WriteLine($"Currently running {this.Name()} with parameters {parameters}");
            Console.WriteLine("Press any key to stop the application");            
            Console.ReadLine();
            await Task.CompletedTask;
        }
    }
}
