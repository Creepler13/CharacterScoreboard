using Il2CppAssets.Scripts.PeroTools.Commons;
using Il2CppAssets.Scripts.PeroTools.Managers;

namespace CharacterScoreboard;

internal static class Utils
{
    private static string GetElfinNameByIndex(int index) =>
        Singleton<ConfigManager>.instance.GetJson("elfin", true)[index]["name"].ToObject<string>();

    private static string GetCharacterNameByIndex(int index) =>
        Singleton<ConfigManager>.instance.GetJson("character", true)[index]["cosName"].ToObject<string>();

    internal static string GetCharacterElfinNameByIds(int characterID, int elfinID) =>
        GetCharacterNameByIndex(characterID) + " & " + GetElfinNameByIndex(elfinID);

    internal static string GetCharacterElfinNameByIds(string characterID, string elfinID) =>
        GetCharacterNameByIndex(int.Parse(characterID)) + " & " + GetElfinNameByIndex(int.Parse(elfinID));
}