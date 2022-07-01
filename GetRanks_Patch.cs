using Account;
using Assets.Scripts.Database;
using Assets.Scripts.PeroTools.Commons;
using CharacterScoreboard;
using HarmonyLib;
using Il2CppNewtonsoft.Json.Linq;
using Il2CppSystem;

[HarmonyPatch(typeof(GameAccountSystem), "GetRanks")]
internal static class GetRanks_Patch
{
    private static Action<JToken, JToken, int> p_callback;

    [HarmonyPriority(Priority.First)]
    private static bool Prefix(string musicUid, int difficulty, ref Action<JToken, JToken, int> callback, Action<long, string> failCallback)
    {

        p_callback = callback;

        if (!PrefferenceManager.CSEnabled)
        {
            System.Action<JToken, JToken, int> test = delegate (JToken a, JToken b, int c)
            {
            MelonLoader.MelonLogger.Msg(JsonUtils.Serialize<JToken>(b));

                JObject rankB = a.Cast<JObject>();

                if (rankB.ContainsKey()) { }
                string nameedit = $"({DBUtils.getCharacterNameByIndex(int.Parse(equip[0]))} & {DBUtils.getElfinNameByIndex(int.Parse(equip[1]))})";   

            if (!((Delegate)(object)p_callback == (Delegate)null))
                {
                    p_callback.Invoke(a, b, c);
                }
            }
           ;


            callback = test;

            return true;
        }

        p_callback = callback;

        JObject runs = Registry.getStage(musicUid, difficulty);

        JObject val = new JObject();

        JArray results = new JArray();


        foreach (string key in JsonUtils.Keys(runs))
        {
            JObject run = new JObject();

            run["play"] = new JObject();
            run["play"]["score"] = runs[key]["score"];
            run["play"]["acc"] = runs[key]["acc"];

            run["user"] = new JObject();

            string[] equip = key.Split("&".ToCharArray());

            string name = DBUtils.getCharacterNameByIndex(int.Parse(equip[0])) + " & " + DBUtils.getElfinNameByIndex(int.Parse(equip[1]));

            run["user"]["nickname"] = name;

            int i;
            for (i = 0; i < results.Count; i++)
                if ((float)run["play"]["score"] > (float)results[i]["play"]["score"])
                    break;

            results.Insert(i, run);
        }


        JObject rank = new JObject();
        rank["detail"] = new JObject();

        string key2 = DataHelper.selectedRoleIndex + "&" + DataHelper.selectedElfinIndex;
        if (runs.ContainsKey(key2))
        {
            rank["detail"]["acc"] = runs[key2]["acc"];
            rank["detail"]["score"] = runs[key2]["score"];
        }
        else
        {
            rank["detail"]["acc"] = 0;
            rank["detail"]["score"] = 0;
        }


        rank["detail"]["nickname"] = DBUtils.getCharacterNameByIndex(DataHelper.selectedRoleIndex) + " & " + DBUtils.getElfinNameByIndex(DataHelper.selectedElfinIndex);

        rank["order"] = null;
        if (runs.ContainsKey(key2))
        {
            for (int i = 0; i < results.Count; i++)
                if ((float)runs[key2]["score"] == (float)results[i]["play"]["score"])
                    rank["order"] = i;
        }
        else rank["order"] = results.Count;

        val["result"] = results;
        val["rank"] = rank;



        JToken val2 = val.GetValue("result");
        JToken val3 = val.GetValue("rank");


        System.Action<JToken, JToken, int> value = delegate (JToken a, JToken b, int c)
        {
            if (!((Delegate)(object)p_callback == (Delegate)null))
            {
                p_callback.Invoke(val2, val3, 0);
            }
        }
       ;

        callback = value;

        if (!((Delegate)(object)p_callback == (Delegate)null))
        {
            p_callback.Invoke(val2, val3, 0);
        }

        return false;
    }
}
