using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Nanoray.Umbral.Constraints;

public interface IPositionalAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue, TAxis> : IAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue>
    where TBaseView : IConstrainableView<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue>
    where TViewSystem : IConstraintViewSystem<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue>
    where TVector : struct
    where TConstraintPriority : struct, IComparable<TConstraintPriority>, IEquatable<TConstraintPriority>
    where TAxis : IAxis
#if NET7_0_OR_GREATER
    where TVectorComponent : struct, INumber<TVectorComponent>
    where TConstraintValue : struct, INumber<TConstraintValue>
#else
    where TVectorComponent : struct, IComparable<TVectorComponent>, IEquatable<TVectorComponent>
    where TConstraintValue : struct, IComparable<TConstraintValue>, IEquatable<TConstraintValue>
#endif
{
    new IPositionalAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue, TAxis> GetSameAnchor(TBaseView view);

    IAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue> IAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue>.GetSameAnchor(TBaseView view)
        => GetSameAnchor(view);

    IPositionalAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue, TAxis> GetOppositeAnchor(TBaseView view);

    LayoutConstraint<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue> MakeConstraint(
        LayoutConstraintRelation relation,
        IPositionalAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue, TAxis> other,
        string? identifier = null,
        TConstraintValue? constant = null,
        TConstraintPriority? priority = null,
        [CallerFilePath] string? callerFilePath = null,
        [CallerMemberName] string? callerMemberName = null,
        [CallerLineNumber] int? callerLineNumber = null
    );
}
