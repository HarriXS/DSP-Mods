using BepInEx;
using HarmonyLib;
using xiaoye97;

namespace DoubleSmelter
{
    [BepInPlugin("org.HarriXS.plugins.DSP.moresmelters", "More Smelters", "0.1.0")]
    public class MoreSmelters : BaseUnityPlugin
    {
        void Start()
        {
            SmelterMk2 smelterMk2 = new SmelterMk2();
            SmelterMk3 smelterMk3 = new SmelterMk3();
            LDBTool.PostAddDataAction += smelterMk2.AddSmelterMk2Data;
            LDBTool.PostAddDataAction += smelterMk3.AddSmelterMk3Data;
            Harmony.CreateAndPatchAll(typeof(MoreSmelters));
        }
    }
}
