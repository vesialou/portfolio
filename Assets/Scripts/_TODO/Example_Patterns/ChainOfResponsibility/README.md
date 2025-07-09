# Level Adaptation – Chain of Responsibility

## Problem

 level needs to be modified step by step before runtime, starting from designer rules, then adjusted based on current mode like bonus or intense, then FTUE logic is applied if needed, and finally seasonal visuals are injected if any event is active. the steps affect the same level data and must be applied in a strict order, while staying decoupled and extendable.

---

## Solution

Implemented using the **Chain of Responsibility** pattern:

- Each transformation is a handler with a single responsibility.
- Handlers are linked via `SetNext(...)`.
- The chain is **lazy-initialized** only when first used.
- Processing is done via a single `ProcessLevel(ref LevelData)` call.

---

## Folder Structure
```
ChainOfResponsibility/
│
├── Handlers/
│   ├── LevelAdaptationHandler.cs     // Base class for all handlers
│   ├── ValidationHandler.cs          // Ensures values are within allowed ranges
│   ├── FTUEHandler.cs                // Adjusts level for first-time users
│   ├── DifficultyHandler.cs          // Tweaks difficulty based on player state
│   └── MonetizationHandler.cs        // Applies VIP bonuses, failure rewards, etc.
├── LevelAdaptationChain.cs           // Lazy-initialized handler chain setup
└── Program.cs                        // Entry point
```