using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using Nanoray.Umbral.Core;
using Nanoray.Umbral.Core.Geometry;

namespace Nanoray.Umbral.Constraints.TwoDimensionalExtensions
{
    public static class ViewMultipleEdgeConstraints
    {
        [Pure]
        public static IEnumerable<LayoutConstraint<TPriority>> MakeHorizontalEdgeConstraintsToSuperview<TPriority>(
            this View self,
            string? identifier,
            TPriority priority,
            float insets = 0f,
            LayoutConstraintMultipleEdgeRelation relation = LayoutConstraintMultipleEdgeRelation.Equal
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
        {
            var superview = self.Superview
                ?? throw new InvalidOperationException($"View {self} does not have a superview.");
            var constraintSystem = self.ViewSystems.FirstOrDefault(s => s is ITwoDimensionalConstraintSystem<TPriority>) as ITwoDimensionalConstraintSystem<TPriority>
                ?? throw new InvalidOperationException($"View {self} does not have a (matching) registered `ITwoDimensionalConstraintSystem`.");
            return constraintSystem.AsConstrainable(self).MakeHorizontalEdgeConstraintsTo(identifier, priority, constraintSystem.AsConstrainable(superview), insets, relation);
        }

        [Pure]
        public static IEnumerable<LayoutConstraint<TPriority>> MakeHorizontalEdgeConstraintsToSuperview<TPriority>(
            this View self,
            TPriority priority,
            float insets = 0f,
            LayoutConstraintMultipleEdgeRelation relation = LayoutConstraintMultipleEdgeRelation.Equal,
            [CallerFilePath] string? callerFilePath = null,
            [CallerMemberName] string? callerMemberName = null,
            [CallerLineNumber] int? callerLineNumber = null
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            => self.MakeHorizontalEdgeConstraintsToSuperview(
                CallerIdentifiers.GetCallerIdentifier(callerFilePath, callerMemberName, callerLineNumber, "horizontalEdges-toSuperview"),
                priority, insets, relation
            );

        [Pure]
        public static IEnumerable<LayoutConstraint<TPriority>> MakeVerticalEdgeConstraintsToSuperview<TPriority>(
            this View self,
            string? identifier,
            TPriority priority,
            float insets = 0f,
            LayoutConstraintMultipleEdgeRelation relation = LayoutConstraintMultipleEdgeRelation.Equal
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
        {
            var superview = self.Superview
                ?? throw new InvalidOperationException($"View {self} does not have a superview.");
            var constraintSystem = self.ViewSystems.FirstOrDefault(s => s is ITwoDimensionalConstraintSystem<TPriority>) as ITwoDimensionalConstraintSystem<TPriority>
                ?? throw new InvalidOperationException($"View {self} does not have a (matching) registered `ITwoDimensionalConstraintSystem`.");
            return constraintSystem.AsConstrainable(self).MakeVerticalEdgeConstraintsTo(identifier, priority, constraintSystem.AsConstrainable(superview), insets, relation);
        }

        [Pure]
        public static IEnumerable<LayoutConstraint<TPriority>> MakeVerticalEdgeConstraintsToSuperview<TPriority>(
            this View self,
            TPriority priority,
            float insets = 0f,
            LayoutConstraintMultipleEdgeRelation relation = LayoutConstraintMultipleEdgeRelation.Equal,
            [CallerFilePath] string? callerFilePath = null,
            [CallerMemberName] string? callerMemberName = null,
            [CallerLineNumber] int? callerLineNumber = null
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            => self.MakeVerticalEdgeConstraintsToSuperview(
                CallerIdentifiers.GetCallerIdentifier(callerFilePath, callerMemberName, callerLineNumber, "verticalEdges-toSuperview"),
                priority, insets, relation
            );

        [Pure]
        public static IEnumerable<LayoutConstraint<TPriority>> MakeEdgeConstraintsToSuperview<TPriority>(
            this View self,
            string? identifier,
            TPriority priority,
            float insets = 0f,
            LayoutConstraintMultipleEdgeRelation relation = LayoutConstraintMultipleEdgeRelation.Equal
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
        {
            var superview = self.Superview
                ?? throw new InvalidOperationException($"View {self} does not have a superview.");
            var constraintSystem = self.ViewSystems.FirstOrDefault(s => s is ITwoDimensionalConstraintSystem<TPriority>) as ITwoDimensionalConstraintSystem<TPriority>
                ?? throw new InvalidOperationException($"View {self} does not have a (matching) registered `ITwoDimensionalConstraintSystem`.");
            return constraintSystem.AsConstrainable(self).MakeEdgeConstraintsTo(identifier, priority, constraintSystem.AsConstrainable(superview), insets, relation);
        }

        [Pure]
        public static IEnumerable<LayoutConstraint<TPriority>> MakeEdgeConstraintsToSuperview<TPriority>(
            this View self,
            TPriority priority,
            float insets = 0f,
            LayoutConstraintMultipleEdgeRelation relation = LayoutConstraintMultipleEdgeRelation.Equal,
            [CallerFilePath] string? callerFilePath = null,
            [CallerMemberName] string? callerMemberName = null,
            [CallerLineNumber] int? callerLineNumber = null
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            => self.MakeEdgeConstraintsToSuperview(
                CallerIdentifiers.GetCallerIdentifier(callerFilePath, callerMemberName, callerLineNumber, "edges-toSuperview"),
                priority, insets, relation
            );

        [Pure]
        public static IEnumerable<LayoutConstraint<TPriority>> MakeCenterConstraintsToSuperview<TPriority>(
            this View self,
            string? identifier,
            TPriority priority,
            UVector2? offset = null,
            LayoutConstraintRelation relation = LayoutConstraintRelation.Equal
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
        {
            var superview = self.Superview
                ?? throw new InvalidOperationException($"View {self} does not have a superview.");
            var constraintSystem = self.ViewSystems.FirstOrDefault(s => s is ITwoDimensionalConstraintSystem<TPriority>) as ITwoDimensionalConstraintSystem<TPriority>
                ?? throw new InvalidOperationException($"View {self} does not have a (matching) registered `ITwoDimensionalConstraintSystem`.");
            return constraintSystem.AsConstrainable(self).MakeCenterConstraintsTo(identifier, priority, constraintSystem.AsConstrainable(superview), offset, relation);
        }

        [Pure]
        public static IEnumerable<LayoutConstraint<TPriority>> MakeCenterConstraintsToSuperview<TPriority>(
            this View self,
            TPriority priority,
            UVector2? offset = null,
            LayoutConstraintRelation relation = LayoutConstraintRelation.Equal,
            [CallerFilePath] string? callerFilePath = null,
            [CallerMemberName] string? callerMemberName = null,
            [CallerLineNumber] int? callerLineNumber = null
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            => self.MakeCenterConstraintsToSuperview(
                CallerIdentifiers.GetCallerIdentifier(callerFilePath, callerMemberName, callerLineNumber, "center-toSuperview"),
                priority, offset, relation
            );
    }
}
