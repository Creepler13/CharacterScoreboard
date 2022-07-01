using Assets.Scripts.Database;

namespace CharacterScoreboard
{
    internal class DBUtils
    {
        public static string getElfinNameByIndex(int index)
        {
            LocalElfinInfo e = GlobalDataBase.dbConfig.m_ConfigDic["elfin"].Cast<DBConfigElfin>().GetLocal().GetInfoByIndex(index);
            return e.name;
        }
        public static string getCharacterNameByIndex(int index)
        {
            LocalCharacterInfo e = GlobalDataBase.dbConfig.m_ConfigDic["character"].Cast<DBConfigCharacter>().GetLocal().GetInfoByIndex(index);
            return e.cosName;
        }

    }
}
