using System;
using System.Collections.Generic;
using System.Linq;
using Nanoray.Umbral.Core;

namespace Nanoray.Umbral.Constraints
{
    public enum LayoutConstraintRelation { LessThanOrEqual, Equal, GreaterThanOrEqual }

    public enum LayoutConstraintMultipleEdgeRelation { Equal, Inside, Outside }

    public static class LayoutConstraintRelationExt
    {
        public static string GetSymbol(this LayoutConstraintRelation self)
            => self switch
            {
                LayoutConstraintRelation.Equal => "==",
                LayoutConstraintRelation.GreaterThanOrEqual => ">=",
                LayoutConstraintRelation.LessThanOrEqual => "<=",
                _ => throw new InvalidOperationException($"{nameof(LayoutConstraintRelation)} has an invalid value."),
            };

        public static LayoutConstraintRelation GetReverse(this LayoutConstraintRelation self)
            => self switch
            {
                LayoutConstraintRelation.Equal => LayoutConstraintRelation.Equal,
                LayoutConstraintRelation.GreaterThanOrEqual => LayoutConstraintRelation.LessThanOrEqual,
                LayoutConstraintRelation.LessThanOrEqual => LayoutConstraintRelation.GreaterThanOrEqual,
                _ => throw new InvalidOperationException($"{nameof(LayoutConstraintRelation)} has an invalid value."),
            };
    }

    public class LayoutConstraint<TPriority> : IEquatable<LayoutConstraint<TPriority>>
        where TPriority : IEquatable<TPriority>, IComparable<TPriority>
    {
        public string? Identifier { get; }
        public TPriority Priority { get; }
        public IAnchor Anchor1 { get; }
        public IAnchor? Anchor2 { get; }
        public float Constant { get; }
        public float Multiplier { get; }
        public LayoutConstraintRelation Relation { get; }

        public View? Owner { get; private set; }

        public IReadOnlyCollection<IAnchor> Anchors
            => Anchor2 is null ? new[] { Anchor1 } : new[] { Anchor1, Anchor2 };

        public LayoutConstraint(
            string? identifier,
            TPriority priority,
            IAnchor anchor1,
            IAnchor? anchor2 = null,
            float constant = 0f,
            float multiplier = 1f,
            LayoutConstraintRelation relation = LayoutConstraintRelation.Equal
        )
        {
            if (anchor2 is not null && !anchor1.IsCompatibleWithAnchor(anchor2))
                throw new ArgumentException($"Anchors {anchor1} and {anchor2} are not compatible with each other.");
            this.Identifier = identifier;
            this.Priority = priority;
            this.Anchor1 = anchor1;
            this.Constant = constant;
            this.Multiplier = multiplier;
            this.Anchor2 = anchor2;
            this.Relation = relation;
        }

        public void Activate()
        {
            if (Owner is not null)
                return;
            var constraintOwningView = GetViewToPutConstraintOn();
            var constraintSystem = constraintOwningView.ViewSystems.FirstOrDefault(s => s is ITwoDimensionalConstraintSystem<TPriority>) as ITwoDimensionalConstraintSystem<TPriority>
                ?? throw new InvalidOperationException($"View {constraintOwningView} does not have a (matching) registered `ITwoDimensionalConstraintSystem`.");
            constraintSystem.Activate(this, constraintOwningView);
            Owner = constraintOwningView;
        }

        public void Deactivate()
        {
            if (Owner is null)
                return;
            var constraintSystem = Owner.ViewSystems.FirstOrDefault(s => s is ITwoDimensionalConstraintSystem<TPriority>) as ITwoDimensionalConstraintSystem<TPriority>
                ?? throw new InvalidOperationException($"View {Owner} does not have a (matching) registered `ITwoDimensionalConstraintSystem`.");
            constraintSystem.Deactivate(this, Owner);
            Owner = null;
        }

        private View GetViewToPutConstraintOn()
        {
            var owner1 = Anchor1.Owner.ConstrainableOwnerView;
            var owner2 = Anchor2?.Owner.ConstrainableOwnerView;
            if (owner2 is null || owner1 == owner2)
            {
                return owner1;
            }
            else
            {
                var commonView = Views.GetCommonSuperview(owner1, owner2);
                if (commonView is null)
                    throw new InvalidOperationException($"Cannot add a constraint between unrelated views {owner1} and {owner2}.");
                return commonView;
            }
        }

        /// <inheritdoc/>
        public bool Equals(LayoutConstraint<TPriority>? obj)
            => obj is LayoutConstraint<TPriority> other
            && other.Anchor1 == Anchor1
            && other.Anchor2 == Anchor2
            && other.Constant == Constant
            && other.Multiplier == Multiplier
            && other.Relation == Relation
            && (IEquatable<TPriority>)other.Priority == (IEquatable<TPriority>)Priority;

        /// <inheritdoc/>
        public override bool Equals(object? obj)
            => obj is LayoutConstraint<TPriority> other && Equals(other);

        /// <inheritdoc/>
        public override int GetHashCode()
            => (Anchor1, Anchor2, Constant, Multiplier, Relation, Priority).GetHashCode();

        /// <inheritdoc/>
        public static bool operator ==(LayoutConstraint<TPriority> lhs, LayoutConstraint<TPriority> rhs)
            => lhs.Equals(rhs);

        /// <inheritdoc/>
        public static bool operator !=(LayoutConstraint<TPriority> lhs, LayoutConstraint<TPriority> rhs)
            => !lhs.Equals(rhs);
    }
}
