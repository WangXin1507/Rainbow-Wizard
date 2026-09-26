using System.Data;
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

    public static Color GetPaintColorColor(PaintColor color)
    {
        return color switch
        {
            PaintColor.NONE => new Color(0f, 0f, 0f, 1f),
            PaintColor.RED => new Color(1f, 0f, 0f, 1f),
            PaintColor.ORANGE => new Color(1f, 0.5f, 0f, 1f),
            PaintColor.YELLOW => new Color(1f, 1f, 0f, 1f),
            PaintColor.GREEN => new Color(0f, 1f, 0f, 1f),
            PaintColor.BLUE => new Color(0f, 0f, 1f, 1f),
            PaintColor.PURPLE => new Color(0.5f, 0f, 1f, 1f),
            PaintColor.ALL => new Color(1f, 1f, 1f, 1f),
            _ => new Color(0f, 0f, 0f, 1f),
        };
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
