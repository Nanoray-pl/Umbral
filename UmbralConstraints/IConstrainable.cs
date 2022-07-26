using System;
using Nanoray.Umbral.Core;

namespace Nanoray.Umbral.Constraints
{
    public interface IConstrainable
    {
        View ConstrainableOwnerView { get; }

        public interface Horizontal : IConstrainable
        {
            public IAnchor.Typed<Horizontal>.Positional.WithOpposite LeftAnchor { get; }
            public IAnchor.Typed<Horizontal>.Positional.WithOpposite RightAnchor { get; }
            public IAnchor.Typed<Horizontal>.Length WidthAnchor { get; }
            public IAnchor.Typed<Horizontal>.Positional CenterXAnchor { get; }
        }

        public interface Vertical : IConstrainable
        {
            public IAnchor.Typed<Vertical>.Positional.WithOpposite TopAnchor { get; }
            public IAnchor.Typed<Vertical>.Positional.WithOpposite BottomAnchor { get; }
            public IAnchor.Typed<Vertical>.Length HeightAnchor { get; }
            public IAnchor.Typed<Vertical>.Positional CenterYAnchor { get; }
        }

        public interface TwoDimensional : Horizontal, Vertical
        {
        }
    }
}
