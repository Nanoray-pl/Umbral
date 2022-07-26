using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Nanoray.Umbral.Core.Geometry;

namespace Nanoray.Umbral.Core
{
    public enum ViewVisitingOrder
    {
        SuperviewFirstSubviewOrder,
        SuperviewLastSubviewOrder,
        VisibleOrder,
        HoverOrder
    }

    public static class Views
    {
        [Pure]
        public static UVector2 ConvertPointBetweenViews(UVector2 point, View from, View to)
        {
            if (from == to)
                return point;
            IList<View> aSuperviews = from.GetViewHierarchy(true).Reverse().ToList();
            IList<View> bSuperviews = to.GetViewHierarchy(true).Reverse().ToList();
            if (aSuperviews.Count == 0 || bSuperviews.Count == 0 || aSuperviews[0] != bSuperviews[0])
                throw new InvalidOperationException($"Views {from} and {to} are not in the same view hierarchy.");

            int GetCommonViewIndex()
            {
                int minCount = Math.Min(aSuperviews.Count, bSuperviews.Count);
                for (int i = 1; i < minCount; i++)
                {
                    if (aSuperviews[i] == bSuperviews[i])
                        continue;
                    return i - 1;
                }
                return minCount - 1;
            }

            int commonIndex = GetCommonViewIndex();
            for (int i = aSuperviews.Count - 1; i > commonIndex; i--)
                point += aSuperviews[i].Position;
            for (int i = commonIndex + 1; i < bSuperviews.Count; i++)
                point -= bSuperviews[i].Position;
            return point;
        }

        [Pure]
        public static IEnumerable<View> GetViewHierarchy(this View self, bool includeSelf)
        {
            if (includeSelf)
                yield return self;
            var current = self.Superview;
            while (current is not null)
            {
                yield return current;
                current = current.Superview;
            }
        }

        [Pure]
        public static View? GetCommonSuperview(View a, View b)
        {
            if (a == b)
                return a;
            var aSuperviews = a.GetViewHierarchy(true).Reverse().ToList();
            var bSuperviews = b.GetViewHierarchy(true).Reverse().ToList();
            if (aSuperviews.Count == 0 || bSuperviews.Count == 0 || aSuperviews[0] != bSuperviews[0])
                return null;
            int minCount = Math.Min(aSuperviews.Count, bSuperviews.Count);
            for (int i = 1; i < minCount; i++)
            {
                if (aSuperviews[i] == bSuperviews[i])
                    continue;
                return aSuperviews[i - 1];
            }
            return aSuperviews[minCount - 1];
        }

        [Pure]
        public static IEnumerable<T> VisitAllViews<T>(this View self, ViewVisitingOrder order = ViewVisitingOrder.SuperviewFirstSubviewOrder, bool includeSelf = true)
            where T : View
        {
            return order switch
            {
                ViewVisitingOrder.SuperviewFirstSubviewOrder => self.VisitAllViewsInSuperviewFirstSubviewOrder<T>(includeSelf),
                ViewVisitingOrder.SuperviewLastSubviewOrder => self.VisitAllViewsInSuperviewLastSubviewOrder<T>(includeSelf),
                ViewVisitingOrder.VisibleOrder => self.VisitAllViewsInVisibleOrder<T>(includeSelf),
                _ => throw new InvalidOperationException($"{nameof(ViewVisitingOrder)} has an invalid value.")
            };
        }

        [Pure]
        private static IEnumerable<T> VisitAllViewsInSuperviewFirstSubviewOrder<T>(this View self, bool includeSelf)
            where T : View
        {
            if (includeSelf && self is T typedView)
                yield return typedView;
            foreach (var subview in self.Subviews)
                foreach (var toReturn in subview.VisitAllViewsInSuperviewFirstSubviewOrder<T>(true))
                    yield return toReturn;
        }

        [Pure]
        private static IEnumerable<T> VisitAllViewsInSuperviewLastSubviewOrder<T>(this View self, bool includeSelf)
            where T : View
        {
            foreach (var subview in self.Subviews)
                foreach (var toReturn in subview.VisitAllViewsInSuperviewLastSubviewOrder<T>(true))
                    yield return toReturn;
            if (includeSelf && self is T typedView)
                yield return typedView;
        }

        [Pure]
        private static IEnumerable<T> VisitAllViewsInVisibleOrder<T>(this View self, bool includeSelf)
            where T : View
        {
            foreach (var subview in self.Subviews.Reverse())
                foreach (var toReturn in subview.VisitAllViewsInVisibleOrder<T>(true))
                    yield return toReturn;
            if (includeSelf && self is T typedView)
                yield return typedView;
        }
    }
}
