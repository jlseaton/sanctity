using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core;

namespace Game.Realm
{
    public class Resist : Thing
    {
        public DamageType Type;
        public int Value = 0;
    }
}
