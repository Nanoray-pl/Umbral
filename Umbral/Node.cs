using System;
using System.Collections.Generic;

namespace Nanoray.Umbral
{
    public sealed partial class Node : INode
    {
        Node INode.UnderlyingNode
            => this;

        /// <inheritdoc/>
        public event ParentChildEvent<INode, INode>? AddedToRoot;

        /// <inheritdoc/>
        public event ParentChildEvent<INode, INode>? RemovedFromRoot;

        /// <inheritdoc/>
        public event ParentChildEvent<INode, INode>? AddedToNode;

        /// <inheritdoc/>
        public event ParentChildEvent<INode, INode>? RemovedFromNode;

        /// <inheritdoc/>
        public event ParentChildEvent<INode, INode>? AddedNode;

        /// <inheritdoc/>
        public event ParentChildEvent<INode, INode>? RemovedNode;

        /// <inheritdoc/>
        public Node? Root
        {
            get => _root;
            set
            {
                if (_root == value)
                    return;
                var oldValue = _root;
                _root = value;

                if (value is null)
                    RemovedFromRoot?.Invoke(oldValue!, this);
                else
                    AddedToRoot?.Invoke(value, this);
            }
        }

        /// <inheritdoc/>
        public Node? Parent { get; private set; }

        /// <inheritdoc/>
        public IReadOnlyList<Node> Nodes => _nodes;

        private Node? _root;
        private readonly List<Node> _nodes = new();
        private readonly Dictionary<IComponentKey, object> _components = new();

        /// <inheritdoc/>
        public bool Equals(INode? other)
            => ReferenceEquals(this, other?.UnderlyingNode);

        /// <inheritdoc/>
        public void AddNode(Node node)
        {
            InsertNode(Nodes.Count, node);
        }

        /// <inheritdoc/>
        public void InsertNode(int index, Node node)
        {
            if (node.Parent is not null)
                throw new InvalidOperationException($"Cannot add child node {node}, as it's already added to {node.Parent}.");
            _nodes.Insert(index, node);
            node.Parent = this;
            node.AddedToNode?.Invoke(this, node);
            AddedNode?.Invoke(this, node);
            node.Root = Root;
        }

        /// <inheritdoc/>
        public void PutNodeFirst(Node node)
        {
            if (node.Parent != this)
                throw new InvalidOperationException($"Node {node} is not a child node of {this}.");
            _nodes.Remove(node);
            _nodes.Insert(0, node);
        }

        /// <inheritdoc/>
        public void PutNodeLast(Node node)
        {
            if (node.Parent != this)
                throw new InvalidOperationException($"Node {node} is not a child node of {this}.");
            _nodes.Remove(node);
            _nodes.Add(node);
        }

        /// <inheritdoc/>
        public void PutNodeAfter(Node node, Node anotherNode)
        {
            if (node.Parent != this)
                throw new InvalidOperationException($"Node {node} is not a child node of {this}.");
            if (anotherNode.Parent != this)
                throw new InvalidOperationException($"Node {anotherNode} is not a child node of {this}.");
            _nodes.Remove(node);
            int index = _nodes.IndexOf(anotherNode);
            if (index == -1)
                throw new InvalidOperationException($"Node {anotherNode} is not a child node of {this}.");
            _nodes.Insert(index + 1, node);
        }

        /// <inheritdoc/>
        public void PutNodeBefore(Node node, Node anotherNode)
        {
            if (node.Parent != this)
                throw new InvalidOperationException($"Node {node} is not a child node of {this}.");
            if (anotherNode.Parent != this)
                throw new InvalidOperationException($"Node {anotherNode} is not a child node of {this}.");
            _nodes.Remove(node);
            int index = _nodes.IndexOf(anotherNode);
            if (index == -1)
                throw new InvalidOperationException($"Node {anotherNode} is not a child node of {this}.");
            _nodes.Insert(index, node);
        }

        /// <inheritdoc/>
        public void RemoveFromParent()
        {
            var parent = Parent;
            if (parent is null)
                return;
            parent._nodes.Remove(this);
            Parent = null;
            parent.RemovedNode?.Invoke(parent, this);
            RemovedFromNode?.Invoke(parent, this);
            Root = null;
        }

        /// <inheritdoc/>
        public TComponent? GetComponent<TComponent>()
            where TComponent : notnull
        {
            if (_components.TryGetValue(new ComponentKey(typeof(TComponent)), out object? component))
                return (TComponent?)(TComponent)component;
            else
                return default;
        }

        /// <inheritdoc/>
        public TComponent? GetComponent<TComponent, TVariant>(TVariant variant)
            where TComponent : notnull
            where TVariant : IEquatable<TVariant>
        {
            if (_components.TryGetValue(new VariantComponentKey<TVariant>(typeof(TComponent), variant), out object? component))
                return (TComponent?)(TComponent)component;
            else
                return default;
        }

        /// <inheritdoc/>
        public void SetComponent<TComponent>(TComponent component)
            where TComponent : notnull
        {
            _components[new ComponentKey(typeof(TComponent))] = component;
        }

        /// <inheritdoc/>
        public void SetComponent<TComponent, TVariant>(TVariant variant, TComponent component)
            where TComponent : notnull
            where TVariant : IEquatable<TVariant>
        {
            _components[new VariantComponentKey<TVariant>(typeof(TComponent), variant)] = component;
        }
    }
}
