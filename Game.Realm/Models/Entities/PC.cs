using Game.Core;

namespace Game.Realm
{
    public class PC : Entity
    {
        public int UserID { get; set; }

        public Connection Conn { get; set; }

        public PC() : base()
        {
            Type = EntityType.PC;
            Gender = GenderType.Male;
        }
    }
}
