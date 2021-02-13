using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xiaoye97;
using BepInEx;
using UnityEngine;
using System.Reflection;
using HarmonyLib;

namespace MoreStorage
{
    class Storage4
    {
        public void AddStorage4Data()
        {
            var storage2 = LDB.items.Select(2102);
            var storage2r = LDB.recipes.Select(91);

            // Load the required technology to access it
            var preTechStorage4 = LDB.techs.Select(1605);

            // Copy the Protos to a new Storage Mk III building
            ItemProto storage4 = storage2.Copy();
            RecipeProto storage4r = storage2r.Copy();

            Traverse.Create(storage4).Field("_iconSprite").SetValue(storage2.iconSprite);
            Traverse.Create(storage4r).Field("_iconSprite").SetValue(storage2.iconSprite);

            storage4r.ID = 302;
            storage4r.Name = "Storage MK. IV";
            storage4r.name = "Storage MK. IV";
            storage4r.Description = "Holds twice as many items as a Storage MK. III";
            storage4r.description = "Holds twice as many items as a Storage MK. III";
            //storage4r.Items = new int[] { 1107, 1108 };   // The items required to craft it
            storage4r.Items = new int[] { 1101 };   // The items required to craft it
            //storage4r.ItemCounts = new int[] { 8, 12 };
            storage4r.ItemCounts = new int[] { 1 };
            storage4r.Results = new int[] { 2104 };   // The result (Storage Mk IV)
            storage4r.GridIndex = 2602;     // Where the recipe is located on the replicator. Format xyzz where x is the page, y is the row, and zz is the column
            storage4r.preTech = preTechStorage4;    // Set the required technology to be able to access the recipe
            storage4r.SID = storage4r.GridIndex.ToString();
            storage4r.sid = storage4r.GridIndex.ToString();
            storage4.Name = "Storage MK. IV";
            storage4.name = "Storage MK. IV";
            storage4.Description = "Holds twice as many items as a Storage MK. III";
            storage4.description = "Holds twice as many items as a Storage MK. III";
            storage4.ID = 2104;
            storage4.makes = new List<RecipeProto>();
            storage4.BuildIndex = 354;
            storage4.GridIndex = storage4r.GridIndex;
            storage4.handcraft = storage4r;
            storage4.maincraft = storage4r;
            storage4.handcrafts = new List<RecipeProto>() { storage4r };
            storage4.recipes = new List<RecipeProto>() { storage4r };
            storage4.prefabDesc = storage2.prefabDesc.Copy();
            storage4.prefabDesc.modelIndex = storage4.ModelIndex;
            storage4.prefabDesc.storageCol = 20;
            storage4.prefabDesc.storageRow = 12;
            storage4.prefabDesc.isStorage = true;

            LDBTool.PostAddProto(ProtoType.Recipe, storage4r);
            LDBTool.PostAddProto(ProtoType.Item, storage4);
        }
    }
}
