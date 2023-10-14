using System;
using System.Numerics;

namespace Nanoray.Umbral.Constraints;

public interface ILengthAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TPriority, TMultiplier> : IAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TPriority, TMultiplier>
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
    bool IAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TPriority, TMultiplier>.IsCompatibleWithAnchor(IAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TPriority, TMultiplier> other)
        => other is ILengthAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TPriority, TMultiplier>;
}
