using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;

namespace DSP_NoAdvisor
{
    [BepInPlugin("org.bepinex.plugins.noadvisor", "No Advisor Plug-in", "1.0.0.0")]
    public class DSP_NoAdvisor : BaseUnityPlugin
    {
        internal void Awake()
        {
            var harmony = new Harmony("org.bepinex.plugins.noadvisor");
            Harmony.CreateAndPatchAll(typeof(NoPlayAdvisorTip));
            Harmony.CreateAndPatchAll(typeof(NoRunAdvisorTip));
        }

        [HarmonyPatch(typeof(UIAdvisorTip), "PlayAdvisorTip")]
        public class NoPlayAdvisorTip
        {
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                var codes = new List<CodeInstruction>(instructions);
                codes[0].opcode = OpCodes.Ret;
                return codes.AsEnumerable();
            }
        }

        [HarmonyPatch(typeof(UIAdvisorTip), "RunAdvisorTip")]
        public class NoRunAdvisorTip
        {
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                var codes = new List<CodeInstruction>(instructions);
                codes[0].opcode = OpCodes.Ret;
                return codes.AsEnumerable();
            }
        }
    }
}