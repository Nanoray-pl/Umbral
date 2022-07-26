using System;
using System.Collections.Generic;
using Nanoray.Umbral.Core;

namespace Nanoray.Umbral.Constraints.Cassowary
{
    internal sealed class CassowaryViewData : IGenericTwoDimensionalConstraintSystem.IntrinsicSizeChangedEventOwner
    {
        // TODO: invoke IntrinsicSizeChanged
        public event OwnerValueChangeEvent<View, (float? x, float? y)>? IntrinsicSizeChanged;

        internal View Owner { get; private init; }
        internal ConstrainableView ConstrainableView { get; private init; }
        internal IAnchor.Typed<IConstrainable.Horizontal>.Positional.WithOpposite LeftAnchor => LazyLeft.Value;
        internal IAnchor.Typed<IConstrainable.Horizontal>.Positional.WithOpposite RightAnchor => LazyRight.Value;
        internal IAnchor.Typed<IConstrainable.Vertical>.Positional.WithOpposite TopAnchor => LazyTop.Value;
        internal IAnchor.Typed<IConstrainable.Vertical>.Positional.WithOpposite BottomAnchor => LazyBottom.Value;
        internal IAnchor.Typed<IConstrainable.Horizontal>.Length WidthAnchor => LazyWidth.Value;
        internal IAnchor.Typed<IConstrainable.Vertical>.Length HeightAnchor => LazyHeight.Value;
        internal IAnchor.Typed<IConstrainable.Horizontal>.Positional CenterXAnchor => LazyCenterX.Value;
        internal IAnchor.Typed<IConstrainable.Vertical>.Positional CenterYAnchor => LazyCenterY.Value;

        internal float? IntrinsicWidth { get; set; } = null;
        internal float? IntrinsicHeight { get; set; } = null;

        internal float HorizontalContentHuggingPriority { get; set; }
        internal float VerticalContentHuggingPriority { get; set; }
        internal float HorizontalCompressionResistancePriority { get; set; }
        internal float VerticalCompressionResistancePriority { get; set; }

        internal readonly Lazy<LinearExpression.Variable> LeftVariable;
        internal readonly Lazy<LinearExpression.Variable> RightVariable;
        internal readonly Lazy<LinearExpression.Variable> TopVariable;
        internal readonly Lazy<LinearExpression.Variable> BottomVariable;
        internal readonly Lazy<LayoutConstraint<float>> RightAfterLeftConstraint;
        internal readonly Lazy<LayoutConstraint<float>> BottomAfterTopConstraint;

        internal readonly HashSet<LayoutConstraint<float>> AffectingConstraints = new();
        internal readonly HashSet<LayoutConstraint<float>> OwnedConstraints = new();

        private readonly CassowaryTwoDimensionalConstraintSystem ConstraintSystem;
        private readonly Lazy<EdgeAnchor<IConstrainable.Horizontal>> LazyLeft;
        private readonly Lazy<EdgeAnchor<IConstrainable.Horizontal>> LazyRight;
        private readonly Lazy<EdgeAnchor<IConstrainable.Vertical>> LazyTop;
        private readonly Lazy<EdgeAnchor<IConstrainable.Vertical>> LazyBottom;
        private readonly Lazy<LengthAnchor<IConstrainable.Horizontal>> LazyWidth;
        private readonly Lazy<LengthAnchor<IConstrainable.Vertical>> LazyHeight;
        private readonly Lazy<CenterAnchor<IConstrainable.Horizontal>> LazyCenterX;
        private readonly Lazy<CenterAnchor<IConstrainable.Vertical>> LazyCenterY;

        public CassowaryViewData(View owner, CassowaryTwoDimensionalConstraintSystem constraintSystem)
        {
            this.Owner = owner;
            this.ConstraintSystem = constraintSystem;
            ConstrainableView = new(owner, constraintSystem);

            LeftVariable = new(() => constraintSystem.CreateVariable($"{owner}.Left"));
            RightVariable = new(() => constraintSystem.CreateVariable($"{owner}.Right"));
            TopVariable = new(() => constraintSystem.CreateVariable($"{owner}.Top"));
            BottomVariable = new(() => constraintSystem.CreateVariable($"{owner}.Bottom"));

            HorizontalContentHuggingPriority = ((IConstraintSystem<float>)constraintSystem).LowPriority;
            VerticalContentHuggingPriority = ((IConstraintSystem<float>)constraintSystem).LowPriority;
            HorizontalCompressionResistancePriority = ((IConstraintSystem<float>)constraintSystem).HighPriority;
            VerticalCompressionResistancePriority = ((IConstraintSystem<float>)constraintSystem).HighPriority;

            LazyLeft = new(() => new(constraintSystem.AsConstrainable(owner), LeftVariable.Value, "Left", c => c.LeftAnchor, c => c.RightAnchor));
            LazyRight = new(() => new(constraintSystem.AsConstrainable(owner), RightVariable.Value, "Right", c => c.RightAnchor, c => c.LeftAnchor));
            LazyTop = new(() => new(constraintSystem.AsConstrainable(owner), TopVariable.Value, "Top", c => c.TopAnchor, c => c.BottomAnchor));
            LazyBottom = new(() => new(constraintSystem.AsConstrainable(owner), BottomVariable.Value, "Bottom", c => c.BottomAnchor, c => c.TopAnchor));
            LazyWidth = new(() => new(constraintSystem.AsConstrainable(owner), RightVariable.Value - LeftVariable.Value, "Width", c => c.WidthAnchor));
            LazyHeight = new(() => new(constraintSystem.AsConstrainable(owner), BottomVariable.Value - TopVariable.Value, "Height", c => c.HeightAnchor));
            LazyCenterX = new(() => new(constraintSystem.AsConstrainable(owner), LeftVariable.Value - WidthAnchor.Expression * 0.5f, "CenterX", c => c.CenterXAnchor));
            LazyCenterY = new(() => new(constraintSystem.AsConstrainable(owner), TopVariable.Value - HeightAnchor.Expression * 0.5f, "CenterY", c => c.CenterYAnchor));

            RightAfterLeftConstraint = new(() => RightAnchor.MakeConstraintTo(ConstraintSystem.RequiredPriority, LeftAnchor, relation: LayoutConstraintRelation.GreaterThanOrEqual));
            BottomAfterTopConstraint = new(() => BottomAnchor.MakeConstraintTo(ConstraintSystem.RequiredPriority, TopAnchor, relation: LayoutConstraintRelation.GreaterThanOrEqual));
        }
    }
}
