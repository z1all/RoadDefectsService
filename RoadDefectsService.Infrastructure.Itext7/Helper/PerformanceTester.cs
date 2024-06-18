using System.Diagnostics;

namespace RoadDefectsService.Infrastructure.Itext7.Helper
{
    public static class PerformanceTester
    {
        public static long MeasureExecutionTime(Action action)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();
            return stopwatch.ElapsedTicks * (1000000000L / Stopwatch.Frequency);
        }
    }
}
