using System;
using Nanoray.Umbral.Constraints.Anchors;
using Nanoray.Umbral.Core;

namespace Nanoray.Umbral.Constraints
{
    public interface IConstrainable
    {
        View ConstrainableOwnerView { get; }

        public interface Horizontal : IConstrainable
        {
            public ITypedPositionalAnchorWithOpposite<Horizontal> LeftAnchor { get; }
            public ITypedPositionalAnchorWithOpposite<Horizontal> RightAnchor { get; }
            public ITypedLengthAnchor<Horizontal> WidthAnchor { get; }
            public ITypedPositionalAnchor<Horizontal> CenterXAnchor { get; }

            public interface RightToLeft : Horizontal
            {
                public ITypedPositionalAnchorWithOpposite<RightToLeft> LeadingAnchor { get; }
                public ITypedPositionalAnchorWithOpposite<RightToLeft> TrailingAnchor { get; }
            }
        }

        public interface Vertical : IConstrainable
        {
            public ITypedPositionalAnchorWithOpposite<Vertical> TopAnchor { get; }
            public ITypedPositionalAnchorWithOpposite<Vertical> BottomAnchor { get; }
            public ITypedLengthAnchor<Vertical> HeightAnchor { get; }
            public ITypedPositionalAnchor<Vertical> CenterYAnchor { get; }
        }

        public interface TwoDimensional : Horizontal, Vertical
        {
            public new interface RightToLeft : TwoDimensional, Horizontal.RightToLeft
            {
            }
        }
    }
}
