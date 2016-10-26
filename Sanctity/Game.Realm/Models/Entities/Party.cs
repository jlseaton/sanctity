using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core;

namespace Game.Realm
{
    public class Party : Thing
    {
        public List<Player> Players = new List<Player>();
    }
}
