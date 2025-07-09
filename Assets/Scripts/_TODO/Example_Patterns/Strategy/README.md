# Offer Strategy – Strategy Pattern

## Problem

we need to choose an offer strategy after level end based on player behavior signals such as repeatedly skipping dialogs or animations, restarting the same level manually more than once, going idle for a long time before losing, or failing a level despite using an active power-up. each of these cases reflects a different player state and should map to a specific offer strategy like retry encouragement, helper pack, or soft boost.

---

## Solution

Implemented using the **Strategy** pattern:

- Each offer strategy implements `IOfferStrategy`
- `OfferManager` chooses the strategy based on evaluated behavior flags
- `PlayerBehaviorTracker` tracks raw actions (skips, restarts, idle time, boost usage)
- Behavior is evaluated into flags via `Evaluate()`, avoiding magic conditions in the selector
- Mock simulation can be run via `Program.cs` for fast testing

---

## Folder Structure
```
 Strategy/
│
├── Strategies/
│   ├── IOfferStrategy.cs              // Interface
│   ├── RetryOfferStrategy.cs          // "Try again" offer
│   ├── HelpPackOfferStrategy.cs       // Help bundle offer
│   ├── SoftMotivationOfferStrategy.cs // Encouraging gift offer
│   └── BoostRefundOfferStrategy.cs    // Refund/compensation offer
├── PlayerBehaviorTracker.cs           // Tracks user actions
├── OfferManager.cs                    // Selects and applies strategy
├── PlayerBehaviorMock.cs              // Mock for player behavior signals
├── OfferStrategyFactory.cs            // Factory to create strategies
└── Program.cs                         // Entry point
```