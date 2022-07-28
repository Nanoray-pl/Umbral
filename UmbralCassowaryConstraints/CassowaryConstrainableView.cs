using Nanoray.Umbral.Constraints.Anchors;
using Nanoray.Umbral.UI;

namespace Nanoray.Umbral.Constraints.Cassowary
{
    public class CassowaryConstrainableView : IConstrainable.TwoDimensional.RightToLeft
    {
        /// <inheritdoc/>
        public View ConstrainableOwnerView => View;

        /// <inheritdoc/>
        public ITypedPositionalAnchorWithOpposite<IConstrainable.Horizontal> LeftAnchor => ConstraintSystem.GetLeftAnchor(View);

        /// <inheritdoc/>
        public ITypedPositionalAnchorWithOpposite<IConstrainable.Horizontal> RightAnchor => ConstraintSystem.GetRightAnchor(View);

        /// <inheritdoc/>
        public ITypedPositionalAnchorWithOpposite<IConstrainable.Vertical> TopAnchor => ConstraintSystem.GetTopAnchor(View);

        /// <inheritdoc/>
        public ITypedPositionalAnchorWithOpposite<IConstrainable.Vertical> BottomAnchor => ConstraintSystem.GetBottomAnchor(View);

        /// <inheritdoc/>
        public ITypedPositionalAnchorWithOpposite<IConstrainable.Horizontal.RightToLeft> LeadingAnchor => ConstraintSystem.GetLeadingAnchor(View);

        /// <inheritdoc/>
        public ITypedPositionalAnchorWithOpposite<IConstrainable.Horizontal.RightToLeft> TrailingAnchor => ConstraintSystem.GetTrailingAnchor(View);

        /// <inheritdoc/>
        public ITypedLengthAnchor<IConstrainable.Horizontal> WidthAnchor => ConstraintSystem.GetWidthAnchor(View);

        /// <inheritdoc/>
        public ITypedPositionalAnchor<IConstrainable.Horizontal> CenterXAnchor => ConstraintSystem.GetCenterXAnchor(View);

        /// <inheritdoc/>
        public ITypedLengthAnchor<IConstrainable.Vertical> HeightAnchor => ConstraintSystem.GetHeightAnchor(View);

        /// <inheritdoc/>
        public ITypedPositionalAnchor<IConstrainable.Vertical> CenterYAnchor => ConstraintSystem.GetCenterYAnchor(View);

        private readonly View View;
        private readonly IGenericRightToLeftTwoDimensionalConstraintSystem ConstraintSystem;

        public CassowaryConstrainableView(View view, IGenericRightToLeftTwoDimensionalConstraintSystem constraintSystem)
        {
            this.View = view;
            this.ConstraintSystem = constraintSystem;
        }
    }
}
