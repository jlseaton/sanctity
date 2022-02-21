﻿using Game.Core;

namespace Game.Realm
{
    public class PC : Entity
    {
        public int UserID { get; set; }
        public string Token { get; private set; }
        public string Secret { get; private set; }
        public bool Authenticated { get; private set; }
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

        public Connection Conn { get; set; }

        public PC() : base()
        {
            Type = EntityType.PC;
            Gender = GenderType.Male;
            Secret = Constants.PCDefaultSecret;
        }

        public bool Authenticate(string secret)
        {
            if (secret == Secret)
            {
                Authenticated = true;
            }
            return Authenticated;
        }
    }
}
