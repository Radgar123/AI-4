using System;
using System.IO;
using RadgarGames.Interface;
using UnityEngine;

public class Debugger : MonoBehaviour, IDebugger
{
    [SerializeField] private string defaultTag = "DEBUG";
    [SerializeField] private bool logToFile = false;
    [SerializeField] private bool enableLogging = true;
    [SerializeField] private string logFileName = "debug_log.txt";

    private string logFilePath;

    private void Awake()
    {
        if (logToFile)
        {
            logFilePath = Path.Combine(Application.persistentDataPath, logFileName);
            if (File.Exists(logFilePath))
            {
                File.WriteAllText(logFilePath, string.Empty);
            }
        }
    }
    
    public void LogMessage(string message)
    {
        if (!enableLogging) return;

        string formattedMessage = FormatMessage(defaultTag, message);
        Debug.Log(formattedMessage);
        LogToFile(formattedMessage);
    }
    
    public void LogMessageWithCustomTag(string message, string customTag)
    {
        if (!enableLogging) return;

        string formattedMessage = FormatMessage(customTag, message);
        Debug.Log(formattedMessage);
        LogToFile(formattedMessage);
    }
    
    public void LogWarning(string message)
    {
        if (!enableLogging) return;

        string formattedMessage = FormatMessage(defaultTag, message);
        Debug.LogWarning(formattedMessage);
        LogToFile(formattedMessage);
    }
    
    public void LogError(string message)
    {
        if (!enableLogging) return;

        string formattedMessage = FormatMessage(defaultTag, message);
        Debug.LogError(formattedMessage);
        LogToFile(formattedMessage);
    }
    
    public void LogFromUnityEvent(string message)
    {
        LogMessage(message);
    }
    
    public void LogFromUnityEventWithCustomTag(string message, string customTag)
    {
        LogMessageWithCustomTag(message, customTag);
    }
    
    private string FormatMessage(string tag, string message)
    {
        return $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{tag}] {message}";
    }
    
    private void LogToFile(string message)
    {
        if (logToFile)
        {
            try
            {
                File.AppendAllText(logFilePath, message + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Debug.LogError($"[{defaultTag}] Failed to log to file: {ex.Message}");
            }
        }
    }
}
