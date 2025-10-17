using System.Windows;
using System.Windows.Controls;

namespace WpfHelpers.CustomControls;

public class ContentSwitcher : ContentControl
{
    #region Properties

    /// <summary>
    /// Gets or sets the condition that determines which content to display.
    /// </summary>
    public bool Condition
    {
        get => (bool)GetValue(ConditionProperty);
        set => SetValue(ConditionProperty, value);
    }

    /// <summary>
    /// Gets or sets the content to display when the condition is false.
    /// </summary>
    public object FalseContent
    {
        get => GetValue(FalseContentProperty);
        set => SetValue(FalseContentProperty, value);
    }

    /// <summary>
    /// Gets or sets the content to display when the condition is true.
    /// </summary>
    public object TrueContent
    {
        get => GetValue(TrueContentProperty);
        set => SetValue(TrueContentProperty, value);
    }

    public static readonly DependencyProperty ConditionProperty =
    DependencyProperty.Register(
        nameof(Condition),
        typeof(bool),
        typeof(ContentSwitcher),
        new(false, OnPropertyChanged));

    public static readonly DependencyProperty FalseContentProperty =
        DependencyProperty.Register(
            nameof(FalseContent),
            typeof(object),
            typeof(ContentSwitcher),
            new(null, OnPropertyChanged));

    public static readonly DependencyProperty TrueContentProperty =
        DependencyProperty.Register(
            nameof(TrueContent),
            typeof(object),
            typeof(ContentSwitcher),
            new(null, OnPropertyChanged));

    #endregion

    static ContentSwitcher() =>
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(ContentSwitcher),
            new FrameworkPropertyMetadata(typeof(ContentSwitcher)));

    #region Events

    #region OnPropertyChanged
    /// <summary>
    /// Updates the displayed content based on the current condition.
    /// </summary>
    /// <param name="d"></param>
    /// <param name="e"></param>
    private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ContentSwitcher contentSwitcher = (ContentSwitcher)d;
        contentSwitcher.Content =
            contentSwitcher.Condition
                ? contentSwitcher.TrueContent
                : contentSwitcher.FalseContent;
    }
    #endregion

    #endregion
}
