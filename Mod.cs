using MelonLoader;


namespace CharacterScoreboard
{
    public class MyMod : MelonMod
    {
        public override void OnApplicationStart()
        {

            PrefferenceManager.Load();
        }


    }
}
