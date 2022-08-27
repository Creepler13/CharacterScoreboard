using Assets.Scripts.PeroTools.Commons;
using Il2CppNewtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace CharacterScoreboard
{
    internal class Registry
    {

        private static Dictionary<string, JObject> loadedCategories = new Dictionary<string, JObject>();
        public static JObject getStage(string musicUid, int musicDifficulty)
        {
            JObject stages = loadChart(musicUid);

            if (stages.ContainsKey(musicDifficulty + ""))
                return stages[musicDifficulty + ""].ToObject<JObject>();
            return new JObject();
        }



        public static void saveRun(string musicUid, int musicDifficulty, string characterUid, string elfinUid, int score, float acc)
        {

            string key = characterUid + "&" + elfinUid;

            JObject stage = getStage(musicUid, musicDifficulty);

            if (stage.ContainsKey(key))
            {
                if ((float)stage[key]["score"] < score)
                {
                    stage[key]["score"] = score;
                    stage[key]["acc"] = acc;
                }
            }
            else
            {
                stage[key] = new JObject();
                stage[key]["score"] = score;
                stage[key]["acc"] = acc;
            }

            saveStage(musicUid, musicDifficulty, stage);
        }

        private static void saveStage(string musicUid, int musicDifficulty, JObject stage)
        {
            JObject chart = loadChart(musicUid);
            chart[musicDifficulty + ""] = stage;
            saveChart(musicUid, chart);
        }


        private static void saveChart(string id, JObject chart)
        {
            loadedCategories[id] = chart;

            if (!Directory.Exists("UserData/CharacterScoreboard")) Directory.CreateDirectory("UserData/CharacterScoreboard/");
            string path = "UserData/CharacterScoreboard/" + id + ".json";
            File.WriteAllText(path, JsonUtils.Serialize<JObject>(chart));
        }

        private static JObject loadChart(string id)
        {
            string path = "UserData/CharacterScoreboard/" + id + ".json";

            if (loadedCategories.ContainsKey(id))
                return loadedCategories[id];


            JObject chart = new JObject();
            if (File.Exists(path))
            {
                string text = File.ReadAllText(path);
                chart = JObject.Parse(text);
            }


            loadedCategories.Add(id, chart);
            return chart;
        }




    }
}
