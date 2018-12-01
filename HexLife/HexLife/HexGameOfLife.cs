﻿using System;
using System.Linq;

namespace HexLife
{
    public class HexGameOfLife
    {
        public HexGrid Grid { get; private set; }

        public int[] SurviveNeighborCount = {2, 4};
        public int[] BornNeighborCount = {1, 2};

        public HexGameOfLife(int width, int height)
        {
            Grid = new HexGrid(height, width);
        }

        public void ResetToSingleCell()
        {
            Grid.Clear();
            GetCenterCell().IsAlive = true;
        }

        public void MakeNextGeneration()
        {
            var nextGrid = new HexGrid(Grid.Rows, Grid.Columns);

            for (int i = 0; i < Grid.Rows; i++)
                for (int j = 0; j < Grid.Columns; j++)
                    nextGrid[i, j].IsAlive = DetermineIfAlive(i, j);

            Grid = nextGrid;
        }

        private bool DetermineIfAlive(int i, int j)
        {
            var nCount = Grid.CountLiveNeighbors(i, j);
            return DetermineIfAlive(i, j, nCount);
        }

        private bool DetermineIfAlive(int i, int j, int nCount)
        {
            return Grid[i, j].IsAlive
                ? DetermineIfSurvives(nCount)
                : DetermineIfBorns(nCount);
        }

        private bool DetermineIfSurvives(int nCount) =>
            SurviveNeighborCount.Contains(nCount);

        private bool DetermineIfBorns(int nCount) =>
            BornNeighborCount.Contains(nCount);

        private HexCell GetCenterCell() =>
            Grid[Grid.Rows / 2, Grid.Columns / 2];
    }
}