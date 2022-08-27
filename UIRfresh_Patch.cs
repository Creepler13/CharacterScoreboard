using Assets.Scripts.Database;
using Assets.Scripts.PeroTools.Commons;
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

    public static Dictionary<string, RankCell[][]> scorebaordData = new Dictionary<string, RankCell[][]>();
    public static Dictionary<string, bool> tainted = new Dictionary<string, bool>();

    [HarmonyPostfix, HarmonyPriority(Priority.Last)]
    private static void Postfix(PnlRank __instance, string uid)
    {
        lastUIUpdatedUID = uid;

        if (!tainted.ContainsKey(uid))
            scorebaordData[uid] = __instance.scrollView.GetComponentsInChildren<RankCell>();

        if (PrefferenceManager.LCSEnabled)
            upDatedScoreboardLCS(__instance, uid);

        else if (PrefferenceManager.OCSEnabled)
            upDatedScoreboardOCS(__instance, uid);

        else
        {
            scorebaordData[uid] = __instance.scrollView.GetComponentsInChildren<RankCell>();
            if (tainted.ContainsKey(uid))
            {
                tainted.Remove(uid);
            };
        }
    }

    private static void upDatedScoreboardLCS(PnlRank __instance, string uid)
    {

        foreach (RankCell cell in __instance.scrollView.GetComponents<RankCell>())
        {
            cell.enabled = false;
        }

        JObject runs = Registry.getStage(uid.Split('_')[0], int.Parse(uid.Split('_')[1]));


        string[] keys = { };

        foreach (string key in JsonUtils.Keys(runs))
        {
            int i;
            for (i = 0; i < keys.Length; i++)
                if ((float)runs[key]["play"]["score"] > (float)runs[keys[i]]["play"]["score"])
                    break;

            keys.AddItem(key);

        }



        for (int i = 0; i < keys.Length; i++)
        {
            string key = keys[i];
            JObject run = runs[key].Cast<JObject>();

            RankCell cell = __instance.scrollView.AddComponent<RankCell>();

            string name = DBUtils.getCharacterElfinNameByIds(key);

            int score = 0;
            int.TryParse(runs[key]["score"].ToString(), out score);

            float acc = 0;
            float.TryParse(runs[key]["acc"].ToString(), out acc);

            cell.SetValue(i + 1, name, score, acc);
        }



        __instance.txtServerName.text = DBUtils.getCharacterElfinNameByIds(DataHelper.selectedRoleIndex + "&" + DataHelper.selectedElfinIndex);
    }

    private static void upDatedScoreboardOCS(PnlRank __instance, string uid)
    {
        if (__instance.m_Ranks.entries.Count > 0 && __instance.m_Ranks.ContainsKey(uid))
        {

            JArray ranks = __instance.m_Ranks[uid].Cast<JArray>();

            RankCell[] cells = __instance.scrollView.GetComponentsInChildren<RankCell>();

            int smallerCount = ranks.Count > cells.Length ? cells.Length : ranks.Count;

            if (ranks.Count > 0 && cells.Length > 0)
                for (int index = 0; index < (smallerCount > 100 ? 100 : smallerCount); index++)
                {
                    JObject data = ranks[index].Cast<JObject>();

                    string newNick = DBUtils.getCharacterElfinNameByIds(data["play"]["character_uid"].ToString() + "&" + data["play"]["elfin_uid"].ToString());

                    float acc;
                    if (!float.TryParse(data["play"]["acc"].ToString(), out acc))
                        continue;

                    int score;
                    if (!int.TryParse(data["play"]["score"].ToString(), out score))
                        continue;


                    cells[index].SetValue(index + 1, newNick, score, acc / 100);
                }

        }
    }


    /**
    public static Dictionary<string, string> refreshedAfterChange = new Dictionary<string, string>();

    [HarmonyPrefix]
    private static bool Prefix(string uid)
    {
        if (refreshedAfterChange.ContainsKey(uid) || uid == null) return true;

        if (ToggleManager.rank != null)
        {
            refreshedAfterChange.Add(uid, "yes");
            ToggleManager.rank.RefreshGeneral(uid);
            return false;
        }

        return true;
    }
    */
}

