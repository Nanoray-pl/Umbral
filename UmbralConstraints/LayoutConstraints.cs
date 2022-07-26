using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Nanoray.Umbral.Constraints.Anchors;
using Nanoray.Umbral.Core;

namespace Nanoray.Umbral.Constraints
{
    public static class LayoutConstraints
    {
        public static void Activate<TPriority>(this IEnumerable<LayoutConstraint<TPriority>> constraints)
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
        {
            foreach (var constraint in constraints)
                constraint.Activate();
        }

        public static void Deactivate<TPriority>(this IEnumerable<LayoutConstraint<TPriority>> constraints)
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
        {
            foreach (var constraint in constraints)
                constraint.Deactivate();
        }

        [Pure]
        public static IEnumerable<LayoutConstraint<TPriority>> MakeEqualConstraints<TPriority, TConstrainable>(
            string? identifier,
            TPriority priority,
            Func<TConstrainable, IAnchor> anchor,
            IEnumerable<TConstrainable> constrainables
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            where TConstrainable : IConstrainable
        {
            var enumerator = constrainables.GetEnumerator();
            TConstrainable? first = default;
            while (enumerator.MoveNext())
            {
                if (first is null)
                    first = enumerator.Current;
                else
                    yield return new LayoutConstraint<TPriority>(identifier, priority, anchor(enumerator.Current), anchor2: anchor(first));
            }
        }

        [Pure]
        public static IEnumerable<LayoutConstraint<TPriority>> MakeEqualConstraints<TPriority, TConstrainable>(
            TPriority priority,
            Func<TConstrainable, IAnchor> anchor,
            IEnumerable<TConstrainable> constrainables,
            [CallerFilePath] string? callerFilePath = null,
            [CallerMemberName] string? callerMemberName = null,
            [CallerLineNumber] int? callerLineNumber = null
        )
            where TPriority : IEquatable<TPriority>, IComparable<TPriority>
            where TConstrainable : IConstrainable
            => MakeEqualConstraints(
                CallerIdentifiers.GetCallerIdentifier(callerFilePath, callerMemberName, callerLineNumber, "equalConstraints"),
                priority, anchor, constrainables
            );
    }
}
