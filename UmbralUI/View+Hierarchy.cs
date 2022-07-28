using System;
using System.Collections.Generic;

namespace Nanoray.Umbral.UI
{
    public partial class View
    {
        public event ParentChildEvent<View, View>? AddedToRoot;
        public event ParentChildEvent<View, View>? RemovedFromRoot;
        public event ParentChildEvent<View, View>? AddedToSuperview;
        public event ParentChildEvent<View, View>? RemovedFromSuperview;
        public event ParentChildEvent<View, View>? AddedSubview;
        public event ParentChildEvent<View, View>? RemovedSubview;

        public View? Root
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

        public View? Superview { get; private set; }

        public IReadOnlyList<View> Subviews => _subviews;

        private View? _root;
        private readonly List<View> _subviews = new();

        public void AddSubview(View subview)
        {
            InsertSubview(Subviews.Count, subview);
        }

        public void InsertSubview(int index, View subview)
        {
            if (subview.Superview is not null)
                throw new InvalidOperationException($"Cannot add subview {subview}, as it's already added to {subview.Superview}.");
            _subviews.Insert(index, subview);
            subview.Superview = this;
            subview.AddedToSuperview?.Invoke(this, subview);
            AddedSubview?.Invoke(this, subview);
            subview.Root = Root;
        }

        public void BringSubviewToFront(View subview)
        {
            if (subview.Superview != this)
                throw new InvalidOperationException($"View {subview} is not a subview of {this}.");
            _subviews.Remove(subview);
            _subviews.Add(subview);
        }

        public void SendSubviewToBack(View subview)
        {
            if (subview.Superview != this)
                throw new InvalidOperationException($"View {subview} is not a subview of {this}.");
            _subviews.Remove(subview);
            _subviews.Insert(0, subview);
        }

        public void PutSubviewAbove(View subview, View anotherSubview)
        {
            if (subview.Superview != this)
                throw new InvalidOperationException($"View {subview} is not a subview of {this}.");
            if (anotherSubview.Superview != this)
                throw new InvalidOperationException($"View {anotherSubview} is not a subview of {this}.");
            _subviews.Remove(subview);
            int index = _subviews.IndexOf(anotherSubview);
            if (index == -1)
                throw new InvalidOperationException($"View {anotherSubview} is not a subview of {this}.");
            _subviews.Insert(index + 1, subview);
        }

        public void PutSubviewBelow(View subview, View anotherSubview)
        {
            if (subview.Superview != this)
                throw new InvalidOperationException($"View {subview} is not a subview of {this}.");
            if (anotherSubview.Superview != this)
                throw new InvalidOperationException($"View {anotherSubview} is not a subview of {this}.");
            _subviews.Remove(subview);
            int index = _subviews.IndexOf(anotherSubview);
            if (index == -1)
                throw new InvalidOperationException($"View {anotherSubview} is not a subview of {this}.");
            _subviews.Insert(index, subview);
        }

        public void RemoveFromSuperview()
        {
            var superview = Superview;
            if (superview is null)
                return;
            superview._subviews.Remove(this);
            Superview = null;
            superview.RemovedSubview?.Invoke(superview, this);
            RemovedFromSuperview?.Invoke(superview, this);
            Root = null;
        }
    }
}
