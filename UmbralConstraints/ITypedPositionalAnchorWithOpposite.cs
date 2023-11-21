using System;
using System.Numerics;

namespace Nanoray.Umbral.Constraints;

public interface ITypedPositionalAnchorWithOpposite<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue> : ITypedPositionalAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue>
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
    new ITypedPositionalAnchorWithOpposite<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue> GetSameAnchor(TBaseView view);

    ITypedPositionalAnchorWithOpposite<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue> GetOppositeAnchor(TBaseView view);
}
