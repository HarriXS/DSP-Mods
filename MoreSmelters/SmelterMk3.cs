using System.Collections.Generic;
using HarmonyLib;
using xiaoye97;
using UnityEngine;
using System.Reflection;

namespace DoubleSmelter
{
    class SmelterMk3
    {
        public void AddSmelterMk3Data()
        {
            var smelter = LDB.items.Select(2302);
            var smelterr = LDB.recipes.Select(56);

            // Copy the Protos to a new Storage Mk III building
            ItemProto smelterMk3 = smelter.Copy();
            RecipeProto smelterMk3r = smelterr.Copy();

            Traverse.Create(smelterMk3).Field("_iconSprite").SetValue(smelter.iconSprite);
            Traverse.Create(smelterMk3r).Field("_iconSprite").SetValue(smelterr.iconSprite);

            smelterMk3r.ID = 10002;
            smelterMk3r.Name = "Smelter MK. III";
            smelterMk3r.name = "Smelter MK. III".Translate();
            smelterMk3r.Description = "2x the speed of a Smelter MK. I";
            smelterMk3r.description = "2x the speed of a Smelter MK. I".Translate();
            smelterMk3r.Items = new int[] { 10001, 1206, 1305, 1202 };   // The items required to craft it
            //smelterMk3r.Items = new int[] { 1001 };
            smelterMk3r.ItemCounts = new int[] { 1, 8, 2, 20 };
            //smelterMk3r.ItemCounts = new int[] { 1 };
            smelterMk3r.Results = new int[] { 10002 };
            smelterMk3r.GridIndex = 2406;     // Where the recipe is located on the replicator. Format xyzz where x is the page, y is the row, and zz is the column
            smelterMk3r.SID = smelterMk3r.GridIndex.ToString();
            smelterMk3r.sid = smelterMk3r.GridIndex.ToString();
            smelterMk3.Name = "Smelter MK. III";
            smelterMk3.name = "Smelter MK. III".Translate();
            smelterMk3.Description = "2x the speed of a Smelter MK. I";
            smelterMk3.description = "2x the speed of a Smelter MK. I".Translate();
            smelterMk3.ID = 10002;
            smelterMk3.makes = new List<RecipeProto>();
            smelterMk3.BuildIndex = 10002;
            smelterMk3.GridIndex = smelterMk3r.GridIndex;
            smelterMk3.handcraft = smelterMk3r;
            smelterMk3.maincraft = smelterMk3r;
            smelterMk3.handcrafts = new List<RecipeProto>() { smelterMk3r };
            smelterMk3.recipes = new List<RecipeProto>() { smelterMk3r };
            smelterMk3.prefabDesc = smelter.prefabDesc.Copy();
            smelterMk3.prefabDesc.modelIndex = smelterMk3.ModelIndex;
            smelterMk3.prefabDesc.isAssembler = true;
            smelterMk3.prefabDesc.assemblerSpeed = 20000;
            smelterMk3.prefabDesc.assemblerRecipeType = ERecipeType.Smelt;
            smelterMk3.prefabDesc.idleEnergyPerTick = 300;    // desired watts / 60
            smelterMk3.prefabDesc.workEnergyPerTick = 15000;    // desired watts / 60
            smelterMk3.Grade = 3;
            smelterMk3.Upgrades = new int[] { 2302, 10001, 10002 };
            smelterMk3.StackSize = 50;

            var quantum = LDB.techs.Select(1303);
            var metallurgy = LDB.techs.Select(1401);

            TechProto mk3 = quantum.Copy();

            Traverse.Create(mk3).Field("_iconSprite").SetValue(metallurgy.iconSprite);

            mk3.name = "Quantum Metallurgy";
            mk3.Name = "Quantum Metallurgy";
            mk3.ID = 506;
            mk3.SID = "";
            mk3.sid = "";
            mk3.Desc = "Quantum mechanics can be utilised to smelt at super speeds";
            mk3.description = "Quantum mechanics can be utilised to smelt at super speeds";
            mk3.Conclusion = "";
            mk3.Published = true;
            mk3.Level = 0;
            mk3.MaxLevel = 0;
            mk3.Position = new Vector2(49f, -15f);
            mk3.PreTechsImplicit = new int[] { };
            mk3.postTechArray = new TechProto[] { };
            mk3.PreTechsImplicit = new int[] { 505 };
            mk3.PreTechs = new int[] { 1303 };
            mk3.Items = new int[] { 6001, 6002, 6003, 6004 };
            mk3.itemArray = new ItemProto[] { LDB.items.Select(6001), LDB.items.Select(6002), LDB.items.Select(6003), LDB.items.Select(6004) };
            mk3.ItemPoints = new int[] { 10, 10, 10, 10 };
            mk3.HashNeeded = 432000;

            quantum.postTechArray = new TechProto[] { LDB.techs.Select(1705), LDB.techs.Select(1203), mk3 };

            // Load the required technology to access it
            mk3.unlockRecipeArray = new RecipeProto[] { };
            mk3.UnlockRecipes = new int[] { };
            smelterMk3r.preTech = mk3;    // Set the required technology to be able to access the recipe

            LDBTool.PostAddProto(ProtoType.Tech, mk3);
            LDBTool.PostAddProto(ProtoType.Recipe, smelterMk3r);
            LDBTool.PostAddProto(ProtoType.Item, smelterMk3);
        }
    }
}
