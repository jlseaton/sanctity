using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core;

namespace Sanctity
{
    public interface INativeAPI
    {
        void PlaySound(string fileName);
        void PlayMusic(string fileName, bool loop = false);
    }
}
