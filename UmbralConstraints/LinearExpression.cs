using System;

namespace Nanoray.Umbral.Constraints
{
    public record LinearExpression : IEquatable<LinearExpression>
    {
        internal LinearExpression()
        {
        }

        public static LinearExpression operator -(LinearExpression expression)
            => new Multiplication(expression, new Constant(-1f));

        public static LinearExpression operator +(LinearExpression lhs, LinearExpression rhs)
            => new Addition(lhs, rhs);

        public static LinearExpression operator +(LinearExpression lhs, float scalar)
            => new Addition(lhs, new Constant(scalar));

        public static LinearExpression operator +(float scalar, LinearExpression rhs)
            => new Addition(new Constant(scalar), rhs);

        public static LinearExpression operator -(LinearExpression lhs, LinearExpression rhs)
            => new Subtraction(lhs, rhs);

        public static LinearExpression operator -(LinearExpression lhs, float scalar)
            => new Subtraction(lhs, new Constant(scalar));

        public static LinearExpression operator -(float scalar, LinearExpression rhs)
            => new Subtraction(new Constant(scalar), rhs);

        public static LinearExpression operator *(LinearExpression lhs, LinearExpression rhs)
            => new Multiplication(lhs, rhs);

        public static LinearExpression operator *(LinearExpression lhs, float scalar)
            => new Multiplication(lhs, new Constant(scalar));

        public static LinearExpression operator *(float scalar, LinearExpression rhs)
            => new Multiplication(new Constant(scalar), rhs);

        public sealed record Constant : LinearExpression
        {
            public float Value { get; init; }

            public Constant(float value)
            {
                this.Value = value;
            }
        }

        public sealed record Variable : LinearExpression
        {
            public string Name { get; init; }
            public IVariable<float> Value { get; init; }

            public Variable(string name, IVariable<float> value)
            {
                this.Name = name;
                this.Value = value;
            }
        }

        public sealed record Addition : LinearExpression
        {
            public LinearExpression Left { get; init; }
            public LinearExpression Right { get; init; }

            public Addition(LinearExpression left, LinearExpression right)
            {
                this.Left = left;
                this.Right = right;
            }
        }

        public sealed record Subtraction : LinearExpression
        {
            public LinearExpression Left { get; init; }
            public LinearExpression Right { get; init; }

            public Subtraction(LinearExpression left, LinearExpression right)
            {
                this.Left = left;
                this.Right = right;
            }
        }

        public sealed record Multiplication : LinearExpression
        {
            public LinearExpression Left { get; init; }
            public LinearExpression Right { get; init; }

            public Multiplication(LinearExpression left, LinearExpression right)
            {
                this.Left = left;
                this.Right = right;
            }
        }
    }
}
