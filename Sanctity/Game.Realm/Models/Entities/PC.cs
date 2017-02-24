using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core;

namespace Game.Realm
{
    public class PC : Entity
    {
        public int UserID { get; set; }

        public Connection Conn { get; set; }

        public PC() : base()
        {
            Type = EntityType.Player;
            Gender = GenderType.Male;
        }
    }
}
