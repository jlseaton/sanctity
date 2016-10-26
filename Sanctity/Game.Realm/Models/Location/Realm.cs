using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core;

namespace Game.Realm
{
    public class Realm : Thing
    {
        public string Title { get; set; }

        public Connection Address { get; set; }

        public List<Area> Areas { get; set; }
    }
}
