using UnityEngine;

public class PlayerDirection
{
    public enum Direction
    {
        SouthEast, SouthWest, NorthWest, NorthEast
    }

    private Direction current = Direction.SouthEast;
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
            Direction.SouthEast => new Vector2(1, -1).normalized,
            Direction.SouthWest => new Vector2(-1, -1).normalized,
            Direction.NorthWest => new Vector2(1, -1).normalized,
            Direction.NorthEast => new Vector2(1, 1).normalized,
            _ => Vector2.zero,
        };
    }


    public (bool, bool) IsSouthAndEastBooleanRepresentation()
    {
        return current switch
        {
            Direction.SouthEast => (true, true),
            Direction.SouthWest => (true, false),
            Direction.NorthWest => (false, false),
            Direction.NorthEast => (false, true),
            _ => (false, false),
        };
    }

    public void UpdateWithSouthAndEastBooleanRepresentation(bool south, bool east)
    {
        if (south)
        {
            if (east)
            {
                direction = Direction.SouthEast;
            }
            else
            {
                direction = Direction.SouthWest;
            }
        }
        else
        {
            if (east)
            {
                direction = Direction.NorthEast;
            }
            else
            {
                direction = Direction.NorthWest;
            }
        }
    }
}
