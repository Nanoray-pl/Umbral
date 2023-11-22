using System;
using System.Numerics;
using Nanoray.Umbral.Constraints;

namespace Nanoray.Umbral.TwoD.Constraints;

public interface IConstrainableView2D<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue> : IView<TBaseView, TViewSystem, TVector, TVectorComponent>
    where TBaseView : IConstrainableView<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue>, IView2D<TBaseView, TViewSystem, TVector, TVectorComponent>
    where TViewSystem : IConstraintViewSystem<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue>, IViewSystem2D<TBaseView, TViewSystem, TVector, TVectorComponent>
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
    IPositionalAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue, IAxis2D.IHorizontal> MinXAnchor { get; }
    IPositionalAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue, IAxis2D.IHorizontal> MaxXAnchor { get; }
    IPositionalAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue, IAxis2D.IVertical> MinYAnchor { get; }
    IPositionalAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue, IAxis2D.IVertical> MaxYAnchor { get; }
    ILengthAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue> WidthAnchor { get; }
    ILengthAnchor<TBaseView, TViewSystem, TVector, TVectorComponent, TConstraintPriority, TConstraintValue> HeightAnchor { get; }
}
