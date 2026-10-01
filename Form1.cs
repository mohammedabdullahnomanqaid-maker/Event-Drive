using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using WMPLib;

namespace Publisher_Subscriber_Design_Pattern
{
    public partial class Form1 : Form
    {

        class Publisher :EventArgs
        {
         
            public Publisher()
            {
            
            }
        }

        class Order
        {
            public event EventHandler<Publisher> OnOrderReady;

            public  void RaiseEvent()
            {
                OnOrderReady(this, new Publisher());
            }
        }

        class Reverse
        {

            private PictureBox _pic = new PictureBox();
            public void Subscribe(Order order)
            {
                order.OnOrderReady += HandleSender;
            }

            public void UnSubscribe(Order order)
            {
                order.OnOrderReady -= HandleSender;
            }
            private void HandleSender(object sender,Publisher e)
            {
              
                _pic.Left += 2;
            }

            public void SetPic(PictureBox pic)
            {
                _pic = pic;
            }
        }

        class Drive
        {
            private PictureBox _pic = new PictureBox();
            public void Subscribe(Order order)
            {
                order.OnOrderReady += HandleSender;
            }

            public void UnSubscribe(Order order)
            {
                order.OnOrderReady -= HandleSender;
            }
            private void HandleSender(object sender, Publisher e)
            {

                _pic.Left -= 2;
            }

            public void SetPic(PictureBox pic)
            {
                _pic = pic;
            }
        }

        class Up
        {
            private PictureBox _pic = new PictureBox();
            public void Subscribe(Order order)
            {
                order.OnOrderReady += HandleSender;
            }

            public void UnSubscribe(Order order)
            {
                order.OnOrderReady -= HandleSender;
            }
            private void HandleSender(object sender, Publisher e)
            {

                _pic.Top -= 3;
            }

            public void SetPic(PictureBox pic)
            {
                _pic = pic;
            }
        }

        class Down
        {
            private PictureBox _pic = new PictureBox();
            public void Subscribe(Order order)
            {
                order.OnOrderReady += HandleSender;
            }

            public void UnSubscribe(Order order)
            {
                order.OnOrderReady -= HandleSender;
            }
            private void HandleSender(object sender, Publisher e)
            {

                _pic.Top += 3;
            }

            public void SetPic(PictureBox pic)
            {
                _pic = pic;
            }
        }
        class Detain
        {
           
            public void Subscribe(Order order)
            {
                order.OnOrderReady += HandleSender;
            }

            public void UnSubscribe(Order order)
            {
                order.OnOrderReady -= HandleSender;
            }
            private void HandleSender(object sender, Publisher e)
            {
            
                Form frm = new frmDetaindCard();
                frm.ShowDialog();
                
            }
        }




        public Form1()
        {
            InitializeComponent();

          
        }
        Order order = new Order();
        Drive drive = new Drive();
        Reverse reverse = new Reverse();
        Detain detain = new Detain();

        private void Form1_Load(object sender, EventArgs e)
        {
            uctTraficLight1.Start();
            timer2.Enabled = true;

            drive.SetPic(picImage);

            reverse.SetPic(picImage);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {




            if (picJear.Tag == "R")
            {
                reverse.Subscribe(order);
                order.RaiseEvent();
                reverse.UnSubscribe(order);
            }
            else
            {
                drive.Subscribe(order);
                order.RaiseEvent();
                drive.UnSubscribe(order);
            }




        }

        private void btnUp_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if(picJear.Tag.ToString()=="D")
            {
                picJear.Image = Properties.Resources.PointOnR;
                picJear.Tag = "R";
            }
            else
            {
                picJear.Image = Properties.Resources.PointOnD;
                picJear.Tag = "D";
            }
        }

        bool IsPassed = false;

        private void timer2_Tick(object sender, EventArgs e)
        {

            if (IsPassRedLightTrafic())
                IsPassed = false;
                
        }


        private bool IsPassRedLightTrafic()
        {
            if (!IsPassed&&(uctTraficLight1.CurrentLight==uctTraficLight.enLight.eRed) && (picImage.Location.X > 785 && picImage.Location.X < 851))
            {
                timer2.Enabled = false;
                MusicOfDetained();
                Form frm = new frmDetaindCard();
                frm.ShowDialog();
                IsPassed = true;
                timer2.Enabled = true;
                return true;
            }
            return false;
        }


        WMPLib.WindowsMediaPlayer wmp = new WMPLib.WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer wmp2 = new WMPLib.WindowsMediaPlayer();
        private void button3_Click(object sender, EventArgs e)
        {
          
        }

        private void MusicOfDetained()
        {
            string path = Path.Combine(Path.GetTempPath(), "Screenshot_Sound.mp3");
            if (!File.Exists(path))
                File.WriteAllBytes(path, Properties.Resources.Screenshot_Sound);

            wmp2.URL = path;
            wmp2.controls.play();
        }

        private void picUp_Click(object sender, EventArgs e)
        {
            Up up = new Up();
            up.Subscribe(order);
            up.SetPic(picImage);
            order.RaiseEvent();
            up.UnSubscribe(order);
        }

        private void picDown_Click(object sender, EventArgs e)
        {
            Down down = new Down();
            down.Subscribe(order);
            down.SetPic(picImage);
            order.RaiseEvent();
            down.UnSubscribe(order);
        }

        private void picHorn_Click(object sender, EventArgs e)
        {

            string path = Path.Combine(Path.GetTempPath(), "Car_Horn.mp3");
            if (!File.Exists(path))
                File.WriteAllBytes(path, Properties.Resources.car_horn);

            wmp.URL = path;
            wmp.controls.play();
        }
    }
}
