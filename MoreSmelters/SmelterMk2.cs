using System.Collections.Generic;
using HarmonyLib;
using xiaoye97;
using UnityEngine;
using System.Reflection;

namespace DoubleSmelter
{
    class SmelterMk2
    {
        public void AddSmelterMk2Data()
        {
            var smelter = LDB.items.Select(2302);
            var smelterr = LDB.recipes.Select(56);

            // Copy the Protos to a new Storage Mk III building
            ItemProto smelterMk2 = smelter.Copy();
            RecipeProto smelterMk2r = smelterr.Copy();

            Traverse.Create(smelterMk2).Field("_iconSprite").SetValue(smelter.iconSprite);
            Traverse.Create(smelterMk2r).Field("_iconSprite").SetValue(smelterr.iconSprite);

            smelterMk2r.ID = 10001;
            smelterMk2r.Name = "Smelter MK. II";
            smelterMk2r.name = "Smelter MK. II".Translate();
            smelterMk2r.Description = "1.5x the speed of a Smelter MK. I";
            smelterMk2r.description = "1.5x the speed of a Smelter MK. I".Translate();
            smelterMk2r.Items = new int[] { 2302, 1107, 1303, 1202 };   // The items required to craft it (Smelter, titanium alloy, processor, magnetic coil)
            //smelterMk2r.Items = new int[] { 1001 };   // The items required to craft it
            smelterMk2r.ItemCounts = new int[] { 1, 4, 4, 8 };
            //smelterMk2r.ItemCounts = new int[] { 1 };
            smelterMk2r.Results = new int[] { 10001 };   // The result (Smelter MK 2)
            smelterMk2r.GridIndex = 2405;     // Where the recipe is located on the replicator. Format xyzz where x is the page, y is the row, and zz is the column
            smelterMk2r.SID = smelterMk2r.GridIndex.ToString();
            smelterMk2r.sid = smelterMk2r.GridIndex.ToString();
            smelterMk2.Name = "Smelter MK. II";
            smelterMk2.name = "Smelter MK. II".Translate();
            smelterMk2.Description = "1.5x the speed of a Smelter MK. I";
            smelterMk2.description = "1.5x the speed of a Smelter MK. I".Translate();
            smelterMk2.ID = 10001;
            smelterMk2.makes = new List<RecipeProto>();
            smelterMk2.BuildIndex = 508;
            smelterMk2.GridIndex = smelterMk2r.GridIndex;
            smelterMk2.handcraft = smelterMk2r;
            smelterMk2.maincraft = smelterMk2r;
            smelterMk2.handcrafts = new List<RecipeProto>() { smelterMk2r };
            smelterMk2.recipes = new List<RecipeProto>() { smelterMk2r };
            smelterMk2.prefabDesc = smelter.prefabDesc.Copy();
            smelterMk2.prefabDesc.modelIndex = smelterMk2.ModelIndex;
            smelterMk2.prefabDesc.isAssembler = true;
            smelterMk2.prefabDesc.assemblerSpeed = 15000;   // default is 10000
            smelterMk2.prefabDesc.assemblerRecipeType = ERecipeType.Smelt;
            smelterMk2.prefabDesc.idleEnergyPerTick = 250;      // desired watts / 60
            smelterMk2.prefabDesc.workEnergyPerTick = 10667;    // desired watts / 60
            smelterMk2.Grade = 2;
            smelterMk2.Upgrades = new int[] { 2302, 10001, 10002 };
            smelterMk2.StackSize = 50;

            var processor = LDB.techs.Select(1302);
            var metallugry = LDB.techs.Select(1401);

            TechProto mk2 = processor.Copy();

            Traverse.Create(mk2).Field("_iconSprite").SetValue(metallugry.iconSprite);

            mk2.name = "Improved Metallurgy";
            mk2.Name = "Improved Metallurgy";
            mk2.ID = 505;
            mk2.SID = "";
            mk2.sid = "";
            mk2.Desc = "Advances in computer components allow you to smelt items faster";
            mk2.description = "Advances in computer components allow you to smelt items faster";
            mk2.Conclusion = "";    // the message that appears when you unlock
            mk2.Published = true;   // false = accelerants
            mk2.Level = 0;
            mk2.MaxLevel = 0;
            mk2.Position = new Vector2(33f, -4.75f);
            mk2.PreTechsImplicit = new int[] { 1401 };
            mk2.postTechArray = new TechProto[] { };

            // I have no fucking clue how exactly the amount of items is calculated, but this works so I'm sticking with it
            // What I did for now is just take the values for High-speed processing and manipulate them.
            // So the total blue for High-speed is 600, I wanted 800 for this research. 800 / 600 * 108000 (the HashNeeded for high-speed) = 144000
            // Set the items to blue and red jelly
            mk2.Items = new int[] { 6001, 6002 };
            mk2.itemArray = new ItemProto[] { LDB.items.Select(6001), LDB.items.Select(6002) };
            // Time the labs take to use an item?
            mk2.ItemPoints = new int[] { 20, 10 };
            // Total hash
            mk2.HashNeeded = 144000;

            processor.postTechArray = new TechProto[] { LDB.techs.Select(1202), LDB.techs.Select(1312), mk2 };

            // Load the required technology to access it
            mk2.unlockRecipeArray = new RecipeProto[] { };
            mk2.UnlockRecipes = new int[] { };
            smelterMk2r.preTech = mk2;    // Set the required technology to be able to access the recipe

            LDBTool.PostAddProto(ProtoType.Tech, mk2);
            LDBTool.PostAddProto(ProtoType.Recipe, smelterMk2r);
            LDBTool.PostAddProto(ProtoType.Item, smelterMk2);
        }
    }
}
