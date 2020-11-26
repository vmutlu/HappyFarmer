namespace HappyFarmer.UI.Logging
{
    public interface ILogManager<T> where T : class
    {
        void Verbose(string message);
        void Verbose(string message, T t);
        void Fatal(string message);
        void Fatal(string message, T t);
        void Information(string message);
        void Information(string message, T t);
        void Warning(string message);
        void Warning(string message, T t);
        void Debug(string message);
        void Debug(string message, T t);
        void Error(string message);
        void Error(string message, T t);
    }
}
