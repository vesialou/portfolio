# Burst Performance Test Scene

## 📍 Scene Path
`Assets/Scenes/BurstPerformanceTest.scene`

This scene benchmarks computational performance using Unity's **Burst Compiler** and **Jobs System**.

---

## 🚀 Features

- **Two benchmarks**:
  - Standard vs NativeArray vs Burst job
  - Complex math in regular C# vs Burst
- Measures execution time and prints performance stats
- Shows speedup and gain from using Burst

---

## 🔧 Components

### `BurstPerformanceTest.cs`
- Entry script, runs tests at startup or via context menu
- Logs performance comparison (e.g. “🐌 Regular: 1,800ms → 🚀 Burst: 34ms”)
- Tests:
  - Basic math on large arrays
  - Complex math (sin, cos, sqrt)

### `BurstMathJob.cs`
- Burst-compiled job to fill and sum squares of an array

### `ComplexMathJob.cs`
- Burst-parallel job with math-heavy operations using `Unity.Mathematics`

---

## 🧪 Example Output

```
📊 PERFORMANCE RESULTS
🐌 Regular: 329ms
⚡ NativeArray: 520ms
🚀 Burst: 138ms
🎯 Speedup: 2.4x

📊 ADVANCED TEST RESULTS
🐌 Regular: 1848ms
🚀 Burst: 34ms
🎯 Speedup: 53.8x
```

---

## 📋 Requirements

- Unity Burst Package
- Unity Collections & Jobs
- Unity.Mathematics

---

## ✅ Usage

1. Open scene `BurstPerformanceTest`.
2. Run via `Start()` or call context menu functions:
   - `Run Performance Test`
   - `Run Advanced Test`
