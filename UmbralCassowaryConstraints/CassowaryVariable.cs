using System;
using System.Reflection;
using Cassowary;

namespace Nanoray.Umbral.Constraints.Cassowary
{
    internal class CassowaryVariable : ClVariable, IVariable<float>
    {
        float IVariable<float>.Value
        {
            get => (float)Value;
            set => Setter.Value(value);
        }

        private readonly Lazy<Action<double>> Setter;

        public CassowaryVariable(string name, float value) : base(name, value)
        {
            Setter = new(() =>
            {
                Type clVariableType = typeof(ClVariable);
                PropertyInfo valueProperty = clVariableType.GetProperty(nameof(Value), BindingFlags.Instance | BindingFlags.NonPublic)!;
                MethodInfo valueSetter = valueProperty.GetSetMethod()!;
                return valueSetter.CreateDelegate<Action<double>>();
            });
        }
    }
}
