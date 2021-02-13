using System.Collections.Generic;
using BepInEx;
using HarmonyLib;
using UnityEngine.UI;
using UnityEngine;

namespace DSPModTesting
{
    [BepInPlugin("org.bepinex.plugins.morereplication", "More Replication", "1.2.1")]
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
			public static bool OnPlusButtonClick(UIReplicatorWindow __instance, ref RecipeProto ___selectedRecipe, ref Dictionary<int, int> ___multipliers, ref Text ___multiValueText, ref MechaForge ___mechaForge)
			{
				if (___selectedRecipe == null)
					return false;

				var stepModifier = 1;

				if (Input.GetKey(KeyCode.LeftControl)) stepModifier = 10;
				if (Input.GetKey(KeyCode.LeftShift)) stepModifier = 100;
				if (Input.GetKey(KeyCode.LeftAlt)) stepModifier = ___mechaForge.PredictTaskCount(___selectedRecipe.ID, 1000000) - 1;

				if (!__instance.multipliers.ContainsKey(___selectedRecipe.ID))
					__instance.multipliers[___selectedRecipe.ID] = 1;
				int num = __instance.multipliers[___selectedRecipe.ID] + 1 * stepModifier;
				if (num < 1)
					num = 1;
				__instance.multipliers[___selectedRecipe.ID] = num;
				___multiValueText.text = num.ToString() + "x";

				return false;
			}

			[HarmonyPrefix]
			[HarmonyPatch("OnMinusButtonClick")]
			public static bool OnMinusButtonClick(UIReplicatorWindow __instance, ref RecipeProto ___selectedRecipe, ref Dictionary<int, int> ___multipliers, ref Text ___multiValueText, ref MechaForge ___mechaForge)
			{
				if (___selectedRecipe == null)
					return false;

				var stepModifier = 1;

				if (Input.GetKey(KeyCode.LeftControl)) stepModifier = 10;
				if (Input.GetKey(KeyCode.LeftShift)) stepModifier = 100;
				if (Input.GetKey(KeyCode.LeftAlt)) stepModifier = ___mechaForge.PredictTaskCount(___selectedRecipe.ID, 1000000) - 1;

				if (!__instance.multipliers.ContainsKey(___selectedRecipe.ID))
					__instance.multipliers[___selectedRecipe.ID] = 1;
				int num = __instance.multipliers[___selectedRecipe.ID] - 1 * stepModifier;
				if (num < 1)
					num = 1;
				__instance.multipliers[___selectedRecipe.ID] = num;
				___multiValueText.text = num.ToString() + "x";

				return false;
			}

			[HarmonyPrefix]
            [HarmonyPatch("OnOkButtonClick")]
            public static bool OnOkButtonClick(UIReplicatorWindow __instance, ref RecipeProto ___selectedRecipe, ref Dictionary<int, int> ___multipliers, ref MechaForge ___mechaForge)
            {
				if (___selectedRecipe != null)
				{
					if (!___selectedRecipe.Handcraft)
					{
						UIRealtimeTip.Popup("该配方".Translate() + ___selectedRecipe.madeFromString + "生产".Translate(), true, 0);
						return false;
					}
					int id = ___selectedRecipe.ID;
					if (!GameMain.history.RecipeUnlocked(id))
					{
						UIRealtimeTip.Popup("配方未解锁".Translate(), true, 0);
						return false;
					}
					int num = 1;
					if (___multipliers.ContainsKey(id))
					{
						num = ___multipliers[id];
					}
					if (num < 1)
					{
						num = 1;
					}
					int num2 = ___mechaForge.PredictTaskCount(___selectedRecipe.ID, 1000000);
					if (num > num2)
					{
						num = num2;
					}
					if (num == 0)
					{
						UIRealtimeTip.Popup("材料不足".Translate(), true, 0);
						return false;
					}
					if (___mechaForge.AddTask(id, num) == null)
					{
						UIRealtimeTip.Popup("材料不足".Translate(), true, 0);
					}
					else
					{
						GameMain.history.RegFeatureKey(1000104);
					}
				}

				return false;
			}
        }
    }
}