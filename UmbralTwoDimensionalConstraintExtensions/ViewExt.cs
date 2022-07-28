using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Nanoray.Umbral.Constraints.Anchors;
using Nanoray.Umbral.UI;

namespace Nanoray.Umbral.Constraints.TwoDimensionalExtensions
{
    public static class ViewExt
    {
        private static IGenericTwoDimensionalConstraintSystem GetGenericConstraintSystem(this View self)
        {
            return self.ViewSystems.FirstOrDefault(s => s is IGenericTwoDimensionalConstraintSystem) as IGenericTwoDimensionalConstraintSystem
                ?? throw new InvalidOperationException($"View {self} does not have a (matching) registered `IGenericTwoDimensionalConstraintSystem`.");
        }

        private static ITwoDimensionalConstraintSystem<TPriority> GetConstraintSystem<TPriority>(this View self)
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
        {
            return self.ViewSystems.FirstOrDefault(s => s is ITwoDimensionalConstraintSystem<TPriority>) as ITwoDimensionalConstraintSystem<TPriority>
                ?? throw new InvalidOperationException($"View {self} does not have a (matching) registered `ITwoDimensionalConstraintSystem`.");
        }

        #region Constrainable views

        [Pure]
        public static IConstrainable.TwoDimensional AsConstrainable(this View self)
            => self.GetGenericConstraintSystem().AsConstrainable(self);

        #endregion

        #region Anchors

        [Pure]
        public static ITypedPositionalAnchorWithOpposite<IConstrainable.Horizontal> GetLeftAnchor(this View self)
            => self.GetGenericConstraintSystem().GetLeftAnchor(self);

        [Pure]
        public static ITypedPositionalAnchorWithOpposite<IConstrainable.Horizontal> GetRightAnchor(this View self)
            => self.GetGenericConstraintSystem().GetRightAnchor(self);

        [Pure]
        public static ITypedPositionalAnchorWithOpposite<IConstrainable.Vertical> GetTopAnchor(this View self)
            => self.GetGenericConstraintSystem().GetTopAnchor(self);

        [Pure]
        public static ITypedPositionalAnchorWithOpposite<IConstrainable.Vertical> GetBottomAnchor(this View self)
            => self.GetGenericConstraintSystem().GetBottomAnchor(self);

        [Pure]
        public static ITypedLengthAnchor<IConstrainable.Horizontal> GetWidthAnchor(this View self)
            => self.GetGenericConstraintSystem().GetWidthAnchor(self);

        [Pure]
        public static ITypedLengthAnchor<IConstrainable.Vertical> GetHeightAnchor(this View self)
            => self.GetGenericConstraintSystem().GetHeightAnchor(self);

        [Pure]
        public static ITypedPositionalAnchor<IConstrainable.Horizontal> GetCenterXAnchor(this View self)
            => self.GetGenericConstraintSystem().GetCenterXAnchor(self);

        [Pure]
        public static ITypedPositionalAnchor<IConstrainable.Vertical> GetCenterYAnchor(this View self)
            => self.GetGenericConstraintSystem().GetCenterYAnchor(self);

        #endregion

        #region Intrinsic size

        [Pure]
        public static IGenericTwoDimensionalConstraintSystem.IntrinsicSizeChangedEventOwner GetIntrinsicSizeChangedEvent(this View self)
            => self.GetGenericConstraintSystem().GetIntrinsicSizeChangedEventOwner(self);

        [Pure]
        public static float? GetIntrinsicWidth(this View self)
            => self.GetGenericConstraintSystem().GetIntrinsicWidth(self);

        [Pure]
        public static float? GetIntrinsicHeight(this View self)
            => self.GetGenericConstraintSystem().GetIntrinsicHeight(self);

        public static void SetIntrinsicWidth(this View self, float? value)
            => self.GetGenericConstraintSystem().SetIntrinsicWidth(self, value);

        public static void SetIntrinsicHeight(this View self, float? value)
            => self.GetGenericConstraintSystem().SetIntrinsicHeight(self, value);

        [Pure]
        public static TPriority GetHorizontalContentHuggingPriority<TPriority>(this View self)
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            => self.GetConstraintSystem<TPriority>().GetHorizontalContentHuggingPriority(self);

        [Pure]
        public static TPriority GetVerticalContentHuggingPriority<TPriority>(this View self)
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            => self.GetConstraintSystem<TPriority>().GetVerticalContentHuggingPriority(self);

        [Pure]
        public static TPriority GetHorizontalCompressionResistancePriority<TPriority>(this View self)
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            => self.GetConstraintSystem<TPriority>().GetHorizontalCompressionResistancePriority(self);

        [Pure]
        public static TPriority GetVerticalCompressionResistancePriority<TPriority>(this View self)
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            => self.GetConstraintSystem<TPriority>().GetVerticalCompressionResistancePriority(self);

        public static void SetHorizontalContentHuggingPriority<TPriority>(this View self, TPriority priority)
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            => self.GetConstraintSystem<TPriority>().SetHorizontalContentHuggingPriority(self, priority);

        public static void SetVerticalContentHuggingPriority<TPriority>(this View self, TPriority priority)
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            => self.GetConstraintSystem<TPriority>().SetVerticalContentHuggingPriority(self, priority);

        public static void SetHorizontalCompressionResistancePriority<TPriority>(this View self, TPriority priority)
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            => self.GetConstraintSystem<TPriority>().SetHorizontalCompressionResistancePriority(self, priority);

        public static void SetVerticalCompressionResistancePriority<TPriority>(this View self, TPriority priority)
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            => self.GetConstraintSystem<TPriority>().SetVerticalCompressionResistancePriority(self, priority);

        #endregion
    }
}
