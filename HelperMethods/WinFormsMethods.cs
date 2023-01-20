using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HelperExtensions;
using HelpersClasses.Enums;

// ReSharper disable UnusedMember.Global

namespace HelperMethods;

public static class WinFormsMethods
{
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

    #region Public Methods

    #region BindField
    /// <summary>
    /// Binds control's property to a data source field.
    /// </summary>
    /// <param name="control">Destination control.</param>
    /// <param name="property">Destination control's property.</param>
    /// <param name="source">Data source.</param>
    /// <param name="sourceField">Data source field.</param>
    /// <exception cref="ArgumentNullException"/>
    public static void BindField(Control control, string property, object source, string sourceField)
    {
        control.DataBindings.Clear();
        control.DataBindings.Add(property, source, sourceField);
    }
    #endregion

    #region ClearDataBindings
    /// <summary>
    /// Clears controls binds.
    /// </summary>
    /// <param name="controls"></param>
    public static void ClearDataBindings(params Control[] controls) =>
        controls.ForEach(control => control.DataBindings.Clear());
    #endregion

    #region FitText(Control)
    /// <summary>
    /// Fits text in the control replacing the characters in the center with ellipsis.
    /// </summary>
    /// <param name="control"></param>
    public static void FitText(Control control)
    {
        StringBuilder stringTest = new();

        //Calculates the label maximum needed width.
        using (Graphics graphics = control.CreateGraphics())
        {
            do
            {
                stringTest.Append("a");
            } while (graphics.MeasureString(stringTest.ToString(), control.Font, Point.Empty, StringFormat.GenericTypographic).Width < control.Width);
        }

        //Removes ellipsis.
        if (control.Text.EndsWith("...") && control.Text.Length > stringTest.Length)
            control.Text = control.Text.Substring(0, control.Text.Length - 3);

        control.Text = stringTest.Length >= 3 ? StringMethods.InsertEllipsis(control.Text, stringTest.Length) : control.Text = String.Empty.PadLeft(stringTest.Length, '.');
    }
    #endregion

    #region FitText(Control, string)
    /// <summary>
    /// Fits text in the control replacing the characters in the center with ellipsis.
    /// </summary>
    /// <param name="control"></param>
    /// <param name="text"></param>
    public static void FitText(Control control, string text)
    {
        control.Text = text;
        FitText(control);
    }
    #endregion

    #region GeneratePopupLocation(Control, Control, [PosicaoPopUp])
    /// <summary>
    /// Returns the point in which a popup should be located for a specified control
    /// </summary>
    /// <param name="baseControl"></param>
    /// <param name="popUpControl"></param>
    /// <param name="location"></param>
    /// <returns></returns>
    public static Point GeneratePopupLocation(Control baseControl, Control popUpControl, PopupLocation location = PopupLocation.Bottom) =>
        GeneratePopupLocation(baseControl, popUpControl.Width, popUpControl.Height, location);
    #endregion

    #region GeneratePopupLocation(Control, int, int, [PosicaoPopUp])

    /// <summary>
    /// Returns the point in which a popup should be located for a specified control
    /// </summary>
    /// <param name="baseControl"></param>
    /// <param name="popUpHeight"></param>
    /// <param name="location"></param>
    /// <param name="popUpWidth"></param>
    /// <returns></returns>
    public static Point GeneratePopupLocation(Control baseControl, int popUpWidth, int popUpHeight, PopupLocation location = PopupLocation.Bottom)
    {
        Point selectedLocation = location switch
        {
            PopupLocation.Middle => baseControl.PointToScreen(new(baseControl.Width / 2, baseControl.Height / 2)),
            PopupLocation.Top => baseControl.PointToScreen(new(baseControl.Width / 2, 0)),
            PopupLocation.Right => baseControl.PointToScreen(new(baseControl.Width, baseControl.Height / 2)),
            PopupLocation.Left => baseControl.PointToScreen(new(0, baseControl.Height)),
            _ => baseControl.PointToScreen(new(baseControl.Width / 2, baseControl.Height))
        };

        Screen currentScreen = Screen.FromControl(baseControl);
        Point maxPosition = new(currentScreen.WorkingArea.Width - popUpWidth, currentScreen.WorkingArea.Height - popUpHeight);

        if (selectedLocation.X < 0)
            selectedLocation.X = 0;
        else if (selectedLocation.X > maxPosition.X)
            selectedLocation.X = maxPosition.X;

        if (selectedLocation.Y < 0)
            selectedLocation.Y = 0;
        else if (selectedLocation.Y > maxPosition.Y)
            selectedLocation.Y = maxPosition.Y;

        return selectedLocation;
    }
    #endregion

    #region GetDataRow<T>(DataGridViewRow)
    /// <summary>
    /// Returns the current row DataRow.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="row"></param>
    /// <returns></returns>
    /// <exception cref="InvalidCastException" />
    /// <exception cref="ArgumentNullException" />
    public static T GetDataRow<T>(DataGridViewRow row) =>
        (T)Convert.ChangeType(((DataRowView)row.DataBoundItem).Row, typeof(T));
    #endregion

    #region GetDataRow<T>(object)
    /// <summary>
    /// Returns the current DataRowView DataRow.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="InvalidCastException" />
    /// <exception cref="ArgumentNullException" />
    public static T GetDataRow<T>(object data) =>
        (T)Convert.ChangeType(((DataRowView)data).Row, typeof(T));
    #endregion

    #region HideWaitCursor(Control)
    /// <summary>
    /// Hides wait cursor for the selected control.
    /// </summary>
    /// <param name="control"></param>
    public static void HideWaitCursor(Control control) =>
        control.UseWaitCursor = false;
    #endregion

    #region HideWaitCursor(Form)
    /// <summary>
    /// Hides wait cursor for the selected form.
    /// </summary>
    /// <param name="form"></param>
    public static void HideWaitCursor(Form form) =>
        form.UseWaitCursor = false;
    #endregion

    #region SetListControlSource (ListControl, object, string, string)

    /// <summary>
    /// Sets the data source for the list control.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="control"></param>
    /// <param name="displayMember"></param>
    /// <param name="valueMember"></param>
    /// <param name="isBindAfter">Indicates whether data must be bound after setting the value and data member.</param>
    public static BindingSource SetListControlSource(ListControl control, object source, string displayMember, string valueMember, bool isBindAfter = true) =>
        SetListControlSource(control, source, displayMember, valueMember, null, null, isBindAfter);
    #endregion

    #region SetListControlSource(ListControl, object, string, string, string, string)
    /// <summary>
    /// Sets the data source for the list control.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="control"></param>
    /// <param name="displayMember"></param>
    /// <param name="valueMember"></param>
    /// <param name="sorting">Sorting field.</param>
    /// <param name="filter"></param>
    /// <param name="isBindAfter">Indicates whether data must be bound after setting the value and data member.</param>
    /// <returns></returns>
    public static BindingSource SetListControlSource(ListControl control, object source, string displayMember, string valueMember, string sorting, string filter, bool isBindAfter = true)
    {
        BindingSource bindingSource = new(source, null) { Sort = sorting, Filter = filter };

        if (isBindAfter)
        {
            control.ValueMember = valueMember;
            control.DisplayMember = displayMember;
            control.DataSource = bindingSource;
        }
        else
        {
            control.DataSource = bindingSource;
            control.ValueMember = valueMember;
            control.DisplayMember = displayMember;
        }

        return bindingSource;
    }
    #endregion

    #region ShowErrorMessage(IWin32Window, string)

    /// <summary>
    /// Shows error message.
    /// </summary>
    /// <param name="window"></param>
    /// <param name="message"></param>
    public static void ShowErrorMessage(IWin32Window window, string message) =>
        ShowErrorMessage(window, message, null);
    #endregion

    #region ShowErrorMessage(IWin32Window, string, string)
    /// <summary>
    /// Shows error message.
    /// </summary>
    /// <param name="window"></param>
    /// <param name="message"></param>
    /// <param name="title"></param>
    public static void ShowErrorMessage(IWin32Window window, string message, string title) =>
        MessageBox.Show(window, message, title ?? (window as Form)?.Text ?? "", MessageBoxButtons.OK, MessageBoxIcon.Error);
    #endregion

    #region ShowErrorQuestionMessage(IWin32Window, string, string)
    /// <summary>
    /// Shows question message with the error icon.
    /// </summary>
    /// <param name="window"></param>
    /// <param name="message"></param>
    /// <param name="defaultResult"></param>
    public static bool ShowErrorQuestionMessage(IWin32Window window, string message, DialogResult defaultResult) =>
        ShowErrorQuestionMessage(window, message, defaultResult, null);
    #endregion

    #region ShowErrorQuestionMessage(IWin32Window, string, string)
    /// <summary>
    /// Shows question message with the error icon.
    /// </summary>
    /// <param name="window"></param>
    /// <param name="message"></param>
    /// <param name="title"></param>
    public static bool ShowErrorQuestionMessage(IWin32Window window, string message, string title) =>
        ShowErrorQuestionMessage(window, message, DialogResult.No, title);
    #endregion

    #region ShowErrorQuestionMessage(IWin32Window, string, DialogResult, string)
    /// <summary>
    /// Shows question message with the error icon.
    /// </summary>
    /// <param name="window"></param>
    /// <param name="message"></param>
    /// <param name="defaultResult"></param>
    /// <param name="title"></param>
    public static bool ShowErrorQuestionMessage(IWin32Window window, string message, DialogResult defaultResult, string title) =>
        MessageBox.Show(window, message, title ?? (window as Form)?.Text ?? "", MessageBoxButtons.YesNo, MessageBoxIcon.Error, defaultResult == DialogResult.Yes ? MessageBoxDefaultButton.Button1 : MessageBoxDefaultButton.Button2) == DialogResult.Yes;
    #endregion

    #region ShowInfoMessage(IWin32Window, string)
    /// <summary>
    /// Shows information message.
    /// </summary>
    /// <param name="window"></param>
    /// <param name="message"></param>
    public static void ShowInfoMessage(IWin32Window window, string message) =>
        ShowInfoMessage(window, message, null);
    #endregion

    #region ShowInfoMessage(IWin32Window, string, string)
    /// <summary>
    /// Shows information message.
    /// </summary>
    /// <param name="window"></param>
    /// <param name="message"></param>
    /// <param name="title"></param>
    public static void ShowInfoMessage(IWin32Window window, string message, string title) =>
        MessageBox.Show(window, message, title ?? (window as Form)?.Text ?? "", MessageBoxButtons.OK, MessageBoxIcon.Information);
    #endregion

    #region ShowQuestionMessage(IWin32Window, string)
    /// <summary>
    /// Shows a question message.
    /// </summary>
    /// <param name="window"></param>
    /// <param name="message"></param>
    public static bool ShowQuestionMessage(IWin32Window window, string message) =>
        ShowQuestionMessage(window, message, DialogResult.No, null);
    #endregion

    #region ShowQuestionMessage(IWin32Window, string, DialogResult)
    /// <summary>
    /// Shows a question message.
    /// </summary>
    /// <param name="window"></param>
    /// <param name="message"></param>
    /// <param name="defaultResult"></param>
    public static bool ShowQuestionMessage(IWin32Window window, string message, DialogResult defaultResult) =>
        ShowQuestionMessage(window, message, defaultResult, null);
    #endregion

    #region ShowQuestionMessage(IWin32Window, string, string)
    /// <summary>
    /// Shows a question message.
    /// </summary>
    /// <param name="window"></param>
    /// <param name="message"></param>
    /// <param name="title"></param>
    public static bool ShowQuestionMessage(IWin32Window window, string message, string title) =>
        ShowQuestionMessage(window, message, DialogResult.No, title);
    #endregion

    #region ShowQuestionMessage(IWin32Window, string, [DialogResult], [string])
    /// <summary>
    /// Shows a question message.
    /// </summary>
    /// <param name="window"></param>
    /// <param name="message"></param>
    /// <param name="defaultResult"></param>
    /// <param name="title"></param>
    public static bool ShowQuestionMessage(IWin32Window window, string message, DialogResult defaultResult, string title) =>
        MessageBox.Show(window, message, title ?? (window as Form)?.Text ?? "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, defaultResult == DialogResult.Yes ? MessageBoxDefaultButton.Button1 : MessageBoxDefaultButton.Button2) == DialogResult.Yes;
    #endregion

    #region ShowWaitCursor(Control)

    /// <summary>
    /// Shows wait cursor for the selected control.
    /// </summary>
    /// <param name="control"></param>
    public static void ShowWaitCursor(Control control)
    {
        control.UseWaitCursor = true;
        SendMessage(control.Handle, 0x20, control.Handle, (IntPtr)1);
    }

    #endregion

    #region ShowWaitCursor(Form)

    /// <summary>
    /// Shows wait cursor for the selected form.
    /// </summary>
    /// <param name="form"></param>
    public static void ShowWaitCursor(Form form)
    {
        form.UseWaitCursor = true;
        SendMessage(form.Handle, 0x20, form.Handle, (IntPtr)1);
    }

    #endregion

    #region ShowWarningMessage(IWin32Window, string)
    /// <summary>
    /// Shows warning popup.
    /// </summary>
    /// <param name="window"></param>
    /// <param name="message"></param>
    public static void ShowWarningMessage(IWin32Window window, string message) =>
        ShowWarningMessage(window, message, null);
    #endregion

    #region ShowWarningMessage(IWin32Window, string, string)
    /// <summary>
    /// Shows warning popup.
    /// </summary>
    /// <param name="window"></param>
    /// <param name="message"></param>
    /// <param name="title"></param>
    public static void ShowWarningMessage(IWin32Window window, string message, string title) =>
        MessageBox.Show(window, message, title ?? (window as Form)?.Text ?? "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    #endregion

    #region ShowWarningQuestionMessage(IWin32Window, string)
    /// <summary>
    /// Shows question message with the warning icon.
    /// </summary>
    /// <param name="window"></param>
    /// <param name="message"></param>
    public static bool ShowWarningQuestionMessage(IWin32Window window, string message) =>
        ShowWarningQuestionMessage(window, message, DialogResult.No, null);
    #endregion

    #region ShowWarningQuestionMessage(IWin32Window, string, DialogResult)
    /// <summary>
    /// Shows question message with the warning icon.
    /// </summary>
    /// <param name="window"></param>
    /// <param name="message"></param>
    /// <param name="defaultResult"></param>
    public static bool ShowWarningQuestionMessage(IWin32Window window, string message, DialogResult defaultResult) =>
        ShowWarningQuestionMessage(window, message, defaultResult, null);
    #endregion

    #region ShowWarningQuestionMessage(IWin32Window, string, string)
    /// <summary>
    /// Shows question message with the warning icon.
    /// </summary>
    /// <param name="window"></param>
    /// <param name="message"></param>
    /// <param name="title"></param>
    public static bool ShowWarningQuestionMessage(IWin32Window window, string message, string title) =>
        ShowWarningQuestionMessage(window, message, DialogResult.No, title);
    #endregion

    #region ShowWarningQuestionMessage(IWin32Window, string, DialogResult, string)
    /// <summary>
    /// Shows question message with the warning icon.
    /// </summary>
    /// <param name="window"></param>
    /// <param name="message"></param>
    /// <param name="defaultResult"></param>
    /// <param name="title"></param>
    public static bool ShowWarningQuestionMessage(IWin32Window window, string message, DialogResult defaultResult, string title) =>
        MessageBox.Show(window, message, title ?? (window as Form)?.Text ?? "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, defaultResult == DialogResult.Yes ? MessageBoxDefaultButton.Button1 : MessageBoxDefaultButton.Button2) == DialogResult.Yes;
    #endregion

    #endregion
}