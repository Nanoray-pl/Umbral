using System;

namespace Nanoray.Umbral.Constraints.Anchors
{
    public abstract class BaseAnchor : IAnchor
    {
        public IConstrainable Owner { get; private set; }

        public LinearExpression Expression
            => ExpressionProvider();

        private readonly Func<LinearExpression> ExpressionProvider;
        private readonly string AnchorName;

        public BaseAnchor(IConstrainable owner, Func<LinearExpression> expressionProvider, string anchorName)
        {
            this.Owner = owner;
            this.ExpressionProvider = expressionProvider;
            this.AnchorName = anchorName;
        }

        public BaseAnchor(IConstrainable owner, LinearExpression expression, string anchorName) : this(owner, () => expression, anchorName)
        {
        }

        /// <inheritdoc/>
        public override string ToString()
            => $"{Owner}.{AnchorName}";

        /// <inheritdoc/>
        public abstract bool IsCompatibleWithAnchor(IAnchor other);
    }
}
