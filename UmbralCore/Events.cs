using System.Collections.Generic;

namespace Nanoray.Umbral.Core
{
    public delegate void ParentChildEvent<Parent, Child>(Parent parent, Child child);

    public delegate void NoValueEvent();
    public delegate void OwnerNoValueEvent<Owner>(Owner owner);

    public delegate void ValueChangeEvent<T>(T oldValue, T newValue);
    public delegate void OwnerValueChangeEvent<Owner, T>(Owner owner, T oldValue, T newValue);
    public delegate void ContextValueChangeEvent<Context, T>(Context context, T oldValue, T newValue);
    public delegate void RootContextValueChangeEvent<Root, Context, T>(Root root, Context context, T oldValue, T newValue);

    public delegate void CollectionValueEvent<T>(T value);
    public delegate void ActualCollectionValueEvent<Collection, T>(Collection collection, T value) where Collection : ICollection<T>;
    public delegate void OwnerCollectionValueEvent<Owner, T>(Owner owner, T value);
    public delegate void OwnerActualCollectionValueEvent<Owner, Collection, T>(Owner owner, T value) where Collection : ICollection<T>;
}
