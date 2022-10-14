using System;

public struct Point : IEquatable<Point>
{
    public int x;
    public int y;
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    bool IEquatable<Point>.Equals(Point other)
    {
        return x == other.x && y == other.y;
    }
    public static Point operator +(Point a, Point b) => new Point(a.x + b.x, a.y + b.y);
    public static Point operator /(Point a, int b) => new Point(a.x / b, a.y / b);

}
