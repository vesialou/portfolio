# Example_ConcurrentDictionary

## 📦 Overview

This example demonstrates a multithreaded asset caching system in Unity using `ConcurrentDictionary` and asynchronous loading via `UniTask`.

It is designed to:
- Load multiple assets in parallel
- Cache raw data per asset path
- Allow fast retrieval of previously loaded assets

---

## 🔧 Components

### `SomeAssetCache.cs`

Main class that manages loading and caching.

```csharp
var cache = new SomeAssetCache(processor, loader);
await cache.LoadAssetsAsync(assetPaths, cancellationToken);
var cached = cache.TryGetFromCache("path/to/asset");
