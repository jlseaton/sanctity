﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core
{
    public class Tile
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int SoundID { get; set; }
        public int MusicID { get; set; }

        public string Text { get; set; }

        public int Tile1ID { get; set; }
        public int Tile2ID { get; set; }
        public int Tile3ID { get; set; }

        public int North { get; set; }
        public int South { get; set; }
        public int East { get; set; }
        public int West { get; set; }
        public int Up { get; set; }
        public int Down { get; set; }


        public int NorthTileID { get; set; }
        public int SouthTileID { get; set; }
        public int EastTileID { get; set; }
        public int WestTileID { get; set; }
        public int UpTileID { get; set; }
        public int DownTileID { get; set; }

        public string GetExits()
        {
            string exits = String.Empty;

            if (North > 0)
                exits += ", North";
            if (South > 0)
                exits += ", South";
            if (East > 0)
                exits += ", East";
            if (West > 0)
                exits += ", West";
            if (Up > 0 || Up == -1)
                exits += ", Up";
            if (Down > 0)
                exits += ", Down";

            if (String.IsNullOrEmpty(exits))
            {
                exits = "There are no apparent exits.";
            }
            else
            {
                exits = "You see these exits: " + exits.TrimStart(',').TrimEnd(',');
            }

            return exits;
        }
    }
}
