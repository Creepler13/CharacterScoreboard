
using MelonLoader;
using MelonLoader.Preferences;

internal class PrefferenceManager
{
    private static MelonPreferences_Category ToggleCategory;

    public static MelonPreferences_Entry<bool> CSEnabled_P;

    internal static bool CSEnabled
    {
        get { return CSEnabled_P.Value; }
        set { CSEnabled_P.Value = value; }
    }

    internal static void Load()
    {
        ToggleCategory = MelonPreferences.CreateCategory("Toggle");
        ToggleCategory.SetFilePath("UserData/CharacterScoreboard.cfg", true);
        CSEnabled_P = ToggleCategory.CreateEntry<bool>("CharacterScoreboard Enabled", false, (string)null, "Whether the CharacterScoreboard is enabled.", false, false, (ValueValidator)null, (string)null);
    }
}
