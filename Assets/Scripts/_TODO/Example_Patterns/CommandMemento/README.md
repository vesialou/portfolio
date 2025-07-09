# Upgrade Queue – Command + Memento Pattern

## Problem

in an idle casual game, players can queue upgrade actions (like leveling up factory or power plant). each upgrade deducts resources instantly but only takes effect after a short delay (e.g., 2 seconds). during that window, players are allowed to undo one or more of the last queued upgrades. this gives them a chance to fix accidental clicks or rethink their strategy.  
currently, the logic is mixed between ui handlers and upgrade processors, making it hard to track the upgrade history or revert changes cleanly.  
we need a command-driven system with lightweight state snapshots to support undoing queued upgrades before they're committed.

## Solution

implemented using the **Command + Memento** pattern:

- each upgrade is represented as a command object that can be executed or undone  
- upgrade commands store lightweight snapshots of affected building components  
- a manager tracks pending and executed commands  
- queued upgrades can be rolled back using undo before they are finalized  
- undo history is bounded and separated from core game logic

## Folder Structure

```
/CommandMemento
│
├── /Core
│   ├── ICommand.cs                    // Command interfaces
│   ├── IBuildingComponent.cs          // Component interface
│   └── IUndoableCommand.cs            // Command interface
├── /Components
│   ├── ProductionComponent.cs         // Production logic
│   ├── WorkerComponent.cs             // Worker logic
│   ├── ProductionSnapshot.cs          // Snapshot: production
│   └── WorkerSnapshot.cs              // Snapshot: workers
├── /Building
│   ├── BuildingData.cs                // Building information
│   ├── ComponentBuilding.cs           // Building with components
│   └── ComponentBuildingMemento.cs    // Building state snapshot
├── /Commands
│   ├── ModifyProductionCommand.cs     // Change production
│   ├── ModifyWorkersCommand.cs        // Change workers
│   ├── UpgradeComponentBuildingCommand.cs // Upgrade building
│   └── CommandManager.cs              // Executes and tracks commands
└── Program.cs                         // Entry point
```