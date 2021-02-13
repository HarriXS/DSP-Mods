using BepInEx;
using HarmonyLib;
using xiaoye97;
using UnityEngine;

namespace DoubleSmelter
{
    [BepInPlugin("org.HarriXS.plugins.DSP.moresmelters", "More Smelters", "0.1.0")]
    public class MoreSmelters : BaseUnityPlugin
    {
        void Start()
        {
            SmelterMk1 smelterMk1 = new SmelterMk1();
            SmelterMk2 smelterMk2 = new SmelterMk2();
            SmelterMk3 smelterMk3 = new SmelterMk3();
            DoubleSmelter doubleSmelter = new DoubleSmelter();
            SteelFactory steelFactory = new SteelFactory();
            CustomResearch customResearch = new CustomResearch();
            LDBTool.PostAddDataAction += customResearch.AddCustomResearch;
            LDBTool.PostAddDataAction += EditOtherBuildings;
            LDBTool.PostAddDataAction += smelterMk1.EditSmelterData;
            LDBTool.PostAddDataAction += smelterMk2.AddSmelterMk2Data;
            LDBTool.PostAddDataAction += smelterMk3.AddSmelterMk3Data;
            LDBTool.PostAddDataAction += doubleSmelter.AddDoubleSmelterData;
            LDBTool.PostAddDataAction += steelFactory.AddSteelFactoryData;
            SetBuildBar();
            //var ab = AssetBundle.LoadFromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("MoreStorage.storagemkiii"));    // Load the storagemkiii AssetBundle
            Harmony.CreateAndPatchAll(typeof(MoreSmelters));
        }

        void SetBuildBar()
        {
            LDBTool.SetBuildBar(5, 2, 10001);
            LDBTool.SetBuildBar(5, 3, 10002);
            LDBTool.SetBuildBar(5, 4, 2303);
            LDBTool.SetBuildBar(5, 5, 2304);
            LDBTool.SetBuildBar(5, 6, 2305);
            LDBTool.SetBuildBar(5, 7, 2308);
            LDBTool.SetBuildBar(5, 8, 2309);
            LDBTool.SetBuildBar(5, 9, 2314);
            LDBTool.SetBuildBar(5, 10, 2310);
        }

        void EditOtherBuildings()
        {
            var frac = LDB.items.Select(2314);
            var fracr = LDB.recipes.Select(110);
            fracr.GridIndex = 2310;
            frac.GridIndex = fracr.GridIndex;
        }
    }
}
