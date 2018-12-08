using HexLife.Domain;
using NUnit.Framework;

namespace HexLife.Test
{
    public class HexGameOfLifeBenchmark
    {
        [Test]
        public void Benchmark()
        {
            var hexLife = new HexGameOfLife(1000, 1000);
            hexLife.ResetToSingleCell();

            var benchmark = new Benchmark();

            benchmark.Start();
            for (var i = 0; i < 100; i++)
                hexLife.MakeNextGeneration();
            benchmark.Stop();

            Assert.Fail($"{benchmark.MeanElapsed().TotalMilliseconds}");
        }
    }
}