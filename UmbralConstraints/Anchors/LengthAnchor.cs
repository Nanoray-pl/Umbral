using System;
using System.Diagnostics.Contracts;

namespace Nanoray.Umbral.Constraints.Anchors
{
    public class LengthAnchor<TConstrainable> : BaseAnchor, ITypedLengthAnchor<TConstrainable>
        where TConstrainable : IConstrainable
    {
        private Func<TConstrainable, ITypedLengthAnchor<TConstrainable>> AnchorFunction { get; }

        public LengthAnchor(
            TConstrainable owner,
            Func<LinearExpression> expressionProvider,
            string anchorName,
            Func<TConstrainable, ITypedLengthAnchor<TConstrainable>> anchorFunction
        ) : base(owner, expressionProvider, anchorName)
        {
            this.AnchorFunction = anchorFunction;
        }

        public LengthAnchor(
            TConstrainable owner,
            LinearExpression expression,
            string anchorName,
            Func<TConstrainable, ITypedLengthAnchor<TConstrainable>> anchorFunction
        ) : this(owner, () => expression, anchorName, anchorFunction)
        {
        }

        /// <inheritdoc/>
        [Pure]
        public ITypedAnchor<TConstrainable> GetSameAnchorInConstrainable(TConstrainable constrainable)
            => AnchorFunction(constrainable);

        /// <inheritdoc/>
        [Pure]
        ITypedLengthAnchor<TConstrainable> ITypedLengthAnchor<TConstrainable>.GetSameAnchorInConstrainable(TConstrainable constrainable)
            => AnchorFunction(constrainable);

        /// <inheritdoc/>
        public override bool IsCompatibleWithAnchor(IAnchor other)
            => other is ILengthAnchor;
    }
}
