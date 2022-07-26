using System;
using Nanoray.Umbral.Core.Geometry;

namespace Nanoray.Umbral.Core
{
    public partial class View
    {
        public event OwnerValueChangeEvent<View, UVector2>? SizeChanged;

        public UVector2 Position { get; set; } = UVector2.Zero;

        public UVector2 Size
        {
            get => _size;
            set
            {
                if (_size == value)
                    return;
                var oldValue = _size;
                _size = value;
                SizeChanged?.Invoke(this, oldValue, value);
            }
        }

        private UVector2 _size = UVector2.Zero;
    }
}
