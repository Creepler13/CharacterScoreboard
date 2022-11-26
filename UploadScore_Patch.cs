using Account;
using CharacterScoreboard;
using HarmonyLib;
using Il2CppNewtonsoft.Json.Linq;
using System;

[HarmonyPatch(typeof(GameAccountSystem), "UploadScore")]
internal static class UploadScore_Patch
{
    [HarmonyPrefix]
    private static void Prefix(string musicUid, int musicDifficulty, string characterUid, string elfinUid,  int score, float acc)
    {
        try
        {
            Registry.saveRun(musicUid, musicDifficulty, characterUid, elfinUid, score, acc);
        }
        catch (Exception e)
        { 
            MelonLoader.MelonLogger.Error(e);
        }
        }
}