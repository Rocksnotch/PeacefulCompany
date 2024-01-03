using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using EnermyRemoverPatch;
using OutdoorHazardRemoverPatch;
using IndoorHazardRemoverPatch;
using Amrv.ConfigurableCompany;
using System;

namespace PeacefulCompany
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class Plugin : BaseUnityPlugin
    {
        private const string modName = "Peaceful Company";
        private const string modVersion = "1.0.0";
        private const string modGUID = "Rocksnotch.PeacefulCompany";
        internal static Plugin Instance;
        public static ManualLogSource logSrc = BepInEx.Logging.Logger.CreateLogSource("loggingSource");
        public static Amrv.ConfigurableCompany.content.model.Configuration enemyRemoval;
        public static Amrv.ConfigurableCompany.content.model.Configuration outdoorHazardsRemoval;
        public static Amrv.ConfigurableCompany.content.model.Configuration indoorHazardsRemoval;
        public Plugin()
        {
            //Constructor
            enemyRemoval = LethalConfiguration.CreateConfig()
                .SetID("enemy_removal")
                .SetName("Disable Enemy Spawning")
                .SetType(ConfigurationTypes.Boolean)
                .SetValue(true)
                .SetSynchronized(true)
                .Build();
            outdoorHazardsRemoval = LethalConfiguration.CreateConfig()
                .SetID("outdoor_removal")
                .SetName("Disable Outdoor Hazard Spawning")
                .SetType(ConfigurationTypes.Boolean)
                .SetValue(true)
                .SetSynchronized(true)
                .Build();
            indoorHazardsRemoval = LethalConfiguration.CreateConfig()
                .SetID("indoor_removal")
                .SetName("Disable Indoor Hazard Spawning")
                .SetType(ConfigurationTypes.Boolean)
                .SetValue(true)
                .SetSynchronized(true)
                .Build();
        }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            try {
                EnemyPatch.Init();
                logSrc.LogInfo($"Patched EnemyPatch!");

                OutdoorHazardPatch.Init();
                logSrc.LogInfo($"Patched OutdoorHazardPatch!");

                IndoorHazardPatch.Init();
                logSrc.LogInfo($"Patched IndoorHazardPatch!");
            } catch (System.Exception e) {
                logSrc.LogError($"Error patching Patch files: {e}");
            }
            
            logSrc.LogInfo($"Plugin {modGUID} is loaded!");
        }
    }
}
