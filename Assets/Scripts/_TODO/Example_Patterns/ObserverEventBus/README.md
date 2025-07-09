# Special Gem Event Flow – Observer / Event Bus Pattern

## Problem

in a casual match-3 game with temporary power-ups and limited-time events, multiple systems need to respond when the player matches a special gem (e.g., a bomb gem). the UI needs to show a visual effect, the score system must apply a bonus multiplier, active quests may progress, and analytics should log the event. currently, this is handled by tightly coupled method calls, making it fragile and hard to maintain when more systems are added. we need a centralized event bus to broadcast “special gem matched” events to all interested listeners in a decoupled and ordered way.

---

## Solution

implemented using the **Observer / Event Bus** pattern:

- each listener implements `IEventHandler<T>` to subscribe to events.
- `EventBus` allows dynamic subscription and publishing.
- events are strongly typed via `IGameEvent`.
- multiple systems can handle the same event independently.
- avoids `UnityEvent` and direct `C# event` coupling.
- priority support is optional and not included in this minimal version.

---

## Folder Structure

```
/ObserverEventBus
│
├── EventBus/                        # event system core
│   ├── IEventBus.cs                // defines event publish/subscribe interface
│   ├── IEventHandler.cs            // generic event handler interface
│   ├── IGameEvent.cs               // base marker interface for all game events
│   ├── EventPriority.cs            // optional priorities for handlers (not required in this sample)
│   └── EventBus.cs                 // central event bus implementation
├── Events/                         # specific event definitions
│   ├── SpecialGemMatchedEvent.cs  // dispatched when a special gem is matched
│   ├── ScoreChangedEvent.cs       // dispatched when player score changes
│   └── QuestProgressEvent.cs      // dispatched when a quest progresses
├── Controllers/                    # logic reacting to events
│   ├── ScoreController.cs         // listens to scoring-related events
│   ├── QuestController.cs         // handles quest updates and triggers
│   └── AnalyticsController.cs     // logs gameplay actions for analytics
├── Models/                         # passive data-holding classes
│   ├── ScoreModel.cs              // tracks and stores player score
│   ├── QuestModel.cs              // stores active quest state
│   ├── QuestData.cs               // defines quest type and goal
│   ├── AnalyticsModel.cs          // mocks logging to analytics system
│   ├── GemType.cs                 // enum: available gem types
│   └── QuestType.cs               // enum: available quest types
├── GameService.cs                  // composition root: wires controllers + event bus
└── Program.cs                      // entry point: simulates gameplay and test events
```