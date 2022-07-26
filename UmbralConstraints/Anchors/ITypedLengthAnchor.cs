using System.Diagnostics.Contracts;

namespace Nanoray.Umbral.Constraints.Anchors
{
    public interface ITypedLengthAnchor<in ConstrainableType> : ITypedAnchor<ConstrainableType>, ILengthAnchor
        where ConstrainableType : IConstrainable
    {
        [Pure]
        new ITypedLengthAnchor<ConstrainableType> GetSameAnchorInConstrainable(ConstrainableType constrainable);
    }
}
