using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using System.Diagnostics;
using System.Text;

public class BurstPerformanceTest : MonoBehaviour
{
    private void Start()
    {
        RunPerformanceTest();
        RunAdvancedTest();
    }

    [ContextMenu("Run Performance Test")]
    public void RunPerformanceTest()
    {
        var sb = new StringBuilder();
        const int arraySize = 1_000_000;
        const int iterations = 100;
        sb.AppendLine("🚀[Running Performance Test]");

        // Test 1: Regular int[] array with standard C# method
        var regularArrayTime = TestRegularArray(arraySize, iterations);

        // Test 2: NativeArray without Burst
        var nativeArrayTime = TestNativeArray(arraySize, iterations);

        // Test 3: NativeArray with Burst Job
        var burstJobTime = TestBurstJob(arraySize, iterations);

        PrintResults(regularArrayTime, nativeArrayTime, burstJobTime, sb);
        /*
            🚀[Running Performance Test]

            ==================================================
            📊 PERFORMANCE RESULTS
            ==================================================
            🐌 Regular Array:     329,58ms
            ⚡ NativeArray:       520,32ms
            🚀 Burst Job:         138,79ms
            --------------------------------------------------
            🎯 Speedup Factor:    2,4x faster!
            💡 Performance Gain:  137%
            ==================================================
         */
    }

    [ContextMenu("Run Advanced Test")]
    public void RunAdvancedTest()
    {
        var sb = new StringBuilder();
        const int arraySize = 500_000;
        const int iterations = 50;
        sb.AppendLine("🚀[Running Advanced Performance Test]");

        // Test 1: complex mathematical operations with standard C# method
        var regularTime = TestRegularWithComplexMath(arraySize, iterations);

        // Test 2: complex mathematical operations with Burst Job
        var burstTime = TestBurstWithComplexMath(arraySize, iterations);

        PrintResultsAdvaced(regularTime, burstTime, sb);
        /*
            🚀[Running Advanced Performance Test]

            ==================================================
            📊 ADVANCED TEST RESULTS
            ==================================================
            🐌 Regular C#:        1847,98ms
            🚀 Burst Job:         34,37ms
            --------------------------------------------------
            🎯 Speedup:           53,8x faster!
            ==================================================
        */
    }

    private static float TestRegularArray(int arraySize, int iterations)
    {
        var stopwatch = Stopwatch.StartNew();

        for (var iter = 0; iter < iterations; iter++)
        {
            var array = new int[arraySize];

            // Fill array with values
            for (var i = 0; i < arraySize; i++)
            {
                array[i] = i * 2;
            }

            // Sum all values (to prevent optimization)
            var sum = 0L;
            for (var i = 0; i < arraySize; i++)
            {
                sum += array[i] * array[i]; // Square each element
            }
        }

        stopwatch.Stop();
        var timeMs = (float)stopwatch.Elapsed.TotalMilliseconds;
        return timeMs;
    }

    private static float TestNativeArray(int arraySize, int iterations)
    {
        var stopwatch = Stopwatch.StartNew();

        for (var iter = 0; iter < iterations; iter++)
        {
            var array = new NativeArray<int>(arraySize, Allocator.TempJob);

            // Fill array with values
            for (var i = 0; i < arraySize; i++)
            {
                array[i] = i * 2;
            }

            // Sum all values
            var sum = 0L;
            for (var i = 0; i < arraySize; i++)
            {
                sum += array[i] * array[i];
            }

            array.Dispose();
        }

        stopwatch.Stop();
        var timeMs = (float)stopwatch.Elapsed.TotalMilliseconds;
        return timeMs;
    }

    private static float TestBurstJob(int arraySize, int iterations)
    {
        var stopwatch = Stopwatch.StartNew();

        for (var iter = 0; iter < iterations; iter++)
        {
            var inputArray = new NativeArray<int>(arraySize, Allocator.TempJob);
            var outputArray = new NativeArray<long>(1, Allocator.TempJob);

            // Create and schedule job
            var job = new BurstMathJob
            {
                Input = inputArray,
                Result = outputArray,
                ArraySize = arraySize
            };

            JobHandle handle = job.Schedule();
            handle.Complete();

            // Get result (to prevent optimization)
            _ = outputArray[0];

            inputArray.Dispose();
            outputArray.Dispose();
        }

        stopwatch.Stop();
        var timeMs = (float)stopwatch.Elapsed.TotalMilliseconds;
        return timeMs;
    }

    private static void PrintResults(float regularArrayTime, float nativeArrayTime, float burstJobTime, StringBuilder sb)
    {
        // Calculate speedup
        var speedupFactor = regularArrayTime / burstJobTime;
        sb.AppendLine("\n" + "=".PadRight(50, '='));
        sb.AppendLine("📊 PERFORMANCE RESULTS");
        sb.AppendLine("=".PadRight(50, '='));
        sb.AppendLine($"🐌 Regular Array:     {regularArrayTime:F2}ms");
        sb.AppendLine($"⚡ NativeArray:       {nativeArrayTime:F2}ms");
        sb.AppendLine($"🚀 Burst Job:         {burstJobTime:F2}ms");
        sb.AppendLine("".PadRight(50, '-'));
        sb.AppendLine($"🎯 Speedup Factor:    {speedupFactor:F1}x faster!");
        sb.AppendLine($"💡 Performance Gain:  {((speedupFactor - 1) * 100):F0}%");
        sb.AppendLine("=".PadRight(50, '='));
        UnityEngine.Debug.Log(sb);
    }

    private static float TestBurstWithComplexMath(int arraySize, int iterations)
    {
        var stopwatch = Stopwatch.StartNew();
        // Burst version
        stopwatch.Restart();
        for (var iter = 0; iter < iterations; iter++)
        {
            var data = new NativeArray<float>(arraySize, Allocator.TempJob);
            var job = new ComplexMathJob { Data = data, Size = arraySize };
            job.Schedule(arraySize, 1000).Complete();
            data.Dispose();
        }

        var burstTime = (float)stopwatch.Elapsed.TotalMilliseconds;
        return burstTime;
    }

    private static float TestRegularWithComplexMath(int arraySize, int iterations)
    {
        var stopwatch = Stopwatch.StartNew();
        stopwatch.Restart();
        for (var iter = 0; iter < iterations; iter++)
        {
            var data = new float[arraySize];
            for (var i = 0; i < arraySize; i++)
            {
                data[i] = Mathf.Sin(i * 0.01f) * Mathf.Cos(i * 0.02f) + Mathf.Sqrt(i);
            }
        }

        var regularTime = (float)stopwatch.Elapsed.TotalMilliseconds;
        return regularTime;
    }

    private static void PrintResultsAdvaced(float regularTime, float burstTime, StringBuilder sb)
    {
        var advancedSpeedup = regularTime / burstTime;

        sb.AppendLine("\n" + "=".PadRight(50, '='));
        sb.AppendLine("📊 ADVANCED TEST RESULTS");
        sb.AppendLine("=".PadRight(50, '='));
        sb.AppendLine($"🐌 Regular C#:        {regularTime:F2}ms");
        sb.AppendLine($"🚀 Burst Job:         {burstTime:F2}ms");
        sb.AppendLine("".PadRight(50, '-'));
        sb.AppendLine($"🎯 Speedup:           {advancedSpeedup:F1}x faster!");
        sb.AppendLine("=".PadRight(50, '='));
        UnityEngine.Debug.Log(sb);
    }
}