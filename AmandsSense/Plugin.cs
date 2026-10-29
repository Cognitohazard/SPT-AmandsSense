using AmandsSense.Components;
using AmandsSense.Helpers;
using BepInEx;
using System.IO;
using System.Reflection;
using AmandsSense.Patches;

namespace AmandsSense
{
    [BepInPlugin("xyz.drakia.Sense", "Sense", "3.1.0")]
    // A Fika headless has no camera or human player to show markers to
    [BepInIncompatibility("com.fika.headless")]
    public class AmandsSensePlugin : BaseUnityPlugin
    {
        public static string PluginFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public void Start()
        {
            Settings.Init(Config, Info);

            new GameStartedPatch().Enable();
            new AmandsPlayerPatch().Enable();
            new AmandsKillPatch().Enable();
            new AmandsSenseExfiltrationPatch().Enable();
            new AmandsSensePrismEffectsPatch().Enable();

            AmandsSenseHelper.Init();
        }
    }
}
