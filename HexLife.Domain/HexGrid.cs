namespace HexLife.Domain
{
    public class HexGrid
    {
        private HexCell[,] _grid;

        public HexGrid(int rows, int cols)
        {
            Rows = rows;
            Columns = cols;

            InitializeGrid();
        }

        public int Rows { get; }
        public int Columns { get; }

        public void Clear() =>
            InitializeGrid();

        public void SetIsAlive(int i, int j, bool isAlive)
        {
            var ii = i + 1;  // == I(i)
            var jj = j + 1;  // == J(j)

            // Do nothing if alive value doesn't change
            if (_grid[ii, jj].IsAlive == isAlive) return;

            _grid[ii, jj].IsAlive = isAlive;

            var iMod2 = i % 2; // i (not ii) because it's not accessing grid

            var iiPlus1  = ii + 1;
            var iiMinus1 = i;
            var jjPlus1  = jj + 1;
            var jjMinus1 = j;

            var jjPlus_iMod2  = jj + iMod2;
            var jjPlus_iMod2_Minus1 = jjPlus_iMod2 - 1;

            var neighbors = isAlive ? 1 : -1;

            _grid[iiMinus1, jjPlus_iMod2       ].Neighbors += neighbors;
            _grid[ii,       jjPlus1            ].Neighbors += neighbors;
            _grid[iiPlus1,  jjPlus_iMod2       ].Neighbors += neighbors;
            _grid[iiPlus1,  jjPlus_iMod2_Minus1].Neighbors += neighbors;
            _grid[ii,       jjMinus1           ].Neighbors += neighbors;
            _grid[iiMinus1, jjPlus_iMod2_Minus1].Neighbors += neighbors;
        }

        public bool IsAlive(int i, int j) =>
            _grid[I(i), J(j)].IsAlive;

        public int Neighbors(int i, int j) =>
            _grid[I(i), J(j)].Neighbors;

        private int I(int i) => i + 1;
        private int J(int j) => j + 1;

        private void InitializeGrid() =>
            _grid = new HexCell[Rows + 2, Columns + 2];
    }
}