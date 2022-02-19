using Game.Core;

namespace Game.Realm
{
    public class RealmManager : Thing
    {
        #region Fields

        public bool Running { get; private set; }
        public int PulseRate { get; set; }
        public int RoundDuration { get; set; }
        public long Round { get; private set; }
        public int EntityCount = 1;

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

        public RealmManager(int id, string name, int roundDuration = 2000, int pulseRate = 2000)
        {
            ID = id;
            Name = name;
            RoundDuration = roundDuration;
            PulseRate = pulseRate;
            Round = 0;
        }

        private void ClearAllData()
        {
            EntityCount = 1;
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
            //Players = Data.LoadPlayers();
        }

        public void Start()
        {
            Combat = new CombatManager(this);

            ClearAllData();
            LoadAllData();

            SpawnNPCs();

            Running = true;
        }

        public void Stop()
        {
            RemovePC(0); // Remove all players

            Running = false;
        }

        public void SpawnNPCs()
        {
            foreach (var e in GetEncounterNPCs(EncounterType.Insect, 4).ToList())
            {
                e.Loc.HexID = 2;
                Areas[0].Hexes[1].NPCs.Add(e);
            }

            foreach (var e in GetEncounterNPCs(EncounterType.Undead, 4).ToList())
            {
                e.Loc.HexID = 3;
                Areas[0].Hexes[2].NPCs.Add(e);
            }

            foreach (var e in GetEncounterNPCs(EncounterType.RareUndead, 3).ToList())
            {
                e.Loc.HexID = 4;
                Areas[0].Hexes[3].NPCs.Add(e);
            }

            foreach (var e in GetEncounterNPCs(EncounterType.Common, 5).ToList())
            {
                e.Loc.HexID = 8;
                Areas[0].Hexes[7].NPCs.Add(e);
            }

            foreach (var e in GetEncounterNPCs(EncounterType.Animal, 8).ToList())
            {
                e.Loc.HexID = 8;
                Areas[0].Hexes[7].NPCs.Add(e);
            }

            foreach (var e in GetEncounterNPCs(EncounterType.VeryRare, 5).ToList())
            {
                e.Loc.HexID = 10;
                Areas[0].Hexes[9].NPCs.Add(e);
            }

            foreach (var e in GetEncounterNPCs(EncounterType.Demonic, 5).ToList())
            {
                e.Loc.HexID = 11;
                Areas[0].Hexes[10].NPCs.Add(e);
            }

            foreach (var e in GetEncounterNPCs(EncounterType.DragonKind, 8).ToList())
            {
                e.Loc.HexID = 5;
                Areas[0].Hexes[4].NPCs.Add(e);
            }

            //var minotaur = GetEncounterNPCs(EncounterType.VeryRare, 5, 9).Single();
            //minotaur.Loc.HexID = 12;
            //Areas[0].Hexes[11].NPCs.Add(minotaur);

            //var dragon = GetEncounterNPCs(EncounterType.DragonKind, 1).Single();
            //dragon.Loc.HexID = 5;
            //Areas[0].Hexes[4].NPCs.Add(dragon);

            var demogorgon = GetEncounterNPCs(EncounterType.Unique, 1, 1).Single();
            demogorgon.Loc.HexID = 9;
            Areas[0].Hexes[8].NPCs.Add(demogorgon);
        }

        public void ProcessEvents()
        {
            try
            {
                foreach (Area area in Areas)
                {
                    foreach (Hex hex in area.Hexes)
                    {
                        lock (hex.NPCs)
                        {
                            for (int i = 0; i < hex.NPCs.Count; i++)
                            {
                                var npc = hex.NPCs[i];

                                if (npc.State != StateType.Dead && (npc.State == StateType.Combat ||
                                    npc.Mood >= MoodType.Aggressive))
                                {
                                    var target = FindPlayer(npc.LastAttackerID);

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

                                if (npc.State == StateType.Dead)
                                {
                                    if (DateTime.Now.Subtract(npc.DeathTime).Seconds >=
                                        Constants.NPCCorpseDecay)
                                    {
                                        //RemoveEntity(npc);
                                        // Resurrect for now
                                        npc.State = StateType.Normal;
                                        npc.HitPoints = npc.MaxHitPoints;
                                        npc.ManaPoints = npc.MaxManaPoints;
                                        npc.Mood = MoodType.Normal;
                                        npc.Followed = 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch//(Exception ex)
            {
                //TODO: Exception logging
            }
        }

        #endregion

        #region Communications

        public string HandlePacket(Packet packet, int playerId)
        {
            PC player = null;
            string result = String.Empty;

            switch (packet.ActionType)
            {
                case ActionType.Join:
                    AddPlayer(playerId);
                    break;

                case ActionType.Exit:
                    RemovePC(playerId);
                    break;

                case ActionType.Command:
                    if (packet.Text.ToLower().Trim() == "look")
                    {
                        SendPlayerStatus(playerId, "", true);
                    }
                    else if (packet.Text.ToLower().Trim() == "revive")
                    {
                        player = FindPlayer(playerId);

                        if (player != null)
                        {
                            player.State = StateType.Normal;
                            player.HitPoints = player.MaxHitPoints;

                            SendPlayerStatus(playerId);

                            SayMessage(player.FullName + " is revived!",
                                player.Loc.AreaID, player.Loc.HexID);
                        }
                    }
                    else if (packet.Text.ToLower().Trim() == "hide")
                    {
                        player = FindPlayer(playerId);

                        if (player != null)
                        {
                            if (player.State == StateType.Invisible)
                            {
                                player.State = StateType.Normal;
                                SendPlayerStatus(playerId, "You emerge from the shadows.");
                            }
                            else
                            {
                                if (player.AttemptToHide())
                                {
                                    player.State = StateType.Invisible;
                                    SendPlayerStatus(playerId, "You hide in the shadows.");
                                }
                                else
                                {
                                    player.State = StateType.Normal;
                                    SendPlayerStatus(playerId, "You fail to hide in the shadows.");
                                }
                            }
                        }
                    }
                    break;

                case ActionType.Movement:
                    player = FindPlayer(playerId);

                    if (player != null)
                    {
                        if (player.State != StateType.Dead && player.State != StateType.Stunned)
                        {
                            Move(player, packet.Text);
                        }
                    }
                    break;

                case ActionType.Say:
                    player = FindPlayer(playerId);

                    if (player != null)
                    {
                        SayMessage(player.FullName + " says, \"" + packet.Text + "\"",
                            player.Loc.AreaID, player.Loc.HexID);
                    }
                    break;

                case ActionType.Yell:
                    player = FindPlayer(playerId);

                    if (player != null)
                    {
                        YellMessage(player.FullName + " yells, \"" + packet.Text + "\"",
                            player.Loc.AreaID);
                    }

                    break;

                case ActionType.Broadcast:
                    if (player != null)
                    {
                        BroadcastMessage(player.FullName ?? String.Empty + ":" + packet.Text);
                    }
                    else
                    {
                        BroadcastMessage(packet.Text);
                    }
                    break;

                case ActionType.Damage:
                    try
                    {
                        player = FindPlayer(playerId);

                        if (player.State != StateType.Dead && player.State != StateType.Stunned)
                        {
                            NPC target = null;
                            try
                            {
                                target =
                                (NPC)Areas[player.Loc.AreaID].Hexes[player.Loc.HexID - 1]
                                    .NPCs.Where(n => n.Name.ToLower().StartsWith(packet.Text)).Single();
                            }
                            catch { }

                            if (player != null && target != null && (player.Loc.HexID == target.Loc.HexID))
                            {
                                lock (Areas[player.Loc.AreaID].Hexes[player.Loc.HexID - 1].NPCs)
                                {
                                    result = Combat.Attack(player, target);
                                }
                            }
                            else
                            {
                                SendPlayerStatus(playerId, "Your target is out of reach.");
                            }
                        }
                    }
                    catch//(Exception ex)
                    {
                        return "Target was not found.";
                    }

                    break;
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
                        .Players.Add((PC)entity);
                }
            }
            else if (entity is NPC)
            {
                lock (NPCs)
                {
                    Areas[entity.Loc.AreaID]
                        .Hexes[entity.Loc.HexID - 1]
                        .NPCs.Add((NPC)entity);
                }
            }
        }

        public PC FindPlayer(int playerId)
        {
            foreach (Area area in Areas)
            {
                foreach (Hex hex in area.Hexes)
                {
                    lock (hex.Players)
                    {
                        foreach (PC player in hex.Players)
                        {
                            if (player.ID == playerId)
                            {
                                return player;
                            }
                        }
                    }
                }
            }

            return null;
        }

        public void SendPlayerStatus(int playerId, string text = "", bool includeDescription = false)
        {
            var player = FindPlayer(playerId);

            if (player != null)
            {
                var hex = Areas[player.Loc.AreaID].Hexes[player.Loc.HexID - 1];

                var npcs = new Dictionary<int, Stats>();
                foreach (var npc in hex.NPCs)
                {
                    if (npc.State != StateType.Dead)
                    {
                        npcs.Add(npc.ID,
                        new Stats()
                        {
                            ID = npc.ID,
                            Name = npc.Name,
                            Facing = npc.Facing,
                            HPs = npc.HitPoints,
                            MaxHPs = npc.MaxHitPoints,
                            MPs = npc.ManaPoints,
                            MaxMPs = npc.MaxManaPoints
                        });
                    }
                }

                var players = new Dictionary<int, Stats>();
                foreach (var p in hex.Players)
                {
                    players.Add(p.ID,
                        new Stats()
                        {
                            ID = p.ID,
                            Name = p.Name,
                            Facing = p.Facing,
                            Level = p.Level,
                            Experience = p.Experience,
                            Gold = p.Gold,
                            HPs = p.HitPoints,
                            MaxHPs = p.MaxHitPoints,
                            MPs = p.ManaPoints,
                            MaxMPs = p.MaxManaPoints
                        });
                }

                var items = new Dictionary<int, string>();
                foreach (var p in hex.Items)
                {
                    if (!items.Keys.Contains(p.ID))
                    {
                        items.Add(p.ID, p.Name);
                    }
                }

                var packet = new Packet()
                {
                    ActionType = ActionType.Status,
                    Health = new Stats()
                    {
                        ID = player.ID,
                        Name = player.Name,
                        Facing = player.Facing,
                        Level = player.Level,
                        Experience = player.Experience,
                        Gold = player.Gold,
                        HPs = player.HitPoints,
                        MaxHPs = player.MaxHitPoints,
                        MPs = player.ManaPoints,
                        MaxMPs = player.MaxManaPoints,
                    },

                    Tile = hex.Tile,
                    NPCs = npcs,
                    Players = players,
                    Items = items,
                };

                if (includeDescription)
                {
                    packet.Text = hex.GetDescription(playerId);
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
                    lock (hex.Players)
                    {
                        foreach (PC player in hex.Players)
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

                SendPlayerStatus(player.ID, "Welcome", true);

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
                    lock (hex.Players)
                    {
                        for (int i = 0; i < hex.Players.Count; i++)
                        {
                            if (playerId == 0 || hex.Players[i].ID == playerId)
                            {
                                var player = (PC)hex.Players[i];

                                if (player.Conn != null)
                                    player.Conn.Disconnect();

                                hex.Players.RemoveAt(i);
                            }
                        }
                    }
                }
            }
        }

        public void Move(Entity entity, string direction, int areaId = -1, int hexId = -1)
        {
            bool moved = false;

            var hex = Areas[entity.Loc.AreaID].Hexes[entity.Loc.HexID - 1];

            MoveDirection md = MoveDirection.North;

            if (areaId != -1 && hexId != -1)
            {
                moved = true;
            }
            else
            {
                if (areaId == -1)
                    areaId = 0;

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
                        if (hex.Tile.Up > 0)
                        {
                            hexId = hex.Tile.Up;
                            md = MoveDirection.Up;
                            moved = true;
                        }
                        else if (hex.Tile.Up < 0)
                        {
                            var status = entity.FullName + " has left the realm.";

                            WritePacket(FindPlayer(entity.ID).Conn, new Packet()
                            {
                                ActionType = ActionType.Exit,
                                ID = entity.ID,
                                Text = status,
                            });

                            RemovePC(entity.ID);
                            BroadcastMessage(status);
                        }
                        break;
                    case "down":
                        if (hex.Tile.Down > 0)
                        {
                            hexId = hex.Tile.Down;
                            md = MoveDirection.Down;
                            moved = true;
                        }
                        break;
                }
            }

            if (moved)
            {
                string leaving = entity.FullName + " moves " + direction.ToString() + ".";

                var packet =
                    new Packet()
                    {
                        ID = entity.ID,
                        ActionType = ActionType.Movement,
                        MoveDirection = md,
                        Text = leaving,
                    };

                if (entity is PC)
                {
                    var player = entity as PC;

                    lock (hex.Players)
                    {
                        var playerIndex =
                            hex.Players.FindIndex(p => p.ID == entity.ID);

                        hex.Players.RemoveAt(playerIndex);

                        foreach (var p in hex.Players)
                        {
                            WritePacket(p.Conn, packet);
                        }
                    }

                    var newHex = Areas[areaId].Hexes[hexId - 1];

                    string arriving = player.FullName + " enters the area.";

                    lock (newHex.Players)
                    {
                        foreach (var p in newHex.Players)
                        {
                            WritePacket(p.Conn,
                                new Packet()
                                {
                                    ID = player.ID,
                                    ActionType = ActionType.Movement,
                                    MoveDirection = md,
                                    Text = arriving
                                });
                        }

                        newHex.Players.Add(player);
                        player.Loc.HexID = newHex.ID;
                    }

                    //for(int i= 0; i< hex.NPCs.Count; i++)
                    //{
                    //    hex.NPCs[i].SendPacket(this, packet, player);
                    //}
                    //TODO: Debug
                    try
                    {
                        foreach (var npc in hex.NPCs)
                        {
                            npc.SendPacket(this, packet, player);
                        }
                    }
                    catch { }

                    SendPlayerStatus(entity.ID, arriving, true);
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

                        foreach (var p in hex.Players)
                        {
                            WritePacket(p.Conn, packet);
                        }

                        newHex.NPCs.Add(npc);
                        npc.Loc.HexID = newHex.ID;
                    }

                    string arriving = npc.FullName + " enters the area.";

                    lock (newHex.Players)
                    {
                        foreach (var p in newHex.Players)
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

                foreach (NPC monster in group.ToList())
                {
                    var guy = monster.Clone();
                    //lock (EntityCount)
                    {
                        guy.ID = EntityCount++;
                    }

                    encounter.Add(monster.Clone());
                }
            }

            return encounter;
        }

        #endregion

        #region Logging

        public void BroadcastMessage(string message)
        {
            PublishGameEvent(new Packet()
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
            foreach (PC player in Areas[areaId].Hexes[hexId - 1].Players)
            {
                WritePacket(player.Conn,
                    new Packet() { ActionType = ActionType.Text, Text = message });
            }
        }

        public void YellMessage(string message, int areaId)
        {
            foreach (Hex hex in Areas[areaId].Hexes)
            {
                foreach (PC player in hex.Players)
                {
                    WritePacket(player.Conn,
                        new Packet() { ActionType = ActionType.Text, Text = message });
                }
            }
        }

        private void PublishGameEvent(Packet packet)
        {
            foreach (Area area in Areas)
            {
                foreach (Hex hex in area.Hexes)
                {
                    foreach (PC player in hex.Players)
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
