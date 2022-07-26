using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Nanoray.Umbral.Core;
using Nanoray.Umbral.Core.Geometry;

namespace Nanoray.Umbral.Constraints
{
    public static class Constrainables
    {
        #region Self

        [Pure]
        public static LayoutConstraint<TPriority> MakeAspectRatioConstraint<TPriority, TConstrainable>(
            this TConstrainable self,
            string? identifier,
            TPriority priority,
            UVector2 ratio,
            LayoutConstraintRelation relation = LayoutConstraintRelation.Equal
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            where TConstrainable : IConstrainable.Horizontal, IConstrainable.Vertical
            => self.MakeAspectRatioConstraint(identifier, priority, ratio.X / ratio.Y, relation);

        [Pure]
        public static LayoutConstraint<TPriority> MakeAspectRatioConstraint<TPriority, TConstrainable>(
            this TConstrainable self,
            TPriority priority,
            UVector2 ratio,
            LayoutConstraintRelation relation = LayoutConstraintRelation.Equal,
            [CallerFilePath] string? callerFilePath = null,
            [CallerMemberName] string? callerMemberName = null,
            [CallerLineNumber] int? callerLineNumber = null
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            where TConstrainable : IConstrainable.Horizontal, IConstrainable.Vertical
            => self.MakeAspectRatioConstraint(
                CallerIdentifiers.GetCallerIdentifier(callerFilePath, callerMemberName, callerLineNumber, "aspectRatio"),
                priority, ratio, relation
            );

        [Pure]
        public static LayoutConstraint<TPriority> MakeAspectRatioConstraint<TPriority, TConstrainable>(
            this TConstrainable self,
            string? identifier,
            TPriority priority,
            float ratio = 1f,
            LayoutConstraintRelation relation = LayoutConstraintRelation.Equal
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            where TConstrainable : IConstrainable.Horizontal, IConstrainable.Vertical
            => new(identifier, priority, self.WidthAnchor, self.HeightAnchor, 0f, ratio, relation);

        [Pure]
        public static LayoutConstraint<TPriority> MakeAspectRatioConstraint<TPriority, TConstrainable>(
            this TConstrainable self,
            TPriority priority,
            float ratio = 1f,
            LayoutConstraintRelation relation = LayoutConstraintRelation.Equal,
            [CallerFilePath] string? callerFilePath = null,
            [CallerMemberName] string? callerMemberName = null,
            [CallerLineNumber] int? callerLineNumber = null
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            where TConstrainable : IConstrainable.Horizontal, IConstrainable.Vertical
            => self.MakeAspectRatioConstraint(
                CallerIdentifiers.GetCallerIdentifier(callerFilePath, callerMemberName, callerLineNumber, "aspectRatio"),
                priority, ratio, relation
            );

        #endregion

        #region Any other constrainable

        [Pure]
        public static IEnumerable<LayoutConstraint<TPriority>> MakeHorizontalEdgeConstraintsTo<TPriority>(
            this IConstrainable.Horizontal self,
            string? identifier,
            TPriority priority,
            IConstrainable.Horizontal other,
            float insets = 0f,
            LayoutConstraintMultipleEdgeRelation relation = LayoutConstraintMultipleEdgeRelation.Equal
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
        {
            var singleEdgeLeftRelation = relation switch
            {
                LayoutConstraintMultipleEdgeRelation.Equal => LayoutConstraintRelation.Equal,
                LayoutConstraintMultipleEdgeRelation.Inside => LayoutConstraintRelation.GreaterThanOrEqual,
                LayoutConstraintMultipleEdgeRelation.Outside => LayoutConstraintRelation.LessThanOrEqual,
                _ => throw new InvalidOperationException($"{nameof(LayoutConstraintRelation)} has an invalid value."),
            };
            var singleEdgeRightRelation = singleEdgeLeftRelation.GetReverse();
            yield return self.LeftAnchor.MakeConstraintTo(identifier, priority, other, insets, relation: singleEdgeLeftRelation);
            yield return self.RightAnchor.MakeConstraintTo(identifier, priority, other, -insets, relation: singleEdgeRightRelation);
        }

        [Pure]
        public static IEnumerable<LayoutConstraint<TPriority>> MakeHorizontalEdgeConstraintsTo<TPriority>(
            this IConstrainable.Horizontal self,
            TPriority priority,
            IConstrainable.Horizontal other,
            float insets = 0f,
            LayoutConstraintMultipleEdgeRelation relation = LayoutConstraintMultipleEdgeRelation.Equal,
            [CallerFilePath] string? callerFilePath = null,
            [CallerMemberName] string? callerMemberName = null,
            [CallerLineNumber] int? callerLineNumber = null
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            => self.MakeHorizontalEdgeConstraintsTo(
                CallerIdentifiers.GetCallerIdentifier(callerFilePath, callerMemberName, callerLineNumber, "horizontalEdges"),
                priority, other, insets, relation
            );

        [Pure]
        public static IEnumerable<LayoutConstraint<TPriority>> MakeVerticalEdgeConstraintsTo<TPriority>(
            this IConstrainable.Vertical self,
            string? identifier,
            TPriority priority,
            IConstrainable.Vertical other,
            float insets = 0f,
            LayoutConstraintMultipleEdgeRelation relation = LayoutConstraintMultipleEdgeRelation.Equal
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
        {
            var singleEdgeTopRelation = relation switch
            {
                LayoutConstraintMultipleEdgeRelation.Equal => LayoutConstraintRelation.Equal,
                LayoutConstraintMultipleEdgeRelation.Inside => LayoutConstraintRelation.GreaterThanOrEqual,
                LayoutConstraintMultipleEdgeRelation.Outside => LayoutConstraintRelation.LessThanOrEqual,
                _ => throw new InvalidOperationException($"{nameof(LayoutConstraintRelation)} has an invalid value."),
            };
            var singleEdgeBottomRelation = singleEdgeTopRelation.GetReverse();
            yield return self.TopAnchor.MakeConstraintTo(identifier, priority, other, insets, relation: singleEdgeTopRelation);
            yield return self.BottomAnchor.MakeConstraintTo(identifier, priority, other, -insets, relation: singleEdgeBottomRelation);
        }

        [Pure]
        public static IEnumerable<LayoutConstraint<TPriority>> MakeVerticalEdgeConstraintsTo<TPriority>(
            this IConstrainable.Vertical self,
            TPriority priority,
            IConstrainable.Vertical other,
            float insets = 0f,
            LayoutConstraintMultipleEdgeRelation relation = LayoutConstraintMultipleEdgeRelation.Equal,
            [CallerFilePath] string? callerFilePath = null,
            [CallerMemberName] string? callerMemberName = null,
            [CallerLineNumber] int? callerLineNumber = null
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            => self.MakeVerticalEdgeConstraintsTo(
                CallerIdentifiers.GetCallerIdentifier(callerFilePath, callerMemberName, callerLineNumber, "verticalEdges"),
                priority, other, insets, relation
            );

        [Pure]
        public static IEnumerable<LayoutConstraint<TPriority>> MakeEdgeConstraintsTo<TPriority, TConstrainableA, TConstrainableB>(
            this TConstrainableA self,
            string? identifier,
            TPriority priority,
            TConstrainableB other,
            float insets = 0f,
            LayoutConstraintMultipleEdgeRelation relation = LayoutConstraintMultipleEdgeRelation.Equal
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            where TConstrainableA : IConstrainable.Horizontal, IConstrainable.Vertical
            where TConstrainableB : IConstrainable.Horizontal, IConstrainable.Vertical
        {
            foreach (var constraint in self.MakeHorizontalEdgeConstraintsTo(identifier, priority, other, insets, relation))
                yield return constraint;
            foreach (var constraint in self.MakeVerticalEdgeConstraintsTo(identifier, priority, other, insets, relation))
                yield return constraint;
        }

        [Pure]
        public static IEnumerable<LayoutConstraint<TPriority>> MakeEdgeConstraintsTo<TPriority, TConstrainableA, TConstrainableB>(
            this TConstrainableA self,
            TPriority priority,
            TConstrainableB other,
            float insets = 0f,
            LayoutConstraintMultipleEdgeRelation relation = LayoutConstraintMultipleEdgeRelation.Equal,
            [CallerFilePath] string? callerFilePath = null,
            [CallerMemberName] string? callerMemberName = null,
            [CallerLineNumber] int? callerLineNumber = null
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            where TConstrainableA : IConstrainable.Horizontal, IConstrainable.Vertical
            where TConstrainableB : IConstrainable.Horizontal, IConstrainable.Vertical
            => self.MakeEdgeConstraintsTo(
                CallerIdentifiers.GetCallerIdentifier(callerFilePath, callerMemberName, callerLineNumber, "edges"),
                priority, other, insets, relation
            );

        [Pure]
        public static IEnumerable<LayoutConstraint<TPriority>> MakeCenterConstraintsTo<TPriority, TConstrainableA, TConstrainableB>(
            this TConstrainableA self,
            string? identifier,
            TPriority priority,
            TConstrainableB other,
            UVector2? offset = null,
            LayoutConstraintRelation relation = LayoutConstraintRelation.Equal
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            where TConstrainableA : IConstrainable.Horizontal, IConstrainable.Vertical
            where TConstrainableB : IConstrainable.Horizontal, IConstrainable.Vertical
        {
            yield return self.CenterXAnchor.MakeConstraintTo(identifier, priority, other, offset?.X ?? 0f, relation: relation);
            yield return self.CenterYAnchor.MakeConstraintTo(identifier, priority, other, offset?.Y ?? 0f, relation: relation);
        }

        [Pure]
        public static IEnumerable<LayoutConstraint<TPriority>> MakeCenterConstraintsTo<TPriority, TConstrainableA, TConstrainableB>(
            this TConstrainableA self,
            TPriority priority,
            TConstrainableB other,
            UVector2? offset = null,
            LayoutConstraintRelation relation = LayoutConstraintRelation.Equal,
            [CallerFilePath] string? callerFilePath = null,
            [CallerMemberName] string? callerMemberName = null,
            [CallerLineNumber] int? callerLineNumber = null
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            where TConstrainableA : IConstrainable.Horizontal, IConstrainable.Vertical
            where TConstrainableB : IConstrainable.Horizontal, IConstrainable.Vertical
            => self.MakeCenterConstraintsTo(
                CallerIdentifiers.GetCallerIdentifier(callerFilePath, callerMemberName, callerLineNumber, "center"),
                priority, other, offset, relation
            );

        #endregion
    }
}
