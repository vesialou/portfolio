# Concurrent Asset Cache Example

## 📍 Namespace
`ExampleConcurrentDictionary`

Demonstrates a thread-safe asset caching system using `ConcurrentDictionary` and async loading via `UniTask`.

---

## 🧩 Overview

- Uses `ConcurrentDictionary<string, AssetWrapper>` for caching.
- Loads assets in parallel using `UniTask.RunOnThreadPool`.
- Each asset is processed and stored with its raw data.

---

## 🧠 Key Components

### `SomeAssetCache`
- Main caching class.
- Loads assets asynchronously via `LoadAssetsAsync`.
- Caches results and allows lookup via `TryGetFromCache`.

### `AssetWrapper`
- Contains asset path and raw byte data.

### Interfaces
- `IAssetDataLoader`: loads bytes from a given path.
- `IAssetProcessor`: processes raw asset data after load.
- `IAssetFactory`: (reserved for object creation, not used here).

---

## ✅ Example Usage

```csharp
var cache = new SomeAssetCache(processor, loader);
await cache.LoadAssetsAsync(new[] { "path1", "path2" }, cancellationToken);

var asset = cache.TryGetFromCache("path1");
