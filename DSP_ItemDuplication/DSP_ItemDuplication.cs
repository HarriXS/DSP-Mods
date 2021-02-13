using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
/*
*  public void Awake()
{
new ILHook(typeof(PlanetGen).GetMethod("CreatePlanet"), new ILContext.Manipulator(ScalePatch));
}

public static void ScalePatch(ILContext il)
{
ILCursor c = new ILCursor(il);

c.GotoNext(MoveType.Before,
x => x.MatchStfld(typeof(PlanetData).GetField("scale"))
);

c.Emit(OpCodes.Pop);
c.Emit(OpCodes.Ldc_R4, 40f);
}*/

namespace DSP_ItemDuplication
{
    [BepInPlugin("org.harrixs.plugins.DSP.itemduplication", "Item Duplicator Plug-in", "1.0")]
    public class DSP_ItemDuplication : BaseUnityPlugin
    {
        /*public void Awake()
        {
            //new ILHook(typeof(Player).GetMethod("AddHandItemCount_Unsafe"), new ILContext.Manipulator(AddItemsPatch));
            new ILHook(typeof(StorageComponent).GetMethod("TakeItemFromGrid"), new ILContext.Manipulator(TakeItemFromGridPatch));
        }*/

        /*public static void TakeItemFromGridPatch(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            c.GotoNext(MoveType.Before,
                x => x.MatchStfld(typeof(StorageComponent.GRID).GetField("itemId")),
                x => x.MatchLdarg(0),
                x => x.MatchLdfld(typeof(StorageComponent.GRID[]).GetField("grids")),
                x => x.MatchLdarg(1),
                x => x.MatchLdelema(typeof(StorageComponent.GRID)),
                x => x.MatchLdcI4(0),
                x => x.MatchStfld(typeof(StorageComponent.GRID).GetField("count")),
                x => x.MatchLdarg(0)//,
                //x => x.MatchLdfld(typeof(StorageComponent.GRID).GetField("grids"))
            );
            
            for (int i = 0; i < 8; i++)
            {
                c.Next.OpCode = OpCodes.Nop;
                c.Index++;
            }
        }*/

        /*public static void AddItemsPatch(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            c.GotoNext(MoveType.Before,
                x => x.MatchLdarg(1)
            );

            c.Index += 1;
            c.EmitDelegate<Func<int, int>>((num) =>
            {
                if (Input.GetKey(KeyCode.LeftAlt))
                {
                    return 0;
                }
                return num;
            });
        }*/

        public void Awake()
        {
            var harmony = new Harmony("org.harrixs.plugins.DSP.itemduplication");
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }

        [HarmonyPatch(typeof(StorageComponent))]
        public class Patch
        {
            [HarmonyPrefix]
            [HarmonyPatch("TakeItemFromGrid")]
            public static bool TakeItemFromGridPrefix(StorageComponent __instance, ref int gridIndex, ref int itemId, ref int count)
            {
                if (__instance.grids[gridIndex].itemId == 0 || (itemId != 0 && __instance.grids[gridIndex].itemId != itemId))
                {
                    itemId = 0;
                    count = 0;
                    return false;
                }
                itemId = __instance.grids[gridIndex].itemId;
                if (count == 0)
                {
                    count = __instance.grids[gridIndex].count;
                    if (!Input.GetKey(KeyCode.LeftAlt))
                    {
                        __instance.grids[gridIndex].itemId = __instance.grids[gridIndex].filter;
                        __instance.grids[gridIndex].count = 0;
                        if (__instance.grids[gridIndex].filter == 0)
                        {
                            __instance.grids[gridIndex].stackSize = 0;
                        }
                    }
                }
                else if (__instance.grids[gridIndex].count > count)
                {
                    StorageComponent.GRID[] array = __instance.grids;
                    array[gridIndex].count = array[gridIndex].count - count;
                }
                else
                {
                    count = __instance.grids[gridIndex].count;
                    __instance.grids[gridIndex].itemId = __instance.grids[gridIndex].filter;
                    __instance.grids[gridIndex].count = 0;
                    if (__instance.grids[gridIndex].filter == 0)
                    {
                        __instance.grids[gridIndex].stackSize = 0;
                    }
                }
                if (count == 0)
                {
                    itemId = 0;
                    count = 0;
                }
                else
                {
                    __instance.lastFullItem = -1;
                    __instance.NotifyStorageChange();
                }
                return false;
            }
        }
    }
}

        /*[HarmonyPatch(typeof(UIStorageGrid))]
        public class ItemDuplications
        {
            [HarmonyPatch("HandPut")]
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {

                /* IL_00CC: neg
                 * IL_00CD: callvirt  instance void Player::AddHandItemCount_Unsafe(int32) 

                var codes = new List<CodeInstruction>(instructions);

                for (int i = 0; i < codes.Count - 2; i++)
                {
                    if (codes[i].opcode == OpCodes.Ldloc_0)
                    {
                        if (codes[i + 1].opcode == OpCodes.Neg)
                        {
                            if (codes[i + 2].opcode == OpCodes.Callvirt)
                            {
                                yield return Transpilers.EmitDelegate<Func<int, int>>((num) =>
                                {
                                    if (!Input.GetKey(KeyCode.LeftAlt))
                                    {
                                        return 0;
                                    }
                                    else
                                    {
                                        return num;
                                    }
                                });
                            }
                        }
                    }
                    yield return codes[i];
                }
            }
        }
    }
}

/*
ldc.i4 308
call bool[UnityEngine.CoreModule] UnityEngine.Input::GetKey(valuetype[UnityEngine.CoreModule] UnityEngine.KeyCode)
ldc.i4.0
ceq
stloc.1
ldloc.1
brfalse.s
ldloc.0
neg
callvirt  instance void Player::AddHandItemCount_Unsafe(int32)
*/
