
using HarmonyLib;
using Assets.Scripts.UI.Panels;
using UnityEngine.UI;

namespace CharacterScoreboard
{


    [HarmonyPatch(typeof(PnlRank),"UIRefresh")]
    internal class RankCellConstructor_Patch

    {

        private static void Postfix()
        {
        //    ToggleManager.rank.scrollView.get
            


        }
    }

}
