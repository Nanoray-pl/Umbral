using System;
using System.Diagnostics.Contracts;
using Nanoray.Umbral.Constraints.Anchors;
using Nanoray.Umbral.UI;

namespace Nanoray.Umbral.Constraints
{
    public interface IGenericTwoDimensionalConstraintSystem
    {
        [Pure]
        IConstrainable.TwoDimensional AsConstrainable(View view);

        #region View anchors

        [Pure]
        ITypedPositionalAnchorWithOpposite<IConstrainable.Horizontal> GetLeftAnchor(View view);

        [Pure]
        ITypedPositionalAnchorWithOpposite<IConstrainable.Horizontal> GetRightAnchor(View view);

        [Pure]
        ITypedPositionalAnchorWithOpposite<IConstrainable.Vertical> GetTopAnchor(View view);

        [Pure]
        ITypedPositionalAnchorWithOpposite<IConstrainable.Vertical> GetBottomAnchor(View view);

        [Pure]
        ITypedLengthAnchor<IConstrainable.Horizontal> GetWidthAnchor(View view);

        [Pure]
        ITypedLengthAnchor<IConstrainable.Vertical> GetHeightAnchor(View view);

        [Pure]
        ITypedPositionalAnchor<IConstrainable.Horizontal> GetCenterXAnchor(View view);

        [Pure]
        ITypedPositionalAnchor<IConstrainable.Vertical> GetCenterYAnchor(View view);

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
