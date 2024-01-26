using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILogger
{
    public string Prefix { get; }
}

public static class Logger
{
    public static void Log(this ILogger instance, string message)
    {
        Debug.Log($"{instance.Prefix} {message}");
    }

    public static void LogError(this ILogger instance, string message)
    {
        Debug.LogError($"{instance.Prefix} {message}");
    }
}
