using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace K4WHi.KinectInteractions
{
    public class KinectInteractionsConfig
    {

        private KinectRegion KinectRegion;
        private KinectUserViewer KinectUserViewer;

        public KinectInteractionsConfig(KinectRegion KinectRegion, KinectUserViewer KinectUserViewer)
        {
            this.KinectRegion = KinectRegion;
            this.KinectUserViewer = KinectUserViewer;
        }


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
                    args.NewSensor.SkeletonFrameReady += NewSensor_SkeletonFrameReady;
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

            if (!error)
            {
                this.KinectRegion.KinectSensor = args.NewSensor;
            }
        }

        void NewSensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs args)
        {
            var colors = new Dictionary<int, Color>();
            Skeleton[] skeletons;

            using (var frame = args.OpenSkeletonFrame())
            {
                if (frame == null)
                    return;

                skeletons = new Skeleton[frame.SkeletonArrayLength];
                frame.CopySkeletonDataTo(skeletons);
            }

            foreach (var skeleton in skeletons.Where(s => SkeletonTrackingState.Tracked.Equals(s.TrackingState)))
            {
                Random _rnd = new Random();
                var userId = skeleton.TrackingId;
                
                var crazyColors = Color.FromRgb((byte)_rnd.Next(255), (byte)_rnd.Next(255), (byte)_rnd.Next(255));

                colors.Add(userId, KinectRegion.PrimaryUserTrackingId == userId ?
                    crazyColors : 
                    Colors.Blue);
            }

            this.KinectUserViewer.UserColors = colors;
        }
    }
}