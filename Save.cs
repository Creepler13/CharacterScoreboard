using Tomlet.Attributes;

namespace CharacterScoreboard;

internal static class Save
{
    internal static Data Setting { get; private set; } = new(true);

    internal static void Load()
    {
        if (!File.Exists(Path.Combine("UserData", $"{Name}.cfg")))
        {
            var defaultConfig = TomletMain.TomlStringFrom(Setting);
            File.WriteAllText(Path.Combine("UserData", $"{Name}.cfg"), defaultConfig);
        }

        var data = File.ReadAllText(Path.Combine("UserData", $"{Name}.cfg"));
        Setting = TomletMain.To<Data>(data);
    }
}

internal class Data
{
    [TomlPrecedingComment("Whether the CharacterScoreboard is enabled")]
    internal bool OcsEnabled { get; set; }

    public Data()
    {
    }

    internal Data(bool ocsEnabled) => OcsEnabled = ocsEnabled;
}