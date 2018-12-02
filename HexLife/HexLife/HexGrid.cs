using System.Linq;

namespace HexLife
{
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

        public void Clear()
        {
            for (int i = 0; i < Rows; i++)
            for (int j = 0; j < Columns; j++)
                _grid[i, j].IsAlive = false;
        }

        public void SetIsAlive(int i, int j, bool isAlive) =>
            _grid[i, j].IsAlive = isAlive;

        public bool IsAlive(int i, int j) =>
            _grid[i, j].IsAlive;

        public int CountLiveNeighbors(int i, int j)
        {
            return BoolToInt(ExistsAndIsAlive(i - 1, j)) +
                   BoolToInt(ExistsAndIsAlive(i + 1, j)) +
                   BoolToInt(ExistsAndIsAlive(i, j - 1)) +
                   BoolToInt(ExistsAndIsAlive(i, j + 1)) +
                   BoolToInt(i % 2 == 1
                       ? ExistsAndIsAlive(i - 1, j + 1)
                       : ExistsAndIsAlive(i - 1, j - 1)) +
                   BoolToInt(i % 2 == 1
                       ? ExistsAndIsAlive(i + 1, j + 1)
                       : ExistsAndIsAlive(i + 1, j - 1));
        }

        private bool ExistsAndIsAlive(int i, int j) =>
            i >= 0 && i < Rows && j >= 0 && j < Columns && _grid[i, j].IsAlive;

        private int BoolToInt(bool b) =>
            b ? 1 : 0;
    }
}