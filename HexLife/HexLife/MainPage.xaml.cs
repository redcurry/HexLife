using System;
using System.Timers;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace HexLife
{
    public partial class MainPage : ContentPage
    {
        private readonly HexGameOfLife _gol;

        public MainPage()
        {
            InitializeComponent();

            _gol = new HexGameOfLife(100, 100);
            _gol.ResetToSingleCell();

            var timer = new Timer(1000);
            timer.Elapsed += (sender, args) =>
            {
                _gol.MakeNextGeneration();
                Canvas.InvalidateSurface();
            };
            timer.Start();
        }

        private void SKCanvasView_OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear();

            var grid = _gol.Grid;
            for (var i = 0; i < grid.Rows; i++)
                for (var j = 0; j < grid.Columns; j++)
                {
                    if (grid[i, j].IsAlive)
                        DrawCell(canvas, i, j);
                }
        }

        private void DrawCell(SKCanvas canvas, int i, int j)
        {
            const int CellRadius = 8;

            var paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = Color.Red.ToSKColor(),
            };

            float x, y;

            if (i % 2 == 0)
            {
                x = j * 2 * CellRadius + CellRadius;
                y = i * (float)Math.Sqrt(3) * CellRadius + CellRadius;
            }
            else
            {
                x = j * 2 * CellRadius + CellRadius + CellRadius;
                y = i * (float)Math.Sqrt(3) * CellRadius + CellRadius;
            }

            canvas.DrawCircle(x, y, CellRadius, paint);
        }
    }
}
