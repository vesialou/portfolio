# Win Conditions – Composite Pattern

## Problem

in a casual puzzle game, level designers want to define win conditions using simple rule-like expressions such as  
“collect 5 red gems AND defeat the slime boss” or  
“break all ice tiles OR collect the golden key AND activate the portal”.  
currently, win logic is hardcoded per level, which limits scalability and makes iteration slow.  
we need a system to parse and evaluate such expressions at runtime so designers can define conditions externally without modifying game code.

## Goals

- support composability of simple and complex win rules (AND, OR, nested)  
- evaluate conditions at runtime using context data (e.g. “HasItem”, “DefeatedEnemy”)  
- allow non-programmers (e.g. designers) to define or edit conditions externally (future goal)  
- make condition logic isolated, testable and reusable  
- lay groundwork for serializable, inspectable conditions later (not implemented now)

## Solution

implemented using the **Composite** pattern:

- each win condition implements a shared interface for evaluation  
- leaf conditions encapsulate specific checks like item collection or boss defeat  
- composite conditions (AND / OR) combine other conditions and recursively evaluate logic  
- builder class supports creating testable win condition structures  
- runtime controller checks game state and evaluates win status on request

## Folder Structure

```
Composite/
│
├── Conditions/                // Leaf conditions for win logic
│   ├── CollectItemCondition.cs      // Checks item collection count
│   ├── DefeatEnemyCondition.cs      // Checks if enemy is defeated
│   ├── ActivateObjectCondition.cs   // Checks if object is activated
│   ├── BreakAllTilesCondition.cs    // Checks if all tiles are broken
├── Composites/               // Composite conditions (AND / OR)
│   ├── AndCondition.cs             // Evaluates multiple conditions with AND
│   ├── OrCondition.cs              // Evaluates multiple conditions with OR
├── Builder/                  // Fluent or static builder
│   └── WinConditionBuilder.cs      // Simplifies creation of complex conditions
├── Core/                     // Core interfaces and state
│   ├── IWinCondition.cs           // Interface for all win conditions
│   ├── GameState.cs               // Holds current game state
│   └── WinConditionController.cs  // Manages evaluation and win check
└── Program.cs                // Entry point for running the example
```