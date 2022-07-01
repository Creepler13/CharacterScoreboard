
using Assets.Scripts.UI.Panels;
using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(PnlStage), "Awake")]
internal class Awake_Patch
{
    private static void Postfix(PnlStage __instance)
    {
        ToggleManager.stage = __instance;

        if (PrefferenceManager.CSEnabled)
        {
            ToggleManager.Characterscoreboard_On();
        }

        GameObject vSelect = null;
        foreach (Il2CppSystem.Object @object in __instance.transform.parent.parent.Find("Forward"))
        {
            Transform transform = @object.Cast<Transform>();
            bool flag3 = transform.name == "PnlVolume";
            if (flag3)
            {
                vSelect = transform.gameObject;
            }
        }
        ToggleManager.vSelect = vSelect;
    }
}
