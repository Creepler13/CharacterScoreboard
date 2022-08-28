using Assets.Scripts.PeroTools.Nice.Events;
using Assets.Scripts.PeroTools.Nice.Variables;
using Assets.Scripts.UI.Panels;
using UnityEngine;
using UnityEngine.UI;

public class ToggleManager
{
    public static GameObject LCSToggle;

    public static GameObject OCSToggle;

    public static GameObject vSelect;

    public static PnlStage stage;

    public static PnlRank rank;

    public static bool LCS;

    public static void Characterscoreboard_On()
    {
        ToggleManager.LCS = true;
    }

    public static void Characterscoreboard_Off()
    {
        ToggleManager.LCS = false;
    }

    public static bool OCS;

    public static void DCS_On()
    {
        ToggleManager.OCS = true;
    }

    public static void DCS_Off()
    {
        ToggleManager.OCS = false;
    }

    public static void SetupCharacterScoreboardToggle()
    {
        LCSToggle.name = "CharacterScoreboard";
        UnityEngine.UI.Text component = LCSToggle.transform.Find("Txt").GetComponent<UnityEngine.UI.Text>();
        Image component2 = LCSToggle.transform.Find("Background").GetComponent<Image>();
        Image component3 = LCSToggle.transform.Find("Background").GetChild(0).GetComponent<Image>();
        Toggle component4 = LCSToggle.GetComponent<Toggle>();
        LCSToggle.transform.position = new Vector3(-4f, 1.2f, 100f);
        LCSToggle.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
        LCSToggle.GetComponent<OnToggle>().enabled = false;
        LCSToggle.GetComponent<OnToggleOn>().enabled = false;
        LCSToggle.GetComponent<OnActivate>().enabled = false;
        LCSToggle.GetComponent<VariableBehaviour>().enabled = false;
        component4.group = null;
        component4.SetIsOnWithoutNotify(PrefferenceManager.LCSEnabled);
        System.Action<bool> value = delegate (bool val)
                    {
                        // UIRefresh_Patch.refreshedAfterChange.Clear();
                        PrefferenceManager.LCSEnabled = val;
                        //    if (rank != null) rank.Refresh(true);
                        if (rank != null) rank.UIRefresh(UIRefresh_Patch.lastUIUpdatedUID);
                    };
        component4.onValueChanged.AddListener(value);
        component.text = "LCS".ToUpper();
        component.fontSize = 40;
        component.color = rank.refresh.image.color;
        component.transform.localScale = new Vector3(1.2f, 1.45f, 1f);
        RectTransform val2 = component.transform.Cast<RectTransform>();
        Vector2 offsetMax = val2.offsetMax;
        val2.offsetMax = new Vector2(component.preferredWidth + 10f, offsetMax.y);
        component2.color = rank.refresh.image.color;// new Color(0.235294119f, 8f / 51f, 37f / 85f);
        component3.color = rank.refresh.image.color; //new Color(0.403921574f, 31f / 85f, 26f / 51f);
    }

    public static void SetupDCSToggle()
    {
        OCSToggle.name = "OCS";
        UnityEngine.UI.Text component = OCSToggle.transform.Find("Txt").GetComponent<UnityEngine.UI.Text>();
        Image component2 = OCSToggle.transform.Find("Background").GetComponent<Image>();
        Image component3 = OCSToggle.transform.Find("Background").GetChild(0).GetComponent<Image>();
        Toggle component4 = OCSToggle.GetComponent<Toggle>();
        OCSToggle.transform.position = new Vector3(-5.5f, 1.2f, 100f);
        OCSToggle.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
        OCSToggle.GetComponent<OnToggle>().enabled = false;
        OCSToggle.GetComponent<OnToggleOn>().enabled = false;
        OCSToggle.GetComponent<OnActivate>().enabled = false;
        OCSToggle.GetComponent<VariableBehaviour>().enabled = false;
        component4.group = null;
        component4.SetIsOnWithoutNotify(PrefferenceManager.OCSEnabled);
        System.Action<bool> value = delegate (bool val)
        {
            PrefferenceManager.OCSEnabled = val;
            if (rank != null) rank.UIRefresh(UIRefresh_Patch.lastUIUpdatedUID);
        };
        component4.onValueChanged.AddListener(value);
        component.text = "OCS".ToUpper();
        component.fontSize = 40;
        component.color = rank.refresh.image.color;
        component.transform.localScale = new Vector3(1.2f, 1.45f, 1f);
        RectTransform val2 = component.transform.Cast<RectTransform>();
        Vector2 offsetMax = val2.offsetMax;
        val2.offsetMax = new Vector2(component.preferredWidth + 10f, offsetMax.y);
        component2.color = rank.refresh.image.color;// new Color(0.235294119f, 8f / 51f, 37f / 85f);
        component3.color = rank.refresh.image.color; //new Color(0
                                                     //.403921574f, 31f / 85f, 26f / 51f);
    }

}
