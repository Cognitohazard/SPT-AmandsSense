using BepInEx.Bootstrap;

namespace AmandsSense.Helpers
{
    // Detects Project Fika by plugin GUID only, so there is no hard dependency and the DLL still loads solo
    public static class FikaCompat
    {
        public static bool IsFika
        {
            get { return Chainloader.PluginInfos.ContainsKey("com.fika.core"); }
        }
    }
}
