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
    [BepInDependency("me.xiaoye97.plugin.Dyson.LDBTool", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin("org.harrixs.plugins.DSP.morestorage", "More Storage", "0.1.2")]
    public class MoreStorage : BaseUnityPlugin
    {
        void Start()
        {
            Storage3 storage3 = new Storage3();
            Tank2 tank2 = new Tank2();
            //Storage4 storage4 = new Storage4();
            LDBTool.PostAddDataAction += EditOtherBuildings;
            LDBTool.PostAddDataAction += storage3.AddStorage3Data;
            LDBTool.PostAddDataAction += tank2.AddTank2Data;
            //LDBTool.PostAddDataAction += storage4.AddStorage4Data;
            //var ab = AssetBundle.LoadFromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("MoreStorage.storagemkiii"));    // Load the storagemkiii AssetBundle
            SetBuildBar();
            Harmony.CreateAndPatchAll(typeof(MoreStorage));
        }

        void SetBuildBar()
        {
            LDBTool.SetBuildBar(4, 3, 2107);
            LDBTool.SetBuildBar(4, 4, 2106);
            LDBTool.SetBuildBar(4, 5, 10102);
        }

        void EditOtherBuildings()
        {
            var tank = LDB.items.Select(2106);
            var tankr = LDB.recipes.Select(114);
            tankr.name = "Storage tank MK. I";
            tankr.Name = "Storage tank MK. I";
            tank.name = "Storage tank MK. I";
            tank.Name = "Storage tank MK. I";
        }
    }
}
