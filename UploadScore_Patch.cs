using Account;
using CharacterScoreboard;
using HarmonyLib;
using Il2CppNewtonsoft.Json.Linq;

[HarmonyPatch(typeof(GameAccountSystem), "UploadScore")]
internal static class UploadScore_Patch
{
    [HarmonyPrefix]
    private static void Prefix(string musicUid, int musicDifficulty, string characterUid, string elfinUid,  int score, float acc)
    {
        Registry.saveRun(musicUid, musicDifficulty, characterUid, elfinUid, score, acc);
    }
}