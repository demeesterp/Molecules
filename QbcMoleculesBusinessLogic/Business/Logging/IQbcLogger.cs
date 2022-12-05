namespace QbcMoleculesBusinessLogic.Business.Logging
{
    public interface IQbcLogger
    {
        void LogInformation(string message);

        void LogWarning(Exception e, string message);

        void LogWarning(string message);

        void LogError(Exception e, string message);

        void LogError(string message);

        void LogCritical(Exception e, string message);

        void LogCritical(string message);
    }
}
