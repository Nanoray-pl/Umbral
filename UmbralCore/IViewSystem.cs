using System.Linq;

namespace Nanoray.Umbral.Core
{
    public interface IViewSystem
    {
        protected internal void OnRegister(View view)
        {
        }

        protected internal void OnUnregister(View view)
        {
        }

        bool HasRegistered(View view)
            => view.ViewSystems.Contains(this);
    }
}
