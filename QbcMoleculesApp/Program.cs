
using QbcMoleculesBusinessLogic.Applications;
using QbcMoleculesBusinessLogic.Applications.Data;

string[] cmdArgs = Environment.GetCommandLineArgs();
string ? applicationName = cmdArgs.FirstOrDefault(i => i.StartsWith("-"));
if (!String.IsNullOrWhiteSpace(applicationName))
{
    if (cmdArgs.Length > 2)
    {
        await ApplicationFactory.Create(applicationName).RunAsync(new ApplicationParameters(cmdArgs.Skip(2).ToArray()));
    }
    else
    {
        await ApplicationFactory.Create(applicationName).RunAsync(new ApplicationParameters());
    }
}
else
{
    await ApplicationFactory.Create().RunAsync(new ApplicationParameters(cmdArgs));
}

