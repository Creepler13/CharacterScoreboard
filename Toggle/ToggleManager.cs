using Assets.Scripts.PeroTools.Nice.Events;
using Assets.Scripts.PeroTools.Nice.Variables;
using Assets.Scripts.UI.Panels;
using UnityEngine;
using UnityEngine.UI;

public class ToggleManager
{
    public static GameObject Toggle;

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

    public static void SetupCharacterScoreboardToggle()
    {
        Toggle.name = "CharacterScoreboard Toggle";
        UnityEngine.UI.Text component = Toggle.transform.Find("Txt").GetComponent<UnityEngine.UI.Text>();
        Image component2 = Toggle.transform.Find("Background").GetComponent<Image>();
        Image component3 = Toggle.transform.Find("Background").GetChild(0).GetComponent<Image>();
        Toggle component4 = Toggle.GetComponent<Toggle>();
        Toggle.transform.position = new Vector3(-6f, 1.2f, 100f);
        Toggle.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
        Toggle.GetComponent<OnToggle>().enabled = false;
        Toggle.GetComponent<OnToggleOn>().enabled = false;
        Toggle.GetComponent<OnActivate>().enabled = false;
        Toggle.GetComponent<VariableBehaviour>().enabled = false;
        component4.group = null;
        component4.SetIsOnWithoutNotify(PrefferenceManager.CSEnabled);
        System.Action<bool> value = delegate (bool val)
                    {
                        Refresh_Patch.refreshedAfterChange.Clear();
                        PrefferenceManager.CSEnabled = val;
                        if (rank != null) rank.Refresh(true);
                    };
        component4.onValueChanged.AddListener(value);
        component.text = "Character Scoreboard".ToUpper();
        component.fontSize = 40;
        component.color = rank.refresh.image.color;
        component.transform.localScale = new Vector3(1.2f, 1.45f, 1f);
        RectTransform val2 = component.transform.Cast<RectTransform>();
        Vector2 offsetMax = val2.offsetMax;
        val2.offsetMax = new Vector2(component.preferredWidth + 10f, offsetMax.y);
        component2.color = rank.refresh.image.color;// new Color(0.235294119f, 8f / 51f, 37f / 85f);
        component3.color = rank.refresh.image.color; //new Color(0.403921574f, 31f / 85f, 26f / 51f);
    }

}
