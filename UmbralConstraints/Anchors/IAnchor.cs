using System.Diagnostics.Contracts;

namespace Nanoray.Umbral.Constraints.Anchors
{
    public interface IAnchor
    {
        IConstrainable Owner { get; }
        LinearExpression Expression { get; }

        [Pure]
        bool IsCompatibleWithAnchor(IAnchor other);
    }
}
