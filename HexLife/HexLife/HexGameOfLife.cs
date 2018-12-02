using System;
using System.Linq;

namespace HexLife
{
    public class HexGameOfLife
    {
        private HexGrid _nextGrid;

        public HexGameOfLife(int width, int height)
        {
            // In a grid, rows are vertical and columns are horizontal
            Grid = new HexGrid(height, width);
            _nextGrid = new HexGrid(height, width);
        }

        public HexGrid Grid { get; private set; }

        public int[] BornNeighborCount { get; set; } = {1, 2};
        public int[] SurviveNeighborCount { get; set; } = {2, 4};

        public void ResetToSingleCell()
        {
            Grid.Clear();
            SetCenterCellAlive();
        }

        public void MakeNextGeneration()
        {
            for (int i = 0; i < Grid.Rows; i++)
                for (int j = 0; j < Grid.Columns; j++)
                    _nextGrid.SetIsAlive(i, j, DetermineIfAlive(i, j));

            var temp = Grid;
            Grid = _nextGrid;
            _nextGrid = temp;
        }

        private bool DetermineIfAlive(int i, int j)
        {
            var nCount = Grid.CountLiveNeighbors(i, j);
            return DetermineIfAlive(i, j, nCount);
        }

        private bool DetermineIfAlive(int i, int j, int nCount)
        {
            return Grid.IsAlive(i, j)
                ? DetermineIfSurvives(nCount)
                : DetermineIfIsBorn(nCount);
        }

        private bool DetermineIfSurvives(int nCount) =>
            SurviveNeighborCount.Contains(nCount);

        private bool DetermineIfIsBorn(int nCount) =>
            BornNeighborCount.Contains(nCount);

        private void SetCenterCellAlive() =>
            Grid.SetIsAlive(Grid.Rows / 2, Grid.Columns / 2, true);
    }
}