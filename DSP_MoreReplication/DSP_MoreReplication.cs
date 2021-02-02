using System.Collections.Generic;
using BepInEx;
using HarmonyLib;
using UnityEngine.UI;

namespace DSPModTesting
{
    [BepInPlugin("org.bepinex.plugins.morereplication", "More Replication Plug-in", "1.0.0.0")]
    class MoreReplication : BaseUnityPlugin
    {
        internal void Awake()
        {
            var harmony = new Harmony("org.bepinex.plugins.morereplication");
            Harmony.CreateAndPatchAll(typeof(Patch));
        }

        [HarmonyPatch(typeof(UIReplicatorWindow))]
        private class Patch
        {
            [HarmonyPrefix]
            [HarmonyPatch("OnPlusButtonClick")]
            public static bool OnPlusButtonClick(UIReplicatorWindow __instance, ref RecipeProto ___selectedRecipe, ref Dictionary<int, int> ___multipliers, ref Text ___multiValueText)
            {
                if (___selectedRecipe != null)
                {
                    // if the recipe hasn't been loaded before, set the craft amount to default value (1)
                    if (!___multipliers.ContainsKey(___selectedRecipe.ID))
                    {
                        ___multipliers[___selectedRecipe.ID] = 1;
                    }
                    ___multipliers[___selectedRecipe.ID]++;
                    ___multiValueText.text = ___multipliers[___selectedRecipe.ID].ToString() + "x";
                    return false;
                }
                return false;
            }

            [HarmonyPostfix]
            [HarmonyPatch("OnOkButtonClick")]
            public static void OnOkButtonClick(UIReplicatorWindow __instance, ref RecipeProto ___selectedRecipe, ref Dictionary<int, int> ___multipliers, ref MechaForge ___mechaForge)
            {
                if (___selectedRecipe != null)
                {
                    int num = 1;

                    if (___multipliers.ContainsKey(___selectedRecipe.ID))
                    {
                        num = ___multipliers[___selectedRecipe.ID];
                    }

                    if (num < 1)
                    {
                        num = 1;
                    }

                    int num2 = ___mechaForge.PredictTaskCount(___selectedRecipe.ID, 99);
                    if (num > num2)
                    {
                        num = num2;
                    }
                    ___mechaForge.AddTask(___selectedRecipe.ID, num);
                }
            }
        }
    }
}
