using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public static class Logger
{
    [Conditional("DEVELOPMENT")]
    public static void Log(object message, Object context = null)
    {
        Debug.Log(message, context);
    }

    [Conditional("DEVELOPMENT")]
    public static void LogWarning(string message, Object context = null)
    {
        Debug.LogWarning(message, context);
    }

    [Conditional("DEVELOPMENT")]
    public static void LogError(string message, Object context = null)
    {
        Debug.LogError(message, context);
    }
}