using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SkiaSharp;

namespace HexLife.Domain
{
    public class HexGridPainter
    {
        private const float Sqrt3 = 1.732050807568877f;

        public void DrawGrid(HexGrid grid, SKCanvas canvas, float cellRadius)
        {
            canvas.Clear(SKColors.Black);

            var paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColor.Parse("#FFFF00")
            };

            for (var i = 0; i < grid.Rows; i++)
                for (var j = 0; j < grid.Columns; j++)
                {
                    if (grid.IsAlive(i, j))
                        DrawCell(canvas, i, j, paint, cellRadius);
                }
        }

        private void DrawCell(SKCanvas canvas, int i, int j, SKPaint paint, float cellRadius)
        {
            var (x, y) = GetCanvasCoords(i, j, cellRadius);
            DrawCircle(canvas, x, y, paint, cellRadius);
            // DrawHexagon(canvas, x, y, paint);
        }

        private void DrawCircle(SKCanvas canvas, float x, float y, SKPaint paint, float cellRadius) =>
            canvas.DrawCircle(x, y, cellRadius, paint);

        private void DrawHexagon(SKCanvas canvas, float x, float y, SKPaint paint, float cellRadius)
        {
            var path = new SKPath();

            path.MoveTo(x, y - 2 * cellRadius / Sqrt3);
            path.LineTo(x + cellRadius, y - cellRadius / Sqrt3);
            path.LineTo(x + cellRadius, y + cellRadius / Sqrt3);
            path.LineTo(x, y + 2 * cellRadius / Sqrt3);
            path.LineTo(x - cellRadius, y + cellRadius / Sqrt3);
            path.LineTo(x - cellRadius, y - cellRadius / Sqrt3);
            path.Close();

            canvas.DrawPath(path, paint);
        }

        private (float, float) GetCanvasCoords(int i, int j, float cellRadius)
        {
            var x = j * 2 * cellRadius + cellRadius * (i % 2);
            var y = i * Sqrt3 * cellRadius;
            return (x, y);
        }
    }
}
