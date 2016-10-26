using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Game.Core;

[assembly: Xamarin.Forms.Dependency(typeof(Sanctity.WinPhone.NativeAPI))]

namespace Sanctity.WinPhone
{
    public class NativeAPI : INativeAPI
    {
        public NativeAPI()
        {

        }

        public void PlaySound(string fileName)
        {

        }

        public void PlayMusic(string fileName, bool loop = false)
        {

        }
    }
}
