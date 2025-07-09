# Example_SemaphoreSlim

## 🧩 Overview

This example shows how to use `SemaphoreSlim` to limit concurrent analytics requests in Unity using async tasks via `UniTask`.

---

## 🧠 Key Concepts

- Limits parallel calls to analytics with `SemaphoreSlim(2, 2)`
- Ensures thread-safe access to async `SendAsync` calls
- Prevents overload during burst events (e.g. multiple UI clicks)

---

## 📦 Components

### `AnalyticsService.cs`
- Singleton analytics manager
- Tracks events like:
  - `level_start`
  - `level_complete`
  - `purchase`
- Serializes and sends JSON payloads via injected `IAnalyticsSender`

### `SomeGameUI.cs`
- Simulates game events:
  - Start level
  - Complete level
  - Purchase item
- Calls tracking methods on button press

---

## 📋 Example

```csharp
AnalyticsService.Instance.TrackLevelStart(1);
AnalyticsService.Instance.TrackPurchase("item0001", 0.99m);
```

## ✅ Dependencies
- Cysharp UniTask