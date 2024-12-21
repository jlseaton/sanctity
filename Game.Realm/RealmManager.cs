using Game.Core;
using System.Text;
using OpenAI_API;

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

        protected OpenAIAPI api = new OpenAIAPI("sk-proj-clLZtNfqXD9Q0EL2XI5iT3BlbkFJxa6uCVJt2YoFfiifVWcu");

        #endregion

        #region Application

        public RealmManager(int id = 1, string name = "Dungeon Lab", bool ai = false,
            int roundDuration = 2000, int pulseRate = 2000)
        {
            ID = id;
            Name = name;
            RoundDuration = roundDuration;
            PulseRate = pulseRate;
            Round = 0;
            Version = "v1.0.0";

            if (ai)
            {
                InitializeAI();
            }
        }

        private async Task<string> InitializeAI()
        {
            try
            {
                //var chat = api.Chat.CreateConversation();
                //chat.AppendUserInput("How to make a hamburger?");

                //await chat.StreamResponseFromChatbotAsync(res =>
                //{
                //    Console.Write(res);
                //});

                var result = await api.Chat.CreateChatCompletionAsync("Hello!");
                Console.WriteLine(result);
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
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
                RemovePC(""); // Remove all players
                //SaveNPCs();
                foreach (var npc in NPCs)
                {
                    RemoveNPC(npc);
                }

                Data.SavePCs(this.Players);
                Data.SaveAreas(this);
            }
            catch (Exception ex)
            { 
                BroadcastMessage(ex.Message);
            }

            Running = false;
        }

        public void SpawnNPCs()
        {
            foreach (var e in GetEncounterNPCs(EncounterType.Insect, 4).ToList())
            {
                e.Loc.HexID = 2;
                //Areas[1].Hexes[1].NPCs.Add(e);
            }

            foreach (var e in GetEncounterNPCs(EncounterType.Undead, 4).ToList())
            {
                e.Loc.HexID = 3;
                //Areas[1].Hexes[2].NPCs.Add(e);
            }

            foreach (var e in GetEncounterNPCs(EncounterType.RareUndead, 3).ToList())
            {
                e.Loc.HexID = 4;
                //Areas[1].Hexes[3].NPCs.Add(e);
            }

            foreach (var e in GetEncounterNPCs(EncounterType.Common, 10).ToList())
            {
                e.Loc.HexID = 6;
                //Areas[1].Hexes[5].NPCs.Add(e);
            }

            foreach (var e in GetEncounterNPCs(EncounterType.Animal, 8).ToList())
            {
                e.Loc.HexID = 8;
                //Areas[1].Hexes[7].NPCs.Add(e);
            }

            foreach (var e in GetEncounterNPCs(EncounterType.Aquatic, 5).ToList())
            {
                e.Loc.HexID = 10;
                //Areas[1].Hexes[9].NPCs.Add(e);
            }

            foreach (var e in GetEncounterNPCs(EncounterType.Demonic, 5).ToList())
            {
                e.Loc.HexID = 11;
                //Areas[1].Hexes[10].NPCs.Add(e);
            }

            foreach (var e in GetEncounterNPCs(EncounterType.VeryRare, 5).ToList())
            {
                e.Loc.HexID = 11;
                //Areas[1].Hexes[10].NPCs.Add(e);
            }

            foreach (var e in GetEncounterNPCs(EncounterType.DragonKind, 8).ToList())
            {
                e.Loc.AreaID = 0; e.Loc.Y = 0; e.Loc.X = 3;
                Areas[0].Hexes[0, 3].NPCs.Add(e);
                //var e2 = e.Clone();
                //e2.Loc.AreaID = 1; e2.Loc.Y = 0; e2.Loc.X = 3;
                //Areas[1].Hexes[0, 3].NPCs.Add(e2);
            }

            //var barkeep = GetEncounterNPCs(EncounterType.Unique, 1).Single();
            //Areas[0].Hexes[0, 0].NPCs.Add(barkeep);

            var dummy = GetEncounterNPCs(EncounterType.Unique, 1).Single();
            Areas[0].Hexes[0, 0].NPCs.Add(dummy);

            var demogorgon1 = 
                GetEncounterNPCs(EncounterType.Unique, 1, "demogorgon").Single(e => e.Name == "demogorgon");
            Areas[0].Hexes[6, 0].NPCs.Add(demogorgon1);
            var demogorgon2 =
                GetEncounterNPCs(EncounterType.Unique, 1, "demogorgon").Single(e => e.Name == "demogorgon");
            demogorgon2.Loc.AreaID = 2; demogorgon2.Loc.X = 1; demogorgon2.Loc.Y = 1;
            Areas[2].Hexes[1, 1].NPCs.Add(demogorgon2);

            var queen = GetEncounterNPCs(EncounterType.Unique, 1, "Natillah Lesbun").Single();
            Areas[0].Hexes[6, 6].NPCs.Add(queen);

            var king = GetEncounterNPCs(EncounterType.Unique, 1, "Gundarik Lesbun").Single();
            Areas[0].Hexes[6, 6].NPCs.Add(king);
        }

        public void ProcessEvents()
        {
            try
            {
                foreach (Area area in Areas)
                {
                    foreach (Hex hex in area.Hexes)
                    {
                        for (int i = 0; i < hex.NPCs.Count; i++)
                        {
                            var npc = hex.NPCs[i];

                            // NPCs that are dead and not pacifist will attack their last attacker
                            if (npc.State != StateType.Dead && npc.Mood != MoodType.Pacifist)
                            {
                                if (!String.IsNullOrEmpty(npc.LastAttackerID))
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
                                        npc.LastAttackerID = "";
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
            catch(Exception ex)
            {
                BroadcastMessage(ex.Message);
            }
        }

        #endregion

        #region Communications

        public string HandlePacket(Packet packet, string playerId)
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
                SendPlayerStatusToHex(player.Loc);
                SendPlayerMessage(player.ID, "Welcome to the Realm of " + this.Name + 
                    "! Type /help for a list of commands.");
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
                if (!String.IsNullOrEmpty(packet.TargetID))
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
                            SendPlayerStatus(player.ID, "List of / commands:\r\nbio, despawn, help, hide, hps, get, give, inspect, kill, levelup, list, loc, look, played, pvp, quit, reboot, revive, shutdown, spawn, stats, summon, tile, tp, who, yell");
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
                                    var despawned = FindEntity("", despawnTarget);
                                    if (despawned != null && despawned is NPC)
                                    {
                                        despawnResult = despawned.FullName + " has been despawned by " + player.Name + "!";
                                        RemoveNPC(despawned as NPC);
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
                                    var pc = FindPC("", inspectedName);
                                    if (pc != null)
                                    {
                                        desc = "You see " + pc.GetDescription();
                                    }
                                    else
                                    {
                                        var npc = FindNPC("", inspectedName);
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
                                    Entity giveTarget = FindEntity("", giveTargetName);
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
                                    Entity killed = FindEntity("", killTarget);
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
                                    Entity leveled = FindEntity("", levelupTarget);
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
                                    var pc = FindPC("", targetName);
                                    if (pc != null)
                                    {
                                        pc.Revive();
                                        SendPlayerStatusToHex(pc.Loc,
                                            pc.Name + " has been revived by " + player.Name + "!");
                                    }
                                    else
                                    {
                                        var npc = FindNPC("", targetName);
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
                                        Areas[player.Loc.AreaID].Hexes[player.Loc.Y, player.Loc.X].NPCs.Add(spawn);

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
                                        Areas[player.Loc.AreaID].Hexes[player.Loc.Y, player.Loc.X].NPCs.Add(spawn);

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
                        else if (packet.Text.StartsWith("hex"))
                        {
                            string tileResult = "Unable to set hex data. Usage: hex <row> <col> <tile> <description> <solid> <transparent> <combatallowed>.";

                            try
                            {
                                int row = 3;
                                int col = 3;
                                string title = String.Empty;
                                string description = String.Empty;

                                try
                                {
                                    //HACK
                                    row = Int16.Parse(packet.Text.Split(" ")[1].Trim());
                                    col = Int16.Parse(packet.Text.Split(" ")[2].Trim());

                                    if (!String.IsNullOrEmpty(packet.Text.Split(" ")[3].Trim()))
                                    {
                                        title = packet.Text.Split(" ")[3].Trim();
                                    }
                                    if (!String.IsNullOrEmpty(packet.Text.Split(" ")[4].Trim()))
                                    {
                                        title = packet.Text.Split(" ")[4].Trim();
                                    }
                                }
                                catch { }

                                var loc = MapTileGridToLoc(player.Loc, row, col);
                                if (loc != null)
                                {
                                    var hex =
                                        Areas[loc.AreaID].Hexes[loc.Y, loc.X];

                                    if (hex != null)
                                    {
                                        if (!String.IsNullOrEmpty(title))
                                            hex.Title = title;
                                        if (!String.IsNullOrEmpty(description))
                                            hex.Description = description;
                                    }

                                    SendPlayerStatusToHex(player.Loc);
                                }
                            }
                            catch
                            {

                            }
                        }
                        else if (packet.Text.StartsWith("tile"))
                        {
                            string tileResult = "Unable to set area tiles. Usage: tile <row> <col> <tile number> <tile name> <tile size> <tile xoffset> <tile yoffset>.";

                            try
                            {
                                var row = 3;
                                var col = 3;
                                var tileNumber = 1;
                                string tileName = String.Empty;
                                int tileSize = 100;
                                int tileXOffset = 0;
                                int tileYOffset = 0;
                                try
                                {
                                    //HACK
                                    row = Int16.Parse(packet.Text.Split(" ")[1].Trim());
                                    col = Int16.Parse(packet.Text.Split(" ")[2].Trim());
                                    tileNumber = Int16.Parse(packet.Text.Split(" ")[3].Trim());

                                    if (!String.IsNullOrEmpty(packet.Text.Split(" ")[4].Trim()))
                                    {
                                        tileName = packet.Text.Split(" ")[4].Trim();
                                    }
                                    else
                                    {
                                        tileName = "";
                                    }

                                    //SLASH
                                    try
                                    {
                                        tileSize = Int16.Parse(packet.Text.Split(" ")[5].Trim());
                                    }
                                    catch { }
                                    try
                                    {
                                        tileXOffset = Int16.Parse(packet.Text.Split(" ")[6].Trim());
                                    }
                                    catch { }
                                    try
                                    {
                                        tileYOffset = Int16.Parse(packet.Text.Split(" ")[7].Trim());
                                    }
                                    catch { }
                                }
                                catch { }

                                var loc = MapTileGridToLoc(player.Loc, row, col);
                                if (loc != null)
                                {
                                    var hex =
                                        Areas[loc.AreaID].Hexes[loc.Y, loc.X];

                                    if (tileNumber == 1)
                                    {
                                        hex.Tile.Tile1ID = tileName;
                                        hex.Tile.Tile1Size = tileSize;
                                        hex.Tile.Tile1XOffset = tileXOffset;
                                        hex.Tile.Tile1YOffset = tileYOffset;
                                    }
                                    else if (tileNumber == 2)
                                    {
                                        hex.Tile.Tile2ID = tileName;
                                        hex.Tile.Tile2Size = tileSize;
                                        hex.Tile.Tile2XOffset = tileXOffset;
                                        hex.Tile.Tile2YOffset = tileYOffset;
                                    }
                                    else if (tileNumber == 3)
                                    {
                                        hex.Tile.Tile3ID = tileName;
                                        hex.Tile.Tile3Size = tileSize;
                                        hex.Tile.Tile3XOffset = tileXOffset;
                                        hex.Tile.Tile3YOffset = tileYOffset;
                                    }
                                    else
                                    {
                                        SendPlayerStatus(player.ID, tileResult);
                                        return tileResult;
                                    }

                                    SendPlayerStatusToHex(player.Loc);

                                    tileResult = player.FullName + " has changed tile " + tileNumber.ToString() + " to " + tileName;

                                }
                            }
                            catch
                            {
                            
                            }
                        }
                        else if (packet.Text.StartsWith("tp"))
                        {
                            string tpResult = "Unable to teleport target. Usage: tp <target name> <target2 name> or tp <target name> <area id> <x> <y>";

                            try
                            {
                                var targetName = packet.Text.Split(" ")[1].Trim();
                                Entity tpTarget = FindEntity("", targetName);

                                if (tpTarget != null)
                                {
                                    if (packet.Text.Split(" ").Length <= 3)
                                    {
                                        var target2Name = packet.Text.Split(" ")[2].Trim();
                                        Entity tpTarget2 = FindEntity("", target2Name);
                                        if (tpTarget2 != null)
                                        {
                                            EntityTeleport(tpTarget, tpTarget2.Loc);

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
                                        var col = packet.Text.Split(" ")[3].Trim();
                                        var row = packet.Text.Split(" ")[4].Trim();
                                        var newAreaId = Int16.Parse(areaId);
                                        var newCol = Int16.Parse(col);
                                        var newRow = Int16.Parse(row);

                                        if (newAreaId >= 0 && newAreaId < Areas.Count &&
                                            newRow >= 0 && newRow < Areas[newAreaId].Hexes.GetLength(0) &&
                                            newCol >= 0 && newCol < Areas[newAreaId].Hexes.GetLength(1))
                                        {
                                            var fromLoc = player.Loc;
                                            var toLoc = new Loc()
                                            {
                                                AreaID = newAreaId,
                                                X = newCol,
                                                Y = newRow,
                                            };

                                            EntityTeleport(tpTarget, toLoc);
                                        
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
                            sb.Append("List of players:");
                            foreach (var p in GetAllPCs())
                            {
                                sb.Append(Environment.NewLine);
                                sb.Append(p.GetDescription(false, true));
                            }
                            SendPlayerStatus(player.ID, sb.ToString(), true);
                        }
                        else if (packet.Text == "loc")
                        {
                            player = FindPC(playerId);
                            string loc = "Your Location: Area=" + 
                                player.Loc.AreaID.ToString() + " X=" +
                                player.Loc.X.ToString() + " Y=" +
                                player.Loc.Y.ToString();
                            SendPlayerStatus(player.ID, loc, true);
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
                            if (EntityMove(player, packet.Text))
                            {
                                SendPlayerStatus(player.ID, "", true);
                            }
                        }
                        break;

                    case ActionType.Say:
                        SayMessage(player.FullName + " says, \"" + packet.Text + "\"",
                            player.Loc.AreaID, player.Loc.X, player.Loc.Y);
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
                        else if(Areas[player.Loc.AreaID].Hexes[player.Loc.Y, player.Loc.X].NoCombat &&
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
                                        .Hexes[player.Loc.Y, player.Loc.X].NPCs.Find(n => n.Name == packet.Text);

                                    //if (target != null)
                                    //{
                                    //    target = Areas[player.Loc.AreaID]
                                    //        .Hexes[player.Loc.Y, player.Loc.X].PCs.Find(n => n.Name == packet.Text);
                                    //}
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

        public Loc MapTileGridToLoc(Loc loc, int row, int col, bool checkSolid = true)
        {
            var newLoc = loc.Clone();

            if (row < 3)
                newLoc.Y -= 3 - row;
            if (col < 3)
                newLoc.X -= 3 - col;
            if (row > 3)
                newLoc.Y += row - 3;
            if (col > 3)
                newLoc.X += col - 3;

            if (newLoc.Y >= 0 && newLoc.X >= 0 &&
                newLoc.Y < Areas[loc.AreaID].Hexes.GetLength(0) &&
                newLoc.X < Areas[loc.AreaID].Hexes.GetLength(1))
            {
                if (checkSolid)
                {
                    if (Areas[newLoc.AreaID].Hexes[newLoc.Y, newLoc.X].Solid)
                    {
                        return null;
                    }
                }
                return newLoc;
            }

            return null;
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
                lock(Players)
                {
                    Areas[entity.Loc.AreaID]
                    .Hexes[entity.Loc.Y, entity.Loc.X]
                        .PCs.Add((PC)entity);
                }
                TotalPCs++;
                PCLogins++;
            }
            else if (entity is NPC)
            {
                lock(NPCs)
                {
                    Areas[entity.Loc.AreaID]
                        .Hexes[entity.Loc.Y, entity.Loc.X]
                        .NPCs.Add((NPC)entity);
                }
                TotalNPCs++;
            }
        }

        public Entity FindEntity(string id, string name = "")
        {
            foreach (Area area in Areas)
            {
                foreach (Hex hex in area.Hexes)
                {
                    lock(hex.PCs)
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

        public PC FindPC(string id, string name = "")
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

        public NPC FindNPC(string id, string name = "")
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

        public Tile[,] GetVisibleTiles(Entity e)
        {
            Tile[,] tiles = 
                new Tile[Constants.VisibleTilesHeight, Constants.VisibleTilesWidth];

            int y = e.Loc.Y - Constants.VisibleTilesOffset;
            int x = e.Loc.X - Constants.VisibleTilesOffset;

            for (int row = 0; row < Constants.VisibleTilesHeight; row++)
            {
                for (int col = 0; col < Constants.VisibleTilesWidth; col++)
                {
                    try
                    {
                        // Only add tiles within the area hex bounds
                        if (x >= 0 && y >= 0 &&
                            x < Areas[e.Loc.AreaID].Width &&
                            y < Areas[e.Loc.AreaID].Height)
                        {
                            tiles[row, col] = Areas[e.Loc.AreaID].Hexes[y, x].Tile;
                        }
                        else
                        {
                            // Return inaccessible blank tiles
                            tiles[row, col] = new Tile()
                            {
                                
                            };
                        }
                        x++;
                    }
                    catch
                    {
                    }
                }

                y++;
                x = e.Loc.X - Constants.VisibleTilesOffset;
            }

            return tiles;
        }

        public void SendPlayerCommand(string playerId, ActionType type, string text = "")
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

        public void SendPlayerMessage(string playerId, string text = "", string fromId = "")
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
            string text = "", bool hexDescription = false, string skipId = "")
        {
            var hex = Areas[loc.AreaID].Hexes[loc.Y, loc.X];

            foreach (var pc in hex.PCs)
            {
                if (pc.ID != skipId)
                {
                    SendPlayerStatus(pc.ID, text, hexDescription);
                }
            }
        }

        public void SendPlayerStatus(string playerId, string text = "", 
            bool hexDescription = false)
        {
            var player = FindPC(playerId);

            if (player != null)
            {
                var hex = Areas[player.Loc.AreaID].Hexes[player.Loc.Y, player.Loc.X];

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
                    NPCs = npcs,
                    PCs = players,
                    Items = items,
                    Tiles = GetVisibleTiles(player),
                };

                if (hexDescription)
                {
                    packet.Text = hex.GetDescription(true) + text;
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
                    lock(hex.PCs)
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

        public PC AddPlayer(string playerId = "", int partyId = 1, string playerName = "",
            Connection conn = null)
        {
            if (!Running)
            {
                return null;
            }

            PC player = new PC();

            try
            {
                if (!String.IsNullOrEmpty(playerId))
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

                if (!String.IsNullOrEmpty(player.MainHandID))
                {
                    var weapon = Data.LoadItems()
                        .Where(i => i.ID == player.MainHandID).Single();

                    if (weapon != null)
                        player.MainHand = weapon;
                }

                AddEntity(player);
                return player;
            }
            catch(Exception ex) 
            {
                AddEntity(player);
                return player;
            }
        }

        public void RemoveNPC(NPC e)
        {
            var hex = 
                Areas[e.Loc.AreaID].Hexes[e.Loc.Y, e.Loc.X];

            lock(hex.NPCs)
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

        public void RemovePC(string playerId = "")
        {
            foreach (Area area in Areas)
            {
                foreach (Hex hex in area.Hexes)
                {
                    lock(hex.PCs)
                    {
                        for (int i = 0; i < hex.PCs.Count; i++)
                        {
                            if (playerId == "" || hex.PCs[i].ID == playerId)
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

        public bool EntityTeleport(Entity entity, Loc loc)
        {
            bool moved = false;

            try
            {
                var fromHex =
                    Areas[entity.Loc.AreaID]
                        .Hexes[entity.Loc.Y, entity.Loc.X];

                var toHex =
                    Areas[loc.AreaID].Hexes[loc.Y, loc.X];

                lock (fromHex)
                {
                    if (entity is PC)
                    {
                        fromHex.PCs.Remove(entity as PC);
                    }
                    else if (entity is NPC)
                    {
                        fromHex.NPCs.Remove(entity as NPC);
                    }

                    entity.Loc = loc;
                }

                lock (toHex)
                {
                    if (entity is PC)
                    {
                        toHex.PCs.Add(entity as PC);
                    }
                    else if (entity is NPC)
                    {
                        toHex.NPCs.Add(entity as NPC);
                    }
                }

                moved = true;
            }
            catch { } 
            
            return moved;
        }

        public bool EntityMove(Entity entity, string direction)
        {
            bool moved = false;

            Loc loc = null;

            MoveDirection md = MoveDirection.South;

            if (entity.State == StateType.Stunned ||
                entity.State == StateType.Dead)
            {
                return moved;
            }

            var hex = Areas[entity.Loc.AreaID]
                .Hexes[entity.Loc.Y, entity.Loc.X];

            if (direction.StartsWith("move "))
            {
                try
                {
                    // Check if move to location is valid
                    string row = direction.Split(" ")[1];
                    string col = direction.Split(" ")[2];

                    loc = MapTileGridToLoc(entity.Loc,
                        Int16.Parse(row), Int16.Parse(col));

                    try
                    {
                        if (loc != null)
                        {
                            // If new location has a teleport, move there
                            var newHex = Areas[loc.AreaID]
                                .Hexes[loc.Y, loc.X];

                            if (newHex.Teleport != null)
                            {
                                loc = newHex.Teleport;
                                moved = true;
                            }
                        }
                    }
                    catch { }

                    moved = true;
                }
                catch { }
            }

            // Update only if movement was successful
            if (moved && loc != null)
            {
                var packet =
                    new Packet()
                    {
                        ID = entity.ID,
                        ActionType = ActionType.Movement,
                        MoveDirection = md,
                    };

                moved = EntityTeleport(entity, loc);

                if (entity is PC)
                {
                    var player = entity as PC;

                    SendPlayerStatusToHex(player.Loc, "", false, player.ID);
                    SendPlayerStatus(player.ID);

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

                    var newHex = Areas[loc.AreaID].Hexes[loc.Y, loc.X];

                    foreach (var p in newHex.PCs)
                    {
                        WritePacket(p.Conn,
                            new Packet()
                            {
                                ID = npc.ID,
                                ActionType = ActionType.Movement,
                                MoveDirection = md,
                            });
                    }
                }
            }
            else
            {
                //SendPlayerMessage(entity.ID, "The way is shut.");
            }

            return moved;
        }

        public List<NPC> GetEncounterNPCs(EncounterType type, int maximum = 99, string npcId = "")
        {
            List<NPC> encounter = new List<NPC>();

            if (!String.IsNullOrEmpty(npcId))
            {
                encounter.Add(NPCs
                    .Where(npc => npc.ID == npcId).Single().Clone());
            }
            else
            {
                var group = NPCs.Where(npc => npc.EncounterClass == type).Take(maximum);

                foreach (NPC npc in group.ToList())
                {
                    //var guy = npc.Clone();
                    //lock(EntityCount)
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

        public void SayMessage(string message, int areaId, int X, int Y)
        {
            foreach (PC player in Areas[areaId].Hexes[Y, X].PCs)
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
