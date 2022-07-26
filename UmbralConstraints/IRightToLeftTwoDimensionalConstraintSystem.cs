using System;
using Nanoray.Umbral.Constraints.Anchors;
using Nanoray.Umbral.Core;
using System.Diagnostics.Contracts;

namespace Nanoray.Umbral.Constraints
{
    public interface IGenericRightToLeftTwoDimensionalConstraintSystem : IGenericTwoDimensionalConstraintSystem
    {
        [Pure]
        IConstrainable.TwoDimensional IGenericTwoDimensionalConstraintSystem.AsConstrainable(View view)
            => AsConstrainable(view);

        [Pure]
        new IConstrainable.TwoDimensional.RightToLeft AsConstrainable(View view);

        [Pure]
        ITypedPositionalAnchorWithOpposite<IConstrainable.Horizontal.RightToLeft> GetLeadingAnchor(View view);

        [Pure]
        ITypedPositionalAnchorWithOpposite<IConstrainable.Horizontal.RightToLeft> GetTrailingAnchor(View view);

        [Pure]
        LayoutTextDirection GetEnvironmentLayoutTextDirection();

        [Pure]
        LayoutTextDirection GetLayoutTextDirection(View view);

        void SetLayoutTextDirection(View view, LayoutTextDirection direction);

        [Pure]
        LayoutTextDirection GetEffectiveLayoutTextDirection(View view);
    }

    public interface IRightToLeftTwoDimensionalConstraintSystem<TPriority> : ITwoDimensionalConstraintSystem<TPriority>, IGenericRightToLeftTwoDimensionalConstraintSystem
        where TPriority : IEquatable<TPriority>, IComparable<TPriority>
    {
    }
}
