using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Nanoray.Umbral.Core;

namespace Nanoray.Umbral.Constraints.TwoDimensionalExtensions
{
    public static class SuperviewConstraints
    {
        [Pure]
        public static LayoutConstraint<TPriority> MakeConstraintToSuperview<TPriority>(
            this IAnchor.Typed<IConstrainable.TwoDimensional>.Positional self,
            string? identifier,
            TPriority priority,
            float constant = 0f,
            float multiplier = 1f,
            LayoutConstraintRelation relation = LayoutConstraintRelation.Equal
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
        {
            var superview = self.Owner.ConstrainableOwnerView.Superview
                ?? throw new InvalidOperationException($"View {self.Owner.ConstrainableOwnerView} does not have a superview.");
            return self.MakeConstraintTo(identifier, priority, self.GetSameAnchorInConstrainable(superview.AsConstrainable()), constant, multiplier, relation);
        }

        [Pure]
        public static LayoutConstraint<TPriority> MakeConstraintToSuperview<TPriority>(
            this IAnchor.Typed<IConstrainable.TwoDimensional>.Positional self,
            TPriority priority,
            float constant = 0f,
            float multiplier = 1f,
            LayoutConstraintRelation relation = LayoutConstraintRelation.Equal,
            [CallerFilePath] string? callerFilePath = null,
            [CallerMemberName] string? callerMemberName = null,
            [CallerLineNumber] int? callerLineNumber = null
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            => self.MakeConstraintToSuperview(
                CallerIdentifiers.GetCallerIdentifier(callerFilePath, callerMemberName, callerLineNumber, "toSuperview"),
                priority, constant, multiplier, relation
            );

        [Pure]
        public static LayoutConstraint<TPriority> MakeConstraintToSuperview<TPriority>(
            this IAnchor.Typed<IConstrainable.TwoDimensional>.Length self,
            string? identifier,
            TPriority priority,
            float constant = 0f,
            float multiplier = 1f,
            LayoutConstraintRelation relation = LayoutConstraintRelation.Equal
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
        {
            var superview = self.Owner.ConstrainableOwnerView.Superview
                ?? throw new InvalidOperationException($"View {self.Owner.ConstrainableOwnerView} does not have a superview.");
            return self.MakeConstraintTo(identifier, priority, self.GetSameAnchorInConstrainable(superview.AsConstrainable()), constant, multiplier, relation);
        }

        [Pure]
        public static LayoutConstraint<TPriority> MakeConstraintToSuperview<TPriority>(
            this IAnchor.Typed<IConstrainable.TwoDimensional>.Length self,
            TPriority priority,
            float constant = 0f,
            float multiplier = 1f,
            LayoutConstraintRelation relation = LayoutConstraintRelation.Equal,
            [CallerFilePath] string? callerFilePath = null,
            [CallerMemberName] string? callerMemberName = null,
            [CallerLineNumber] int? callerLineNumber = null
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            => self.MakeConstraintToSuperview(
                CallerIdentifiers.GetCallerIdentifier(callerFilePath, callerMemberName, callerLineNumber, "toSuperview"),
                priority, constant, multiplier, relation
            );

        [Pure]
        public static LayoutConstraint<TPriority> MakeConstraintToSuperviewOpposite<TPriority>(
            this IAnchor.Typed<IConstrainable.TwoDimensional>.Positional.WithOpposite self,
            string? identifier,
            TPriority priority,
            float constant = 0f,
            float multiplier = 1f,
            LayoutConstraintRelation relation = LayoutConstraintRelation.Equal
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
        {
            var superview = self.Owner.ConstrainableOwnerView.Superview
                ?? throw new InvalidOperationException($"View {self.Owner.ConstrainableOwnerView} does not have a superview.");
            return self.MakeConstraintTo(identifier, priority, self.GetOppositeAnchorInConstrainable(superview.AsConstrainable()), constant, multiplier, relation);
        }

        [Pure]
        public static LayoutConstraint<TPriority> MakeConstraintToSuperviewOpposite<TPriority>(
            this IAnchor.Typed<IConstrainable.TwoDimensional>.Positional.WithOpposite self,
            TPriority priority,
            float constant = 0f,
            float multiplier = 1f,
            LayoutConstraintRelation relation = LayoutConstraintRelation.Equal,
            [CallerFilePath] string? callerFilePath = null,
            [CallerMemberName] string? callerMemberName = null,
            [CallerLineNumber] int? callerLineNumber = null
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            => self.MakeConstraintToSuperviewOpposite(
                CallerIdentifiers.GetCallerIdentifier(callerFilePath, callerMemberName, callerLineNumber, "toSuperview"),
                priority, constant, multiplier, relation
            );
    }
}
