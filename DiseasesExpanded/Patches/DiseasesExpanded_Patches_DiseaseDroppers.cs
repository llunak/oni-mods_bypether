﻿using HarmonyLib;
using Database;
using System;
using UnityEngine;
using Klei.AI;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_DiseaseDroppers
    {
        [HarmonyPatch(typeof(EvilFlowerConfig))]
        [HarmonyPatch("CreatePrefab")]
        public static class EvilFlowerConfig_CreatePrefab_Patch
        {
            public static void Postfix(GameObject __result)
            {
                __result.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = ZombieSpores.ID;
            }
        }

        [HarmonyPatch(typeof(PrickleFlowerConfig))]
        [HarmonyPatch("CreatePrefab")]
        public static class PrickleFlowerConfig_CreatePrefab_Patch
        {
            public static void Postfix(GameObject __result)
            {
                __result.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = PollenGerms.ID;
            }
        }

        [HarmonyPatch(typeof(BulbPlantConfig))]
        [HarmonyPatch("CreatePrefab")]
        public static class BulbPlantConfig_CreatePrefab_Patch
        {
            public static void Postfix(GameObject __result)
            {
                __result.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = PollenGerms.ID;
            }
        }

        [HarmonyPatch(typeof(CrabFreshWaterConfig))]
        [HarmonyPatch("CreatePrefab")]
        public static class CrabFreshWaterConfig_CreatePrefab_Patch
        {
            public static void Postfix(GameObject __result)
            {
                DiseaseEmitter diseaseEmitter = __result.GetComponent<DiseaseEmitter>();
                if (diseaseEmitter == null)
                    return;

                List<Disease> diseases = new List<Disease>()
                {
                  Db.Get().Diseases.FoodGerms,
                  Db.Get().Diseases.PollenGerms,
                  Db.Get().Diseases.SlimeGerms,
                  Db.Get().Diseases.ZombieSpores
                };

                if (Settings.Instance.FrostPox.IncludeDisease)
                    diseases.Add(Db.Get().Diseases.Get(FrostShards.ID));
                if (Settings.Instance.MooFlu.IncludeDisease)
                    diseases.Add(Db.Get().Diseases.Get(GassyGerms.ID));
                if (Settings.Instance.AlienGoo.IncludeDisease)
                    diseases.Add(Db.Get().Diseases.Get(AlienGerms.ID));
                if (Settings.Instance.MutatingVirus.IncludeDisease)
                    diseases.Add(Db.Get().Diseases.Get(MutatingGerms.ID));

                if (DlcManager.IsExpansion1Active())
                    diseases.Add(Db.Get().Diseases.RadiationPoisoning);
                if (DlcManager.IsExpansion1Active() && Settings.Instance.BogInsects.IncludeDisease)
                    diseases.Add(Db.Get().Diseases.Get(BogInsects.ID));
                if (DlcManager.IsExpansion1Active() && Settings.Instance.HungerGerms.IncludeDisease)
                    diseases.Add(Db.Get().Diseases.Get(HungerGerms.ID));

                diseaseEmitter.SetDiseases(diseases);
            }
        }
    }
}
