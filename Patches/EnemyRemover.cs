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
            On.RoundManager.Awake += Awake;
            On.RoundManager.Update += Update;
        }

        private static void Awake(On.RoundManager.orig_Awake orig, RoundManager self)
        {
            orig(self);
            if (Plugin.enemyRemoval.Get<bool>())
            {
                self.currentLevel.maxEnemyPowerCount = -1;
                self.currentLevel.maxDaytimeEnemyPowerCount = -1;
                self.currentLevel.maxOutsideEnemyPowerCount = -1;
                Plugin.logSrc.LogWarning($"All enemy counts set to 0.");
                self.currentEnemyPower = 999;
                self.currentDaytimeEnemyPower = 999;
                self.currentOutsideEnemyPower = 999;
                Plugin.logSrc.LogWarning($"All enemy power set to 999.");
                self.cannotSpawnMoreInsideEnemies = true;
                Plugin.logSrc.LogWarning($"Cannot spawn more inside enemies is set to: {self.cannotSpawnMoreInsideEnemies}");
            }
            
        }
        private static void Update(On.RoundManager.orig_Update orig, RoundManager self) {
            orig(self);
            if (Plugin.enemyRemoval.Get<bool>()) {
                if (self.minEnemiesToSpawn > 0) {
                    self.minEnemiesToSpawn = 0;
                    self.currentEnemyPower = 999;
                    PeacefulCompany.Plugin.logSrc.LogWarning($"Min enemies to spawn set to 0.");
                }
                if (self.minOutsideEnemiesToSpawn > 0) {
                    self.minOutsideEnemiesToSpawn = 0;
                    self.currentDaytimeEnemyPower = 999;
                    self.currentOutsideEnemyPower = 999;
                    PeacefulCompany.Plugin.logSrc.LogWarning($"Min outside enemies to spawn set to 0.");
                }
            }
        }
    }
}