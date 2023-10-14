using System;
using System.Numerics;

namespace Nanoray.Umbral.Constraints;

public interface ITypedPositionalAnchorWithOpposite<TBaseView, TViewSystem, TVector, TVectorComponent, TPriority, TMultiplier> : ITypedPositionalAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TPriority, TMultiplier>
    where TBaseView : IConstrainableView<TBaseView, TViewSystem, TVector, TVectorComponent, TPriority, TMultiplier>
    where TViewSystem : IConstraintViewSystem<TBaseView, TViewSystem, TVector, TVectorComponent, TPriority, TMultiplier>
    where TVector : struct
#if NET7_0_OR_GREATER
    where TVectorComponent : struct, INumber<TVectorComponent>
    where TPriority : struct, INumber<TPriority>
    where TMultiplier : struct, INumber<TMultiplier>
#else
    where TVectorComponent : struct, IComparable<TVectorComponent>
    where TPriority : struct, IComparable<TPriority>
    where TMultiplier : struct, IComparable<TMultiplier>
#endif
{
    new ITypedPositionalAnchorWithOpposite<TBaseView, TViewSystem, TVector, TVectorComponent, TPriority, TMultiplier> GetSameAnchor(TBaseView view);

    ITypedPositionalAnchorWithOpposite<TBaseView, TViewSystem, TVector, TVectorComponent, TPriority, TMultiplier> GetOppositeAnchor(TBaseView view);
}
