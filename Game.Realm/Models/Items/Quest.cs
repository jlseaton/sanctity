using Game.Core;

namespace Game.Realm
{
    public class Quest : Thing
    {
        public bool Completed { get; set; }

        public int RequiredID { get; set; }
        public int FollowUpID { get; set; }

        public string Description { get; set; }

        public string RewardText { get; set; }

        public int Experience { get; set; }
        public int Gold { get; set; }

        public LootType LootClass { get; set; }
        public List<int> SpecificLoot { get; set; }
    }
}
