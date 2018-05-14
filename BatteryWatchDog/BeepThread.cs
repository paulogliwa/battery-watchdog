using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BatteryWatchDog
{
    class BeepThread
    {
        private bool beep;

        public bool Beep
        {
            get { return this.beep; }
            set { this.beep = value; }
        }

        public void start()
        {
            beep = false;
            while (true)
            {
                if (beep)
                    SystemSounds.Asterisk.Play();

                Thread.Sleep(1000);
            }
        }
    }
}
