using System;

namespace Game.Realm
{
    public enum EffectType
    {
        Unknown = 0,
        Damage = 1,
        Stun = 2,
        Poison = 3,
        Charm = 4,
        Death = 5,
    };

    public class Effect : Item
    {
        public Effect()
        {

        }

        public new EffectType Type { get; set; }

        public virtual int FizzleChance { get; set; }

        public int ManaCost { get; set; }
        public bool AreaEffect { get; set; }

        public int MaximumTargets { get; set; }

        public int Duration { get; set; }
        public string Verb { get; set; }

        public DateTime Start { get; set; }

        public new Effect Clone()
        {
            return (Effect)this.MemberwiseClone();
        }
    }
}
