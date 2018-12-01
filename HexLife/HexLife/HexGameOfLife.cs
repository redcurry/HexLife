using System;

namespace HexLife
{
    public class HexGameOfLife
    {
        public HexGrid Grid => CreateRandomGrid();

        public HexGameOfLife()
        {
        }

        private HexGrid CreateRandomGrid()
        {
            var grid = new HexGrid(100, 100);
            var random = new Random();
            for (var i = 0; i < 100; i++)
                for (var j = 0; j < 100; j++)
                    grid[i, j].IsAlive = random.NextDouble() < 0.5;
            return grid;
        }
    }

    public class HexGrid
    {
        private readonly HexCell[,] _grid;

        public HexGrid(int rows, int cols)
        {
            Rows = rows;
            Columns = cols;

            _grid = new HexCell[rows, cols];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    _grid[i, j] = new HexCell();
        }

        public int Rows { get; }
        public int Columns { get; }

        public HexCell this[int i, int j] => _grid[i, j];
    }

    public class HexCell
    {
        public bool IsAlive { get; set; }
    }
}