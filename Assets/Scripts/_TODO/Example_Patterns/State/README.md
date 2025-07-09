# Quest State – State Pattern

## Problem

in a casual game with daily quests, each quest tile needs to switch clearly between states like: locked, active, completed, claimed. these states control how the UI behaves and what logic is allowed (e.g., whether reward can be given).  
currently, the logic is scattered across flags and UI checks, which leads to inconsistent behavior when syncing or resetting.  
we need a simple state-driven structure to manage transitions and isolate per-state behavior.

---

## Solution

implemented using the **State** pattern:

- each quest state encapsulates its own behavior and transition rules  
- UI and logic delegate to the current state instance  
- transitions (e.g., `Unlock()`, `Complete()`, `Claim()`) are explicitly defined in the state machine  
- external code doesn't manipulate state directly — all changes go through the FSM

---

## Folder Structure

```
State/
│
├── States/
│   ├── IQuestState.cs              // interface for all quest states
│   ├── LockedQuestState.cs         // state: not yet unlocked
│   ├── ActiveQuestState.cs         // state: quest in progress
│   ├── CompletedQuestState.cs      // state: goal reached, ready to claim
│   └── ClaimedQuestState.cs        // state: reward collected
├── Core/
│   ├── QuestStateMachine.cs        // finite state machine managing quest state
│   ├── QuestData.cs                // data container for quest configuration
│   ├── QuestStateExtensions.cs     // extension methods for identifying state
├── DailyQuestTile.cs               // UI logic and visual updates for a quest tile
└── Program.cs                      // entry point
```