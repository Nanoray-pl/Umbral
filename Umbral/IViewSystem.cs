using System;
using System.Collections.Generic;
using System.Numerics;

namespace Nanoray.Umbral;

public interface IViewSystem<TBaseView, TViewSystem, TVector, TVectorComponent>
    where TBaseView : IView<TBaseView, TViewSystem, TVector, TVectorComponent>
    where TViewSystem : IViewSystem<TBaseView, TViewSystem, TVector, TVectorComponent>
    where TVector : struct
#if NET7_0_OR_GREATER
    where TVectorComponent : struct, INumber<TVectorComponent>
#else
    where TVectorComponent : struct, IComparable<TVectorComponent>
#endif
{
    IEnumerable<TVectorComponent> GetVectorComponents(TVector vector);
    void SetVectorComponents(ref TVector vector, TVectorComponent value);
}
