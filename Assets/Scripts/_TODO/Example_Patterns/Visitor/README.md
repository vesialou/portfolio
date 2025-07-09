# Tile Interaction – Visitor Pattern

## Problem

In a casual match-3 puzzle game, different tile types — like regular gems, blockers, and hidden chests — need to respond differently to the same user interaction (e.g. a tap, a hint animation, or auto-highlight).  
Currently, this logic is spread across `if`-checks and type switches inside UI scripts, which makes it hard to scale as more tile types or interactions are added.

## Goal

We need a clean way to **separate interaction triggers from tile behavior**, so new tile types can define their own responses without modifying the interaction system.

---

## Solution

Implemented using the **Visitor** pattern:

- Each tile type (`GemTile`, `BlockerTile`, `ChestTile`) accepts a visitor that encapsulates interaction logic.
- Visitors (`HighlightVisitor`, `HintVisitor`, `AnalysisVisitor`) define behavior for each tile type.
- The interaction system is decoupled from tile-specific logic.
- Adding new tile types or new interactions requires no changes to existing systems.

---

## Folder Structure

```
/Visitor
│
├── /Models
│   ├── TileModel.cs             // Core data model for tiles (type, position, health, state)
│   ├── TilePosition.cs          // Struct representing X/Y coordinates of a tile
│   └── TileType.cs              // Enum defining available tile types (Gem, Blocker, Chest)
├── /Tiles
│   ├── BaseTile.cs              // Abstract base class for all tile types
│   ├── GemTile.cs               // Concrete tile representing a gem with color
│   ├── BlockerTile.cs           // Tile representing a destructible or indestructible blocker
│   └── ChestTile.cs             // Tile representing a chest with open state and rewards
└── /Visitors
│   ├── ITileVisitor.cs          // Visitor interface for processing different tile types
│   ├── HighlightVisitor.cs      // Visitor for applying highlight visual effects
│   ├── HintVisitor.cs           // Visitor for showing hint suggestions and interactions
│   └── AnalysisVisitor.cs       // Example visitor for analyzing tiles (e.g. score, strategy)
├── TileVisitorService.cs        // Service for applying visitors to tile collections
├── TileInteractionController.cs // Coordinates tile initialization and interactions
└── Program.cs                   // Entry point
```