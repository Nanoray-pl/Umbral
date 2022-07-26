using System;
using System.Diagnostics.Contracts;
using Nanoray.Umbral.Core;

namespace Nanoray.Umbral.Constraints
{
    public interface IGenericTwoDimensionalConstraintSystem
    {
        #region Constrainable views

        [Pure]
        IConstrainable.TwoDimensional AsConstrainable(View view);

        #endregion

        #region View anchors

        [Pure]
        IAnchor.Typed<IConstrainable.Horizontal>.Positional.WithOpposite GetLeftAnchor(View view);

        [Pure]
        IAnchor.Typed<IConstrainable.Horizontal>.Positional.WithOpposite GetRightAnchor(View view);

        [Pure]
        IAnchor.Typed<IConstrainable.Vertical>.Positional.WithOpposite GetTopAnchor(View view);

        [Pure]
        IAnchor.Typed<IConstrainable.Vertical>.Positional.WithOpposite GetBottomAnchor(View view);

        [Pure]
        IAnchor.Typed<IConstrainable.Horizontal>.Length GetWidthAnchor(View view);

        [Pure]
        IAnchor.Typed<IConstrainable.Vertical>.Length GetHeightAnchor(View view);

        [Pure]
        IAnchor.Typed<IConstrainable.Horizontal>.Positional GetCenterXAnchor(View view);

        [Pure]
        IAnchor.Typed<IConstrainable.Vertical>.Positional GetCenterYAnchor(View view);

        #endregion

        #region Intrinsic size

        [Pure]
        IntrinsicSizeChangedEventOwner GetIntrinsicSizeChangedEventOwner(View view);

        [Pure]
        float? GetIntrinsicWidth(View view);

        [Pure]
        float? GetIntrinsicHeight(View view);

        void SetIntrinsicWidth(View view, float? value);

        void SetIntrinsicHeight(View view, float? value);

        #endregion

        public interface IntrinsicSizeChangedEventOwner
        {
            event OwnerValueChangeEvent<View, (float? x, float? y)>? IntrinsicSizeChanged;
        }
    }

    public interface ITwoDimensionalConstraintSystem<TPriority> : IConstraintSystem<TPriority>, IGenericTwoDimensionalConstraintSystem
        where TPriority : IEquatable<TPriority>, IComparable<TPriority>
    {
        #region Intrinsic size

        [Pure]
        TPriority GetHorizontalContentHuggingPriority(View view);

        [Pure]
        TPriority GetVerticalContentHuggingPriority(View view);

        [Pure]
        TPriority GetHorizontalCompressionResistancePriority(View view);

        [Pure]
        TPriority GetVerticalCompressionResistancePriority(View view);

        void SetHorizontalContentHuggingPriority(View view, TPriority priority);

        void SetVerticalContentHuggingPriority(View view, TPriority priority);

        void SetHorizontalCompressionResistancePriority(View view, TPriority priority);

        void SetVerticalCompressionResistancePriority(View view, TPriority priority);

        #endregion
    }
}
