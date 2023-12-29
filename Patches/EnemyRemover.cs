using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using MonoMod.Cil;
using PeacefulCompany;
using Unity.Netcode;
using UnityEngine;

namespace EnermyRemoverPatch {
    internal class EnemyPatch {
        public static void Init() {
            On.RoundManager.SpawnRandomDaytimeEnemy += SpawnRandomDaytimeEnemy;
            On.RoundManager.SpawnRandomOutsideEnemy += SpawnRandomOutsideEnemy;
            On.RoundManager.AssignRandomEnemyToVent += AssignRandomEnemyToVent;
        }
        private static GameObject SpawnRandomDaytimeEnemy(On.RoundManager.orig_SpawnRandomDaytimeEnemy orig, RoundManager self, GameObject[] spawnPoints, float timeUpToCurrentHour) {
            if (Plugin.enemyRemoval.Get<bool>()) {
                Plugin.logSrc.LogWarning("Tried to Spawn Random Daytime Enemy, Removed Function.");
                return null;
            } else {
                return orig(self, spawnPoints, timeUpToCurrentHour);
            }
        }
        private static GameObject SpawnRandomOutsideEnemy(On.RoundManager.orig_SpawnRandomOutsideEnemy orig, RoundManager self, GameObject[] spawnPoints, float timeUpToCurrentHour) {
            if (Plugin.enemyRemoval.Get<bool>()) {
                Plugin.logSrc.LogWarning("Tried to Spawn Random Daytime Enemy, Removed Function.");
                return null;
            } else {
                return orig(self, spawnPoints, timeUpToCurrentHour);
            }
        }
        private static bool AssignRandomEnemyToVent(On.RoundManager.orig_AssignRandomEnemyToVent orig, RoundManager self, EnemyVent vent, float spawnTime) {
            if (Plugin.enemyRemoval.Get<bool>()) {
                Plugin.logSrc.LogWarning("Tried to Spawn Random Daytime Enemy, Removed Function.");
                return false;
            } else {
                return orig(self, vent, spawnTime);
            }
        }
    }
}