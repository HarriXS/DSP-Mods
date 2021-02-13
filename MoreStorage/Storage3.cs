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
    class Storage3
    {
        public void AddStorage3Data()
        {
            var storage2 = LDB.items.Select(2102);
            var storage2r = LDB.recipes.Select(91);

            // Load the required technology to access it
            var preTechStorage3 = LDB.techs.Select(1604);

            // Copy the Protos to a new Storage Mk III building
            ItemProto storage3 = storage2.Copy();
            RecipeProto storage3r = storage2r.Copy();

            Traverse.Create(storage3).Field("_iconSprite").SetValue(storage2.iconSprite);
            Traverse.Create(storage3r).Field("_iconSprite").SetValue(storage2.iconSprite);

            storage3r.ID = 2107;
            storage3r.Name = "Storage MK. III";
            storage3r.name = "Storage MK. III";
            storage3r.Description = "Holds twice as many items as a Storage MK. II";
            storage3r.description = "Holds twice as many items as a Storage MK. II";
            storage3r.Items = new int[] { 1106, 1108 };   // The items required to craft it
            storage3r.ItemCounts = new int[] { 12, 12 };
            storage3r.Results = new int[] { 2107 };   // The result (Storage Mk III)
            storage3r.GridIndex = 2601;     // Where the recipe is located on the replicator. Format xyzz where x is the page, y is the row, and zz is the column
            storage3r.preTech = preTechStorage3;    // Set the required technology to be able to access the recipe
            storage3r.SID = storage3r.GridIndex.ToString();
            storage3r.sid = storage3r.GridIndex.ToString();
            storage3.Name = "Storage MK. III";
            storage3.name = "Storage MK. III";
            storage3.Description = "Holds twice as many items as a Storage MK. II";
            storage3.description = "Holds twice as many items as a Storage MK. II";
            storage3.ID = 2107;
            storage3.makes = new List<RecipeProto>();
            storage3.BuildIndex = 2107;
            storage3.GridIndex = storage3r.GridIndex;
            storage3.handcraft = storage3r;
            storage3.maincraft = storage3r;
            storage3.handcrafts = new List<RecipeProto>() { storage3r };
            storage3.recipes = new List<RecipeProto>() { storage3r };
            storage3.prefabDesc = storage2.prefabDesc.Copy();
            storage3.prefabDesc.modelIndex = storage3.ModelIndex;
            storage3.prefabDesc.storageCol = 10;
            storage3.prefabDesc.storageRow = 12;
            storage3.prefabDesc.isStorage = true;

            LDBTool.PostAddProto(ProtoType.Recipe, storage3r);
            LDBTool.PostAddProto(ProtoType.Item, storage3);
        }
    }
}
