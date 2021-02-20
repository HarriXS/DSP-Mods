using System.Reflection;
using System.Collections.Generic;
using BepInEx;
using HarmonyLib;
using UnityEngine.UI;
using UnityEngine;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;

namespace DSPModTesting
{
    [BepInPlugin("org.bepinex.plugins.morereplication", "More Replication", "1.2.1")]
    class MoreReplication : BaseUnityPlugin
    {
        internal void Awake()
        {
            var harmony = new Harmony("org.bepinex.plugins.morereplication");
            Harmony.CreateAndPatchAll(typeof(Patch));
			new ILHook(typeof(UIReplicatorWindow).GetMethod("OnOkButtonClick", BindingFlags.NonPublic | BindingFlags.Instance), new ILContext.Manipulator(Patch.OkButtonClickIL));
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

			public static void OkButtonClickIL(ILContext il)
			{
				ILCursor c = new ILCursor(il);

				ILLabel placeToJump = null;

				c.GotoNext(MoveType.Before,
					x => x.MatchLdloc(1),
					x => x.MatchLdcI4(10),
					x => x.MatchBle(out placeToJump),
					x => x.MatchLdcI4(10),
					x => x.MatchStloc(1)
				);

				var labels = c.IncomingLabels;

				c.Emit(OpCodes.Br, placeToJump);

				foreach (var label in labels)
					label.Target = c.Prev;

				c.GotoNext(MoveType.Before,
					x => x.MatchLdcI4(99),
					x => x.MatchCallOrCallvirt(typeof(MechaForge).GetMethod("PredictTaskCount"))
				);

				c.Next.OpCode = OpCodes.Ldc_I4;
				c.Next.Operand = int.MaxValue - 1;
			}
        }
    }
}