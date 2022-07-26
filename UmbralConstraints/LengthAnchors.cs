using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Nanoray.Umbral.Constraints.Anchors;
using Nanoray.Umbral.Core;

namespace Nanoray.Umbral.Constraints
{
    public static class UILengthAnchors
    {
        [Pure]
        public static LayoutConstraint<TPriority> MakeConstraint<TPriority>(
            this ILengthAnchor self,
            string? identifier,
            TPriority priority,
            float constant,
            LayoutConstraintRelation relation = LayoutConstraintRelation.Equal
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            => new(identifier, priority, self, constant: constant, relation: relation);

        [Pure]
        public static LayoutConstraint<TPriority> MakeConstraint<TPriority>(
            this ILengthAnchor self,
            TPriority priority,
            float constant,
            LayoutConstraintRelation relation = LayoutConstraintRelation.Equal,
            [CallerFilePath] string? callerFilePath = null,
            [CallerMemberName] string? callerMemberName = null,
            [CallerLineNumber] int? callerLineNumber = null
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            => self.MakeConstraint(
                CallerIdentifiers.GetCallerIdentifier(callerFilePath, callerMemberName, callerLineNumber),
                priority, constant, relation
            );

        [Pure]
        public static LayoutConstraint<TPriority> MakeConstraintTo<TPriority>(
            this ILengthAnchor self,
            string? identifier,
            TPriority priority,
            ILengthAnchor other,
            float constant = 0f,
            float multiplier = 1f,
            LayoutConstraintRelation relation = LayoutConstraintRelation.Equal
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            => new(identifier, priority, self, other, constant, multiplier, relation);

        [Pure]
        public static LayoutConstraint<TPriority> MakeConstraintTo<TPriority>(
            this ILengthAnchor self,
            TPriority priority,
            ILengthAnchor other,
            float constant = 0f,
            float multiplier = 1f,
            LayoutConstraintRelation relation = LayoutConstraintRelation.Equal,
            [CallerFilePath] string? callerFilePath = null,
            [CallerMemberName] string? callerMemberName = null,
            [CallerLineNumber] int? callerLineNumber = null
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            => self.MakeConstraintTo(
                CallerIdentifiers.GetCallerIdentifier(callerFilePath, callerMemberName, callerLineNumber),
                priority, other, constant, multiplier, relation
            );

        [Pure]
        public static LayoutConstraint<TPriority> MakeConstraintTo<TPriority, TConstrainable>(
            this ITypedLengthAnchor<TConstrainable> self,
            string? identifier,
            TPriority priority,
            TConstrainable other,
            float constant = 0f,
            float multiplier = 1f,
            LayoutConstraintRelation relation = LayoutConstraintRelation.Equal
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            where TConstrainable : IConstrainable
            => new(identifier, priority, self, self.GetSameAnchorInConstrainable(other), constant, multiplier, relation);

        [Pure]
        public static LayoutConstraint<TPriority> MakeConstraintTo<TPriority, TConstrainable>(
            this ITypedLengthAnchor<TConstrainable> self,
            TPriority priority,
            TConstrainable other,
            float constant = 0f,
            float multiplier = 1f,
            LayoutConstraintRelation relation = LayoutConstraintRelation.Equal,
            [CallerFilePath] string? callerFilePath = null,
            [CallerMemberName] string? callerMemberName = null,
            [CallerLineNumber] int? callerLineNumber = null
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            where TConstrainable : IConstrainable
            => self.MakeConstraintTo(
                CallerIdentifiers.GetCallerIdentifier(callerFilePath, callerMemberName, callerLineNumber),
                priority, other, constant, multiplier, relation
            );
    }
}
