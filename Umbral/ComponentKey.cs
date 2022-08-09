using System;

namespace Nanoray.Umbral
{
    public interface IComponentKey : IEquatable<IComponentKey>
    {
        Type Type { get; }
    }

    public struct ComponentKey : IComponentKey, IEquatable<ComponentKey>
    {
        public Type Type { get; init; }

        public ComponentKey(Type type)
        {
            this.Type = type;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
            => obj is ComponentKey componentKey && Equals(componentKey);

        /// <inheritdoc/>
        public bool Equals(IComponentKey? other)
            => other is ComponentKey componentKey && Equals(componentKey);

        /// <inheritdoc/>
        public bool Equals(ComponentKey other)
            => Type == other.Type;

        /// <inheritdoc/>
        public override int GetHashCode()
            => Type.GetHashCode();
    }

    public struct VariantComponentKey<TVariant> : IComponentKey, IEquatable<VariantComponentKey<TVariant>>
        where TVariant : IEquatable<TVariant>
    {
        public Type Type { get; init; }
        public TVariant Variant { get; init; }

        public VariantComponentKey(Type type, TVariant variant)
        {
            this.Type = type;
            this.Variant = variant;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
            => obj is VariantComponentKey<TVariant> variantComponentKey && Equals(variantComponentKey);

        /// <inheritdoc/>
        public bool Equals(IComponentKey? other)
            => other is VariantComponentKey<TVariant> variantComponentKey && Equals(variantComponentKey);

        /// <inheritdoc/>
        public bool Equals(VariantComponentKey<TVariant> other)
            => Type == other.Type && (IEquatable<TVariant>)Variant == (IEquatable<TVariant>)other.Variant;

        /// <inheritdoc/>
        public override int GetHashCode()
            => (Type, Variant).GetHashCode();
    }
}
