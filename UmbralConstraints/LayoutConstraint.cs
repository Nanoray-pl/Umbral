using System;
using System.Collections.Generic;
using System.Numerics;

namespace Nanoray.Umbral.Constraints;

public sealed class LayoutConstraint<TBaseView, TViewSystem, TVector, TVectorComponent, TPriority, TMultiplier>
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
    public string? Identifier { get; }
    public TPriority Priority { get; }
    public IAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TPriority, TMultiplier> Anchor1 { get; }
    public IAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TPriority, TMultiplier>? Anchor2 { get; }
    public TMultiplier Constant { get; }
    public TMultiplier Multiplier { get; }
    public LayoutConstraintRelation Relation { get; }

    public TBaseView? Owner { get; private set; }

    public IReadOnlyCollection<IAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TPriority, TMultiplier>> Anchors
        => Anchor2 is null ? new[] { Anchor1 } : new[] { Anchor1, Anchor2 };
}
