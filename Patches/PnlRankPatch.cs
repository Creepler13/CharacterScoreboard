using Il2CppAssets.Scripts.Database;
using Il2CppAssets.Scripts.UI.Controls;
using Il2CppNewtonsoft.Json.Linq;

namespace CharacterScoreboard.Patches;

[HarmonyPatch(typeof(PnlRank), nameof(PnlRank.UIRefresh))]
internal static class PnlRankPatch
{
    private static void Postfix(PnlRank __instance, string uid)
    {
        Rank = __instance;
        Uid = uid;

        if (Setting.OcsEnabled)
        {
            UpdateScoreboard(uid);
        }

        if (OcsToggle != null || VSelect == null)
        {
            return;
        }

        OcsToggle = Object.Instantiate(VSelect.transform.Find("LogoSetting").Find("Toggles").Find("TglOn").gameObject,
            __instance.transform.parent);
        SetupToggle();
    }

    private static void UpdateScoreboard(string uid)
    {
        if (!Rank.m_Ranks.ContainsKey(uid) || !Rank.m_Ranks[uid].HasValues)
        {
            return;
        }

        var ranks = Rank.m_Ranks[uid].Cast<JArray>();
        var cells = Rank.scrollView.GetComponentsInChildren<RankCell>();
        var count = Math.Min(ranks.Count, cells.Length);

        if (count <= 0)
        {
            return;
        }

        for (var index = 0; index < count; index++)
        {
            UpdateCellWithRankData(ranks[index], cells[index], index);
        }

        Rank.txtServerName.text = GetCharacterElfinNameByIds(DataHelper.selectedRoleIndex, DataHelper.selectedElfinIndex);
    }

    private static void UpdateCellWithRankData(JToken data, RankCell cell, int index)
    {
        var nickName = GetCharacterElfinNameByIds(data["play"]["character_uid"].ToString(),
            data["play"]["elfin_uid"].ToString());

        var acc = float.Parse(data["play"]["acc"].ToString());
        var score = int.Parse(data["play"]["score"].ToString());

        cell.SetValue(index + 1, nickName, score, acc / 100);
    }
}