namespace QbcMoleculesBusinessLogic.Applications
{
    public abstract class Application
    {
        public abstract Task RunAsync(ApplicationParameters parameters);

        public abstract string Name();

    }
}
