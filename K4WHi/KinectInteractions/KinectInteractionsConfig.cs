using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K4WHi.KinectInteractions
{
    public class KinectInteractionsConfig
    {
        public void Config(object sender, KinectChangedEventArgs args)
        {
            bool error = false;

            if (args.OldSensor != null)
            {
                try
                {
                    args.OldSensor.DepthStream.Disable();
                    args.OldSensor.SkeletonStream.Disable();
                }
                catch (Exception e)
                {
                    error = true;
                }
            }

            if (args.NewSensor == null)
            {
                return;
            }

            try
            {
                args.NewSensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                args.NewSensor.SkeletonStream.Enable();
                try
                {
                    args.NewSensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
                    args.NewSensor.DepthStream.Range = DepthRange.Near;
                    args.NewSensor.SkeletonStream.EnableTrackingInNearRange = true;
                }
                catch (InvalidOperationException)
                {
                    args.NewSensor.DepthStream.Range = DepthRange.Default;
                    args.NewSensor.SkeletonStream.EnableTrackingInNearRange = false;
                }
            }
            catch (InvalidOperationException)
            {
                error = true;
            }
        }
    }
}