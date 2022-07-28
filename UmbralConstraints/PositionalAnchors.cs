using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Nanoray.Umbral.Constraints.Anchors;
using Nanoray.Umbral.UI;

namespace Nanoray.Umbral.Constraints
{
    public static class UIPositionalAnchors
    {
        [Pure]
        public static LayoutConstraint<TPriority> MakeConstraintTo<TPriority, TConstrainable>(
            this ITypedPositionalAnchor<TConstrainable> self,
            string? identifier,
            TPriority priority,
            ITypedPositionalAnchor<TConstrainable> other,
            float constant = 0f,
            float multiplier = 1f,
            LayoutConstraintRelation relation = LayoutConstraintRelation.Equal
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            where TConstrainable : IConstrainable
            => new(identifier, priority, self, other, constant, multiplier, relation);

        [Pure]
        public static LayoutConstraint<TPriority> MakeConstraintTo<TPriority, TConstrainable>(
            this ITypedPositionalAnchor<TConstrainable> self,
            TPriority priority,
            ITypedPositionalAnchor<TConstrainable> other,
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

        [Pure]
        public static LayoutConstraint<TPriority> MakeConstraintTo<TPriority, TConstrainable>(
            this ITypedPositionalAnchor<TConstrainable> self,
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
            this ITypedPositionalAnchor<TConstrainable> self,
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

        [Pure]
        public static LayoutConstraint<TPriority> MakeConstraintToOpposite<TPriority, TConstrainable>(
            this ITypedPositionalAnchorWithOpposite<TConstrainable> self,
            string? identifier,
            TPriority priority,
            TConstrainable other,
            float constant = 0f,
            float multiplier = 1f,
            LayoutConstraintRelation relation = LayoutConstraintRelation.Equal
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            where TConstrainable : IConstrainable
            => new(identifier, priority, self, self.GetOppositeAnchorInConstrainable(other), constant, multiplier, relation);

        [Pure]
        public static LayoutConstraint<TPriority> MakeConstraintToOpposite<TPriority, TConstrainable>(
            this ITypedPositionalAnchorWithOpposite<TConstrainable> self,
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
            => self.MakeConstraintToOpposite(
                CallerIdentifiers.GetCallerIdentifier(callerFilePath, callerMemberName, callerLineNumber),
                priority, other, constant, multiplier, relation
            );
    }
}
