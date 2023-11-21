using System;
using System.Numerics;

namespace Nanoray.Umbral;

public interface IViewSystem<TBaseView, TViewSystem, TVector, TVectorComponent>
    where TBaseView : IView<TBaseView, TViewSystem, TVector, TVectorComponent>
    where TViewSystem : IViewSystem<TBaseView, TViewSystem, TVector, TVectorComponent>
    where TVector : struct
#if NET7_0_OR_GREATER
    where TVectorComponent : struct, INumber<TVectorComponent>
#else
    where TVectorComponent : struct, IComparable<TVectorComponent>, IEquatable<TVectorComponent>
#endif
{
#if NET7_0_OR_GREATER
    TVectorComponent Zero => TVectorComponent.AdditiveIdentity;
#else
    TVectorComponent Zero { get; }
#endif

    TVectorComponent Half { get; }

    TVector Add(TVector lhs, TVector rhs);
    TVector Subtract(TVector lhs, TVector rhs);
    TVector Multiply(TVector vector, TVectorComponent scalar);
}
