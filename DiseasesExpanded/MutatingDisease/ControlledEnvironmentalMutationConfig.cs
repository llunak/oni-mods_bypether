﻿using UnityEngine;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class ControlledEnvironmentalMutationConfig : IEntityConfig
    {
        public const string ID = "ControlledEnvironmentalMutation";

        public string[] GetDlcIds() => DlcManager.AVAILABLE_EXPANSION1_ONLY;

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
            MutationData.Instance.BulkModifyMutation(MutationVectors.GetEnvironmentalVectors(), -1);
            MutationData.Instance.Mutate();
            PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, STRINGS.CONTROLLEDMUTATION.ENVIRONMENTAL.NAME, inst.transform);
            Util.KDestroyGameObject(inst);
        }

        public GameObject CreatePrefab()
        {
            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[2]
            {
                new ComplexRecipe.RecipeElement((Tag)MutatingGermFlask.ID, 1f),
                new ComplexRecipe.RecipeElement(SimHashes.UraniumOre.CreateTag(), VaccineApothecaryConfig.UraniumOreCost)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement((Tag) ID, 1, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            /*(ComplexRecipe recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(VaccineApothecaryConfig.ID, ingredients, results), ingredients, results)
            {
                time = VaccineApothecaryConfig.MutationRecipeTime,
                description = STRINGS.CONTROLLEDMUTATION.ENVIRONMENTAL.DESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { (Tag)VaccineApothecaryConfig.ID },
                sortOrder = 15
            };*/

            GameObject looseEntity = EntityTemplates.CreateLooseEntity(
                ID,
                STRINGS.CONTROLLEDMUTATION.ENVIRONMENTAL.NAME,
                STRINGS.CONTROLLEDMUTATION.ENVIRONMENTAL.DESC,
                1f,
                true,
                Assets.GetAnim(Kanims.ControlledMutation),
                "object",
                Grid.SceneLayer.Front,
                EntityTemplates.CollisionShape.RECTANGLE,
                0.8f,
                0.4f,
                true);

            return looseEntity;
        }
    }
}
