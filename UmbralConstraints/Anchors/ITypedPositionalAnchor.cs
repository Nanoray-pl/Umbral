using System.Diagnostics.Contracts;

namespace Nanoray.Umbral.Constraints.Anchors
{
    public interface ITypedPositionalAnchor<in ConstrainableType> : ITypedAnchor<ConstrainableType>
        where ConstrainableType : IConstrainable
    {
        [Pure]
        bool IAnchor.IsCompatibleWithAnchor(IAnchor other)
            => other is ITypedPositionalAnchor<ConstrainableType>;

        [Pure]
        new ITypedPositionalAnchor<ConstrainableType> GetSameAnchorInConstrainable(ConstrainableType constrainable);
    }
}
