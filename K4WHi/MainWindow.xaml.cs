using K4WHi.KinectInteractions;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace K4WHi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string kinectStatus = "status";
        private KinectSensorChooser _sensorChooser;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            _sensorChooser = new KinectSensorChooser();
            _sensorChooser.KinectChanged += KinectChanged;
            _sensorChooser.PropertyChanged += PropertyChanged;

            KinectSensorChooser.KinectSensorChooser = _sensorChooser;
            _sensorChooser.Start();
        }

        void PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs args)
        {
            if (kinectStatus.Equals(args.PropertyName))
            {
                Debug.WriteLine("Status = " + _sensorChooser.Status);
            }
        }

        void KinectChanged(object sender, KinectChangedEventArgs args)
        {
            KinectInteractionsConfig interactionConfig = new KinectInteractionsConfig();
            interactionConfig.Config(sender, args);
            /**
            bool error = false;

            if (args.OldSensor != null)
            {
                try
                {
                    args.OldSensor.DepthStream.Disable();
                    args.OldSensor.SkeletonStream.Disable();
                }catch(Exception e){
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
             */
        }
    }
}