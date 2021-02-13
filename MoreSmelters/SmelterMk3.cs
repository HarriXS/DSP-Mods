using System.Collections.Generic;
using HarmonyLib;
using xiaoye97;

namespace DoubleSmelter
{
    class SmelterMk3
    {
        public void AddSmelterMk3Data()
        {
            var smelter = LDB.items.Select(2302);
            var smelterr = LDB.recipes.Select(56);

            // Load the required technology to access it
            var preTechsmelterMk3 = LDB.techs.Select(1203);

            // Copy the Protos to a new Storage Mk III building
            ItemProto smelterMk3 = smelter.Copy();
            RecipeProto smelterMk3r = smelterr.Copy();

            Traverse.Create(smelterMk3).Field("_iconSprite").SetValue(smelter.iconSprite);
            Traverse.Create(smelterMk3r).Field("_iconSprite").SetValue(smelterr.iconSprite);

            smelterMk3r.ID = 10002;
            smelterMk3r.Name = "Smelter MK. III";
            smelterMk3r.name = "Smelter MK. III";
            smelterMk3r.Description = "2x the speed of a Smelter";
            smelterMk3r.description = "2x the speed of a Smelter";
            smelterMk3r.Items = new int[] { 10001, 1206, 1305, 1202 };   // The items required to craft it
            //smelterMk3r.Items = new int[] { 1001 };
            smelterMk3r.ItemCounts = new int[] { 1, 8, 2, 20 };
            //smelterMk3r.ItemCounts = new int[] { 1 };
            smelterMk3r.Results = new int[] { 10002 };   // The result (Storage Mk III)
            smelterMk3r.GridIndex = 2406;     // Where the recipe is located on the replicator. Format xyzz where x is the page, y is the row, and zz is the column
            smelterMk3r.preTech = preTechsmelterMk3;    // Set the required technology to be able to access the recipe
            smelterMk3r.SID = smelterMk3r.GridIndex.ToString();
            smelterMk3r.sid = smelterMk3r.GridIndex.ToString();
            smelterMk3.Name = "Smelter MK. III";
            smelterMk3.name = "Smelter MK. III";
            smelterMk3.Description = "2x the speed of a Smelter";
            smelterMk3.description = "2x the speed of a Smelter";
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

            LDBTool.PostAddProto(ProtoType.Recipe, smelterMk3r);
            LDBTool.PostAddProto(ProtoType.Item, smelterMk3);
        }
    }
}
