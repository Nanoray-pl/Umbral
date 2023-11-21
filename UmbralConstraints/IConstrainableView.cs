using System;
using System.Collections.Generic;
using System.Numerics;

namespace Nanoray.Umbral.Constraints;

public interface IConstrainableView<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue> : IView<TBaseView, TViewSystem, TVector, TVectorComponent>
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
    IReadOnlySet<LayoutConstraint<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue>> OwnedConstraints { get; }

    IReadOnlySet<LayoutConstraint<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue>> DirectlyAffectingConstraints { get; }
}
