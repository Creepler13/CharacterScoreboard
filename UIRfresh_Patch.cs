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

        fixToggle();

        lastUIUpdatedUID = uid;

        if (PrefferenceManager.LCSEnabled)
            upDatedScoreboardLCS(__instance, uid);

        else if (PrefferenceManager.OCSEnabled)
            upDatedScoreboardOCS(__instance, uid);


    }

    private static void upDatedScoreboardLCS(PnlRank __instance, string uid)
    {
        RankCell[] cells = __instance.scrollView.GetComponentsInChildren<RankCell>();
        JObject runs = Registry.getStage(uid.Split('_')[0], int.Parse(uid.Split('_')[1]));
        Il2CppSystem.Collections.Generic.List<string> keysT = JsonUtils.Keys(runs);
        Il2CppSystem.Collections.Generic.List<string> keys = new Il2CppSystem.Collections.Generic.List<string>();
        foreach (string key in keysT)
        {
            int i = 0;
            for (i = 0; i < keys.Count; i++)
            {
                if ((float)runs[key]["score"] > (float)runs[keysT[i]]["score"])
                    break;
            }
            keys.Insert(i, key);
        }

        for (int index = 0; index < cells.Length; index++)
        {

            if (index > keys.Count - 1)
            {
                cells[index].txtNumber.text = " ";
                cells[index].txtAcc.text = " ";
                cells[index].txtPlayerName.text = " ";
                cells[index].txtScore.text = " ";
                continue;
            }

            string key = keys[index];

            JObject run = runs[key].Cast<JObject>();

            string name = DBUtils.getCharacterElfinNameByIds(key);

            float acc;
            if (!float.TryParse(run["acc"].ToString(), out acc))
                continue;

            int score;
            if (!int.TryParse(run["score"].ToString(), out score))
                continue;

            cells[index].SetValue(index + 1, name, score, acc / 100);

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
        __instance.txtServerName.text = DBUtils.getCharacterElfinNameByIds(DataHelper.selectedRoleIndex + "&" + DataHelper.selectedElfinIndex);

    }


    public static void fixToggle()
    {
        if(ToggleManager.OCSToggle)
        {
            UnityEngine.UI.Text component = ToggleManager.OCSToggle.transform.Find("Txt").GetComponent<UnityEngine.UI.Text>();
            component.text = "OCS".ToUpper();
        }

        if (ToggleManager.LCSToggle)
        {
            UnityEngine.UI.Text component = ToggleManager.LCSToggle.transform.Find("Txt").GetComponent<UnityEngine.UI.Text>();
            component.text = "LCS".ToUpper();
        }


    }

    /*
    public static Dictionary<string, string> refreshedAfterChange = new Dictionary<string, string>();

    [HarmonyPrefix]
    private static bool Prefix(string uid)
    {
        if (refreshedAfterChange.ContainsKey(uid) || uid == null || !PrefferenceManager.LCSEnabled) return true;

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

