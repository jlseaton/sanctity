using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Core;
using Game.Realm;

namespace Game.Tests
{
    [TestClass]
    public class GameTests
    {
        public TestContext TestContext { get; set; }

        private RealmManager Realm = new RealmManager(0, "Test Realm", 0, 0);

        private Packet lastPacket = null;

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

            lastPacket = null;

            Realm.GameEvents += Realm_GameEvents;

            var packet = new Packet()
            {
                ActionType = ActionType.Broadcast,
                Text = "Testing broadcast message.",
            };

            Realm.HandlePacket(packet, 1);

            Assert.IsTrue(lastPacket != null);
            Assert.IsTrue(lastPacket.ActionType == ActionType.Broadcast);
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
                Player player = Realm.AddPlayer(1);

                var npc = Realm.Data.LoadNPCs().Where(n => n.Name == "red dragon").Single();
                Realm.AddEntity(npc);

                string result = Fight(player, npc);
                Assert.IsTrue(!String.IsNullOrEmpty(result));
                Realm.RemovePlayer(player.ID);
                Realm.RemoveEntity(npc);

                player = Realm.AddPlayer(2);
                npc = Realm.Data.LoadNPCs().Where(n => n.Name.ToLower() == "demogorgon").Single();
                Realm.AddEntity(npc);

                result = Fight(player, npc);
                Assert.IsTrue(!String.IsNullOrEmpty(result));
                Realm.RemovePlayer(player.ID);
                Realm.RemoveEntity(npc);
            }

            Realm.Stop();
        }

        private string Fight(Player player, NPC npc)
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
