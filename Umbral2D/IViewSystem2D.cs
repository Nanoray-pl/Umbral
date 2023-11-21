using System;
using System.Numerics;

namespace Nanoray.Umbral.TwoD;

public interface IViewSystem2D<TBaseView, TViewSystem, TVector, TVectorComponent> : IViewSystem<TBaseView, TViewSystem, TVector, TVectorComponent>
    where TBaseView : IView2D<TBaseView, TViewSystem, TVector, TVectorComponent>
    where TViewSystem : IViewSystem2D<TBaseView, TViewSystem, TVector, TVectorComponent>
    where TVector : struct
#if NET7_0_OR_GREATER
    where TVectorComponent : struct, INumber<TVectorComponent>
#else
    where TVectorComponent : struct, IComparable<TVectorComponent>, IEquatable<TVectorComponent>
#endif
{
    TVectorComponent GetX(TVector vector);
    TVectorComponent GetY(TVector vector);
    void SetX(ref TVector vector, TVectorComponent value);
    void SetY(ref TVector vector, TVectorComponent value);
}
