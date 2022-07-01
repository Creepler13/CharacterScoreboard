
using Assets.Scripts.UI.Panels;
using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(PnlRank), "Update")]



internal class ScoreBoardInit_Patch
{

    private static void Postfix(PnlRank __instance)
    {
        ToggleManager.rank = __instance;
        bool flag4 = ToggleManager.Toggle == null && ToggleManager.vSelect != null;
        if (flag4)
        {
            GameObject CharacterScoreboardToggle = UnityEngine.Object.Instantiate<GameObject>(ToggleManager.vSelect.transform.Find("LogoSetting").Find("Toggles").Find("TglOn").gameObject, __instance.transform.parent);
            ToggleManager.Toggle = CharacterScoreboardToggle;

            ToggleManager.SetupCharacterScoreboardToggle();
        }

    }
}
