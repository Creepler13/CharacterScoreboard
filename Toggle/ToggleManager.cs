using Assets.Scripts.PeroTools.Nice.Events;
using Assets.Scripts.PeroTools.Nice.Variables;
using Assets.Scripts.UI.Panels;
using UnityEngine;
using UnityEngine.UI;

public class ToggleManager
{
    public static GameObject Toggle;

    public static GameObject DCSToggle;

    public static GameObject vSelect;

    public static PnlStage stage;

    public static PnlRank rank;

    public static bool CharacterScoreboard;

    public static void Characterscoreboard_On()
    {
        ToggleManager.CharacterScoreboard = true;
    }

    public static void Characterscoreboard_Off()
    {
        ToggleManager.CharacterScoreboard = false;
    }

    public static bool DCS;

    public static void DCS_On()
    {
        ToggleManager.DCS = true;
    }

    public static void DCS_Off()
    {
        ToggleManager.DCS = false;
    }

    public static void SetupCharacterScoreboardToggle()
    {
        Toggle.name = "CharacterScoreboard";
        UnityEngine.UI.Text component = Toggle.transform.Find("Txt").GetComponent<UnityEngine.UI.Text>();
        Image component2 = Toggle.transform.Find("Background").GetComponent<Image>();
        Image component3 = Toggle.transform.Find("Background").GetChild(0).GetComponent<Image>();
        Toggle component4 = Toggle.GetComponent<Toggle>();
        Toggle.transform.position = new Vector3(-5f, 1.2f, 100f);
        Toggle.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
        Toggle.GetComponent<OnToggle>().enabled = false;
        Toggle.GetComponent<OnToggleOn>().enabled = false;
        Toggle.GetComponent<OnActivate>().enabled = false;
        Toggle.GetComponent<VariableBehaviour>().enabled = false;
        component4.group = null;
        component4.SetIsOnWithoutNotify(PrefferenceManager.CSEnabled);
        System.Action<bool> value = delegate (bool val)
                    {
                        UIRefresh_Patch.refreshedAfterChange.Clear();
                        PrefferenceManager.CSEnabled = val;
                        if (rank != null) rank.Refresh(true);
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
        DCSToggle.name = "OCS";
        UnityEngine.UI.Text component = DCSToggle.transform.Find("Txt").GetComponent<UnityEngine.UI.Text>();
        Image component2 = DCSToggle.transform.Find("Background").GetComponent<Image>();
        Image component3 = DCSToggle.transform.Find("Background").GetChild(0).GetComponent<Image>();
        Toggle component4 = DCSToggle.GetComponent<Toggle>();
        DCSToggle.transform.position = new Vector3(-6.5f, 1.2f, 100f);
        DCSToggle.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
        DCSToggle.GetComponent<OnToggle>().enabled = false;
        DCSToggle.GetComponent<OnToggleOn>().enabled = false;
        DCSToggle.GetComponent<OnActivate>().enabled = false;
        DCSToggle.GetComponent<VariableBehaviour>().enabled = false;
        component4.group = null;
        component4.SetIsOnWithoutNotify(PrefferenceManager.DCSEnabled);
        System.Action<bool> value = delegate (bool val)
        {
            PrefferenceManager.DCSEnabled = val;
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
