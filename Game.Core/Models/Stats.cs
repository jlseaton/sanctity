namespace Game.Core
{
    public class Stats
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }

        public int HPs { get; set; }
        public int MaxHPs { get; set; }

        public int MPs { get; set; }

        public int MaxMPs { get; set; }

        public int Level { get; set; }
        public int Experience { get; set; }
        public int Gold { get; set; }
        public int AttackDelay { get; set; }

        public StateType State { get; set; }

        public MoveDirection Facing { get; set; }
    }
}
