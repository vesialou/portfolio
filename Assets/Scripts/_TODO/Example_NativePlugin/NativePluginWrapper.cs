using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class NativePluginWrapper : MonoBehaviour
{
    private const string DllName = "NativePlugin";

    // ==================== P/Invoke Declarations ====================

    [DllImport(DllName)] private static extern int AddNumbers(int a, int b);
    [DllImport(DllName)] private static extern float CalculateDistance(float x1, float y1, float x2, float y2);
    [DllImport(DllName)] private static extern int GetStringLength([MarshalAs(UnmanagedType.LPStr)] string str);
    [DllImport(DllName)] private static extern IntPtr CreateGreeting([MarshalAs(UnmanagedType.LPStr)] string name);
    [DllImport(DllName)] private static extern void FreeString(IntPtr str);
    [DllImport(DllName)] private static extern void ProcessArray(int[] array, int size);
    [DllImport(DllName)] private static extern float CalculateSum(float[] array, int size);

    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3Native
    {
        public float x, y, z;
        public Vector3Native(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Vector3Native(Vector3 unityVector)
        {
            x = unityVector.x;
            y = unityVector.y;
            z = unityVector.z;
        }
        public Vector3 ToUnityVector3() => new Vector3(x, y, z);
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct GameStats
    {
        public int score;
        public float health;
        [MarshalAs(UnmanagedType.I1)] public bool isAlive;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string playerName;
    }

    [DllImport(DllName)] private static extern float Vector3Magnitude(Vector3Native vec);
    [DllImport(DllName)] private static extern Vector3Native Vector3Normalize(Vector3Native vec);
    [DllImport(DllName)] private static extern void UpdateGameStats(ref GameStats stats, int scoreBonus);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void LogCallback([MarshalAs(UnmanagedType.LPStr)] string message);

    [DllImport(DllName)] private static extern void SetLogCallback(LogCallback callback);
    [DllImport(DllName)] private static extern void ProcessWithLogging(int iterations);
    [DllImport(DllName)] private static extern void FastMatrixMultiply(float[] a, float[] b, float[] result, int size);
    [DllImport(DllName)] private static extern void ParallelProcess(int[] data, int size, int threads);


    public static int Add(int a, int b)
    {
        return AddNumbers(a, b);
    }

    public static float GetDistance(Vector2 point1, Vector2 point2)
        => CalculateDistance(point1.x, point1.y, point2.x, point2.y);

    public static string GetGreeting(string name)
    {
        var ptr = CreateGreeting(name);
        if (ptr == IntPtr.Zero)
        {
            return null;
        }

        var result = Marshal.PtrToStringAnsi(ptr);
        FreeString(ptr);

        return result;
    }

    public static void DoubleArrayValues(int[] array) => ProcessArray(array, array.Length);

    public static float SumArray(float[] array) => CalculateSum(array, array.Length);

    public static float GetMagnitude(Vector3 vector) => Vector3Magnitude(new Vector3Native(vector));

    public static Vector3 NormalizeVector(Vector3 vector)
    {
        var normalized = Vector3Normalize(new Vector3Native(vector));

        return normalized.ToUnityVector3();
    }

    public static void MultiplyMatrices(float[,] matrixA, float[,] matrixB, out float[,] result)
    {
        var size = matrixA.GetLength(0);
        result = new float[size, size];

        var flatA = new float[size * size];
        var flatB = new float[size * size];
        var flatResult = new float[size * size];

        for (var i = 0; i < size; i++)
        {
            for (var j = 0; j < size; j++)
            {
                flatA[i * size + j] = matrixA[i, j];
                flatB[i * size + j] = matrixB[i, j];
            }
        }

        FastMatrixMultiply(flatA, flatB, flatResult, size);

        for (var i = 0; i < size; i++)
        {
            for (var j = 0; j < size; j++)
            {
                result[i, j] = flatResult[i * size + j];
            }
        }
    }

    // ==================== Demo Methods ====================

    void Start()
    {
        DemoBasicOperations();
        DemoStringOperations();
        DemoArrayOperations();
        DemoStructOperations();
        DemoCallbackOperations();
        DemoPerformanceOperations();
    }

    void DemoBasicOperations()
    {
        Debug.Log("=== Basic Operations ===");
        Debug.Log($"15 + 25 = {Add(15, 25)}");

        Vector2 point1 = new(0, 0);
        Vector2 point2 = new(3, 4);
        Debug.Log($"Distance between {point1} and {point2} = {GetDistance(point1, point2)}");
    }

    void DemoStringOperations()
    {
        Debug.Log("=== String Operations ===");
        Debug.Log($"Message in C# : {GetGreeting("text in message unity")}");

        var testStr = "Text from Unity";
        Debug.Log($"Length of '{testStr}': {GetStringLength(testStr)}");
    }

    void DemoArrayOperations()
    {
        Debug.Log("=== Array Operations ===");
        int[] integers = { 1, 2, 3, 4, 5 };
        Debug.Log($"Original: [{string.Join(", ", integers)}]");
        DoubleArrayValues(integers);
        Debug.Log($"Doubled: [{string.Join(", ", integers)}]");

        float[] floats = { 1.5f, 2.5f, 3.5f, 4.5f, 5.5f };
        Debug.Log($"Sum: {SumArray(floats)}");
    }

    void DemoStructOperations()
    {
        Debug.Log("=== Struct Operations ===");

        Vector3 testVector = new(3, 4, 5);
        Debug.Log($"Magnitude of {testVector} = {GetMagnitude(testVector)}");
        Debug.Log($"Normalized: {NormalizeVector(testVector)}");

        GameStats stats = new()
        {
            score = 100,
            health = 75.5f,
            isAlive = true,
            playerName = "TestPlayer"
        };

        Debug.Log($"Before: Score={stats.score}, Health={stats.health}");
        UpdateGameStats(ref stats, 50);
        Debug.Log($"After: Score={stats.score}, Health={stats.health}");
    }

    void DemoCallbackOperations()
    {
        Debug.Log("=== Callback Demo ===");
        SetLogCallback(OnNativeLog);
        ProcessWithLogging(25);
    }

    private static void OnNativeLog(string message)
    {
        Debug.Log($"[C++ Log]: {message}");
    }

    void DemoPerformanceOperations()
    {
        Debug.Log("=== Performance Operations ===");

        const int matrixSize = 3;
        float[,] matrixA = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
        float[,] matrixB = { { 9, 8, 7 }, { 6, 5, 4 }, { 3, 2, 1 } };

        MultiplyMatrices(matrixA, matrixB, out float[,] result);
        Debug.Log("Matrix multiplication result:");
        for (var i = 0; i < matrixSize; i++)
        {
            var row = "";
            for (var j = 0; j < matrixSize; j++)
            {
                row += $"{result[i, j]:F1} ";
            }
            Debug.Log($"[{row.Trim()}]");
        }

        int[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        Debug.Log($"Before processing: [{string.Join(", ", data)}]");
        ParallelProcess(data, data.Length, 2);
        Debug.Log($"After processing: [{string.Join(", ", data)}]");
    }

    // ==================== Performance Benchmark ====================


    [ContextMenu("++BenchmarkTest")]
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public void BenchmarkTest()
    {
        const int iterations = 10_000_000;

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        for (var i = 0; i < iterations; i++)
        {
            Add(i, i + 1);
        }
        stopwatch.Stop();
        Debug.Log($"C++ add {iterations} times: {stopwatch.ElapsedMilliseconds}ms");

        stopwatch.Restart();
        for (var i = 0; i < iterations; i++)
        {
            var r = i + (i + 1);
        }
        stopwatch.Stop();
        Debug.Log($"C# add {iterations} times: {stopwatch.ElapsedMilliseconds}ms");
        /*
            C++ add 10000000 times: 98ms
            C# add 10000000 times: 2ms
         */
    }
}
