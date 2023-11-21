using System;
using System.Collections.Generic;
using System.Numerics;

namespace Nanoray.Umbral.Constraints;

public sealed class LayoutConstraint<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue>
    where TBaseView : IConstrainableView<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue>
    where TViewSystem : IConstraintViewSystem<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue>
    where TVector : struct
    where TConstraintPriority : struct, IComparable<TConstraintPriority>, IEquatable<TConstraintPriority>
#if NET7_0_OR_GREATER
    where TVectorComponent : struct, INumber<TVectorComponent>
    where TConstraintValue : struct, INumber<TConstraintValue>
#else
    where TVectorComponent : struct, IComparable<TVectorComponent>, IEquatable<TVectorComponent>
    where TConstraintValue : struct, IComparable<TConstraintValue>, IEquatable<TConstraintValue>
#endif
{
    public string? Identifier { get; }
    public TConstraintPriority Priority { get; }
    public IAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue> Anchor1 { get; }
    public IAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue>? Anchor2 { get; }
    public TConstraintValue Constant { get; }
    public TConstraintValue Multiplier { get; }
    public LayoutConstraintRelation Relation { get; }

    public TBaseView? Owner { get; private set; }

    public IReadOnlyCollection<IAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue>> Anchors
        => Anchor2 is null ? new[] { Anchor1 } : new[] { Anchor1, Anchor2 };
}
