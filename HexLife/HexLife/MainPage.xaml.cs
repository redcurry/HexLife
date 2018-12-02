using System;
using System.Timers;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace HexLife
{
    public partial class MainPage : ContentPage
    {
        private const int CellRadius = 4;
        private const float Sqrt3 = 1.732050807568877f;
        private const float Sqrt3TimesCellRadius = Sqrt3 * CellRadius;
        private const float TwoTimesCellRadius = 2 * CellRadius;

        private HexGameOfLife _gol;

        private Benchmark _benchmark;
        private bool _isPlaying;

        public MainPage()
        {
            InitializeComponent();

            _gol = new HexGameOfLife(200, 200);
            _benchmark = new Benchmark();
        }

        private void StartButton_OnClicked(object sender, EventArgs e)
        {
            _gol.ResetToSingleCell();
            _isPlaying = true;

            Device.StartTimer(TimeSpan.FromMilliseconds(0), () =>
            {
                _benchmark.Start();
                _gol.MakeNextGeneration();
                _benchmark.Stop();

                Canvas.InvalidateSurface();
                return _isPlaying;
            });
        }

        private void StopButton_OnClicked(object sender, EventArgs e)
        {
            _isPlaying = false;
        }

        private void SKCanvasView_OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear();

            var paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = Color.Red.ToSKColor(),
            };

            var grid = _gol.Grid;
            for (var i = 0; i < grid.Rows; i++)
                for (var j = 0; j < grid.Columns; j++)
                {
                    if (grid.IsAlive(i, j))
                        DrawCell(canvas, i, j, paint);
                }
        }

        private void DrawCell(SKCanvas canvas, int i, int j, SKPaint paint)
        {
            float x, y;

            if (i % 2 == 0)
            {
                x = j * TwoTimesCellRadius + CellRadius;
                y = i * Sqrt3TimesCellRadius + CellRadius;
            }
            else
            {
                x = j * TwoTimesCellRadius + TwoTimesCellRadius;
                y = i * Sqrt3TimesCellRadius + CellRadius;
            }

            canvas.DrawCircle(x, y, CellRadius, paint);
        }
    }
}
