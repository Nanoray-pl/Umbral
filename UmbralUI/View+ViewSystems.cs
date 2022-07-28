using System.Collections.Generic;

namespace Nanoray.Umbral.UI
{
    public partial class View
    {
        public event ParentChildEvent<View, IViewSystem>? RegisteredInViewSystem;
        public event ParentChildEvent<View, IViewSystem>? UnregisteredFromViewSystem;

        public IReadOnlyList<IViewSystem> ViewSystems => _viewSystems;

        private List<IViewSystem> _viewSystems = new();

        public void RegisterInViewSystem(IViewSystem viewSystem)
        {
            if (_viewSystems.Contains(viewSystem))
                return;
            _viewSystems.Add(viewSystem);
            viewSystem.OnRegister(this);
            RegisteredInViewSystem?.Invoke(this, viewSystem);
        }

        public void UnregisterFromViewSystem(IViewSystem viewSystem)
        {
            if (!_viewSystems.Contains(viewSystem))
                return;
            viewSystem.OnUnregister(this);
            _viewSystems.Remove(viewSystem);
            UnregisteredFromViewSystem?.Invoke(this, viewSystem);
        }
    }
}
