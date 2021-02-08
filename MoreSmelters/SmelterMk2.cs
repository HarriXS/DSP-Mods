using System.Collections.Generic;
using HarmonyLib;
using xiaoye97;

namespace DoubleSmelter
{
    class SmelterMk2
    {
        public void AddSmelterMk2Data()
        {
            var smelter = LDB.items.Select(2302);
            var smelterr = LDB.recipes.Select(56);

            // Load the required technology to access it
            var preTechSmelterMk2 = LDB.techs.Select(1202);

            // Copy the Protos to a new Smelter MK II building
            ItemProto smelterMk2 = smelter.Copy();
            RecipeProto smelterMk2r = smelterr.Copy();

            Traverse.Create(smelterMk2).Field("_iconSprite").SetValue(smelter.iconSprite);
            Traverse.Create(smelterMk2r).Field("_iconSprite").SetValue(smelterr.iconSprite);

            smelterMk2r.ID = 10001;
            smelterMk2r.Name = "Smelter MK. II";
            smelterMk2r.name = "Smelter MK. II";
            smelterMk2r.Description = "1.5x the speed of a Smelter";
            smelterMk2r.description = "1.5x the speed of a Smelter";
            smelterMk2r.Items = new int[] { 2302, 1107, 1303, 1202 };   // The items required to craft it
            //smelterMk2r.Items = new int[] { 1001 };   // The items required to craft it
            smelterMk2r.ItemCounts = new int[] { 1, 4, 4, 8 };
            //smelterMk2r.ItemCounts = new int[] { 1 };
            smelterMk2r.Results = new int[] { 10001 };   // The result (Smelter MK II)
            smelterMk2r.GridIndex = 2701;     // Where the recipe is located on the replicator. Format xyzz where x is the page, y is the row, and zz is the column
            smelterMk2r.preTech = preTechSmelterMk2;    // Set the required technology to be able to access the recipe
            smelterMk2r.SID = smelterMk2r.GridIndex.ToString();
            smelterMk2r.sid = smelterMk2r.GridIndex.ToString();
            smelterMk2.Name = "Smelter MK. II";
            smelterMk2.name = "Smelter MK. II";
            smelterMk2.Description = "1.5x the speed of a Smelter";
            smelterMk2.description = "1.5x the speed of a Smelter";
            smelterMk2.ID = 10001;
            smelterMk2.makes = new List<RecipeProto>();
            smelterMk2.BuildIndex = 10001;
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

            LDBTool.PostAddProto(ProtoType.Recipe, smelterMk2r);
            LDBTool.PostAddProto(ProtoType.Item, smelterMk2);
        }
    }
}
