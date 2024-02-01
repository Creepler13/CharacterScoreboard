using Il2CppAssets.Scripts.PeroTools.Nice.Events;
using Il2CppAssets.Scripts.PeroTools.Nice.Variables;
using UnityEngine.Events;
using UnityEngine.UI;
using Text = UnityEngine.UI.Text;

namespace CharacterScoreboard;

internal static class ToggleManager
{
    internal static GameObject OcsToggle { get; set; }
    internal static GameObject VSelect { get; set; }
    internal static PnlRank Rank { get; set; }
    internal static string Uid { get; set; }

    internal static void SetupToggle()
    {
        OcsToggle.name = "OCS";
        var text = OcsToggle.transform.Find("Txt").GetComponent<Text>();
        var foreground = OcsToggle.transform.Find("Background").GetComponent<Image>();
        var background = OcsToggle.transform.Find("Background").GetChild(0).GetComponent<Image>();
        var toggle = OcsToggle.GetComponent<Toggle>();
        OcsToggle.transform.position = new Vector3(-4f, 1.2f, 100f);
        OcsToggle.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
        OcsToggle.GetComponent<OnToggle>().enabled = false;
        OcsToggle.GetComponent<OnToggleOn>().enabled = false;
        OcsToggle.GetComponent<OnActivate>().enabled = false;
        OcsToggle.GetComponent<VariableBehaviour>().enabled = false;
        toggle.group = null;
        toggle.SetIsOnWithoutNotify(Setting.OcsEnabled);
        toggle.onValueChanged.AddListener((UnityAction<bool>)((bool val) =>
        {
            Setting.OcsEnabled = val;
            if (Rank != null)
            {
                Rank.UIRefresh(Uid);
            }
        }));
        text.text = "OCS";
        text.fontSize = 40;
        text.color = Rank.refresh.image.color;
        text.transform.localScale = new Vector3(1.2f, 1.45f, 1f);
        var val2 = text.transform.Cast<RectTransform>();
        var offsetMax = val2.offsetMax;
        val2.offsetMax = new Vector2(text.preferredWidth + 10f, offsetMax.y);
        foreground.color = Rank.refresh.image.color;
        background.color = Rank.refresh.image.color;
    }
}