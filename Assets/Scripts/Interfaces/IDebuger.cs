namespace RadgarGames.Interface
{
    public interface IDebugger
    {
        void LogMessage(string message);
        void LogMessageWithCustomTag(string message, string customTag);
        void LogWarning(string message);
        void LogError(string message);
        void LogFromUnityEvent(string message);
        void LogFromUnityEventWithCustomTag(string message, string customTag);
    }
}