# NativePlugin

## 🧩 Overview

`NativePlugin` is a native C++ plugin integrated with Unity via `P/Invoke`. It provides performance-critical utilities and demonstrates interoperability between Unity C# scripts and a C++ dynamic library.

---

## 🗂 Project Structure 📍 

 ### Scene Path
- `Assets/Scenes/NativePlugin.scene`

## Unity Side

- **DLL Location:** `Assets/Plugins/NativePlugin/NativePlugin.dll`
- **Wrapper Script:** `NativePluginWrapper.cs`
- **Usage:** All C# calls are routed via `DllImport` bindings to the native plugin.

### C++ Project

- Located at: `<UnityProjectRoot>/NativePlugin/`
- CMake-based build system:
  - `CMakeLists.txt` — defines DLL target
  - `NativePlugin.cpp/.h` — implementation
  - `build.bat` / `build_native.bat` — build helpers

Build output is expected to produce `NativePlugin.dll`.

---

## 🔧 Exposed Native Functions

### Math & Utility
- `int AddNumbers(int a, int b)`
- `float CalculateDistance(float x1, float y1, float x2, float y2)`
- `int GetStringLength(const char* str)`
- `char* CreateGreeting(const char* name)` + `void FreeString(char*)`

### Arrays
- `void ProcessArray(int* array, int size)` — doubles each value
- `float CalculateSum(float* array, int size)`

### Vectors
- `float Vector3Magnitude(Vector3 vec)`
- `Vector3 Vector3Normalize(Vector3 vec)`

### Structs
- `void UpdateGameStats(GameStats* stats, int scoreBonus)`

### Logging (Callbacks)
- `void SetLogCallback(LogCallback callback)`
- `void ProcessWithLogging(int iterations)`

### Performance
- `void FastMatrixMultiply(float* a, float* b, float* result, int size)`
- `void ParallelProcess(int* data, int size, int threads)`

---

## 🧠 Data Structures

### `Vector3`
```cpp
struct Vector3 {
    float x, y, z;
};
```

### `GameStats`
```cpp
struct GameStats {
    int score;
    float health;
    bool isAlive;
    char playerName[32];
};
```

---

## 🔄 Unity Wrapper

**Script:** `NativePluginWrapper.cs`

Provides:
- Friendly C# methods for each native function
- Auto-marshaling of structs (e.g., `GameStats`, `Vector3`)
- Logging and benchmark demos via `Start()`

---

## 🧪 Example Usage

```csharp
int sum = NativePluginWrapper.Add(3, 4);
float dist = NativePluginWrapper.GetDistance(new Vector2(0, 0), new Vector2(1, 1));
string msg = NativePluginWrapper.GetGreeting("Unity");
```

---

## ⚙️ Building the DLL

```bash
cd NativePlugin
mkdir out && cd out
cmake ..
cmake --build . --config Release
```

Output DLL will be in the build folder and must be copied to:
```
<UnityProject>/Assets/Plugins/NativePlugin/NativePlugin.dll
```

---

## 🧪 Benchmarking

Included in wrapper:
```csharp
[ContextMenu("++BenchmarkTest")]
public void BenchmarkTest()
```

Compares performance between native C++ and pure C# `Add()` calls.

---

## 🛠️ Requirements

- Unity Editor (Windows preferred for DLL)
- Native C++ toolchain (MSVC, CMake)
- `NativePlugin.dll` in Unity plugin folder

---

## 🧵 Notes

- `CreateGreeting` allocates memory; `FreeString` must be called to avoid memory leaks.
- Callback support via `SetLogCallback()` (delegates mapped to native log).

---
