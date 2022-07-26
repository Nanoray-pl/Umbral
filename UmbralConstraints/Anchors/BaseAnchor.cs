using System;

namespace Nanoray.Umbral.Constraints.Anchors
{
    public abstract class BaseAnchor : IAnchor
    {
        public IConstrainable Owner { get; private set; }
        public LinearExpression Expression { get; private set; }
        private readonly string AnchorName;

        public BaseAnchor(IConstrainable owner, LinearExpression expression, string anchorName)
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
}
