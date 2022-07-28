using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Nanoray.Umbral.UI;

namespace Nanoray.Umbral.Constraints
{
    public interface IConstraintSystem<TPriority> : IViewSystem
        where TPriority : IEquatable<TPriority>, IComparable<TPriority>
    {
        #region Priorities

        [Pure]
        TPriority DisabledPriority { get; }

        [Pure]
        TPriority RequiredPriority { get; }

        [Pure]
        TPriority MixPriorities(TPriority a, TPriority b, float mix);

        [Pure]
        TPriority LowPriority =>
            MixPriorities(DisabledPriority, RequiredPriority, 0.25f);

        [Pure]
        TPriority MediumPriority =>
            MixPriorities(DisabledPriority, RequiredPriority, 0.5f);

        [Pure]
        TPriority HighPriority =>
            MixPriorities(DisabledPriority, RequiredPriority, 0.75f);

        #endregion

        #region Constraints

        void Activate(LayoutConstraint<TPriority> constraint, View owner);

        void Deactivate(LayoutConstraint<TPriority> constraint, View owner);

        #endregion

        #region View constraints

        [Pure]
        IReadOnlySet<LayoutConstraint<TPriority>> GetAffectingConstraints(View view);

        [Pure]
        IReadOnlySet<LayoutConstraint<TPriority>> GetOwnedConstraints(View view);

        [Pure]
        IEnumerable<LayoutConstraint<TPriority>> GetAllDownstreamConstraints(View view)
        {
            foreach (var constraint in GetOwnedConstraints(view))
                yield return constraint;
            foreach (var subview in view.Subviews)
                foreach (var constraint in GetAllDownstreamConstraints(subview))
                    yield return constraint;
        }

        [Pure]
        IEnumerable<LayoutConstraint<TPriority>> GetAllUpstreamConstraints(View view)
        {
            foreach (var constraint in GetOwnedConstraints(view))
                yield return constraint;
            if (view.Superview is not null)
                foreach (var constraint in GetAllUpstreamConstraints(view.Superview))
                    yield return constraint;
        }

        #endregion

        #region Expressions

        LinearExpression.Variable CreateVariable(string name);

        #endregion
    }
}
