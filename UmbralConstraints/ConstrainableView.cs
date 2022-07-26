using Nanoray.Umbral.Core;

namespace Nanoray.Umbral.Constraints
{
    public class ConstrainableView : IConstrainable.TwoDimensional
    {
        /// <inheritdoc/>
        public View ConstrainableOwnerView => View;

        /// <inheritdoc/>
        public IAnchor.Typed<IConstrainable.Horizontal>.Positional.WithOpposite LeftAnchor => ConstraintSystem.GetLeftAnchor(View);

        /// <inheritdoc/>
        public IAnchor.Typed<IConstrainable.Horizontal>.Positional.WithOpposite RightAnchor => ConstraintSystem.GetRightAnchor(View);

        /// <inheritdoc/>
        public IAnchor.Typed<IConstrainable.Vertical>.Positional.WithOpposite TopAnchor => ConstraintSystem.GetTopAnchor(View);

        /// <inheritdoc/>
        public IAnchor.Typed<IConstrainable.Vertical>.Positional.WithOpposite BottomAnchor => ConstraintSystem.GetBottomAnchor(View);

        /// <inheritdoc/>
        public IAnchor.Typed<IConstrainable.Horizontal>.Length WidthAnchor => ConstraintSystem.GetWidthAnchor(View);

        /// <inheritdoc/>
        public IAnchor.Typed<IConstrainable.Horizontal>.Positional CenterXAnchor => ConstraintSystem.GetCenterXAnchor(View);

        /// <inheritdoc/>
        public IAnchor.Typed<IConstrainable.Vertical>.Length HeightAnchor => ConstraintSystem.GetHeightAnchor(View);

        /// <inheritdoc/>
        public IAnchor.Typed<IConstrainable.Vertical>.Positional CenterYAnchor => ConstraintSystem.GetCenterYAnchor(View);

        private readonly View View;
        private readonly IGenericTwoDimensionalConstraintSystem ConstraintSystem;

        public ConstrainableView(View view, IGenericTwoDimensionalConstraintSystem constraintSystem)
        {
            this.View = view;
            this.ConstraintSystem = constraintSystem;
        }
    }
}
