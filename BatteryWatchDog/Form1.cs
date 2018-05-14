using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatteryWatchDog
{
    public partial class Form1 : Form
    {
        private BatteryInfo bi;
        private BeepThread st;

        public NotifyIcon icon;
        Icon ico;

        delegate void SetTextCallback(string s);

        public Form1()
        {
            InitializeComponent();

            //create notification icon
            ico = new Icon("watchdog_ico.ico");
            icon = new NotifyIcon();
            icon.Icon = ico;
            icon.Visible = true;

            //create context menu for icon
            ContextMenu cm = new ContextMenu();
            MenuItem infoMI = new MenuItem("BatteryWatchDog by Paulo Gliwa");
            MenuItem quitMI = new MenuItem("Quit");
            cm.MenuItems.Add(infoMI);
            cm.MenuItems.Add(quitMI);

            //add context menu to the icon
            icon.ContextMenu = cm;

            //menu item actions
            quitMI.Click += QuitMI_Click;

            st = new BeepThread();
            bi = new BatteryInfo(this, st);
            Thread batteryThread = new Thread(new ThreadStart(bi.start));
            Thread soundThread = new Thread(new ThreadStart(st.start));

            batteryThread.IsBackground = true; //stops when the form is closed
            batteryThread.Start();
            soundThread.IsBackground = true;
            soundThread.Start();
            
            this.Visible = false;

            Form f = new Form();
            f.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            f.ShowInTaskbar = false;

            this.Owner = f;
        }

        private void QuitMI_Click(object sender, EventArgs e)
        {
            icon.Dispose();
            ico.Dispose();
            this.Close();
        }

        public void set_text(string s)
        {
            if(txtBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(set_text);
                this.Invoke(d, new object[] { s });
            }
            else
            {
                this.txtBox.Text = s;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            txtBox.Text = bi.isCharging().ToString();
            st.Beep = !st.Beep;
        }
    }
}
