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
            _sensorChooser.KinectChanged += (o, args) =>
            {
                if (args.NewSensor != null)
                {
                    Debug.WriteLine("we have a new sensor");
                }
                else
                {
                    Debug.WriteLine("no Kinect sensor connected");
                }
            };

            _sensorChooser.PropertyChanged += (o, args) => 
            {
                if (kinectStatus.Equals(args.PropertyName))
                {
                    Debug.WriteLine("Status = " + _sensorChooser.Status);
                }
                _sensorChooser.Start();
            };
        }
    }
}
