﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core;

namespace Game.Realm
{
    public class Skill : Effect
    {
        public override int FizzleChance { get; set; }

        public Skill()
        {

        }
    }
}
