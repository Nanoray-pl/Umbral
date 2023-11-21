using System;
using System.Collections.Generic;
using System.Numerics;

namespace Nanoray.Umbral;

public interface IView<TBaseView, TViewSystem, TVector, TVectorComponent>
    where TBaseView : IView<TBaseView, TViewSystem, TVector, TVectorComponent>
    where TViewSystem : IViewSystem<TBaseView, TViewSystem, TVector, TVectorComponent>
    where TVector : struct
#if NET7_0_OR_GREATER
    where TVectorComponent : struct, INumber<TVectorComponent>
#else
    where TVectorComponent : struct, IComparable<TVectorComponent>, IEquatable<TVectorComponent>
#endif
{
    TViewSystem ViewSystem { get; }

    TBaseView? Parent { get; }
    IEnumerable<TBaseView> Subviews { get; }

    TVector MinPosition { get; set; }
    TVector Size { get; set; }

    TVector MaxPosition
    {
        get => ViewSystem.Add(MinPosition, Size);
        set => MinPosition = ViewSystem.Subtract(value, Size);
    }

    TVector Center
    {
        get => ViewSystem.Add(MinPosition, ViewSystem.Multiply(Size, ViewSystem.Half));
        set => MinPosition = ViewSystem.Subtract(value, ViewSystem.Multiply(Size, ViewSystem.Half));
    }
}
