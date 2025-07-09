# 🧠 Unity Portfolio Project — Engineering Demo

## 🎯 Project Goal

Showcase of core Unity engineering skills at a senior level:
- Architecture and design patterns.
- Performance-focused code (Burst, Native Collections).
- Asset management with Addressables.
- Native Plugin integration (C++, CMake, batch build).
- Custom tools, test scenes, and UI integration.

---

## 🗂️ Project Structure

```
Assets/
  ├── Scenes/                        # Demo scenes
  ├── Scripts/                       # Core examples
  │     └── _TODO/                   # Experimental and task-focused modules
  │          └── Example_Patterns/   # Design pattern implementations
  ├── Shaders/                       # (in progress) Custom shaders
  ├── Plugins/                       # Native plugins and external builds
  └── PublicAssets/                 # Shared assets
NativePlugin/                       # C++ native plugin project
```

---

## 📚 Demos & Examples

### 🧩 [Design Patterns Folder](https://github.com/vesialou/portfolio/tree/main/Assets/Scripts/_TODO/Example_Patterns)

| Pattern | Description | Link |
|--------|-------------|------|
| 🔗 **Chain of Responsibility** | Sequential level data processing chain, ideal for FTUE, bonuses, balance steps. | [Open](https://github.com/vesialou/portfolio/tree/main/Assets/Scripts/_TODO/Example_Patterns/ChainOfResponsibility) |
| 🧬 **Strategy** | Runtime decision-making between interchangeable behaviors. | [Open](https://github.com/vesialou/portfolio/tree/main/Assets/Scripts/_TODO/Example_Patterns/Strategy) |
| 🔁 **State** | Manages internal state transitions cleanly. | [Open](https://github.com/vesialou/portfolio/tree/main/Assets/Scripts/_TODO/Example_Patterns/State) |
| 👁️ **Visitor** | Operations on elements without changing their classes. | [Open](https://github.com/vesialou/portfolio/tree/main/Assets/Scripts/_TODO/Example_Patterns/Visitor) |
| 🧩 **Observer Event Bus** | Centralized event dispatching, decouples components. | [Open](https://github.com/vesialou/portfolio/tree/main/Assets/Scripts/_TODO/Example_Patterns/ObserverEventBus) |
| 🧠 **Command & Memento** | Undo/redo system, useful for editors and history stacks. | [Open](https://github.com/vesialou/portfolio/tree/main/Assets/Scripts/_TODO/Example_Patterns/CommandMemento) |
| 🧱 **Composite** | Treat individual objects and groups uniformly. | [Open](https://github.com/vesialou/portfolio/tree/main/Assets/Scripts/_TODO/Example_Patterns/ObserverEventBus/Composite) |

---

### 🚀 [Performance & Systems Examples](https://github.com/vesialou/portfolio/tree/main/Assets/Scripts/_TODO)

| System | Description | Link |
|--------|-------------|------|
| 💡 **Player Loop Customization** | Adds a custom update step in Unity’s player loop. | [Open](https://github.com/vesialou/portfolio/tree/main/Assets/Scripts/_TODO/Example_PlayerLoop) |
| 🚀 **Burst Performance Test** | Scene to compare Burst-optimized vs standard C# code. | [Open](https://github.com/vesialou/portfolio/tree/main/Assets/Scripts/_TODO/Example_BurstNativeArray) |
| 🧪 **Concurrent Dictionary / SemaphoreSlim** | Thread-safe and low-level async coordination examples. | [Open](https://github.com/vesialou/portfolio/tree/main/Assets/Scripts/_TODO/Example_ConcurentDictionary) |

---

### 🎯 Other Features & Integration

| Feature | Description | Link |
|--------|-------------|------|
| 🎯 **Addressables Harry Scene** | Scene demonstrating dynamic Addressables loading. | [Open](https://github.com/vesialou/portfolio/tree/main/Assets/Scenes/AddressablesHarryScene) |
| 🛠️ **Native Plugin (C++)** | Integration of custom C++ code via CMake and batch scripts. | [Open](https://github.com/vesialou/portfolio/tree/main/NativePlugin) |
| ✨ **Shaders (WIP)** | Custom shaders planned (dissolve, outline, stylized FX). | [Open](https://github.com/vesialou/portfolio/tree/main/Assets/Shaders) |


---

### 🎯 [Other Features & Integration](https://github.com/vesialou/portfolio/tree/main)

| Feature | Description | Link |
|--------|-------------|------|
| 🎯 **Addressables Harry Scene** | Scene demonstrating dynamic Addressables loading. | [Open](https://github.com/vesialou/portfolio/tree/main/Assets/Scenes) |
| 🛠️ **Native Plugin (C++)** | Integration of custom C++ code via CMake and batch scripts. | [Open](https://github.com/vesialou/portfolio/tree/main/NativePlugin) |
| ✨ **Shaders (WIP)** | Custom shaders planned (dissolve, outline, stylized FX). | [Open](#) |

---

## 🛠️ Tech Stack

- `Burst`, `Jobs`, `NativeArray`, `UnsafeUtility`
- `Addressables`, `ScriptableObject`, `Editor Scripting`
- `C++/CLI`, `CMake`, `DllImport`, `Batch scripting`
- Design Patterns: Strategy, Visitor, State, Observer, Chain of Responsibility, etc.
- Testable code architecture

---

## 🚧 Roadmap

- [ ] Add Shader Graph examples (dissolve / outline).
- [ ] Visual UI scene to tweak and apply level data.
- [ ] Add DOTween-based UI transitions.
- [ ] Custom Unity Editor tools (e.g., inspector for level chain).
- [ ] Add unit tests for key systems and patterns.

---

## 🔧 Native Plugin Build

The `/NativePlugin` folder contains a standalone C++ plugin setup:

```
build.bat          # DLL build script
build_native.bat   # Alternate build
CMakeLists.txt     # CMake configuration
NativePlugin.cpp   # Core source file
NativePlugin.h     # Header
```

The compiled `.dll` is loaded by Unity from `Plugins/NativePlugin`.

---

## 🧾 License

For educational and portfolio demonstration use only. Third-party assets/libraries belong to their respective owners.

