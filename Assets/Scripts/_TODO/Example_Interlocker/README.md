# Example_Interlocker

## 🎯 Purpose

This example demonstrates safe multi-threaded asset preloading using `Interlocked.Increment` for progress tracking and `UniTask` for async flow in Unity.

---

## 🔧 Key Class: `SomeAssetPreloader`

Handles asynchronous loading of multiple assets while tracking progress in a thread-safe manner.

### ✅ Features

- Loads 6 assets in parallel via `UniTask.WhenAll`
- Tracks load completion using `Interlocked.Increment`
- Reports progress to UI via event callbacks
- Invokes completion events upon finish

---

## 🔐 Thread Safety

`_loadedAssets` is updated using:

```csharp
Interlocked.Increment(ref _loadedAssets);
