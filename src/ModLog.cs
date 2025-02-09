using Verse;
using UnityEngine;

public class ModLog
{
    // Präfix wird pro Instanz gespeichert
    public string LogPrefix { get; set; }

    // Konstruktor zum Setzen eines individuellen Präfixes
    public ModLog(string prefix)
    {
        LogPrefix = prefix;
    }

    public void Message(string text)
    {
        Log.Message($"{LogPrefix} {text}");
    }

    public void Warning(string text)
    {
        Log.Warning($"{LogPrefix} {text}");
    }

    public void WarningOnce(string text, int key)
    {
        Log.WarningOnce($"{LogPrefix} {text}", key);
    }

    public void Error(string text)
    {
        Log.Error($"{LogPrefix} {text}");
    }

    public void ErrorOnce(string text, int key)
    {
        Log.ErrorOnce($"{LogPrefix} {text}", key);
    }

    // ** Extra: Farbige Debug-Ausgabe in der Unity-Konsole (außerhalb von RimWorld) **
    public void DebugMessage(string text, Color color)
    {
        string hexColor = ColorUtility.ToHtmlStringRGB(color);
        Debug.Log($"<color=#{hexColor}>{LogPrefix} {text}</color>");
    }
}