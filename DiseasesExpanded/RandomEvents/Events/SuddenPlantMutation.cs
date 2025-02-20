﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace DiseasesExpanded.RandomEvents.Events
{
    class SuddenPlantMutation : RandomDiseaseEvent
    {
        public SuddenPlantMutation(int weight = 1)
        {
            ID = nameof(SuddenPlantMutation);
            GeneralName = "Sudden Mutation";
            NameDetails = "Plant";
            AppearanceWeight = weight;
            DangerLevel = ONITwitchLib.Danger.Small;

            Condition = new Func<object, bool>(data => DlcManager.IsExpansion1Active());

            Event = new Action<object>(
                data =>
                {
                    int numberOfPlants = GameClock.Instance.GetCycle() / 5;
                    int max = Mathf.Min(100, Components.MutantPlants.Count);
                    numberOfPlants = Mathf.Clamp(numberOfPlants, 1, 100);

                    List<int> possibleIdx = new List<int>();
                    for (int i = 0; i < Components.MutantPlants.Count; i++)
                        possibleIdx.Add(i);
                    possibleIdx.Shuffle();

                    for (int i  =0; i < max; i++)
                    {
                        if (possibleIdx.Count == 0)
                            break;

                        int idx = possibleIdx[0];
                        possibleIdx.RemoveAt(0);

                        if (Components.MutantPlants[idx] != null && Components.MutantPlants[idx].GetComponent<SeedProducer>() != null)
                        {
                            Components.MutantPlants[idx].Mutate();
                            Components.MutantPlants[idx].ApplyMutations();
                        }
                    }

                    ONITwitchLib.ToastManager.InstantiateToast(GeneralName, "Some of our plants mutated to have bigger leaves, richer fruits and... is that a tentacle?!?!");
                });
        }
    }
}
