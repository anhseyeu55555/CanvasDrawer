# Canvas Drawer

A simple console-based drawing program built with C# and Clean Architecture.

## Features

- Create a canvas with custom dimensions
- Draw horizontal and vertical lines
- Draw rectangle outlines
- Bucket fill (flood fill) areas with a character
- Clean, extensible architecture using SOLID principles

## Commands

| Command | Description |
|---------|-------------|
| `C w h` | Create a new canvas with width `w` and height `h` |
| `L x1 y1 x2 y2` | Draw a line from `(x1, y1)` to `(x2, y2)` |
| `R x1 y1 x2 y2` | Draw a rectangle with corners at `(x1, y1)` and `(x2, y2)` |
| `B x y c` | Fill the area at `(x, y)` with character `c` |
| `Q` | Quit the application |

## Usage
- Enter command: C 20 4
- Enter command: L 1 2 6 2
- Enter command: Q


## Architecture
CanvasDrawer/  
├── Domain/ # Core business logic  
│ ├── Canvas.cs # Canvas entity  
│ ├── Point.cs # Value object  
│ └── CanvasConstants.cs # Constants  
├── Commands/ # Command layer  
│ ├── DrawingOperations/ # Drawing strategies  
│ ├── CanvasCommand.cs # Command classes  
│ └── CommandParser.cs # Input parser  
└── Application/ # Application layer  
└── DrawingApp.cs # Main application

## Running
dotnet run

## Requirements
.NET 8.0 or later
