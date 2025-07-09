# PlayerLoop Customization Example

## 🧩 Overview

This scene demonstrates **runtime modification of Unity's PlayerLoop** to inject custom update logic using `PlayerLoopSystem`. It allows fine-grained control over the execution order of systems, useful for profiling, custom systems, or performance monitoring.

---

## 📍 Scene Location

`Assets/Scenes/PlayerLoop.unity`

---

## 🔧 Components

### `PlayerLoopModifier.cs`
- Adds two custom systems:
  - `CustomUpdateSystem` → inserted at the start of `Update`
  - `CustomLateUpdateSystem` → inserted at the end of `PreLateUpdate`
- Monitors framerate, logs slow frames
- Outputs PlayerLoop structure before/after modification
- Restores original loop on object destroy or via context menu

### `MemoryMonitorSystem.cs`
- Adds a `MemoryMonitor` system into `PostLateUpdate`
- Logs memory usage every 5 seconds
- Warns when usage exceeds 100MB
- Uses `System.GC.GetTotalMemory()` for live monitoring

---

## 🧪 Use Cases

- Add diagnostics/profiling to the update cycle
- Hook into specific phases without MonoBehaviour overhead
- Dynamically analyze or adjust execution flow

---

## 🛠️ Notes

- PlayerLoop modifications apply globally
- Systems are inserted by modifying `PlayerLoopSystem.subSystemList`
- Resets to default when the MonoBehaviour is destroyed

---

## 🧵 Requirements

- Unity 2020.1+ (for full `PlayerLoopSystem` support)
- No additional packages needed
