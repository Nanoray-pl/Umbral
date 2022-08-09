using System;

namespace Nanoray.Umbral
{
    public interface IUnit
    {
    }

    public struct Unit : IUnit
    {
        public static Unit Instance { get; } = new();
        public static Func<IUnit> IProvider { get; } = () => Instance;
        public static Func<Unit> Provider { get; } = () => Instance;
    }
}
