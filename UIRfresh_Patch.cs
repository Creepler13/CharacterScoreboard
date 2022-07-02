using Assets.Scripts.Database;
using Assets.Scripts.UI.Controls;
using Assets.Scripts.UI.Panels;
using CharacterScoreboard;
using HarmonyLib;
using Il2CppNewtonsoft.Json.Linq;

[HarmonyPatch(typeof(PnlRank), "UIRefresh")]
internal static class UIRefresh_Patch
{
    [HarmonyPostfix, HarmonyPriority(Priority.Last)]
    private static void Postfix(PnlRank __instance, string uid)
    {
        if (PrefferenceManager.CSEnabled)
        {
            __instance.txtServerName.text = DBUtils.getCharacterNameByIndex(DataHelper.selectedRoleIndex) + " & " + DBUtils.getElfinNameByIndex(DataHelper.selectedElfinIndex);
        }
        else
        {
            if (PrefferenceManager.DCSEnabled)
                if (__instance.m_Ranks.entries.Count > 0)
                {

                    JArray ranks = __instance.m_Ranks.entries[int.Parse(uid.Split("_".ToCharArray())[1]) - 1].value.Cast<JArray>();

                    RankCell[] cells = __instance.scrollView.GetComponentsInChildren<RankCell>();

                    MelonLoader.MelonLogger.Msg(ranks.Count + " " + cells.Length);

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
}

