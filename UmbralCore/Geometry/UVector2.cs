using System;

namespace Nanoray.Umbral.Core.Geometry
{
    public readonly struct UVector2 : IEquatable<UVector2>
    {
        public static UVector2 Zero { get; } = new(0);
        public static UVector2 One { get; } = new(1);
        public static UVector2 UnitX { get; } = new(1, 0);
        public static UVector2 UnitY { get; } = new(0, 1);

        public float X { get; }
        public float Y { get; }

        public UVector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public UVector2(float v) : this(v, v)
        {
        }

        public void Deconstruct(out float x, out float y)
        {
            x = X;
            y = Y;
        }

        /// <inheritdoc/>
        public override string ToString()
            => $"{{{X}, {Y}}}";

        /// <inheritdoc/>
        public bool Equals(UVector2 other)
            => X == other.X && Y == other.Y;

        /// <inheritdoc/>
        public override bool Equals(object? obj)
            => obj is UVector2 other && Equals(other);

        /// <inheritdoc/>
        public override int GetHashCode()
            => (X, Y).GetHashCode();

        /// <inheritdoc/>
        public static bool operator ==(UVector2 left, UVector2 right)
            => left.Equals(right);

        /// <inheritdoc/>
        public static bool operator !=(UVector2 left, UVector2 right)
            => !(left == right);

        public static UVector2 operator -(UVector2 v)
            => new(-v.X, -v.Y);

        public static UVector2 operator +(UVector2 lhs, UVector2 rhs)
            => new(lhs.X + rhs.X, lhs.Y + rhs.Y);

        public static UVector2 operator -(UVector2 lhs, UVector2 rhs)
            => new(lhs.X - rhs.X, lhs.Y - rhs.Y);

        public static UVector2 operator *(UVector2 lhs, UVector2 rhs)
            => new(lhs.X * rhs.X, lhs.Y * rhs.Y);

        public static UVector2 operator *(UVector2 lhs, float rhs)
            => new(lhs.X * rhs, lhs.Y * rhs);

        public static UVector2 operator *(float lhs, UVector2 rhs)
            => new(lhs * rhs.X, lhs * rhs.Y);

        public static UVector2 operator /(UVector2 lhs, UVector2 rhs)
            => new(lhs.X / rhs.X, lhs.Y / rhs.Y);

        public static UVector2 operator /(UVector2 lhs, float rhs)
            => new(lhs.X / rhs, lhs.Y / rhs);

        public static implicit operator UVector2((float x, float y) t)
            => new(t.x, t.y);

        public static implicit operator UVector2(float v)
            => new(v);
    }
}
