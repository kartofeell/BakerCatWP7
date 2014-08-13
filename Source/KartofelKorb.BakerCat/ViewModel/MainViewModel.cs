using GalaSoft.MvvmLight;
using System;
using System.Windows.Threading;

namespace KartofelKorb.BakerCat.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public const string TimePropertyName = "Time";
        private string _time = "0.0";
        public string Time
        {
            get
            {
                return _time;
            }

            set
            {
                if (_time == value)
                {
                    return;
                }

                var oldValue = _time;
                _time = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(TimePropertyName);

                // Update bindings and broadcast change using GalaSoft.MvvmLight.Messenging
                RaisePropertyChanged(TimePropertyName, oldValue, value, true);
            }
        }

        private readonly DispatcherTimer _timer;
        private DateTime _startTime;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                _timer = new DispatcherTimer();
                _timer.Interval = TimeSpan.FromMilliseconds(50);
                _timer.Tick += OnTimerTick;
            }
        }

        public void Start()
        {
            _startTime = DateTime.Now;
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            TimeSpan timeSpan = DateTime.Now - _startTime;
            Time = string.Format("{0:d2}:{1:d2}.{2:d2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);
        }

    }
}