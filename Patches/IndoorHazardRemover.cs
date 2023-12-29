using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using MonoMod.Cil;
using Unity.Netcode;
using UnityEngine;
using PeacefulCompany;
namespace IndoorHazardRemoverPatch {
    internal class IndoorHazardPatch {
        
        public static void Init() {
            On.RoundManager.GeneratedFloorPostProcessing += GeneratedFloorPostProcessing;
        }
        private static void GeneratedFloorPostProcessing(On.RoundManager.orig_GeneratedFloorPostProcessing orig, RoundManager self) {
            if (self.IsServer && Plugin.indoorHazardsRemoval.Get<bool>()) {
                self.SpawnScrapInLevel();
            } else {
                orig(self);
            }
        }
    }
}