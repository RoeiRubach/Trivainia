using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace Trivainia.Utilities
{
    public static class ConsoleLogger
    {
        private const string INFO_COLOR = "#3AB4F2";
        private const string WARNING_COLOR = "#FFD700";
        private const string ERROR_COLOR = "#FF4C4C";
        private const string NULL_COLOR = "#FF69B4";
        private const string TRACE_COLOR = "#A9A9A9";
        private const string MISSING_REF_COLOR = "#FFA500";

        [Conditional("ENABLE_LOGS")]
        public static void Print(string message, Object context = null) => Log(LogType.Info, "ℹ\ufe0f INFO", message, INFO_COLOR, context);

        [Conditional("ENABLE_LOGS")]
        public static void PrintError(string message, Object context = null) => Log(LogType.Error, "\u274c ERROR", message, ERROR_COLOR, context);

        [Conditional("ENABLE_LOGS")]
        public static void PrintWarning(string message, Object context = null) => Log(LogType.Warning, "\u26a0\ufe0f WARNING", message, WARNING_COLOR, context);

        [Conditional("ENABLE_LOGS")]
        public static void PrintNullObject(string message, Object context = null) => Log(LogType.Warning, "\ud83d\udeab NULL", $"Null Reference Detected → {message}", NULL_COLOR, context);

        [Conditional("ENABLE_LOGS")]
        public static void PrintStackTrace(string message, Object context = null)
        {
            var fullMessage = $"{message}\n<color=#{TRACE_COLOR}>{Environment.StackTrace}</color>";
            Log(LogType.Info, "\ud83e\uddf5 TRACE", fullMessage, TRACE_COLOR, context);
        }

        [Conditional("ENABLE_LOGS")]
        public static void PrintInspectorRefMissing(string message, Object context = null) => Log(LogType.Warning, "\ud83e\udde9 INSPECTOR", $"Missing Reference → {message}", MISSING_REF_COLOR, context);

        private static void Log(LogType type, string prefix, string message, string color, Object context)
        {
            var formatted = $"<b><color=#{color}>[{prefix}]</color></b> {message}";

            switch (type)
            {
                case LogType.Info:
                    Debug.Log(formatted, context);

                    break;
                case LogType.Warning:
                    Debug.LogWarning(formatted, context);

                    break;
                case LogType.Error:
                    Debug.LogError(formatted, context);

                    break;
            }
        }

        private enum LogType { Info, Warning, Error }
    }
}