using System;
using System.Collections.Generic;

public class Maze
{
    private readonly Dictionary<(int x, int y), bool[]> _map;
    private int _x = 1;
    private int _y = 1;

    // Directions are ordered: Up, Right, Down, Left
    // bool[] indicates if you can move in that direction from the cell
    public Maze(Dictionary<(int, int), bool[]> map)
    {
        _map = map;
    }

    public string GetStatus()
    {
        return $"Current location (x={_x}, y={_y})";
    }

    public void MoveUp()
    {
        TryMove(0, -1, 0);
    }

    public void MoveRight()
    {
        TryMove(1, 0, 1);
    }

    public void MoveDown()
    {
        TryMove(0, 1, 2);
    }

    public void MoveLeft()
    {
        TryMove(-1, 0, 3);
    }

    private void TryMove(int dx, int dy, int directionIndex)
    {
        var currentCell = (_x, _y);

        if (!_map.ContainsKey(currentCell))
            throw new InvalidOperationException("Current position not in map!");

        // Check if movement is allowed in this direction
        if (!_map[currentCell][directionIndex])
            throw new InvalidOperationException("Can't go that way!");

        // Calculate new position
        var newX = _x + dx;
        var newY = _y + dy;

        if (!_map.ContainsKey((newX, newY)))
            throw new InvalidOperationException("Can't go that way!");

        // Check if the opposite direction is allowed from the new cell (optional)
        // (Could be omitted if map is consistent)

        _x = newX;
        _y = newY;
    }
}
