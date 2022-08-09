using System;
using System.Collections.Generic;

namespace Nanoray.Umbral
{
    public interface INode : IEquatable<INode>
    {
        protected internal Node UnderlyingNode { get; }

        event ParentChildEvent<INode, INode>? AddedToRoot
        {
            add => UnderlyingNode.AddedToRoot += value;
            remove => UnderlyingNode.AddedToRoot -= value;
        }

        event ParentChildEvent<INode, INode>? RemovedFromRoot
        {
            add => UnderlyingNode.RemovedFromRoot += value;
            remove => UnderlyingNode.RemovedFromRoot -= value;
        }

        event ParentChildEvent<INode, INode>? AddedToNode
        {
            add => UnderlyingNode.AddedToNode += value;
            remove => UnderlyingNode.AddedToNode -= value;
        }

        event ParentChildEvent<INode, INode>? RemovedFromNode
        {
            add => UnderlyingNode.RemovedFromNode += value;
            remove => UnderlyingNode.RemovedFromNode -= value;
        }

        event ParentChildEvent<INode, INode>? AddedNode
        {
            add => UnderlyingNode.AddedNode += value;
            remove => UnderlyingNode.AddedNode -= value;
        }

        event ParentChildEvent<INode, INode>? RemovedNode
        {
            add => UnderlyingNode.RemovedNode += value;
            remove => UnderlyingNode.RemovedNode -= value;
        }

        Node? Root
            => UnderlyingNode.Root;

        Node? Parent
            => UnderlyingNode.Parent;

        IReadOnlyList<Node> Nodes
            => UnderlyingNode.Nodes;

        void AddNode(Node node)
            => UnderlyingNode.AddNode(node);

        void InsertNode(int index, Node node)
            => UnderlyingNode.InsertNode(index, node);

        void PutNodeFirst(Node node)
            => UnderlyingNode.PutNodeFirst(node);

        void PutNodeLast(Node node)
            => UnderlyingNode.PutNodeLast(node);

        void PutNodeAfter(Node node, Node anotherNode)
            => UnderlyingNode.PutNodeAfter(node, anotherNode);

        void PutNodeBefore(Node node, Node anotherNode)
            => UnderlyingNode.PutNodeBefore(node, anotherNode);

        void RemoveFromParent()
            => UnderlyingNode.RemoveFromParent();

        TComponent? GetComponent<TComponent>()
            where TComponent : notnull
            => UnderlyingNode.GetComponent<TComponent>();

        TComponent? GetComponent<TComponent, TVariant>(TVariant variant)
            where TComponent : notnull
            where TVariant : IEquatable<TVariant>
            => UnderlyingNode.GetComponent<TComponent, TVariant>(variant);

        void SetComponent<TComponent>(TComponent component)
            where TComponent : notnull
            => UnderlyingNode.SetComponent(component);

        void SetComponent<TComponent, TVariant>(TVariant variant, TComponent component)
            where TComponent : notnull
            where TVariant : IEquatable<TVariant>
            => UnderlyingNode.SetComponent(variant, component);

        //TNode? WithComponents<TNode, TComponent1>()
        //    where TNode : INode
        //    => UnderlyingNode.WithComponents<TNode, TComponent1>();
    }
}
