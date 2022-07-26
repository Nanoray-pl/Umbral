using System;

namespace Nanoray.Umbral.Constraints
{
    public interface IVariable<T>
    {
        T Value { get; set; }
    }

    public struct DelegateVariable<T> : IVariable<T>
    {
        /// <inheritdoc/>
        public T Value
        {
            get => Getter();
            set => Setter(Value);
        }

        private readonly Func<T> Getter;
        private readonly Action<T> Setter;

        public DelegateVariable(Func<T> getter, Action<T> setter)
        {
            this.Getter = getter;
            this.Setter = setter;
        }
    }

    public struct VariableStore<T> : IVariable<T>
    {
        /// <inheritdoc/>
        public T Value { get; set; }

        public VariableStore(T value)
        {
            this.Value = value;
        }
    }
}
