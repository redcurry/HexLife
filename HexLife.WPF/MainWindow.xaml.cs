using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using HexLife.Domain;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace HexLife.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HexGridPainter _hexPainter;
        private float _cellRadius;
        private HexGameOfLife _gol;
        private DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();

            _hexPainter = new HexGridPainter();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(1);
            _timer.Tick += (o, args) =>
            {
                _gol.MakeNextGeneration();
                Canvas.InvalidateVisual();
            };
        }

        private void StartButton_OnClicked(object sender, RoutedEventArgs e)
        {
            _cellRadius = Canvas.CanvasSize.Width / 200 / 2;
            _gol = new HexGameOfLife(200, 200);
            _gol.ResetToSingleCell();
            _timer.Start();
        }

        private void StopButton_OnClicked(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
        }

        private void Canvas_OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            if (_gol == null) return;

            var canvas = e.Surface.Canvas;
            _hexPainter.DrawGrid(_gol.Grid, canvas, _cellRadius);
        }

        private void SaveButton_OnClicked(object sender, RoutedEventArgs e)
        {
            var width = Canvas.CanvasSize.Width;
            var bounds = SKRect.Create(0, 0, width, width);

            using (var stream = new SKFileWStream(@"C:\Temp\grid.svg"))
            using (var writer = new SKXmlStreamWriter(stream))
            using (var canvas = SKSvgCanvas.Create(bounds, writer))
            {
                _hexPainter.DrawGrid(_gol.Grid, canvas, _cellRadius);
            }
        }
    }
}
