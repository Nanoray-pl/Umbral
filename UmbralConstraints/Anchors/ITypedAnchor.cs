using System;
using System.Diagnostics.Contracts;

namespace Nanoray.Umbral.Constraints.Anchors
{
    public interface ITypedAnchor<in ConstrainableType> : IAnchor
        where ConstrainableType : IConstrainable
    {
        [Pure]
        ITypedAnchor<ConstrainableType> GetSameAnchorInConstrainable(ConstrainableType constrainable);
    }
}
