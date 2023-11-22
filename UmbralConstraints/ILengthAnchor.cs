using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Nanoray.Umbral.Constraints;

public interface ILengthAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue> : IAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue>
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
    new ILengthAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue> GetSameAnchor(TBaseView view);

    IAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue> IAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue>.GetSameAnchor(TBaseView view)
        => GetSameAnchor(view);

    LayoutConstraint<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue> MakeConstraint(
        LayoutConstraintRelation relation,
        ILengthAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue> other,
        string? identifier = null,
        TConstraintValue? multiplier = null,
        TConstraintValue? constant = null,
        TConstraintPriority? priority = null,
        [CallerFilePath] string? callerFilePath = null,
        [CallerMemberName] string? callerMemberName = null,
        [CallerLineNumber] int? callerLineNumber = null
    );
}
