using System;
using System.Numerics;
using Nanoray.Umbral.Constraints;

namespace Nanoray.Umbral.TwoD.Constraints;

public interface IConstrainableView2D<TBaseView, TViewSystem, TVector, TVectorComponent, TPriority, TMultiplier> : IView<TBaseView, TViewSystem, TVector, TVectorComponent>
    where TBaseView : IConstrainableView<TBaseView, TViewSystem, TVector, TVectorComponent, TPriority, TMultiplier>, IView2D<TBaseView, TViewSystem, TVector, TVectorComponent>
    where TViewSystem : IConstraintViewSystem<TBaseView, TViewSystem, TVector, TVectorComponent, TPriority, TMultiplier>, IViewSystem2D<TBaseView, TViewSystem, TVector, TVectorComponent>
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
}
