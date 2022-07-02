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

    }
}
