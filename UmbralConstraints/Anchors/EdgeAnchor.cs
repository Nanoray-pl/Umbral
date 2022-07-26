using System;
using System.Diagnostics.Contracts;

namespace Nanoray.Umbral.Constraints.Anchors
{
    public class EdgeAnchor<TConstrainable> : BaseAnchor, ITypedPositionalAnchorWithOpposite<TConstrainable>
        where TConstrainable : IConstrainable
    {
        private Func<TConstrainable, ITypedPositionalAnchorWithOpposite<TConstrainable>> AnchorFunction { get; }
        private Func<TConstrainable, ITypedPositionalAnchorWithOpposite<TConstrainable>> OppositeAnchorFunction { get; }

        public EdgeAnchor(
            TConstrainable owner,
            LinearExpression expression,
            string anchorName,
            Func<TConstrainable, ITypedPositionalAnchorWithOpposite<TConstrainable>> anchorFunction,
            Func<TConstrainable, ITypedPositionalAnchorWithOpposite<TConstrainable>> oppositeAnchorFunction
        ) : base(owner, expression, anchorName)
        {
            this.AnchorFunction = anchorFunction;
            this.OppositeAnchorFunction = oppositeAnchorFunction;
        }

        /// <inheritdoc/>
        [Pure]
        public ITypedAnchor<TConstrainable> GetSameAnchorInConstrainable(TConstrainable constrainable)
            => AnchorFunction(constrainable);

        /// <inheritdoc/>
        [Pure]
        ITypedPositionalAnchorWithOpposite<TConstrainable> ITypedPositionalAnchorWithOpposite<TConstrainable>.GetSameAnchorInConstrainable(TConstrainable constrainable)
            => AnchorFunction(constrainable);

        /// <inheritdoc/>
        [Pure]
        ITypedPositionalAnchor<TConstrainable> ITypedPositionalAnchor<TConstrainable>.GetSameAnchorInConstrainable(TConstrainable constrainable)
            => AnchorFunction(constrainable);

        /// <inheritdoc/>
        [Pure]
        public ITypedPositionalAnchorWithOpposite<TConstrainable> GetOppositeAnchorInConstrainable(TConstrainable constrainable)
            => OppositeAnchorFunction(constrainable);

        /// <inheritdoc/>
        public override bool IsCompatibleWithAnchor(IAnchor other)
            => other is ITypedPositionalAnchor<TConstrainable>;
    }
}
