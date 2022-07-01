using Account;
using CharacterScoreboard;
using HarmonyLib;
using Il2CppNewtonsoft.Json.Linq;

[HarmonyPatch(typeof(GameAccountSystem), "UploadScore")]
internal static class UploadScore_Patch
{
    [HarmonyPrefix]
    private static void Prefix(string musicUid, int musicDifficulty, string characterUid, string elfinUid, int hp, int score, float acc, int combo, string evaluate, int miss, JArray beats, string bmsVersion, Il2CppSystem.Action<int> callback)
    {
        Registry.saveRun(musicUid, musicDifficulty, characterUid, elfinUid, score, acc);
    }
}