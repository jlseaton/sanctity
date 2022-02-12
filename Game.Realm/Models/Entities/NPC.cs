﻿using Game.Core;

namespace Game.Realm
{
    public class NPCTarget
    {
        public int UID { get; set; }

        public int Difficulty { get; set; }
        public Dictionary<int, long> Aggression { get; set; }
    }

    public class NPC : Entity
    {
        public EncounterType EncounterClass { get; set; }
        public MoodType Mood { get; set; }

        public int WanderRange { get; set; }
        public int Follows { get; set; }
        public int Followed { get; set; }

        public List<NPCTarget> Targets = new List<NPCTarget>();

        public NPC() : base()
        {
            Type = EntityType.NPC;
        }

        public new NPC Clone()
        {
            return (NPC)this.MemberwiseClone();
        }

        public void SendPacket(RealmManager realm, Packet packet, PC player)
        {
            lock (this)
            {
                if (packet.ActionType == ActionType.Movement && Follows > 0 &&
                    State != StateType.Dead && State != StateType.Stunned && player.State != StateType.Invisible)
                {
                    if (packet.ID == LastAttackerID)
                    {
                        if (Randomizer.Next(100) > 25)
                        {
                            if (Followed++ < Follows)
                            {
                                realm.TellMessage(player, FullName + " follows you!");
                                realm.Move(this, packet.MoveDirection.ToString().ToLower());
                            }
                        }
                    }
                }
            }
        }
    }
}
