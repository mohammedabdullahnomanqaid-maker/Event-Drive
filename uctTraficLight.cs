using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Publisher_Subscriber_Design_Pattern
{
    public partial class uctTraficLight : UserControl
    {
        public enum enLight { eRed=0,eOrange=1,eGreen=3};
       private enLight _enCurrentLight = enLight.eRed;

        public class TraficLightEventArgs : EventArgs
        {
            public enLight CurrentLight { get; }
            public int LightDuration { get; }

            public TraficLightEventArgs(enLight CurrentLight,int LightDuration)
            {
                this.CurrentLight = CurrentLight;
                this.LightDuration = LightDuration;
            }

        }

        public event EventHandler<TraficLightEventArgs> RedLightOn;
        public event EventHandler<TraficLightEventArgs> RedLightOff;
        public event EventHandler<TraficLightEventArgs> GreenLightOn;
        public event EventHandler<TraficLightEventArgs> GreenLightOff;
        public event EventHandler<TraficLightEventArgs> OrangeLightOn;

        public void RaiseRedLightOn()
        {
            RaiseRedLightOn(new TraficLightEventArgs(enLight.eRed, RedTime));
        }
        protected virtual void RaiseRedLightOn(TraficLightEventArgs e)
        {
            RedLightOn?.Invoke(this, e);
        }

        public void RaiseRedLightOff()
        {
            RaiseRedLightOff(new TraficLightEventArgs(enLight.eOrange, OrangeTime));
        }

        protected virtual void RaiseRedLightOff(TraficLightEventArgs e)
        {
            RedLightOff?.Invoke(this, e);
        }

        public void RaiseGreenLightOff()
        {
            RaiseGreenLightOff(new TraficLightEventArgs(enLight.eOrange,OrangeTime));
        }

        protected virtual void RaiseGreenLightOff(TraficLightEventArgs e)
        {
            GreenLightOff?.Invoke(this, e);
        }

        public void RaiseGreenLightOn()
        {
            RaiseGreenLightOn(new TraficLightEventArgs(enLight.eGreen, GreenTime));
        }

        protected virtual void RaiseGreenLightOn(TraficLightEventArgs e)
        {
            GreenLightOn?.Invoke(this, e);
        }
        public void RaiseOrangeLightOn()
        {
            RaiseOrangeLightOn(new TraficLightEventArgs(enLight.eOrange, OrangeTime));
        }

        protected virtual void RaiseOrangeLightOn(TraficLightEventArgs e)
        {
            OrangeLightOn?.Invoke(this, e);
        }

        public enLight CurrentLight
        {
            get
            {
                return _enCurrentLight;
            }

            set
            {
                _enCurrentLight = value;

                switch (_enCurrentLight)
                {
                    case enLight.eRed:
                        picLight.Image = Properties.Resources.LightRed;
                        break;

                    case enLight.eGreen:
                        picLight.Image = Properties.Resources.LightGreen;
                        break;

                    case enLight.eOrange:
                        picLight.Image = Properties.Resources.LightOrange;
                        break;

                    default:
                        picLight.Image = Properties.Resources.LightRed;
                        break;
                }
            }
        }

        public uctTraficLight()
        {
            InitializeComponent();
        }

        private int _RedTime = 10;
        private int _OrangeTime = 3;
        private int _GreenTime = 10;

        private int _CurrentCountDownValue;
        private enLight _LightAfterOrangeGreenOrRed;

        public int RedTime
        {
            get
            {
                return _RedTime;
            }
            set
            {
                value = _RedTime;
            }
        }

        public int OrangeTime
        {
            get
            {
                return _OrangeTime;
            }
            set
            {
                value = _OrangeTime;
            }
        }

        public int GreenTime
        {
            get
            {
                return _GreenTime;
            }
            set
            {
                value = GreenTime;
            }
        }

        public int GetCurrentTime()
        {
            switch (_enCurrentLight)
            {
                case enLight.eRed:
                    return RedTime;
                case enLight.eGreen:
                    return GreenTime;
                case enLight.eOrange:
                    return OrangeTime;
                default:
                    return RedTime;
            }
        }

        public void Start()
        {
            _CurrentCountDownValue = GetCurrentTime();
            LightTimer.Start();
        }

        public void Stop()
        {
            LightTimer.Stop();
        }
        private void _ChangeLight()
        {
            switch (_enCurrentLight)
            {
                case enLight.eRed:
                    _LightAfterOrangeGreenOrRed = enLight.eGreen;
                    CurrentLight = enLight.eOrange;
                    _CurrentCountDownValue = OrangeTime;

                    RaiseOrangeLightOn();
                    break;

                case enLight.eOrange:
                    if (_LightAfterOrangeGreenOrRed == enLight.eGreen)
                    {
                        CurrentLight = enLight.eGreen;
                        _CurrentCountDownValue = GreenTime;

                        RaiseGreenLightOn();
                    }
                    else
                    {
                        CurrentLight = enLight.eRed;
                        _CurrentCountDownValue = RedTime;
                        RaiseRedLightOn();
                    }

                    break;

                case enLight.eGreen:
                    _LightAfterOrangeGreenOrRed = enLight.eRed;
                    CurrentLight = enLight.eOrange;
                    _CurrentCountDownValue = OrangeTime;
                    RaiseOrangeLightOn();

                    break;

                default:
                    picLight.Image = Properties.Resources.LightRed;
                    break;
            }
        }

        private void LightTimer_Tick(object sender, EventArgs e)
        {
            if (_CurrentCountDownValue == 0)
            {
                _ChangeLight();
            }
            else
            {
                --_CurrentCountDownValue;
            }
        }
    }
}
