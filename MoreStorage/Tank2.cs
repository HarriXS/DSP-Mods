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
    class Tank2
    {
        public void AddTank2Data()
        {
            var tank = LDB.items.Select(2106);
            var tankr = LDB.recipes.Select(114);

            // Load the required technology to access it
            var preTechTank2 = LDB.techs.Select(1123);

            // Copy the Protos to a new tank Mk III building
            ItemProto tank2 = tank.Copy();
            RecipeProto tank2r = tankr.Copy();

            Traverse.Create(tank2).Field("_iconSprite").SetValue(tank.iconSprite);
            Traverse.Create(tank2r).Field("_iconSprite").SetValue(tank.iconSprite);

            tank2r.ID = 10102;
            tank2r.Name = "Storage tank MK. II";
            tank2r.name = "Storage tank MK. II";
            tank2r.Description = "Holds 4x the fluid as a Storage tank MK. I";
            tank2r.description = "Holds 4x the fluid as a Storage tank MK. I";
            tank2r.Items = new int[] { 1106, 1108, 1110 };   // The items required to craft it
            tank2r.ItemCounts = new int[] { 16, 8, 8 };
            tank2r.Results = new int[] { 10102 };   // The result (tank Mk III)
            tank2r.GridIndex = 2602;     // Where the recipe is located on the replicator. Format xyzz where x is the page, y is the row, and zz is the column
            tank2r.preTech = preTechTank2;    // Set the required technology to be able to access the recipe
            tank2r.SID = tank2r.GridIndex.ToString();
            tank2r.sid = tank2r.GridIndex.ToString();
            tank2.Name = "Storage tank MK. II";
            tank2.name = "Storage tank MK. II";
            tank2.Description = "Holds 4x the fluid as a Storage tank MK. I";
            tank2.description = "Holds 4x the fluid as a Storage tank MK. I";
            tank2.ID = 10102;
            tank2.makes = new List<RecipeProto>();
            tank2.BuildIndex = 10102;
            tank2.GridIndex = tank2r.GridIndex;
            tank2.handcraft = tank2r;
            tank2.maincraft = tank2r;
            tank2.handcrafts = new List<RecipeProto>() { tank2r };
            tank2.recipes = new List<RecipeProto>() { tank2r };
            tank2.prefabDesc = tank.prefabDesc.Copy();
            tank2.prefabDesc.modelIndex = tank2.ModelIndex;
            tank2.prefabDesc.isTank = true;
            tank2.prefabDesc.fluidStorageCount = 40000;

            LDBTool.PostAddProto(ProtoType.Recipe, tank2r);
            LDBTool.PostAddProto(ProtoType.Item, tank2);
        }
    }
}
