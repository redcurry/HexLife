using System;
using System.Timers;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace HexLife
{
    public partial class MainPage : ContentPage
    {
        private const float Sqrt3 = 1.732050807568877f;

        private float CellRadius;
        private float Sqrt3TimesCellRadius;
        private float TwoTimesCellRadius;

        private HexGameOfLife _gol;

        private bool _isPlaying;

        public MainPage()
        {
            InitializeComponent();
        }

        private void StartButton_OnClicked(object sender, EventArgs e)
        {
            CellRadius = Canvas.CanvasSize.Width / 200 / 2;
            Sqrt3TimesCellRadius = Sqrt3 * CellRadius;
            TwoTimesCellRadius = 2 * CellRadius;

            _gol = new HexGameOfLife(200, 200);
            _gol.ResetToSingleCell();
            _isPlaying = true;

            Device.StartTimer(TimeSpan.FromMilliseconds(0), () =>
            {
                _gol.MakeNextGeneration();

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
            if (_gol == null) return;

            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.Black);

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
            var (x, y) = GetCanvasCoords(i, j);
            DrawCircle(canvas, x, y, paint);
            // DrawHexagon(canvas, x, y, paint);
        }

        private void DrawCircle(SKCanvas canvas, float x, float y, SKPaint paint) =>
            canvas.DrawCircle(x, y, CellRadius, paint);

        private void DrawHexagon(SKCanvas canvas, float x, float y, SKPaint paint)
        {
            var path = new SKPath();

            path.MoveTo(x, y - 2 * CellRadius / Sqrt3);
            path.LineTo(x + CellRadius, y - CellRadius / Sqrt3);
            path.LineTo(x + CellRadius, y + CellRadius / Sqrt3);
            path.LineTo(x, y + 2 * CellRadius / Sqrt3);
            path.LineTo(x - CellRadius, y + CellRadius / Sqrt3);
            path.LineTo(x - CellRadius, y - CellRadius / Sqrt3);
            path.Close();

            canvas.DrawPath(path, paint);
        }

        private (float, float) GetCanvasCoords(int i, int j)
        {
            var x = j * TwoTimesCellRadius + CellRadius * (i % 2);
            var y = i * Sqrt3TimesCellRadius;
            return (x, y);
        }
    }
}
