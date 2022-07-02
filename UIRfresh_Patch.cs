using Assets.Scripts.Database;
using Assets.Scripts.UI.Controls;
using Assets.Scripts.UI.Panels;
using CharacterScoreboard;
using HarmonyLib;
using Il2CppNewtonsoft.Json.Linq;
using System.Collections.Generic;

[HarmonyPatch(typeof(PnlRank), "UIRefresh")]
internal static class UIRefresh_Patch
{
    public static string lastUIUpdatedUID;

    [HarmonyPostfix, HarmonyPriority(Priority.Last)]
    private static void Postfix(PnlRank __instance, string uid)
    {
        lastUIUpdatedUID = uid;

        if (PrefferenceManager.CSEnabled)
        {
            __instance.txtServerName.text = DBUtils.getCharacterNameByIndex(DataHelper.selectedRoleIndex) + " & " + DBUtils.getElfinNameByIndex(DataHelper.selectedElfinIndex);
        }
        else
        {
            if (PrefferenceManager.DCSEnabled)
                if (__instance.m_Ranks.entries.Count > 0 && __instance.m_Ranks.ContainsKey(uid))
                {

                    JArray ranks = __instance.m_Ranks[uid].Cast<JArray>();

                    RankCell[] cells = __instance.scrollView.GetComponentsInChildren<RankCell>();

                    if (ranks.Count > 0 && cells.Length > 0)
                        for (int index = 0; index < ranks.Count; index++)
                        {
                            JObject data = ranks[index].Cast<JObject>();
                            string newNick = DBUtils.getCharacterNameByIndex(int.Parse((string)data["play"]["character_uid"])) + " & " + DBUtils.getElfinNameByIndex(int.Parse((string)data["play"]["elfin_uid"]));
                            cells[index].SetValue(index + 1, newNick, (int)data["play"]["score"], (float)data["play"]["acc"] / 100);
                        }

                }

        }

    }

    public static Dictionary<string, string> refreshedAfterChange = new Dictionary<string, string>();

    [HarmonyPrefix]
    private static bool Prefix(string uid)
    {
        if (refreshedAfterChange.ContainsKey(uid)) return true;

        if (ToggleManager.rank != null)
        {
            refreshedAfterChange.Add(uid, "yes");
            ToggleManager.rank.RefreshGeneral(uid);
            return false;
        }

        return true;
    }
}

