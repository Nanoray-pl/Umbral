using System;
using System.Diagnostics.Contracts;

namespace Nanoray.Umbral.Constraints.Anchors
{
    public class CenterAnchor<TConstrainable> : BaseAnchor, ITypedPositionalAnchor<TConstrainable>
        where TConstrainable : IConstrainable
    {
        private Func<TConstrainable, ITypedPositionalAnchor<TConstrainable>> AnchorFunction { get; }

        public CenterAnchor(
            TConstrainable owner,
            Func<LinearExpression> expressionProvider,
            string anchorName,
            Func<TConstrainable, ITypedPositionalAnchor<TConstrainable>> anchorFunction
        ) : base(owner, expressionProvider, anchorName)
        {
            this.AnchorFunction = anchorFunction;
        }

        public CenterAnchor(
            TConstrainable owner,
            LinearExpression expression,
            string anchorName,
            Func<TConstrainable, ITypedPositionalAnchor<TConstrainable>> anchorFunction
        ) : this(owner, () => expression, anchorName, anchorFunction)
        {
        }

        /// <inheritdoc/>
        [Pure]
        public ITypedAnchor<TConstrainable> GetSameAnchorInConstrainable(TConstrainable constrainable)
            => AnchorFunction(constrainable);

        /// <inheritdoc/>
        [Pure]
        ITypedPositionalAnchor<TConstrainable> ITypedPositionalAnchor<TConstrainable>.GetSameAnchorInConstrainable(TConstrainable constrainable)
            => AnchorFunction(constrainable);

        /// <inheritdoc/>
        public override bool IsCompatibleWithAnchor(IAnchor other)
            => other is ITypedPositionalAnchor<TConstrainable>;
    }
}
