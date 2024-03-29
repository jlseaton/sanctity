﻿using Game.Core;

namespace Game.Realm
{
    public class PC : Entity
    {
        public uint UserID { get; set; }
        public string? AccountName { get; set; }
        public AccountType AccountType { get; set; }
        public string? Token { get; private set; }
        public string? Secret { get; private set; }
        public bool Authenticated { get; private set; }
        public DateTime Created { get; set; }
        public DateTime LastActivity { get; set; }
        public bool PVP { get; set; }
        public new string FullName
        {
            get
            {
                if (!String.IsNullOrEmpty(Article))
                {
                    return Article + " " + Name;
                }

                if (!String.IsNullOrEmpty(Surname))
                {
                    return Name + " " + Surname;
                }

                return Name;
            }
        }

        public Connection? Conn { get; set; }

        public PC() : base()
        {
            Created = DateTime.Now;
            PVP = true;
            Type = EntityType.PC;
            Gender = GenderType.Male;
            Secret = Constants.PCDefaultSecret;
            CorpseDecayRate = Constants.PCDefaultCorpseDecay;
        }

        public bool Authenticate(string secret)
        {
            if (secret == Secret)
            {
                Authenticated = true;
            }
            return Authenticated;
        }

        public void Disconnect()
        {
            if (Conn != null)
            {
                Conn.Disconnect();
            }
        }
    }
}
