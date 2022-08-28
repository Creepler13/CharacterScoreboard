using Assets.Scripts.Database;

namespace CharacterScoreboard
{
    internal class DBUtils
    {
        public static string getElfinNameByIndex(int index)
        {
            LocalElfinInfo e = GlobalDataBase.dbConfig.m_ConfigDic["elfin"].Cast<DBConfigElfin>().GetLocal().GetInfoByIndex(index);
            if (e != null)
                return e.name;
            else return "not registered:" + index;
        }
        public static string getCharacterNameByIndex(int index)
        {
            LocalCharacterInfo e = GlobalDataBase.dbConfig.m_ConfigDic["character"].Cast<DBConfigCharacter>().GetLocal().GetInfoByIndex(index);
            if (e != null)
                return e.cosName;
            else return "not registered:" + index;
        }

        public static string getCharacterElfinNameByIds(string s)
        {
            string name = "";

            string[] split = s.Split('&');

            int charID = 0;
            if (int.TryParse(split[0], out charID))
                name = name + getCharacterNameByIndex(charID) + " & ";
            else
                name = name + "CharacterID:" + split[0] + " & ";

            int elfinID = 0;
            if (int.TryParse(split[1], out elfinID))
                name = name + getElfinNameByIndex(elfinID);
            else
                name = name + "ElfinID:" + split[1];

            return name;
        }

    }
}
