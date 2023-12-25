﻿using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using EnermyRemoverPatch;
using OutdoorHazardRemoverPatch;
using IndoorHazardRemoverPatch;
using ConfigurableCompany;
using ConfigurableCompany.model;
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
        public static Configuration enemyRemoval;
        public static Configuration outdoorHazardsRemoval;
        public static Configuration indoorHazardsRemoval;
        public Plugin()
        {
            //Constructor
            enemyRemoval = ConfigurationBuilder.NewConfig("enemy_removal")
                .SetName("Disable Enemy Spawning")
                .SetType(ConfigurationType.Boolean)
                .SetValue(true)
                .SetTooltip("Disables spawning of Hostile Enemies (Minus the Bees) (!!May Require Restart!!)")
                .SetSyncronized(true)
                .Build();
            outdoorHazardsRemoval = ConfigurationBuilder.NewConfig("outdoor_removal")
                .SetName("Disable Outdoor Hazard Spawning")
                .SetType(ConfigurationType.Boolean)
                .SetValue(true)
                .SetTooltip("Disables spawning of Outdoor Hazards (Lightning, Floods, etc) (!!May Require Restart!!)")
                .SetSyncronized(true)
                .Build();
            indoorHazardsRemoval = ConfigurationBuilder.NewConfig("indoor_removal")
                .SetName("Disable Indoor Hazard Spawning")
                .SetType(ConfigurationType.Boolean)
                .SetValue(true)
                .SetTooltip("Disables spawning of Indoor Hazards (Turrets, Landmines) (!!May Require Restart!!)")
                .SetSyncronized(true)
                .Build();
        }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            try {
                if (enemyRemoval.Get<bool>()) {
                    EnemyPatch.Init();
                    logSrc.LogInfo($"Patched EnemyPatch!");
                } else {
                    logSrc.LogWarning($"Did Not Patch 'EnemyRemover' Due to Config Setting.");
                }
                if (outdoorHazardsRemoval.Get<bool>()) {
                    OutdoorHazardPatch.Init();
                    logSrc.LogInfo($"Patched OutdoorHazardPatch!");
                } else {
                    logSrc.LogWarning($"Did Not Patch 'OutdoorHazardRemover' Due to Config Setting.");
                }
                if (indoorHazardsRemoval.Get<bool>()) {
                    IndoorHazardPatch.Init();
                    logSrc.LogInfo($"Patched IndoorHazardPatch!");
                } else {
                    logSrc.LogWarning($"Did Not Patch 'IndoorHazardRemover' Due to Config Setting.");
                }
            } catch (System.Exception e) {
                logSrc.LogError($"Error patching Patch files: {e}");
            }
            
            logSrc.LogInfo($"Plugin {modGUID} is loaded!");
        }
    }
}
