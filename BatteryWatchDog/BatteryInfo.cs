using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatteryWatchDog
{
    class BatteryInfo
    {
        private PowerStatus ps = SystemInformation.PowerStatus;
        private bool charging;
        private Form1 f;
        private BeepThread bt;

        public BatteryInfo(Form1 f, BeepThread bt)
        {
            this.f = f;
            this.bt = bt;
        }

        public bool isCharging()
        {
            return charging;
        }

        public void start()
        {
            while(true)
            {
                if(ps.PowerLineStatus.Equals(PowerLineStatus.Online))
                {
                    charging = true;
                    f.set_text("Charging");
                    bt.Beep = false;
                }
                else
                {
                    this.charging = false;
                    f.set_text("Not charging");
                    bt.Beep = true;
                    f.icon.ShowBalloonTip(90, "Not charging!!!", "You should connect your laptop dumdum.", ToolTipIcon.Warning);
                }

                Thread.Sleep(100);
            }
        }
    }
}
