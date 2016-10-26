using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core;

namespace Game.Realm
{
    public class Player : Entity
    {
        public int UserID { get; set; }

        public Connection Conn { get; set; }

        public Player() : base()
        {
            Type = EntityType.Player;
        }
    }
}
