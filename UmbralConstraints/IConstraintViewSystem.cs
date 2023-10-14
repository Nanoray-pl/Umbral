using System;
using System.Numerics;

namespace Nanoray.Umbral.Constraints;

public interface IConstraintViewSystem<TBaseView, TViewSystem, TVector, TVectorComponent, TPriority, TMultiplier> : IViewSystem<TBaseView, TViewSystem, TVector, TVectorComponent>
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
    TPriority DisabledPriority { get; }
    TPriority LowPriority { get; }
    TPriority MediumPriority { get; }
    TPriority HighPriority { get; }
    TPriority RequiredPriority { get; }

    TPriority MixPriorities(TPriority a, TPriority b, TMultiplier mix);
}
