using Game.Core;
using Game.Realm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Game.Test
{
    [TestClass]
    public class World
    {
        public TestContext? TestContext { get; set; }

        private RealmManager Realm = new RealmManager(0, "Test Realm", false, 0, 0);

        private Packet? lastPacket = null;

        private void Realm_GameEvents(object sender, Packet packet)
        {
            lastPacket = packet;
        }

        [TestMethod]
        public void TestRealmSetup()
        {
            Realm.Start();
            Assert.IsTrue(Realm.Items.Count > 0);
            Realm.Stop();
        }

        [TestMethod]
        public void TestProtocol()
        {
            Realm.Start();
            Realm.AddPlayer("Hoxore");
            Realm.GameEvents += Realm_GameEvents;
            lastPacket = null;

            var packet = new Packet()
            {
                ActionType = ActionType.Broadcast,
                Text = "Testing broadcast message.",
            };

            Realm.HandlePacket(packet, "Hoxore");

            Assert.IsTrue((lastPacket != null) && 
                lastPacket.ActionType == ActionType.Broadcast);
            Assert.IsTrue(!String.IsNullOrEmpty(lastPacket.Text));

            Realm.Stop();
        }

        [TestMethod]
        public void TestCombat()
        {
            Realm.GameEvents += Realm_GameEvents;

            Realm.Start();

            for (int i = 0; i < 10; i++)
            {
                PC player = Realm.AddPlayer("Hoxore");

                var npc = Realm.Data.LoadNPCs().Where(n => n.Name == "reddragon").Single();
                Realm.AddEntity(npc);

                string result = Fight(player, npc);
                Assert.IsTrue(!String.IsNullOrEmpty(result));
                Realm.RemovePC(player.ID);
                Realm.RemoveNPC(npc);

                player = Realm.AddPlayer("Derwin");
                npc = Realm.Data.LoadNPCs().Where(n => n.Name.ToLower() == "demogorgon").Single();
                Realm.AddEntity(npc);

                result = Fight(player, npc);
                Assert.IsTrue(!String.IsNullOrEmpty(result));
                Realm.RemovePC(player.ID);
                Realm.RemoveNPC(npc);
            }

            Realm.Stop();
        }

        private string Fight(PC player, NPC npc)
        {
            string result = String.Empty;
            int rounds = 0;
            do
            {
                string attack = String.Empty;
                if (Randomizer.Next(100) > 50)
                {
                    attack = Realm.Combat.Attack(player, npc);
                    //System.Diagnostics.Debug.WriteLine(attack);
                }
                else
                {
                    attack = Realm.Combat.Attack(npc, player);
                    //System.Diagnostics.Debug.WriteLine(attack);
                }
                rounds++;
            }
            while (player.State != StateType.Dead && npc.State != StateType.Dead);

            if (player.State == StateType.Dead)
            {
                result = npc.FullName + " won!";
            }
            else
            {
                result = player.FullName + " won!";
            }

            //System.Diagnostics.Debug.WriteLine(result);
            TestContext.WriteLine(result);

            return result;
        }
    }
}
