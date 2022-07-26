using System;
using System.Diagnostics.Contracts;

namespace Nanoray.Umbral.Constraints
{
    public interface IAnchor
    {
        IConstrainable Owner { get; }
        LinearExpression Expression { get; }

        [Pure]
        bool IsCompatibleWithAnchor(IAnchor other);

        public interface Length : IAnchor
        {
            [Pure]
            bool IAnchor.IsCompatibleWithAnchor(IAnchor other)
                => other is Length;
        }

        public interface Typed<in ConstrainableType> : IAnchor where ConstrainableType : IConstrainable
        {
            [Pure]
            Typed<ConstrainableType> GetSameAnchorInConstrainable(ConstrainableType constrainable);

            public interface Positional : Typed<ConstrainableType>
            {
                [Pure]
                bool IAnchor.IsCompatibleWithAnchor(IAnchor other)
                    => other is Positional;

                [Pure]
                new Positional GetSameAnchorInConstrainable(ConstrainableType constrainable);

                public interface WithOpposite : Positional
                {
                    [Pure]
                    new WithOpposite GetSameAnchorInConstrainable(ConstrainableType constrainable);

                    [Pure]
                    WithOpposite GetOppositeAnchorInConstrainable(ConstrainableType constrainable);
                }
            }

            public interface Length : Typed<ConstrainableType>, IAnchor.Length
            {
                [Pure]
                new Length GetSameAnchorInConstrainable(ConstrainableType constrainable);
            }
        }
    }

    public abstract class Anchor : IAnchor
    {
        public IConstrainable Owner { get; private set; }
        public LinearExpression Expression { get; private set; }
        private readonly string AnchorName;

        public Anchor(IConstrainable owner, LinearExpression expression, string anchorName)
        {
            this.Owner = owner;
            this.Expression = expression;
            this.AnchorName = anchorName;
        }

        /// <inheritdoc/>
        public override string ToString()
            => $"{Owner}.{AnchorName}";

        /// <inheritdoc/>
        public abstract bool IsCompatibleWithAnchor(IAnchor other);
    }

    public class EdgeAnchor<TConstrainable> : Anchor, IAnchor.Typed<TConstrainable>.Positional.WithOpposite
        where TConstrainable : IConstrainable
    {
        private Func<TConstrainable, IAnchor.Typed<TConstrainable>.Positional.WithOpposite> AnchorFunction { get; }
        private Func<TConstrainable, IAnchor.Typed<TConstrainable>.Positional.WithOpposite> OppositeAnchorFunction { get; }

        public EdgeAnchor(
            TConstrainable owner,
            LinearExpression expression,
            string anchorName,
            Func<TConstrainable, IAnchor.Typed<TConstrainable>.Positional.WithOpposite> anchorFunction,
            Func<TConstrainable, IAnchor.Typed<TConstrainable>.Positional.WithOpposite> oppositeAnchorFunction
        ) : base(owner, expression, anchorName)
        {
            this.AnchorFunction = anchorFunction;
            this.OppositeAnchorFunction = oppositeAnchorFunction;
        }

        /// <inheritdoc/>
        [Pure]
        public IAnchor.Typed<TConstrainable> GetSameAnchorInConstrainable(TConstrainable constrainable)
            => AnchorFunction(constrainable);

        /// <inheritdoc/>
        [Pure]
        IAnchor.Typed<TConstrainable>.Positional.WithOpposite IAnchor.Typed<TConstrainable>.Positional.WithOpposite.GetSameAnchorInConstrainable(TConstrainable constrainable)
            => AnchorFunction(constrainable);

        /// <inheritdoc/>
        [Pure]
        IAnchor.Typed<TConstrainable>.Positional IAnchor.Typed<TConstrainable>.Positional.GetSameAnchorInConstrainable(TConstrainable constrainable)
            => AnchorFunction(constrainable);

        /// <inheritdoc/>
        [Pure]
        public IAnchor.Typed<TConstrainable>.Positional.WithOpposite GetOppositeAnchorInConstrainable(TConstrainable constrainable)
            => OppositeAnchorFunction(constrainable);

        /// <inheritdoc/>
        public override bool IsCompatibleWithAnchor(IAnchor other)
            => other is IAnchor.Typed<TConstrainable>.Positional;
    }

    public class LengthAnchor<TConstrainable> : Anchor, IAnchor.Typed<TConstrainable>.Length
        where TConstrainable : IConstrainable
    {
        private Func<TConstrainable, IAnchor.Typed<TConstrainable>.Length> AnchorFunction { get; }

        public LengthAnchor(
            TConstrainable owner,
            LinearExpression expression,
            string anchorName,
            Func<TConstrainable, IAnchor.Typed<TConstrainable>.Length> anchorFunction
        ) : base(owner, expression, anchorName)
        {
            this.AnchorFunction = anchorFunction;
        }

        /// <inheritdoc/>
        [Pure]
        public IAnchor.Typed<TConstrainable> GetSameAnchorInConstrainable(TConstrainable constrainable)
            => AnchorFunction(constrainable);

        /// <inheritdoc/>
        [Pure]
        IAnchor.Typed<TConstrainable>.Length IAnchor.Typed<TConstrainable>.Length.GetSameAnchorInConstrainable(TConstrainable constrainable)
            => AnchorFunction(constrainable);

        /// <inheritdoc/>
        public override bool IsCompatibleWithAnchor(IAnchor other)
            => other is IAnchor.Typed<IConstrainable.Horizontal>.Length or IAnchor.Typed<IConstrainable.Vertical>.Length;
    }

    public class CenterAnchor<TConstrainable> : Anchor, IAnchor.Typed<TConstrainable>.Positional
        where TConstrainable : IConstrainable
    {
        private Func<TConstrainable, IAnchor.Typed<TConstrainable>.Positional> AnchorFunction { get; }

        public CenterAnchor(
            TConstrainable owner,
            LinearExpression expression,
            string anchorName,
            Func<TConstrainable, IAnchor.Typed<TConstrainable>.Positional> anchorFunction
        ) : base(owner, expression, anchorName)
        {
            this.AnchorFunction = anchorFunction;
        }

        /// <inheritdoc/>
        [Pure]
        public IAnchor.Typed<TConstrainable> GetSameAnchorInConstrainable(TConstrainable constrainable)
            => AnchorFunction(constrainable);

        /// <inheritdoc/>
        [Pure]
        IAnchor.Typed<TConstrainable>.Positional IAnchor.Typed<TConstrainable>.Positional.GetSameAnchorInConstrainable(TConstrainable constrainable)
            => AnchorFunction(constrainable);

        /// <inheritdoc/>
        public override bool IsCompatibleWithAnchor(IAnchor other)
            => other is IAnchor.Typed<TConstrainable>.Positional;
    }
}
