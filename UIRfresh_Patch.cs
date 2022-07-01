using Assets.Scripts.Database;
using Assets.Scripts.UI.Panels;
using CharacterScoreboard;
using HarmonyLib;

using UnityEngine.UI;

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
    }
}
