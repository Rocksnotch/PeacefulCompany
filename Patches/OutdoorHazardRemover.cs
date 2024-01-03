using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using MonoMod.Cil;
using PeacefulCompany;
using Unity.Netcode;
using UnityEngine;

namespace OutdoorHazardRemoverPatch {
    internal class OutdoorHazardPatch {
        
        public static void Init() {
            On.FloodWeather.OnGlobalTimeSync += OnGlobalTimeSync;
            On.StormyWeather.SetStaticElectricityWarning += SetStaticElectricityWarning;
            On.StormyWeather.LightningStrikeRandom += LightningStrikeRandom;
            On.StormyWeather.LightningStrike += LightningStrike;
            On.RoundManager.SpawnOutsideHazards += SpawnOutsideHazards;
        }

        private static void OnGlobalTimeSync(On.FloodWeather.orig_OnGlobalTimeSync orig, FloodWeather self)
        {
            orig(self);
            if (Plugin.outdoorHazardsRemoval.Equals(true)) {
                self.floodLevelOffset = 0;
            }
        }
        private static void SetStaticElectricityWarning(On.StormyWeather.orig_SetStaticElectricityWarning orig, StormyWeather self, NetworkObject warningObject, float particleTime)
        {
            if (Plugin.outdoorHazardsRemoval.Equals(true)) {
                Plugin.logSrc.LogWarning($"Tried to give static warning, removed function.");
            } else {
                orig(self, warningObject, particleTime);
            }
            
        }
        private static void LightningStrikeRandom(On.StormyWeather.orig_LightningStrikeRandom orig, StormyWeather self) {
            if (Plugin.outdoorHazardsRemoval.Get<bool>()) {
                Plugin.logSrc.LogWarning($"Tried to summon random lightning, removed function.");
            } else {
                orig(self);
            
            }
        }
        private static void LightningStrike(On.StormyWeather.orig_LightningStrike orig, StormyWeather self, Vector3 strikePosition, bool useTargetedObject) {
            if (Plugin.outdoorHazardsRemoval.Get<bool>()) {
                Plugin.logSrc.LogWarning($"Tried to summon lightning, removed function.");
            } else {
                orig(self, strikePosition, useTargetedObject);
            
            }
        }
        private static void SpawnOutsideHazards(On.RoundManager.orig_SpawnOutsideHazards orig, RoundManager self) {
            if (Plugin.outdoorHazardsRemoval.Get<bool>()) {
                Plugin.logSrc.LogWarning($"Tried to spawn outside hazards, removed function.");
            } else {
                orig(self);
            }
        }
    }
}