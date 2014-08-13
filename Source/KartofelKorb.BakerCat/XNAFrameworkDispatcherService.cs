using System;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Xna.Framework;

namespace KartofelKorb.BakerCat
{
    /// <summary>
    /// http://msdn.microsoft.com/en-us/magazine/hh708760.aspx
    /// http://msdn.microsoft.com/library/ff842408.aspx
    /// </summary>
    public class XNAFrameworkDispatcherService : IApplicationService
    {
        public XNAFrameworkDispatcherService()
        {
            _frameworkDispatcherTimer = new DispatcherTimer();
            _frameworkDispatcherTimer.Interval = TimeSpan.FromTicks(333333);
            _frameworkDispatcherTimer.Tick += OnFrameworkDispatcherTimerTick;
            FrameworkDispatcher.Update();
        }

        void IApplicationService.StartService(ApplicationServiceContext context) 
        { 
            _frameworkDispatcherTimer.Start(); 
        }

        void IApplicationService.StopService() 
        { 
            _frameworkDispatcherTimer.Stop(); 
        }

        private DispatcherTimer _frameworkDispatcherTimer;

        private void OnFrameworkDispatcherTimerTick(object sender, EventArgs e) 
        { 
            FrameworkDispatcher.Update(); 
        }
    }
}
