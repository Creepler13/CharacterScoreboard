
using MelonLoader;
using MelonLoader.Preferences;

internal class PrefferenceManager
{
    private static MelonPreferences_Category ToggleCategory;

    public static MelonPreferences_Entry<bool> LCSEnabled_P;

    public static MelonPreferences_Entry<bool> OCSEnabled_P;

    internal static bool LCSEnabled
    {
        get { return LCSEnabled_P.Value; }
        set { LCSEnabled_P.Value = value; }
    }

    internal static bool OCSEnabled
    {
        get { return OCSEnabled_P.Value; }
        set { OCSEnabled_P.Value = value; }
    }

    internal static void Load()
    {
        ToggleCategory = MelonPreferences.CreateCategory("Toggle");
        ToggleCategory.SetFilePath("UserData/CharacterScoreboard.cfg", true);
        LCSEnabled_P = ToggleCategory.CreateEntry<bool>("CharacterScoreboard Enabled", false, (string)null, "Whether the CharacterScoreboard is enabled.", false, false, (ValueValidator)null, (string)null);
        OCSEnabled_P = ToggleCategory.CreateEntry<bool>("DCS Enabled", false, (string)null, "Whether the DCS is enabled.", false, false, (ValueValidator)null, (string)null);
    }
}
