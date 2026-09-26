using UnityEngine;

public class PaintColorUtil
{
    public static bool ContainsBlue(PaintColor color)
    {
        if (color == PaintColor.BLUE ||
            color == PaintColor.GREEN ||
            color == PaintColor.PURPLE ||
            color == PaintColor.ALL)
        {
            return true;
        }
        return false;
    }

    public static bool ContainsYellow(PaintColor color)
    {
        if (color == PaintColor.YELLOW ||
            color == PaintColor.GREEN ||
            color == PaintColor.ORANGE ||
            color == PaintColor.ALL)
        {
            return true;
        }
        return false;
    }

    public static bool ContainsRed(PaintColor color)
    {
        if (color == PaintColor.RED ||
            color == PaintColor.ORANGE ||
            color == PaintColor.PURPLE ||
            color == PaintColor.ALL)
        {
            return true;
        }
        return false;
    }
}

public enum PaintColor
{
    NONE,
    RED,
    ORANGE,
    YELLOW,
    GREEN,
    BLUE,
    PURPLE,
    ALL
}
