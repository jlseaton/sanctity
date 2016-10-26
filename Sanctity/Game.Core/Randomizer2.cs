using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core
{
    /// <summary>
    /// Randomizer circa 1995 :)
    /// </summary>
    public class Randomizer2
    {
        Randomizer2()
        {

        }

        // Random number generators.
        public float rand()
        {
            long a = 100001;

            a = (a * 125) % 2796203;
            return (float) a / 2796203;
        }

        public float Random()
        {
            float f;
            f = ran3();

            if (f > .5) return ran1();
            else
                return ran2();
        }

        public float ran1()
        {
            long a = 100001;

            a = (a * 125) % 2796203;
            return (float)a / 32749;
        }

        public float ran2()
        {
            long a = 1;

            a = (a * 32719 + 3) % 32749;
            return (float)a / 32749;
        }

        public float ran3()
        {
            long a = 203;

            //a = a (*10001 + 3) % 1717;
            return (float)a / 1717;
        }
    }
}
