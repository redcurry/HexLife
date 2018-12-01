using System;
using System.Timers;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace HexLife
{
    public partial class MainPage : ContentPage
    {
        private Random _random = new Random();

        public MainPage()
        {
            InitializeComponent();

            var timer = new Timer(1000);
            timer.Elapsed += (sender, args) => Canvas.InvalidateSurface();
            timer.Start();
        }

        private void SKCanvasView_OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.FromRgb(_random.Next(10, 250), _random.Next(10, 250), _random.Next(10, 250)).ToSKColor(),
                StrokeWidth = 25
            };
            canvas.DrawCircle(e.Info.Width / 2, e.Info.Height / 2, 100, paint);
        }
    }
}
