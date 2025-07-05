using UnityEngine;

public class PlayerDirection
{
    public enum Direction
    {
        Down, Left, Up, Right
    }

    private Direction current = Direction.Down;
    private Direction previous;
    
    public Direction direction
    {
        get
        {
            return current;
        }

        set
        {
            previous = current;
            current = value;
        }
    } 

    public bool HasChanged()
    {
        return current != previous;
    }

    public string GetDirectionString()
    {
        return direction.ToString().ToLower();
    }


    public Vector2 GetVectorDirection()
    {
        return current switch
        {
            Direction.Down => Vector2.down,
            Direction.Left => Vector2.left,
            Direction.Up => Vector2.up,
            Direction.Right => Vector2.right,
            _ => Vector2.zero,
        };
    }
}
