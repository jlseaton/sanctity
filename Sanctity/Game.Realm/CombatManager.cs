﻿using System;
using System.Linq;
using Game.Core;

namespace Game.Realm
{
    public class CombatManager
    {
        private RealmManager Realm;

        public CombatManager(RealmManager realm)
        {
            Realm = realm;            
        }

        public string Attack(Entity attacker, Entity target)
        {
            if (attacker == null || target == null || 
                attacker.State == StateType.Dead || target.State == StateType.Dead)
            {
                return String.Empty;
            }

            string result = String.Empty;
            string weaponName = String.Empty;

            int damage = 0, minDamage = 0, maxDamage = 0;
            bool continueAttack = true;

            if (attacker.State == StateType.Invisible)
            {
                attacker.State = StateType.Combat;
            }

            if (attacker.State == StateType.Stunned)
            {
                attacker.State = StateType.Normal;
                result = "\r\n" + attacker.FullName + " recovers from being stunned.";
                Realm.SayMessage(result, target.Loc.AreaID, target.Loc.HexID);
                return result;
            }

            var hex = Realm.Areas[target.Loc.AreaID].Hexes[target.Loc.HexID - 1];

            // NPC skill procs first
            if (attacker.Type == EntityType.NPC
                && attacker.Skills != null && attacker.Skills.Any())
            {
                var skill = attacker.Skills[Randomizer.Next(1, attacker.Skills.Count)];

                var effects = Realm.Effects.Where(e => e.ID == skill);
                if (effects.Any())
                {
                    var effect = effects.Single();

                    int chance = Randomizer.Next(100);

                    if (chance > effect.FizzleChance)
                    {
                        int procDamage = Randomizer.Next(effect.MaxDamage) + effect.MinDamage;

                        damage += procDamage;

                        string verb = effect.Verb == String.Empty 
                            ? " attacks and hits " : effect.Verb;

                        result = attacker.FullName + verb + target.FullName + weaponName +
                            " for " + procDamage.ToString() + " " +
                            effect.Damage.ToString().ToUpper() + " damage";

                        if (effect.Type == EffectType.Stun)
                        {
                            target.State = StateType.Stunned;
                            result += "\r\n" + target.FullName + " has been stunned!";
                        }

                        continueAttack = false;
                    }
                }
            }

            if (continueAttack)
            {
                if (attacker.MainHand != null)
                {
                    minDamage = attacker.MainHand.MinDamage;
                    maxDamage = attacker.MainHand.MaxDamage;

                    weaponName = " with " + attacker.MainHand.FullName;
                }
                else
                {
                    minDamage = attacker.MinDamage;
                    maxDamage = attacker.MaxDamage;
                }

                damage = Randomizer.Next(maxDamage);

                if (damage <= 0)
                {
                    result = attacker.FullName + " attacks " 
                        + target.FullName + " and misses";
                }
                else
                {
                    damage += minDamage;

                    result = attacker.FullName + " attacks and hits " + target.FullName + 
                        weaponName + " for " + damage.ToString() + " damage";

                    // Weapon procs
                    if (attacker.MainHand != null &&
                        attacker.MainHand.Effects != null && 
                        attacker.MainHand.Effects.Any())
                    {
                        foreach (int id in attacker.MainHand.Effects)
                        {
                            var eff = Realm.Effects[id - 1];

                            int chance = Randomizer.Next(100);

                            if (chance > eff.FizzleChance)
                            {
                                int procDamage = Randomizer.Next(eff.MaxDamage) 
                                    + eff.MinDamage;

                                damage += procDamage;

                                string verb = eff.Verb == String.Empty 
                                    ? " attacks and hits " : eff.Verb;

                                result += "\r\n" + attacker.FullName + verb +
                                    target.FullName + weaponName + " for " + procDamage.ToString() +
                                    " " + eff.Damage.ToString().ToUpper() + " damage";
                            }
                        }
                    }
                }
            }

            target.HitPoints -= damage;
            target.LastAttackerID = attacker.ID;
            target.State = StateType.Combat;

            if (target.HitPoints <= 0)
            {
                target.Die();

                attacker.LastAttackerID = 0;
                attacker.State = StateType.Normal;
                
                string rewards = String.Empty;

                if (target.Experience > 0)
                {
                    attacker.Experience += target.Experience;
                    rewards += "You earned " + target.Experience.ToString() 
                        + " experience points.";
                }

                if (target.Gold > 0)
                {
                    attacker.Gold += target.Gold;
                    rewards += "\r\nYou found " + target.Gold.ToString() 
                        + " gold pieces.";
                }

                if (target.Inventory != null && target.Inventory.Any())
                {
                    for (int i = 0; i < target.Inventory.Count; i++)
                    {
                        if (target.Inventory[i] != null)
                        {
                            rewards += "\r\n" + target.FullName + " drops a "
                                + target.Inventory[i].FullName + ".";

                            lock (hex.Items)
                            {
                                hex.Items.Add(target.Inventory[i]);
                            }
                        }
                    }
                }

                result += "\r\n" + target.FullName + " has been killed by " 
                    + attacker.FullName;

                Realm.SayMessage(result, target.Loc.AreaID, target.Loc.HexID);

                if (target is PC)
                {
                    Realm.SendPlayerStatus(target.ID, "You have died.");
                    Realm.BroadcastMessage(target.FullName + " has died.");
                }
                else
                {
                    Realm.SendPlayerStatus(attacker.ID, rewards);
                    Realm.BroadcastMessage(target.FullName + " was killed by " 
                        + attacker.FullName + ".");
                }
            }
            else
            {
                Realm.SayMessage(result, target.Loc.AreaID, target.Loc.HexID);

                if (target is NPC)
                {
                    var npc = target as NPC;
                    npc.Mood = MoodType.Aggressive;
                }

                if (target is PC)
                {
                    Realm.SendPlayerStatus(target.ID);
                }
            }

            return result;
        }
    }
}
