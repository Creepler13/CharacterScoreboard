using Assets.Scripts.UI.Panels;
using HarmonyLib;
using System.Collections.Generic;

[HarmonyPatch(typeof(PnlRank), "UIRefresh")]
internal class Refresh_Patch
{

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