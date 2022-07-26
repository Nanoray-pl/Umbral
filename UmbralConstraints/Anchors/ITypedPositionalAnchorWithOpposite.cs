using System.Diagnostics.Contracts;

namespace Nanoray.Umbral.Constraints.Anchors
{
    public interface ITypedPositionalAnchorWithOpposite<in ConstrainableType> : ITypedPositionalAnchor<ConstrainableType>
        where ConstrainableType : IConstrainable
    {
        [Pure]
        new ITypedPositionalAnchorWithOpposite<ConstrainableType> GetSameAnchorInConstrainable(ConstrainableType constrainable);

        [Pure]
        ITypedPositionalAnchorWithOpposite<ConstrainableType> GetOppositeAnchorInConstrainable(ConstrainableType constrainable);
    }
}
