using Verse;
using UnityEngine;

public static class ModLog
{
    // Standard-Prefix für Logs
    public static string LogPrefix { get; set; } = "[MeinMod]";

    public static void Message(string text)
    {
        Log.Message($"{LogPrefix} {text}");
    }

    public static void Warning(string text)
    {
        Log.Warning($"{LogPrefix} {text}");
    }

    public static void WarningOnce(string text, int key)
    {
        Log.WarningOnce($"{LogPrefix} {text}", key);
    }

    public static void Error(string text)
    {
        Log.Error($"{LogPrefix} {text}");
    }

    public static void ErrorOnce(string text, int key)
    {
        Log.ErrorOnce($"{LogPrefix} {text}", key);
    }

    // ** Extra: Farbige Debug-Ausgabe in der Unity-Konsole (nur außerhalb von RimWorld sichtbar) **
    public static void DebugMessage(string text, Color color)
    {
        string hexColor = ColorUtility.ToHtmlStringRGB(color);
        Debug.Log($"<color=#{hexColor}>{LogPrefix} {text}</color>");
    }
}