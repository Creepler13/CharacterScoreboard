namespace CharacterScoreboard;

internal class Main : MelonMod
{
    public override void OnInitializeMelon()
    {
        LoggerInstance.Msg("CharacterScoreboard is loaded!");
        Load();
    }

    public override void OnDeinitializeMelon() => File.WriteAllText(Path.Combine("UserData", $"{Name}.cfg"), TomletMain.TomlStringFrom(Setting));
}