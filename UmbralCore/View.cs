using System;
using System.Collections.Generic;
using Nanoray.Umbral.Core.Geometry;

namespace Nanoray.Umbral.Core
{
    public class View
    {
        #region Hierarchy

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

        #endregion

        #region Positioning

        public event OwnerValueChangeEvent<View, UVector2>? SizeChanged;

        public UVector2 Position { get; set; } = UVector2.Zero;

        public UVector2 Size
        {
            get => _size;
            set
            {
                if (_size == value)
                    return;
                var oldValue = _size;
                _size = value;
                SizeChanged?.Invoke(this, oldValue, value);
            }
        }

        private UVector2 _size = UVector2.Zero;

        #endregion

        #region View systems

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

        #endregion
    }
}
