using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using Cassowary;
using Nanoray.Umbral.Core;

namespace Nanoray.Umbral.Constraints.Cassowary
{
    public class CassowaryTwoDimensionalConstraintSystem : ITwoDimensionalConstraintSystem<float>
    {
        private readonly ConditionalWeakTable<View, CassowaryViewData> ViewData = new();
        private readonly ClSimplexSolver ConstraintSolver = new() { AutoSolve = false };
        private readonly HashSet<LayoutConstraint<float>> QueuedConstraintsToAdd = new();
        private readonly HashSet<LayoutConstraint<float>> QueuedConstraintsToRemove = new();
        private readonly ConditionalWeakTable<LayoutConstraint<float>, ClConstraint> CassowaryConstraints = new();
        private readonly ConditionalWeakTable<LinearExpression.Variable, CassowaryVariable> CassowaryVariables = new();

        /// <inheritdoc/>
        void IViewSystem.OnRegister(View view)
        {
            var data = ObtainViewData(view);
            view.AddedSubview += OnAddedSubview;

            foreach (var variable in new[] { data.LeftVariable.Value, data.RightVariable.Value, data.TopVariable.Value, data.BottomVariable.Value })
                ConstraintSolver.AddVar(ObtainCassowaryVariable(variable));
            QueuedConstraintsToAdd.Add(data.RightAfterLeftConstraint.Value);
            QueuedConstraintsToAdd.Add(data.BottomAfterTopConstraint.Value);
            foreach (var constraint in data.OwnedConstraints)
                QueuedConstraintsToAdd.Add(constraint);

            // TODO: handle intrinsic size changes and constraints (old IntrinsicSizeChanged and UpdateIntrinsicSizeConstraints)
        }

        /// <inheritdoc/>
        void IViewSystem.OnUnregister(View view)
        {
            var data = ObtainViewData(view);
            view.AddedSubview -= OnAddedSubview;

            QueuedConstraintsToRemove.Add(data.RightAfterLeftConstraint.Value);
            QueuedConstraintsToRemove.Add(data.BottomAfterTopConstraint.Value);
            foreach (var constraint in data.OwnedConstraints)
                QueuedConstraintsToRemove.Add(constraint);
            foreach (var variable in new[] { data.LeftVariable.Value, data.RightVariable.Value, data.TopVariable.Value, data.BottomVariable.Value })
            {
                var cassowary = ObtainCassowaryVariable(variable);
                ConstraintSolver.NoteRemovedVariable(cassowary, cassowary);
            }

            foreach (var subview in view.Subviews)
                subview.UnregisterFromViewSystem(this);
        }

        private void OnAddedSubview(View parent, View child)
            => child.RegisterInViewSystem(this);

        #region Priorities

        /// <inheritdoc/>
        [Pure]
        public float DisabledPriority { get; } = 0f;

        /// <inheritdoc/>
        [Pure]
        public float RequiredPriority { get; } = 1000f;

        /// <inheritdoc/>
        [Pure]
        public float MixPriorities(float a, float b, float mix)
            => a + (b - a) * mix;

        #endregion

        #region Constraints

        /// <inheritdoc/>
        public void Activate(LayoutConstraint<float> constraint, View owner)
        {
            QueuedConstraintsToAdd.Add(constraint);
            foreach (var anchorView in constraint.Anchors.Select(a => a.Owner.ConstrainableOwnerView).Distinct())
                ObtainViewData(anchorView).AffectingConstraints.Add(constraint);
            ObtainViewData(owner).OwnedConstraints.Add(constraint);
        }

        /// <inheritdoc/>
        public void Deactivate(LayoutConstraint<float> constraint, View owner)
        {
            ObtainViewData(owner).OwnedConstraints.Remove(constraint);
            foreach (var anchorView in constraint.Anchors.Select(a => a.Owner.ConstrainableOwnerView).Distinct())
                ObtainViewData(anchorView).AffectingConstraints.Remove(constraint);
            QueuedConstraintsToRemove.Add(constraint);
        }

        #endregion

        #region Expressions

        /// <inheritdoc/>
        public LinearExpression.Variable CreateVariable(string name)
            => new(name, new VariableStore<float>());

        #endregion

        #region View constraints

        /// <inheritdoc/>
        [Pure]
        public IReadOnlySet<LayoutConstraint<float>> GetAffectingConstraints(View view)
            => ObtainViewData(view).AffectingConstraints;

        /// <inheritdoc/>
        [Pure]
        public IReadOnlySet<LayoutConstraint<float>> GetOwnedConstraints(View view)
            => ObtainViewData(view).OwnedConstraints;

        #endregion

        #region Constrainable views

        /// <inheritdoc/>
        [Pure]
        public IConstrainable.TwoDimensional AsConstrainable(View view)
            => ObtainViewData(view).ConstrainableView;

        #endregion

        #region View anchors

        /// <inheritdoc/>
        [Pure]
        public IAnchor.Typed<IConstrainable.Horizontal>.Positional.WithOpposite GetLeftAnchor(View view)
            => ObtainViewData(view).LeftAnchor;

        /// <inheritdoc/>
        [Pure]
        public IAnchor.Typed<IConstrainable.Horizontal>.Positional.WithOpposite GetRightAnchor(View view)
            => ObtainViewData(view).RightAnchor;

        /// <inheritdoc/>
        [Pure]
        public IAnchor.Typed<IConstrainable.Vertical>.Positional.WithOpposite GetTopAnchor(View view)
            => ObtainViewData(view).TopAnchor;

        /// <inheritdoc/>
        [Pure]
        public IAnchor.Typed<IConstrainable.Vertical>.Positional.WithOpposite GetBottomAnchor(View view)
            => ObtainViewData(view).BottomAnchor;

        /// <inheritdoc/>
        [Pure]
        public IAnchor.Typed<IConstrainable.Horizontal>.Length GetWidthAnchor(View view)
            => ObtainViewData(view).WidthAnchor;

        /// <inheritdoc/>
        [Pure]
        public IAnchor.Typed<IConstrainable.Vertical>.Length GetHeightAnchor(View view)
            => ObtainViewData(view).HeightAnchor;

        /// <inheritdoc/>
        [Pure]
        public IAnchor.Typed<IConstrainable.Horizontal>.Positional GetCenterXAnchor(View view)
            => ObtainViewData(view).CenterXAnchor;

        /// <inheritdoc/>
        [Pure]
        public IAnchor.Typed<IConstrainable.Vertical>.Positional GetCenterYAnchor(View view)
            => ObtainViewData(view).CenterYAnchor;

        #endregion

        #region Intrinsic size

        /// <inheritdoc/>
        [Pure]
        IGenericTwoDimensionalConstraintSystem.IntrinsicSizeChangedEventOwner IGenericTwoDimensionalConstraintSystem.GetIntrinsicSizeChangedEventOwner(View view)
            => ObtainViewData(view);

        /// <inheritdoc/>
        [Pure]
        public float? GetIntrinsicWidth(View view)
            => ObtainViewData(view).IntrinsicWidth;

        /// <inheritdoc/>
        [Pure]
        public float? GetIntrinsicHeight(View view)
            => ObtainViewData(view).IntrinsicHeight;

        /// <inheritdoc/>
        public void SetIntrinsicWidth(View view, float? value)
            => ObtainViewData(view).IntrinsicWidth = value;

        /// <inheritdoc/>
        public void SetIntrinsicHeight(View view, float? value)
            => ObtainViewData(view).IntrinsicHeight = value;

        /// <inheritdoc/>
        [Pure]
        public float GetHorizontalContentHuggingPriority(View view)
            => ObtainViewData(view).HorizontalContentHuggingPriority;

        /// <inheritdoc/>
        [Pure]
        public float GetVerticalContentHuggingPriority(View view)
            => ObtainViewData(view).VerticalContentHuggingPriority;

        /// <inheritdoc/>
        [Pure]
        public float GetHorizontalCompressionResistancePriority(View view)
            => ObtainViewData(view).HorizontalCompressionResistancePriority;

        /// <inheritdoc/>
        [Pure]
        public float GetVerticalCompressionResistancePriority(View view)
            => ObtainViewData(view).VerticalCompressionResistancePriority;

        /// <inheritdoc/>
        public void SetHorizontalContentHuggingPriority(View view, float priority)
            => ObtainViewData(view).HorizontalContentHuggingPriority = priority;

        /// <inheritdoc/>
        public void SetVerticalContentHuggingPriority(View view, float priority)
            => ObtainViewData(view).VerticalContentHuggingPriority = priority;

        /// <inheritdoc/>
        public void SetHorizontalCompressionResistancePriority(View view, float priority)
            => ObtainViewData(view).HorizontalCompressionResistancePriority = priority;

        /// <inheritdoc/>
        public void SetVerticalCompressionResistancePriority(View view, float priority)
            => ObtainViewData(view).VerticalCompressionResistancePriority = priority;

        #endregion

        private CassowaryViewData ObtainViewData(View view)
        {
            if (!ViewData.TryGetValue(view, out var data))
            {
                data = new(view, this);
                ViewData.AddOrUpdate(view, data);
            }
            return data;
        }

        private ClConstraint ObtainCassowaryConstraint(LayoutConstraint<float> constraint)
        {
            ClConstraint CreateCassowaryConstraint()
            {
                ClLinearExpression CreateExpression(LinearExpression expression)
                {
                    if (expression is LinearExpression.Constant constant)
                        return new ClLinearExpression(constant.Value);
                    else if (expression is LinearExpression.Variable variable)
                        return new ClLinearExpression(ObtainCassowaryVariable(variable));
                    else if (expression is LinearExpression.Addition addition)
                        return CreateExpression(addition.Left).Plus(CreateExpression(addition.Right));
                    else if (expression is LinearExpression.Subtraction subtraction)
                        return CreateExpression(subtraction.Left).Minus(CreateExpression(subtraction.Right));
                    else if (expression is LinearExpression.Multiplication multiplication)
                        return CreateExpression(multiplication.Left).Times(CreateExpression(multiplication.Right));
                    else
                        throw new ArgumentException($"Unhandled expression {expression}.");
                }

                ClStrength CreateStrength(float priority)
                    => priority >= RequiredPriority ? ClStrength.Required : new ClStrength($"{priority}", priority, 0f, 0f);

                if (constraint.Anchor2 is null)
                {
                    return constraint.Relation switch
                    {
                        LayoutConstraintRelation.Equal =>
                            new ClLinearEquation(CreateExpression(constraint.Anchor1.Expression), new ClLinearExpression(constraint.Constant), CreateStrength(constraint.Priority)),
                        LayoutConstraintRelation.LessThanOrEqual =>
                            new ClLinearInequality(CreateExpression(constraint.Anchor1.Expression), Cl.Operator.LessThanOrEqualTo, new ClLinearExpression(constraint.Constant), CreateStrength(constraint.Priority)),
                        LayoutConstraintRelation.GreaterThanOrEqual =>
                            new ClLinearInequality(CreateExpression(constraint.Anchor1.Expression), Cl.Operator.GreaterThanOrEqualTo, new ClLinearExpression(constraint.Constant), CreateStrength(constraint.Priority)),
                        _ => throw new InvalidOperationException($"{nameof(LayoutConstraintRelation)} has an invalid value."),
                    };
                }
                else
                {
                    return constraint.Relation switch
                    {
                        LayoutConstraintRelation.Equal =>
                            new ClLinearEquation(CreateExpression(constraint.Anchor1.Expression), CreateExpression(constraint.Anchor2.Expression).Times(constraint.Multiplier).Plus(new ClLinearExpression(constraint.Constant)), CreateStrength(constraint.Priority)),
                        LayoutConstraintRelation.LessThanOrEqual =>
                            new ClLinearInequality(CreateExpression(constraint.Anchor1.Expression), Cl.Operator.LessThanOrEqualTo, CreateExpression(constraint.Anchor2.Expression).Times(constraint.Multiplier).Plus(new ClLinearExpression(constraint.Constant)), CreateStrength(constraint.Priority)),
                        LayoutConstraintRelation.GreaterThanOrEqual =>
                            new ClLinearInequality(CreateExpression(constraint.Anchor1.Expression), Cl.Operator.GreaterThanOrEqualTo, CreateExpression(constraint.Anchor2.Expression).Times(constraint.Multiplier).Plus(new ClLinearExpression(constraint.Constant)), CreateStrength(constraint.Priority)),
                        _ => throw new InvalidOperationException($"{nameof(LayoutConstraintRelation)} has an invalid value."),
                    };
                }
            }

            if (!CassowaryConstraints.TryGetValue(constraint, out var cassowary))
            {
                cassowary = CreateCassowaryConstraint();
                CassowaryConstraints.AddOrUpdate(constraint, cassowary);
            }
            return cassowary;
        }

        private CassowaryVariable ObtainCassowaryVariable(LinearExpression.Variable variable)
        {
            if (!CassowaryVariables.TryGetValue(variable, out var cassowary))
            {
                cassowary = new(variable.Name, variable.Value.Value);
                CassowaryVariables.AddOrUpdate(variable, cassowary);
            }
            return cassowary;
        }

        private void SolveLayout()
        {
            // TODO: call SolveLayout

            foreach (var constraint in QueuedConstraintsToRemove)
                ConstraintSolver.RemoveConstraintIfExists(ObtainCassowaryConstraint(constraint));
            QueuedConstraintsToRemove.Clear();

            foreach (var constraint in QueuedConstraintsToAdd.OrderByDescending(c => c.Priority))
            {
                try
                {
                    ConstraintSolver.AddConstraint(ObtainCassowaryConstraint(constraint));
                }
                catch
                {
                    // TODO: implement unsatisfiable constraint event
                    //UnsatifiableConstraintEvent?.Invoke(this, constraint);
                }
            }
            QueuedConstraintsToAdd.Clear();

            ConstraintSolver.Solve();
            // TODO: update position/size to new variable values
        }
    }
}
