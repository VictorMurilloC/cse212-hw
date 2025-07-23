using System;
using System.Collections.Generic;

public class Maze
{
    private readonly Dictionary<(int x, int y), bool[]> _map;
    private (int x, int y) _currentPosition;

    public Maze(Dictionary<(int x, int y), bool[]> map)
    {
        _map = map;
        _currentPosition = (1, 1);
    }

    public string GetStatus()
    {
        return $"Current location (x={_currentPosition.x}, y={_currentPosition.y})";
    }

    private void CheckMove(string direction, (int x, int y) newPosition, int index)
    {
        if (!_map.ContainsKey(_currentPosition))
        {
            throw new InvalidOperationException("Current position not in maze.");
        }

        var walls = _map[_currentPosition];
        if (!walls[index]) // If there's no wall in the direction
        {
            if (!_map.ContainsKey(newPosition))
            {
                throw new InvalidOperationException("Can't go that way!");
            }
            _currentPosition = newPosition;
        }
        else
        {
            throw new InvalidOperationException("Can't go that way!");
        }
    }

    public void MoveUp()
    {
        CheckMove("up", (_currentPosition.x, _currentPosition.y - 1), 0);
    }

    public void MoveRight()
    {
        CheckMove("right", (_currentPosition.x + 1, _currentPosition.y), 1);
    }

    public void MoveDown()
    {
        CheckMove("down", (_currentPosition.x, _currentPosition.y + 1), 2);
    }

    public void MoveLeft()
    {
        CheckMove("left", (_currentPosition.x - 1, _currentPosition.y), 3);
    }
}
