using HelperMethods;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
// ReSharper disable UnusedMember.Global

namespace HelperExtensions;

public static class ControlExtensions
{
    #region Enums

    /// <summary>
    /// Indicates the position of the item in the control's visible panel.
    /// </summary>
    public enum ScrollPosition
    {
        Top,
        Middle,
        Bottom
    }

    #endregion

    #region ItemsControl

    #region GetScrollViewer
    /// <summary>
    /// Retrieves the top-most <see cref="ScrollViewer"/> of the current <see cref="ItemsControl"/>.
    /// </summary>
    /// <param name="control"></param>
    /// <returns></returns>
    public static ScrollViewer GetScrollViewer(this ItemsControl control) =>
        GetScrollViewer((DependencyObject)control);
    #endregion

    #region IsIndexVisible
    /// <summary>
    /// Indicates whether the item at the specified index is currently visible in the control.
    /// </summary>
    /// <param name="control"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static bool IsIndexVisible(this ItemsControl control, int index) =>
        control.GetScrollViewer()?.IsIndexVisible(index) ?? false;
    #endregion

    #region ScrollToIndex
    /// <summary>
    /// Scrolls to the specified index.
    /// </summary>
    /// <param name="control"></param>
    /// <param name="index"></param>
    /// <param name="position"></param>
    public static void ScrollToIndex(
        this ItemsControl control,
        int index,
        ScrollPosition position = ScrollPosition.Middle) =>
        control.GetScrollViewer()?.ScrollToIndex(index, position);
    #endregion

    #endregion

    #region ScrollViewer

    #region IsIndexVisible
    /// <summary>
    /// Indicates whether the item at the specified index is currently visible in the <see cref="ScrollViewer"/>.
    /// </summary>
    /// <param name="scrollViewer"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static bool IsIndexVisible(this ScrollViewer scrollViewer, int index) =>
        NumberMethods.IsBetween(index, scrollViewer.VerticalOffset,
            scrollViewer.VerticalOffset + scrollViewer.ViewportHeight, true);
    #endregion

    #region ScrollToIndex
    /// <summary>
    /// Scrolls to the specified index.
    /// </summary>
    /// <param name="scrollViewer"></param>
    /// <param name="index"></param>
    /// <param name="position"></param>
    public static void ScrollToIndex(this ScrollViewer scrollViewer, int index, ScrollPosition position = ScrollPosition.Middle)
    {
        switch (position)
        {
            case ScrollPosition.Top:
                scrollViewer.ScrollToVerticalOffset(index);
                break;

            case ScrollPosition.Middle:
                scrollViewer.ScrollToVerticalOffset(NumberMethods.Between(index - scrollViewer.ViewportHeight / 2, 0, scrollViewer.ScrollableHeight));
                break;

            case ScrollPosition.Bottom:
                scrollViewer.ScrollToVerticalOffset(NumberMethods.Between(Math.Abs(scrollViewer.ViewportHeight - index - 1), 0, scrollViewer.ScrollableHeight));
                break;
        }
    }
    #endregion

    #endregion

    #region Selector

    #region IsSelectedIndexVisible
    /// <summary>
    /// Indicates whether the selected index is visible.
    /// </summary>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static bool IsSelectedIndexVisible(this Selector selector) =>
        selector.IsIndexVisible(selector.SelectedIndex);
    #endregion

    #endregion

    #region Private Methods

    #region GetScrollViewer
    /// <summary>
    /// Retrieves the top <see cref="ScrollViewer"/> of a given <see cref="DependencyObject"/>.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private static ScrollViewer GetScrollViewer(DependencyObject obj)
    {
        if (obj is ScrollViewer scrollViewer)
            return scrollViewer;

        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
        {
            DependencyObject child = VisualTreeHelper.GetChild(obj, i);
            ScrollViewer result = GetScrollViewer(child);
            if (result != null)
                return result;
        }

        return null;
    }
    #endregion

    #endregion
}
