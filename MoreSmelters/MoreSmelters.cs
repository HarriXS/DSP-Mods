using BepInEx;
using HarmonyLib;
using xiaoye97;
using UnityEngine;
using System.Reflection;

namespace DoubleSmelter
{
    [BepInPlugin("org.HarriXS.plugins.DSP.moresmelters", "More Smelters", "0.3.0")]
    public class MoreSmelters : BaseUnityPlugin
    {
        public Sprite improvedMetIcon;
        public Sprite quantumMetIcon;

        void Start()
        {
            SmelterMk1 smelterMk1 = new SmelterMk1();
            SmelterMk2 smelterMk2 = new SmelterMk2();
            SmelterMk3 smelterMk3 = new SmelterMk3();
            LDBTool.PreAddDataAction += AddTranslation;
            LDBTool.PostAddDataAction += EditOtherBuildings;
            LDBTool.PostAddDataAction += smelterMk1.EditSmelterData;
            LDBTool.PostAddDataAction += smelterMk2.AddSmelterMk2Data;
            LDBTool.PostAddDataAction += smelterMk3.AddSmelterMk3Data;
            SetBuildBar();
            Harmony.CreateAndPatchAll(typeof(MoreSmelters));
        }

        void AddTranslation()
        {
            StringProto mk1Name = new StringProto();
            mk1Name.ID = 10500;
            mk1Name.Name = "Smelter MK. I";
            mk1Name.name = "Smelter MK. I";
            mk1Name.ZHCN = "冶炼厂MK。 I";
            mk1Name.ENUS = "Smelter MK. I";
            mk1Name.FRFR = "Smelter MK. I";

            StringProto mk2Name = new StringProto();
            mk2Name.ID = 10501;
            mk2Name.Name = "Smelter MK. II";
            mk2Name.name = "Smelter MK. II";
            mk2Name.ZHCN = "冶炼厂MK。 II";
            mk2Name.ENUS = "Smelter MK. II";
            mk2Name.FRFR = "Smelter MK. II";

            StringProto mk3Name = new StringProto();
            mk3Name.ID = 10503;
            mk3Name.Name = "Smelter MK. III";
            mk3Name.name = "Smelter MK. III";
            mk3Name.ZHCN = "冶炼厂MK。 III";
            mk3Name.ENUS = "Smelter MK. III";
            mk3Name.FRFR = "Smelter MK. III";

            StringProto mk2Desc = new StringProto();
            mk2Desc.ID = 10502;
            mk2Desc.Name = "1.5x the speed of a Smelter MK. I";
            mk2Desc.name = "1.5x the speed of a Smelter MK. I";
            mk2Desc.ZHCN = "冶炼厂速度的1.5倍";
            mk2Desc.ENUS = "1.5x the speed of a Smelter MK. I";
            mk2Desc.FRFR = "1.5x the speed of a Smelter MK. I";

            StringProto mk3Desc = new StringProto();
            mk3Desc.ID = 10504;
            mk3Desc.Name = "2x the speed of a Smelter MK. I";
            mk3Desc.name = "2x the speed of a Smelter MK. I";
            mk3Desc.ZHCN = "速度是冶炼厂MK的2倍。一世";
            mk3Desc.ENUS = "2x the speed of a Smelter MK. I";
            mk3Desc.FRFR = "2x the speed of a Smelter MK. I";

            LDBTool.PreAddProto(ProtoType.String, mk1Name);
            LDBTool.PreAddProto(ProtoType.String, mk2Name);
            LDBTool.PreAddProto(ProtoType.String, mk3Name);
            LDBTool.PreAddProto(ProtoType.String, mk2Desc);
            LDBTool.PreAddProto(ProtoType.String, mk3Desc);
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
