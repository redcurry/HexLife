using System;
using System.Diagnostics;

namespace HexLife.Test
{
    public class Benchmark
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private int _iterations = 0;
        private TimeSpan _totalElapsed = TimeSpan.Zero;

        public void Start()
        {
            _stopwatch.Restart();
        }

        public void Stop()
        {
            _stopwatch.Stop();
            _totalElapsed += _stopwatch.Elapsed;
            _iterations++;
        }

        public TimeSpan MeanElapsed() =>
            TimeSpan.FromMilliseconds(_totalElapsed.TotalMilliseconds / _iterations);
    }
}