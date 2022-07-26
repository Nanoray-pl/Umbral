using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Nanoray.Umbral.Core;

namespace Nanoray.Umbral.Constraints
{
    public static class UIPositionalAnchors
    {
        [Pure]
        public static LayoutConstraint<TPriority> MakeConstraintTo<TPriority, TConstrainable>(
            this IAnchor.Typed<TConstrainable>.Positional self,
            string? identifier,
            TPriority priority,
            IAnchor.Typed<TConstrainable>.Positional other,
            float constant = 0f,
            float multiplier = 1f,
            LayoutConstraintRelation relation = LayoutConstraintRelation.Equal
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            where TConstrainable : IConstrainable
            => new(identifier, priority, self, other, constant, multiplier, relation);

        [Pure]
        public static LayoutConstraint<TPriority> MakeConstraintTo<TPriority, TConstrainable>(
            this IAnchor.Typed<TConstrainable>.Positional self,
            TPriority priority,
            IAnchor.Typed<TConstrainable>.Positional other,
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
            this IAnchor.Typed<TConstrainable>.Positional self,
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
            this IAnchor.Typed<TConstrainable>.Positional self,
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
            this IAnchor.Typed<TConstrainable>.Positional.WithOpposite self,
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
            this IAnchor.Typed<TConstrainable>.Positional.WithOpposite self,
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
