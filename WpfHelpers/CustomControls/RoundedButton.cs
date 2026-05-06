using System.Windows;
using System.Windows.Controls;
// ReSharper disable UnusedMember.Global

namespace WpfHelpers.CustomControls;

public class RoundedButton : Button
{
    #region Property

    /// <summary>
    /// Gets or sets the corner radius of the button. This property allows you to specify how rounded the corners of the button should be. A higher value will result in more rounded corners, while a value of 0 will produce sharp corners.
    /// </summary>
    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
        nameof(CornerRadius),
        typeof(CornerRadius),
        typeof(RoundedButton),
        new FrameworkPropertyMetadata(
            new CornerRadius(5),
            FrameworkPropertyMetadataOptions.AffectsRender
        )
    );

    #endregion
}
