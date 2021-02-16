using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoubleSmelter
{
    class SmelterMk1
    {
        public void EditSmelterData()
        {
            var smelter = LDB.items.Select(2302);
            var smelterr = LDB.recipes.Select(56);
            smelter.Grade = 1;
            smelter.Upgrades = new int[] { 2302, 10001, 10002 };
            smelterr.name = "Smelter MK. I".Translate();
            smelterr.Name = "Smelter MK. I";
            smelter.Name = "Smelter MK. I";
            smelter.name = "Smelter MK. I".Translate();
            smelter.StackSize = 5;
        }
    }
}
