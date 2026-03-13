using System.Windows;
using System.Windows.Controls.Primitives;

namespace WpfHelpers.CustomControls;

public class RoundedToggleButton : ToggleButton
{
    #region Property

    /// <summary>
    /// Gets or sets the corner radius of the toggle button. This property allows you to specify how rounded the corners of the button should be. A higher value will result in more rounded corners, while a value of 0 will produce sharp corners.
    /// </summary>
    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
        nameof(CornerRadius),
        typeof(CornerRadius),
        typeof(RoundedToggleButton),
        new FrameworkPropertyMetadata(
            new CornerRadius(5),
            FrameworkPropertyMetadataOptions.AffectsRender
        )
    );

    #endregion
}