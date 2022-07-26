using System;
using System.Diagnostics.Contracts;

namespace Nanoray.Umbral.Constraints.Anchors
{
    public interface ILengthAnchor : IAnchor
    {
        [Pure]
        bool IAnchor.IsCompatibleWithAnchor(IAnchor other)
            => other is ILengthAnchor;
    }
}
