using Game.Core;
using System.Text;

namespace Game.Realm
{
    public class RealmManager
    {
        #region Fields

        public int ID { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }

        public bool Running { get; private set; }
        public DateTime Started { get; private set; }

        public int PulseRate { get; set; }
        public int RoundDuration { get; set; }
        public long Round { get; private set; }
        
        public int PCLogins { get; set; }
        public int TotalPCs { get; set; }
        public int TotalNPCs { get; set; }
        public int PCDeaths { get; set; }
        public int NPCDeaths { get; set; }

        public event EventHandler<Packet> GameEvents;

        public CombatManager Combat;
        public DataManager Data = new DataManager();

        public List<Item> Items = new List<Item>();
        public List<Effect> Effects = new List<Effect>();
        public List<Quest> Quests = new List<Quest>();

        public List<Area> Areas = new List<Area>();
        public List<Party> Parties = new List<Party>();

        // Used by single player or perhaps by all if moving away from per hex containers
        public List<NPC> NPCs = new List<NPC>();
        public List<PC> Players = new List<PC>();

        #endregion

        #region Application

        public RealmManager(int id = 1, string name = "Dungeon Lab", 
            int roundDuration = 2000, int pulseRate = 2000)
        {
            ID = id;
            Name = name;
            RoundDuration = roundDuration;
            PulseRate = pulseRate;
            Round = 0;
            Version = "v1.0.0";
        }

        private void ClearAllData()
        {
            PCLogins = PCDeaths = NPCDeaths = 0;
            Quests.Clear();
            Effects.Clear();
            Items.Clear();
            Areas.Clear();
            NPCs.Clear();
            Players.Clear();
        }

        private void LoadAllData()
        {
            Effects = Data.LoadEffects();
            Items = Data.LoadItems();
            Quests = Data.LoadQuests();
            Areas = Data.LoadAreas();
            NPCs = Data.LoadNPCs();
            //PCs = Data.LoadPCs();
        }

        public void Start()
        {
            Combat = new CombatManager(this);

            ClearAllData();
            LoadAllData();

            SpawnNPCs();

            Running = true;
            Started = DateTime.Now;
        }

        public void Stop()
        {
            try
            {
                //SavePCs();
                RemovePC(0); // Remove all players
                             //SaveNPCs();
                foreach (var npc in NPCs)
                {
                    RemoveEntity(npc);
                }
            }
            catch { }

            Running = false;
        }

        public void SpawnNPCs()
        {
            foreach (var e in GetEncounterNPCs(EncounterType.Insect, 4).ToList())
            {
                e.Loc.HexID = 2;
                Areas[1].Hexes[1].NPCs.Add(e);
            }

            foreach (var e in GetEncounterNPCs(EncounterType.Undead, 4).ToList())
            {
                e.Loc.HexID = 3;
                Areas[1].Hexes[2].NPCs.Add(e);
            }

            foreach (var e in GetEncounterNPCs(EncounterType.RareUndead, 3).ToList())
            {
                e.Loc.HexID = 4;
                Areas[1].Hexes[3].NPCs.Add(e);
            }

            foreach (var e in GetEncounterNPCs(EncounterType.Common, 10).ToList())
            {
                e.Loc.HexID = 6;
                Areas[1].Hexes[5].NPCs.Add(e);
            }

            foreach (var e in GetEncounterNPCs(EncounterType.Animal, 8).ToList())
            {
                e.Loc.HexID = 8;
                Areas[1].Hexes[7].NPCs.Add(e);
            }

            foreach (var e in GetEncounterNPCs(EncounterType.Aquatic, 5).ToList())
            {
                e.Loc.HexID = 10;
                Areas[1].Hexes[9].NPCs.Add(e);
            }

            foreach (var e in GetEncounterNPCs(EncounterType.Demonic, 5).ToList())
            {
                e.Loc.HexID = 11;
                Areas[1].Hexes[10].NPCs.Add(e);
            }

            foreach (var e in GetEncounterNPCs(EncounterType.VeryRare, 5).ToList())
            {
                e.Loc.HexID = 11;
                Areas[1].Hexes[10].NPCs.Add(e);
            }

            foreach (var e in GetEncounterNPCs(EncounterType.DragonKind, 8).ToList())
            {
                e.Loc.HexID = 5;
                Areas[1].Hexes[4].NPCs.Add(e);
            }

            int id = 1000;

            var barkeep = GetEncounterNPCs(EncounterType.Unique, 1, id++).Single();
            barkeep.Loc.AreaID = 0;
            barkeep.Loc.HexID = 1;
            Areas[0].Hexes[0].NPCs.Add(barkeep);

            var dummy = GetEncounterNPCs(EncounterType.Unique, 1, id++).Single();
            dummy.Loc.AreaID = 0;
            dummy.Loc.HexID = 1;
            Areas[0].Hexes[0].NPCs.Add(dummy);

            var demogorgon = GetEncounterNPCs(EncounterType.Unique, 1, id++).Single();
            demogorgon.Loc.HexID = 9;
            Areas[1].Hexes[8].NPCs.Add(demogorgon);

            var queen = GetEncounterNPCs(EncounterType.Unique, 1, id++).Single();
            queen.Loc.HexID = 7;
            Areas[1].Hexes[6].NPCs.Add(queen);

            var king = GetEncounterNPCs(EncounterType.Unique, 1, id++).Single();
            king.Loc.HexID = 7;
            Areas[1].Hexes[6].NPCs.Add(king);
        }

        public void ProcessEvents()
        {
            try
            {
                foreach (Area area in Areas)
                {
                    foreach (Hex hex in area.Hexes)
                    {
                        //lock (hex.NPCs)
                        {
                            for (int i = 0; i < hex.NPCs.Count; i++)
                            {
                                var npc = hex.NPCs[i];

                                // NPCs that are dead and not pacifist will attack their last attacker
                                if (npc.State != StateType.Dead && npc.Mood != MoodType.Pacifist)
                                {
                                    if (npc.LastAttackerID > 0)
                                    {
                                        var target = FindPC(npc.LastAttackerID);

                                        if (target != null && target.Loc.HexID ==
                                            npc.Loc.HexID &&
                                            target.State != StateType.Invisible &&
                                            target.State != StateType.Ethereal)
                                        {
                                            Combat.Attack(npc, target);
                                        }
                                        else
                                        {
                                            if (npc.State != StateType.Dead)
                                            {
                                                npc.State = StateType.Normal;
                                                npc.Mood = MoodType.Aggressive;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (npc.Mood >= MoodType.Aggressive)
                                        {
                                            var randomPC = GetPCs().OrderBy(p => Randomizer.Next()).FirstOrDefault();
                                            if (randomPC != null && randomPC.Loc.HexID == npc.Loc.HexID)
                                            {
                                                var target = FindPC(randomPC.ID);
                                                npc.LastAttackerID = randomPC.ID;
                                            }
                                        }
                                    }
                                }

                                // Respawn dead NPCs.
                                if (npc.State == StateType.Dead)
                                {
                                    if (DateTime.Now.Subtract(npc.DeathTime).Seconds >=
                                        Constants.NPCDefaultCorpseDecay)
                                    {
                                        // Revive npcs based on their decary rate
                                        if (Randomizer.Next(100) >= npc.CorpseDecayRate)
                                        {
                                            npc.Revive();
                                            npc.LastAttackerID = 0;
                                            npc.Followed = 0;
                                            SendPlayerStatusToHex(npc.Loc);
                                        }
                                        else
                                        {
                                            //TODO: Corpse cleanup / respawn periodically
                                            //RemoveEntity(npc);
                                        }
                                    }
                                }
                                else
                                {
                                    npc.Regen();
                                }
                            }
                        }

                        //lock(hex.PCs)
                        {
                            for (int i = 0; i < hex.PCs.Count; i++)
                            {
                                var pc = hex.PCs[i];
                                
                                if (pc.Regen())
                                {
                                    SendPlayerStatus(pc.ID);
                                }

                                if (Constants.PCInactivityTimeout > 0 && 
                                    (DateTime.Now - pc.LastActivity).TotalSeconds > 
                                    Constants.PCInactivityTimeout)
                                {
                                    //TODO: Debug a lock race condition
                                    var exitMessage = pc.FullName + " has left the realm due to inactivity.";
                                    SendPlayerCommand(pc.ID, ActionType.Exit, exitMessage);
                                    //SendPlayerStatusToHex(pc.Loc, exitMessage, false, pc.ID);
                                    //RemovePC(pc.ID);
                                    //BroadcastMessage(exitMessage);
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                BroadcastMessage(ex.Message);
            }
        }

        #endregion

        #region Communications

        public string HandlePacket(Packet packet, int playerId)
        {
            PC? player = null;
            Entity? target = null;
            string result = String.Empty;

            // This join only occurs in local single player mode
            if (packet.ActionType == ActionType.Join)
            {
                // Remove any previous instances of player
                player = FindPC(playerId);
                if (player != null)
                {
                    RemovePC(player.ID);
                }
                AddPlayer(playerId);
                player = FindPC(playerId);
                var joined = player.FullName + " has joined the realm.";
                BroadcastMessage(joined);
                SendPlayerStatusToHex(player.Loc, joined);
                SendPlayerStatus(player.ID, Environment.NewLine + "Welcome to the Realm! Type /help for a list of commands.", true);
            }
            else
            {
                player = FindPC(playerId);
            }

            if (player != null)
            {
                player.LastActivity = DateTime.Now;

                if (!String.IsNullOrEmpty(packet.Text))
                {
                    packet.Text = packet.Text.Trim();

                    if (packet.Text.StartsWith("/"))
                    {
                        packet.ActionType = ActionType.Command;

                        // Preserve casing for bio command text
                        if (packet.Text.StartsWith("/bio"))
                        {
                            packet.Text =
                                packet.Text.Replace("/", "").Trim();
                        }
                        else
                        {
                            packet.Text = 
                                packet.Text.Replace("/", "").Trim().ToLower();
                        }
                    }
                }

                // Find a PC or NPC, if a target ID is provided
                if (packet.TargetID > 0)
                {
                    target = FindPC(packet.TargetID);

                    if (target == null)
                    {
                        target = FindNPC(packet.TargetID);
                    }
                }

                switch (packet.ActionType)
                {
                    case ActionType.Exit:
                        var exitMessage = player.FullName + " has left the realm.";
                        SendPlayerStatusToHex(player.Loc);
                        BroadcastMessage(exitMessage);
                        RemovePC(player.ID);
                        break;

                    case ActionType.Command:
                        if (player.AccountType != AccountType.DungeonMaster && 
                            (packet.Text.StartsWith("despawn") ||
                            packet.Text.StartsWith("hps") ||
                            packet.Text.StartsWith("give") ||
                            packet.Text.StartsWith("kill") ||
                            packet.Text.StartsWith("levelup") ||
                            packet.Text.StartsWith("reboot") ||
                            packet.Text.StartsWith("shutdown") ||
                            packet.Text.StartsWith("spawn") ||
                            packet.Text.StartsWith("summon") ||
                            packet.Text.StartsWith("tile") ||
                            packet.Text.StartsWith("tp")))
                        {
                            SendPlayerMessage(player.ID, "You have no knowledge of how to use this power.");
                            return "";
                        }

                        if (packet.Text.ToLower() == "help")
                        {
                            SendPlayerStatus(player.ID, "List of / commands:\r\nbio, despawn, help, hide, hps, get, give, inspect, kill, levelup, list look, played, pvp, quit, reboot, revive, shutdown, spawn, stats, summon, tile, tp, who, yell");
                        }
                        else if (packet.Text.StartsWith("bio"))
                        {
                            string bioResult = "Unable to change bio. Usage: bio <biography text>";
                            try
                            {
                                var newBio =
                                    packet.Text.Substring(4, packet.Text.Length - 4).Trim();

                                if (newBio != null)
                                {
                                    player.Bio = newBio;
                                    bioResult = player.FullName + " bio is now set to " + newBio;
                                    SendPlayerStatusToHex(player.Loc, bioResult);
                                }
                            }
                            catch
                            {
                                SendPlayerStatus(player.ID, bioResult);
                            }
                        }
                        else if (packet.Text.StartsWith("despawn"))
                        {
                            string despawnResult = "Unable to despawn target. Usage: despawn <target name>";

                            try
                            {
                                var despawnTarget = packet.Text.Split(" ")[1].Trim();
                                if (despawnTarget != null)
                                {
                                    Entity despawned = FindEntity(0, despawnTarget);
                                    if (despawned != null)
                                    {
                                        despawnResult = despawned.FullName + " has been despawned by " + player.Name + "!";
                                        RemoveEntity(despawned);
                                        SendPlayerStatusToHex(player.Loc);
                                        BroadcastMessage(despawnResult);
                                    }
                                    else
                                    {
                                        SendPlayerMessage(player.ID, "Unable to find kill target " + despawnTarget + ".");
                                    }
                                }
                            }
                            catch
                            {
                                SendPlayerStatus(player.ID, despawnResult);
                            }
                        }
                        else if (packet.Text == "hide")
                        {
                            if (player.State == StateType.Dead)
                            {
                                SendPlayerMessage(player.ID, "You cannot do that when you are dead.");
                            }
                            else
                            {
                                if (player.State == StateType.Invisible)
                                {
                                    player.State = StateType.Normal;
                                    SendPlayerStatusToHex(player.Loc);
                                    SendPlayerMessage(player.ID, "You emerge from the shadows.");
                                }
                                else
                                {
                                    if (player.AttemptToHide())
                                    {
                                        player.State = StateType.Invisible;
                                        SendPlayerStatusToHex(player.Loc);
                                        SendPlayerMessage(player.ID, "You hide in the shadows.");
                                    }
                                    else
                                    {
                                        player.State = StateType.Normal;
                                        SendPlayerMessage(player.ID, "You fail to hide in the shadows.");
                                    }
                                }
                            }
                        }
                        else if (packet.Text.StartsWith("hps"))
                        {
                            string hpResult = "Unable to change maximum hit points. Usage: hps <hp amount>";
                            try
                            {
                                var newHps = packet.Text.Split(" ")[1].Trim();
                                if (newHps != null)
                                {
                                    player.MaxHPs = Int16.Parse(newHps);
                                    player.Revive();
                                    hpResult = player.FullName + " maximum hit points are now set to " + newHps;
                                    SendPlayerStatusToHex(player.Loc, hpResult);
                                }
                            }
                            catch
                            {
                                SendPlayerStatus(player.ID, hpResult);
                            }
                        }
                        else if (packet.Text.StartsWith("inspect"))
                        {
                            var desc = "You see " + player.GetDescription(true);

                            if (packet.Text.Length > 8)
                            {
                                var inspectedName =
                                    packet.Text.Substring(8, packet.Text.Length - 8).Trim().ToLower();

                                if (!String.IsNullOrEmpty(inspectedName))
                                {
                                    var pc = FindPC(-1, inspectedName);
                                    if (pc != null)
                                    {
                                        desc = "You see " + pc.GetDescription();
                                    }
                                    else
                                    {
                                        var npc = FindNPC(-1, inspectedName);
                                        if (npc != null)
                                        {
                                            desc = "You see " + npc.GetDescription();
                                        }
                                    }
                                }
                            }

                            SendPlayerStatus(player.ID, desc);
                        }
                        else if (packet.Text.StartsWith("get"))
                        {
                            SendPlayerMessage(player.ID, "You cannot pick up items... yet!");
                        }
                        else if (packet.Text.StartsWith("give"))
                        {
                            string itemResult = "Unable to give item. Usage: give <target> <item name>:\r\n";
                            foreach (var item in Items)
                            {
                                itemResult += item.Name.ToLower().Trim() + ", ";
                            }
                            itemResult = itemResult.Trim().TrimEnd(',');

                            try
                            {
                                var giveTargetName = packet.Text.Split(" ")[1].Trim();
                                if (giveTargetName != null)
                                {
                                    Entity giveTarget = FindEntity(0, giveTargetName);
                                    if (giveTarget != null)
                                    {
                                        var giveItemName =
                                            packet.Text.Substring(giveTargetName.Length + 6, 
                                            packet.Text.Length - giveTargetName.Length - 6).Trim();

                                        var weapon = Data.LoadItems()
                                            .Where(i => i.Name.ToLower() == giveItemName).Single();

                                        if (weapon != null)
                                        {
                                            giveTarget.MainHand = weapon;
                                            itemResult = player.Name + " gives " + 
                                                giveTarget.Name + " " + weapon.FullName;

                                            SendPlayerStatusToHex(player.Loc, itemResult);
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                SendPlayerStatus(player.ID, itemResult);
                            }
                        }
                        else if (packet.Text.StartsWith("kill"))
                        {
                            string killResult = "Unable to kill target. Usage: kill <target name>";
                            try
                            {
                                var killTarget = packet.Text.Split(" ")[1].Trim();
                                if (killTarget != null)
                                {
                                    Entity killed = FindEntity(0, killTarget);
                                    if (killed != null)
                                    {
                                        killed.Die();
                                        killResult = killed.FullName + " has been killed by " + player.Name + "!";
                                        SendPlayerStatusToHex(player.Loc);
                                        BroadcastMessage(killResult);
                                    }
                                    else
                                    {
                                        SendPlayerMessage(player.ID, "Unable to find kill target " + killTarget + ".");
                                    }
                                }
                            }
                            catch
                            {
                                SendPlayerStatus(player.ID, killResult);
                            }
                        }
                        else if (packet.Text.StartsWith("levelup"))
                        {
                            string levelupResult = "Unable to level up target. Usage: levelup <target name>";
                            try
                            {
                                var levelupTarget = packet.Text.Split(" ")[1].Trim();
                                if (levelupTarget != null)
                                {
                                    Entity leveled = FindEntity(0, levelupTarget);
                                    if (leveled != null)
                                    {
                                        if (leveled.Level < Data.LevelLookup.Count - 1)
                                        {
                                            leveled.LevelUp();
                                            levelupResult = leveled.FullName + " has been leveled up by " + player.Name + "!";
                                            SendPlayerStatusToHex(player.Loc);
                                            BroadcastMessage(levelupResult);
                                        }
                                        else
                                        {
                                            SendPlayerMessage(player.ID, leveled.FullName + " is already at maximum level.");
                                        }
                                    }
                                    else
                                    {
                                        SendPlayerMessage(player.ID, "Unable to find level up target " + levelupTarget + ".");
                                    }
                                }
                            }
                            catch
                            {
                                SendPlayerStatus(player.ID, levelupResult);
                            }
                        }
                        else if (packet.Text == "look")
                        {
                            SendPlayerStatus(player.ID, "", true);
                        }
                        else if (packet.Text == "played")
                        {
                            var played = (DateTime.Now - player.Created);
                            SendPlayerMessage(player.ID, player.FullName + " has been playing for " +
                                played.Days.ToString() + " days, " + played.Hours.ToString() + " hours, " +
                                played.Seconds.ToString() + " seconds.");
                        }
                        else if (packet.Text == "pvp")
                        {
                            SendPlayerMessage(player.ID,
                                "Usage: PVP <on/off>\r\nYour current PVP combat status: " +
                                (player.PVP == true ? "ON" : "OFF"));
                        }
                        else if (packet.Text == "pvp on")
                        {
                            player.PVP = true;
                            SendPlayerMessage(player.ID, "PVP combat is now turned ON.");
                        }
                        else if (packet.Text == "pvp off")
                        {
                            player.PVP = false;
                            SendPlayerMessage(player.ID, "PVP combat is now turned OFF.");
                        }
                        else if (packet.Text == "quit")
                        {
                            var quitMessage = player.FullName + " has left the realm.";
                            SendPlayerCommand(player.ID, ActionType.Exit);
                            SendPlayerStatusToHex(player.Loc);
                            BroadcastMessage(quitMessage);
                            RemovePC(player.ID);
                        }
                        else if (packet.Text == "reboot")
                        {
                            var rebootMessage = " The realm is rebooting, please exit now.";
                            BroadcastMessage(rebootMessage);
                        }
                        else if (packet.Text.StartsWith("revive"))
                        {
                            if (target != null)
                            {
                                target.Revive();
                                SendPlayerStatus(player.ID, target.Name + " has been revived by " + player.Name + "!");
                            }
                            else if (packet.Text.Contains("("))
                            {
                                var targetName = packet.Text.Split(" ")[1].Trim();

                                if (!String.IsNullOrEmpty(targetName))
                                {
                                    var pc = FindPC(-1, targetName);
                                    if (pc != null)
                                    {
                                        pc.Revive();
                                        SendPlayerStatusToHex(pc.Loc,
                                            pc.Name + " has been revived by " + player.Name + "!");
                                    }
                                    else
                                    {
                                        var npc = FindNPC(-1, targetName);
                                        if (npc != null)
                                        {
                                            npc.Revive();
                                            SendPlayerStatusToHex(npc.Loc,
                                                npc.Name + " has been revived by " + player.Name + "!");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                player.Revive();
                                SendPlayerStatusToHex(player.Loc,
                                    player.Name + " has been revived by " + player.Name + "!");
                            }
                        }
                        else if (packet.Text == "shutdown")
                        {
                            var rebootMessage = " The realm is shutting down in 10 seconds, please exit now!";
                            BroadcastMessage(rebootMessage);
                            Thread.Sleep(10000);
                            Stop();
                        }
                        else if (packet.Text.StartsWith("spawn"))
                        {
                            string spawnResult = "Unable to set spawn. Usage: spawn <target name/type> <spawnrate>:\r\n";
                            foreach(var npc in NPCs)
                            {
                                spawnResult += npc.Name.ToLower().Trim() + ", ";
                            }
                            spawnResult = spawnResult.Trim().TrimEnd(',');

                            try
                            {
                                var spawnTarget =
                                    packet.Text.Substring(6, packet.Text.Length - 6).Trim();

                                if (spawnTarget != null)
                                {
                                    var spawn = NPCs.Where(npc => npc.Name.ToLower() == spawnTarget.ToLower()).Single();
                                    if (spawn != null)
                                    {
                                        spawn.Loc = player.Loc;
                                        Areas[player.Loc.AreaID].Hexes[player.Loc.HexID - 1].NPCs.Add(spawn);

                                        spawnResult = spawn.FullName + " has been spawned by " + player.Name + "!";
                                        SendPlayerStatusToHex(player.Loc);
                                        BroadcastMessage(spawnResult);
                                    }
                                    else
                                    {
                                        SendPlayerMessage(player.ID, "Unable to find spawn target " + spawnTarget + ".");
                                    }
                                }
                            }
                            catch
                            {
                                SendPlayerStatus(player.ID, spawnResult);
                            }
                        }
                        else if (packet.Text == "stats")
                        {
                            var stats = "Server v" + this.Version + ", " +
                                "Realm: " + this.Name + Environment.NewLine +
                                "Started: " + this.Started.ToString() + Environment.NewLine +
                                "Total PC Logins: " + this.PCLogins.ToString() + ", " +
                                "Total PCs: " + this.TotalPCs.ToString() + ", " +
                                "Total PC Deaths: " + this.PCDeaths.ToString() + Environment.NewLine +
                                "Total NPCs: " + this.TotalNPCs.ToString() + ", " +
                                "Total NPC Deaths: " + this.NPCDeaths.ToString();
                            SendPlayerStatus(player.ID, stats);
                        }
                        else if (packet.Text.StartsWith("summon"))
                        {
                            string summonResult = "Unable to summon target. Usage: summon <target name>:\r\n";
                            foreach (var npc in NPCs)
                            {
                                summonResult += npc.Name.ToLower().Trim() + ", ";
                            }
                            summonResult = summonResult.Trim().TrimEnd(',');

                            try
                            {
                                var spawnTarget =
                                    packet.Text.Substring(6, packet.Text.Length - 6).Trim();

                                if (spawnTarget != null)
                                {
                                    var spawn = NPCs.Where(npc => npc.Name.ToLower() == spawnTarget.ToLower()).Single();
                                    if (spawn != null)
                                    {
                                        spawn.Loc = player.Loc;
                                        Areas[player.Loc.AreaID].Hexes[player.Loc.HexID - 1].NPCs.Add(spawn);

                                        summonResult = spawn.FullName + " has been spawned by " + player.Name + "!";
                                        SendPlayerStatusToHex(player.Loc);
                                        BroadcastMessage(summonResult);
                                    }
                                    else
                                    {
                                        SendPlayerMessage(player.ID, "Unable to find spawn target " + spawnTarget + ".");
                                    }
                                }
                            }
                            catch
                            {
                                SendPlayerStatus(player.ID, summonResult);
                            }
                        }
                        else if (packet.Text.StartsWith("tile"))
                        {
                            string tileResult = "Unable to tile area. Usage: tile <tile number> <tile name>:\r\n";

                            try
                            {
                                var tileNumber = Int16.Parse(packet.Text.Split(" ")[1].Trim());

                                string tileName = String.Empty;
                                try
                                {
                                    if (!String.IsNullOrEmpty(packet.Text.Split(" ")[2].Trim()))
                                    {
                                        tileName = packet.Text.Split(" ")[2].Trim();
                                    }
                                }
                                catch { }

                                if (tileNumber == 1)
                                    Areas[player.Loc.AreaID].Hexes[player.Loc.HexID - 1].Tile.Tile1ID = tileName;
                                else if (tileNumber == 2)
                                    Areas[player.Loc.AreaID].Hexes[player.Loc.HexID - 1].Tile.Tile2ID = tileName;
                                else if (tileNumber == 3)
                                    Areas[player.Loc.AreaID].Hexes[player.Loc.HexID - 1].Tile.Tile3ID = tileName;
                                else
                                {
                                    SendPlayerStatus(player.ID, tileResult);
                                    return tileResult;
                                }

                                tileResult = player.FullName + " has changed tile " + tileNumber.ToString() + " to " + tileName;
                                SendPlayerStatusToHex(player.Loc);
                            }
                            catch
                            {
                                SendPlayerStatus(player.ID, tileResult);
                            }
                        }
                        else if (packet.Text.StartsWith("tp"))
                        {
                            string tpResult = "Unable to teleport target. Usage: tp <target name> <target2 name> or tp <target name> <area id> <hex id>";

                            try
                            {
                                var targetName = packet.Text.Split(" ")[1].Trim();
                                Entity tpTarget = FindEntity(0, targetName);

                                if (tpTarget != null)
                                {
                                    if (packet.Text.Split(" ").Length <= 3)
                                    {
                                        var target2Name = packet.Text.Split(" ")[2].Trim();
                                        Entity tpTarget2 = FindEntity(0, target2Name);
                                        if (tpTarget2 != null)
                                        {
                                            MovePC(tpTarget as PC, tpTarget2.Loc);

                                            tpResult = tpTarget.FullName + " has been teleported to " + 
                                                tpTarget2.FullName + " by " + player.Name + "!";

                                            SendPlayerStatusToHex(tpTarget.Loc, tpResult);
                                            SendPlayerStatusToHex(tpTarget2.Loc, tpResult);
                                        }
                                        else
                                        {
                                            tpResult = "Invalid teleport targets. " + tpResult;
                                            SendPlayerStatus(player.ID, tpResult);
                                        }
                                    }
                                    else
                                    {
                                        var areaId = packet.Text.Split(" ")[2].Trim();
                                        var hexId = packet.Text.Split(" ")[3].Trim();
                                        var newAreaId = Int16.Parse(areaId);
                                        var newHexId = Int16.Parse(hexId);

                                        if (newAreaId >= 0 && newAreaId < Areas.Count &&
                                            newHexId > 0 && newHexId <= Areas[newAreaId].Hexes.Count)
                                        {
                                            var fromLoc = player.Loc;
                                            var toLoc = new Loc()
                                            {
                                                AreaID = newAreaId,
                                                HexID = newHexId,
                                            };
                                            MovePC(tpTarget as PC, toLoc);
                                        
                                            tpResult = tpTarget.FullName + " has been teleported by " + player.Name + "!";

                                            SendPlayerStatusToHex(fromLoc, tpResult);
                                            SendPlayerStatusToHex(toLoc, tpResult);
                                        }
                                        else
                                        {
                                            tpResult = "Invalid target location. " + tpResult;
                                            SendPlayerStatus(player.ID, tpResult);
                                        }
                                    }
                                }
                                else
                                {
                                    SendPlayerStatus(player.ID, tpResult);
                                }
                            }
                            catch
                            {
                                SendPlayerStatus(player.ID, tpResult);
                            }
                        }
                        else if (packet.Text.StartsWith("yell"))
                        {
                            var yell = packet.Text.Substring(packet.Text.IndexOf(" ")+1, 
                                packet.Text.Length - packet.Text.IndexOf(" ")-1);

                            if (!String.IsNullOrEmpty(yell))
                            {
                                YellMessage(player.FullName + " yells, \"" + yell + "\"",
                                    player.Loc.AreaID);
                            }
                        }
                        else if (packet.Text == "who" || packet.Text == "list")
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append(Environment.NewLine + "List of players:");
                            foreach (var p in GetAllPCs())
                            {
                                sb.Append(Environment.NewLine);
                                sb.Append(p.GetDescription(false, true));
                            }
                            SendPlayerStatus(player.ID, sb.ToString(), true);
                        }
                        else
                        {
                            SendPlayerStatus(player.ID, "Command not recognized.");
                        }
                        break;

                    case ActionType.Movement:
                        if (player.State != StateType.Dead && 
                            player.State != StateType.Stunned)
                        {
                            if (Move(player, packet.Text))
                            {
                                SendPlayerStatus(player.ID, "", true);
                            }
                        }
                        break;

                    case ActionType.Say:
                        SayMessage(player.FullName + " says, \"" + packet.Text + "\"",
                            player.Loc.AreaID, player.Loc.HexID);
                        break;

                    case ActionType.Yell:
                        YellMessage(player.FullName + " yells, \"" + packet.Text + "\"",
                            player.Loc.AreaID);
                        break;

                    case ActionType.Tell:
                        SendPlayerMessage(player.ID,
                            player.FullName + " tells you, \"" + packet.Text + "\"", target.ID);
                        break;

                    case ActionType.Broadcast:
                        BroadcastMessage(player.FullName ?? String.Empty + ":" + packet.Text);
                        break;

                    case ActionType.Damage:
                        if (player.State == StateType.Dead)
                        {
                            SendPlayerMessage(player.ID, "You cannot do that when you are dead.");
                        }
                        else if(Areas[player.Loc.AreaID].Hexes[player.Loc.HexID - 1].NoCombat &&
                            player.AccountType != AccountType.DungeonMaster)
                        { 
                            SendPlayerMessage(player.ID, "Combat is not allowed in this area.");
                        }
                        else
                        {
                            try
                            {
                                // Find target whether its an NPC or PC
                                if (packet.Text != null)
                                {
                                    target = Areas[player.Loc.AreaID]
                                        .Hexes[player.Loc.HexID - 1].NPCs.Find(n => n.Name == packet.Text);

                                    if (target == null)
                                    {
                                        target = Areas[player.Loc.AreaID]
                                            .Hexes[player.Loc.HexID - 1].PCs.Find(n => n.Name == packet.Text);
                                    }
                                }

                                if (player.State != StateType.Dead &&
                                    player.State != StateType.Stunned)
                                {
                                    if (player != null && target != null &&
                                        (player.Loc.AreaID == target.Loc.AreaID) &&
                                        (player.Loc.HexID == target.Loc.HexID))
                                    {
                                        if (target.Attackable && target.State != StateType.Ethereal)
                                        {
                                            if (player.Type != EntityType.DungeonMaster && 
                                                player is PC && target is PC && ((!((PC)player).PVP) || (!((PC)target).PVP)))
                                            {
                                                SendPlayerMessage(player.ID, "PVP is not enabled for one of the involved combtants.");
                                            }
                                            else
                                            {
                                                result = Combat.Attack(player, target);
                                            }
                                        }
                                        else
                                        {
                                            SendPlayerMessage(player.ID, "This target is not attackable at this time.");
                                        }
                                    }
                                    else
                                    {
                                        SendPlayerMessage(player.ID, "Your target is out of reach.");
                                    }
                                }
                            }
                            catch(Exception ex)
                            {
#if DEBUG                                
                                return "Target was not found. " + ex.Message;
#else
                                return "Target was not found.";
#endif
                            }
                        }
                        break;
                }
            }

            return result;
        }

        private void WritePacket(Connection conn, Packet packet)
        {
            if (conn == null)
            {
                // Local only - Pass packets along
                GameEvents(conn, packet);
            }
            else
            {
                // Write packet to outgoing buffer
                conn.BufferPacket(packet);
            }
        }

#endregion

        #region Realm Controls

        public void AddEntity(Entity entity)
        {
            if (entity is PC)
            {
                lock (Players)
                {
                    Areas[entity.Loc.AreaID]
                        .Hexes[entity.Loc.HexID - 1]
                        .PCs.Add((PC)entity);
                }
                TotalPCs++;
                PCLogins++;
            }
            else if (entity is NPC)
            {
                lock (NPCs)
                {
                    Areas[entity.Loc.AreaID]
                        .Hexes[entity.Loc.HexID - 1]
                        .NPCs.Add((NPC)entity);
                }
                TotalNPCs++;
            }
        }

        public Entity FindEntity(int id, string name = "")
        {
            foreach (Area area in Areas)
            {
                foreach (Hex hex in area.Hexes)
                {
                    lock (hex.PCs)
                    {
                        foreach (PC p in hex.PCs)
                        {
                            if (p.ID == id || p.Name.ToLower() == name.ToLower())
                            {
                                return p;
                            }
                        }
                        foreach (NPC n in hex.NPCs)
                        {
                            if (n.ID == id || n.Name == name)
                            {
                                return n;
                            }
                        }
                    }
                }
            }

            return null;
        }

        public List<PC> GetAllPCs()
        {
            List<PC> list = new List<PC>();

            foreach (Area area in Areas)
            {
                foreach (Hex hex in area.Hexes)
                {
                    foreach (PC p in hex.PCs)
                    {
                        list.Add(p);
                    }
                }
            }

            return list;
        }

        public PC FindPC(int id, string name = "")
        {
            foreach (Area area in Areas)
            {
                foreach (Hex hex in area.Hexes)
                {
                    foreach (PC p in hex.PCs)
                    {
                        if (p.ID == id || 
                            p.Name.ToLower() == name.ToLower())
                        {
                            return p;
                        }
                    }
                }
            }

            return null;
        }

        public NPC FindNPC(int id, string name = "")
        {
            foreach (Area area in Areas)
            {
                foreach (Hex hex in area.Hexes)
                {
                    foreach (NPC n in hex.NPCs)
                    {
                        if (n.ID == id || 
                            n.Name.ToLower() == name.ToLower())
                        {
                            return n;
                        }
                    }
                }
            }

            return null;
        }

        public void SendPlayerCommand(int playerId, ActionType type, string text = "")
        {
            var player = FindPC(playerId);

            if (player != null)
            {
                var packet = new Packet()
                {
                    ActionType = type,
                    Text = text,
                };

                WritePacket(player.Conn, packet);
            }
        }

        public void SendPlayerMessage(int playerId, string text = "", int fromId = 0)
        {
            //if (!String.IsNullOrEmpty(text))
            {
                var player = FindPC(playerId);

                if (player != null)
                {
                    var packet = new Packet()
                    {
                        ActionType = ActionType.Text,
                        TargetID = fromId,
                        Text = text,
                    };

                    WritePacket(player.Conn, packet);
                }
            }
        }

        public void SendPlayerStatusToHex(Loc loc, 
            string text = "", bool hexDescription = false, int skipId = 0)
        {
            var hex = Areas[loc.AreaID].Hexes[loc.HexID-1];

            foreach (var pc in hex.PCs)
            {
                if (pc.ID != skipId)
                {
                    SendPlayerStatus(pc.ID, text, hexDescription);
                }
            }
        }

        public void SendPlayerStatus(int playerId, string text = "", 
            bool hexDescription = false)
        {
            var player = FindPC(playerId);

            if (player != null)
            {
                var hex = Areas[player.Loc.AreaID].Hexes[player.Loc.HexID - 1];

                var npcs = new Dictionary<string, Stats>();
                foreach (var npc in hex.NPCs)
                {
                    if (npc.State == StateType.Invisible)
                    {
                        if (player.AccountType == AccountType.DungeonMaster)
                        {
                            npcs.Add(npc.ID.ToString(), npc.GetStats());
                        }
                    }
                    else
                    {
                        npcs.Add(npc.ID.ToString(), npc.GetStats());
                    }
                }

                var players = new Dictionary<string, Stats>();
                foreach (var pc in hex.PCs)
                {
                    if (pc.State == StateType.Invisible)
                    {
                        if (player.AccountType == AccountType.DungeonMaster)
                        {
                            players.Add(pc.ID.ToString(), pc.GetStats());
                        }
                    }
                    else 
                    { 
                        players.Add(pc.ID.ToString(), pc.GetStats());
                    }
                }

                var items = new Dictionary<string, Stats>();
                foreach (var i in hex.Items)
                {
                    items.Add(i.ID.ToString(), i.GetStats());
                }

                var packet = new Packet()
                {
                    ActionType = ActionType.Status,
                    Health = player.GetStats(),
                    Tile = hex.Tile,
                    NPCs = npcs,
                    PCs = players,
                    Items = items,
                };

                if (hexDescription)
                {
                    packet.Text = Areas[player.Loc.AreaID].Title + " - "  + 
                        hex.GetDescription(playerId) + text;
                }
                else
                {
                    packet.Text = text;
                }

                WritePacket(player.Conn, packet);
            }
        }

        public List<PC> GetPCs()
        {
            List<PC> players = new List<PC>();

            foreach (Area area in Areas)
            {
                foreach (Hex hex in area.Hexes)
                {
                    lock (hex.PCs)
                    {
                        foreach (PC player in hex.PCs)
                        {
                            if (player.Conn != null)
                            {
                                players.Add(player);
                            }
                        }
                    }
                }
            }

            return players;
        }

        public PC AddPlayer(int playerId = 0, int partyId = 1, string playerName = "",
            Connection conn = null)
        {
            if (!Running)
            {
                return null;
            }

            try
            {
                PC player = new PC();

                if (playerId != 0)
                {
                    player = Data.LoadPCs()
                        .Where(p => p.ID == playerId).Single();
                }
                else
                {
                    player = Data.LoadPCs()
                        .Where(p => p.Name == playerName).Single();
                }

                if (conn != null)
                {
                    player.Conn = conn;
                }

                if (player.MainHandID > 0)
                {
                    var weapon = Data.LoadItems()
                        .Where(i => i.ID == player.MainHandID).Single();

                    if (weapon != null)
                        player.MainHand = weapon;
                }

                AddEntity(player);

                return player;
            }
            catch//(Exception ex) 
            {
                return null;
            }
        }

        public void RemoveEntity(Entity e)
        {
            var hex = Areas[e.Loc.AreaID].Hexes[e.Loc.HexID - 1];

            lock (hex.NPCs)
            {
                for (int i = 0; i < hex.NPCs.Count; i++)
                {
                    if (hex.NPCs[i].ID == e.ID)
                    {
                        hex.NPCs.RemoveAt(i);
                    }
                }
            }
        }

        public void RemovePC(int playerId = 0)
        {
            foreach (Area area in Areas)
            {
                foreach (Hex hex in area.Hexes)
                {
                    lock (hex.PCs)
                    {
                        for (int i = 0; i < hex.PCs.Count; i++)
                        {
                            if (playerId == 0 || hex.PCs[i].ID == playerId)
                            {
                                var player = (PC)hex.PCs[i];

                                if (player.Conn != null)
                                {
                                    player.Conn.Disconnect();
                                }
                                hex.PCs.RemoveAt(i);
                            }
                        }
                    }
                }
            }
        }

        public void MovePC(PC entity, Loc loc)
        {
            var fromHex = Areas[entity.Loc.AreaID].Hexes[entity.Loc.HexID - 1];
            var toHex = Areas[loc.AreaID].Hexes[loc.HexID - 1];
            
            lock (fromHex)
            { 
                fromHex.PCs.Remove(entity);
                entity.Loc = loc;
            }

            lock (toHex)
            {
                toHex.PCs.Add(entity);
            }
        }

        public bool Move(Entity entity, string direction, int areaId = -1, int hexId = -1)
        {
            bool moved = false;

            if (entity.State == StateType.Stunned)
            {
                return moved;
            }

            var hex = Areas[entity.Loc.AreaID].Hexes[entity.Loc.HexID - 1];

            MoveDirection md = MoveDirection.North;

            if (areaId != -1 && hexId != -1)
            {
                moved = true;
            }
            else
            {
                if (areaId == -1)
                    areaId = entity.Loc.AreaID;

                switch (direction.ToLower())
                {
                    case "north":
                        if (hex.Tile.North > 0)
                        {
                            hexId = hex.Tile.North;
                            md = MoveDirection.North;
                            moved = true;
                        }
                        break;
                    case "south":
                        if (hex.Tile.South > 0)
                        {
                            hexId = hex.Tile.South;
                            md = MoveDirection.South;
                            moved = true;
                        }
                        break;
                    case "east":
                        if (hex.Tile.East > 0)
                        {
                            hexId = hex.Tile.East;
                            md = MoveDirection.East;
                            moved = true;
                        }
                        break;
                    case "west":
                        if (hex.Tile.West > 0)
                        {
                            hexId = hex.Tile.West;
                            md = MoveDirection.West;
                            moved = true;
                        }
                        break;
                    case "up":
                        if (hex.Tile.Up != null)
                        {
                            areaId = hex.Tile.Up.AreaID;
                            hexId = hex.Tile.Up.HexID;
                            md = MoveDirection.Up;
                            moved = true;
                        }
                        break;
                    case "down":
                        if (hex.Tile.Down != null)
                        {
                            areaId = hex.Tile.Down.AreaID;
                            hexId = hex.Tile.Down.HexID;
                            md = MoveDirection.Down;
                            moved = true;
                        }
                        break;
                }

                if (!moved)
                {
                    SendPlayerMessage(entity.ID, "That does not lead anywhere.");
                }
            }

            if (moved)
            {
                string leaving = entity.FullName + " moves " + 
                    direction.ToString() + ".";

                var packet =
                    new Packet()
                    {
                        ID = entity.ID,
                        ActionType = ActionType.Movement,
                        MoveDirection = md,
                        Text = leaving,
                    };

                if (entity != null && entity is PC)
                {
                    var player = entity as PC;

                    if (player != null)
                    {
                        lock (hex.PCs)
                        {
                            var playerIndex =
                                hex.PCs.FindIndex(p => p.ID == entity.ID);

                            if (playerIndex >= 0)
                            {
                                hex.PCs.RemoveAt(playerIndex);
                            }
                        }

                        if (player.State != StateType.Invisible)
                        {
                            SendPlayerStatusToHex(player.Loc, leaving, false, player.ID);
                        }

                        var newHex = Areas[areaId].Hexes[hexId - 1];

                        lock (newHex.PCs)
                        {
                            newHex.PCs.Add(player);
                            player.Loc.AreaID = areaId;
                            player.Loc.HexID = newHex.ID;
                        }

                        if (player.State != StateType.Invisible)
                        {
                            string arriving = player.Name + " enters the area.";
                            SendPlayerStatusToHex(player.Loc, arriving, false, player.ID);
                        }

                        SendPlayerStatus(player.ID);
                    }

                    // Notify npcs of pc movement
                    try
                    {
                        foreach (var npc in hex.NPCs)
                        {
                            npc.SendPacket(this, packet, player);
                        }
                    }
                    catch { }
                }

                if (entity is NPC)
                {
                    var npc = entity as NPC;
                    var newHex = Areas[areaId].Hexes[hexId - 1];

                    lock (hex.NPCs)
                    {
                        var npcIndex =
                            hex.NPCs.FindIndex(n => n.ID == entity.ID);

                        hex.NPCs.RemoveAt(npcIndex);

                        foreach (var p in hex.PCs)
                        {
                            WritePacket(p.Conn, packet);
                        }

                        newHex.NPCs.Add(npc);
                        npc.Loc.HexID = newHex.ID;
                    }

                    string arriving = npc.Name + " enters the area.";

                    lock (newHex.PCs)
                    {
                        foreach (var p in newHex.PCs)
                        {
                            WritePacket(p.Conn,
                                new Packet()
                                {
                                    ID = npc.ID,
                                    ActionType = ActionType.Movement,
                                    MoveDirection = md,
                                    Text = arriving
                                });
                        }
                    }
                }
            }

            return moved;
        }

        public List<NPC> GetEncounterNPCs(EncounterType type, int maximum = 99, int npcId = 0)
        {
            List<NPC> encounter = new List<NPC>();

            if (npcId != 0)
            {
                encounter.Add(NPCs.Where(npc => npc.ID == npcId).Single().Clone());
            }
            else
            {
                var group = NPCs.Where(npc => npc.EncounterClass == type).Take(maximum);

                foreach (NPC npc in group.ToList())
                {
                    //var guy = npc.Clone();
                    //lock (EntityCount)
                    {
                        //guy.ID = EntityCount++;
                    }

                    encounter.Add(npc.Clone());
                }
            }

            return encounter;
        }

        #endregion

        #region Logging

        public void BroadcastMessage(string message)
        {
            BroadcastGameEvent(new Packet()
            {
                ActionType = ActionType.Broadcast,
                Text = message
            });
        }

        public void TellMessage(PC player, string message)
        {
            WritePacket(player.Conn,
                new Packet() { ActionType = ActionType.Text, Text = message });
        }

        public void SayMessage(string message, int areaId, int hexId)
        {
            foreach (PC player in Areas[areaId].Hexes[hexId - 1].PCs)
            {
                WritePacket(player.Conn,
                    new Packet() { ActionType = ActionType.Text, Text = message });
            }
        }

        public void YellMessage(string message, int areaId)
        {
            foreach (Hex hex in Areas[areaId].Hexes)
            {
                foreach (PC player in hex.PCs)
                {
                    WritePacket(player.Conn,
                        new Packet() { ActionType = ActionType.Text, Text = message });
                }
            }
        }

        private void BroadcastGameEvent(Packet packet)
        {
            foreach (Area area in Areas)
            {
                foreach (Hex hex in area.Hexes)
                {
                    foreach (PC player in hex.PCs)
                    {
                        if (player.Conn != null)
                        {
                            WritePacket(player.Conn, packet);
                        }
                    }
                }
            }

            GameEvents(this, packet);
        }

        #endregion
    }
}
