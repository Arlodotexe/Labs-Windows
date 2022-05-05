// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Windows.UI.Core;

#if !WINAPPSDK
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using CursorEnum = Windows.UI.Core.CoreCursorType;
#else
using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using CursorEnum = Microsoft.UI.Input.InputSystemCursorShape;
#endif

namespace CommunityToolkit.Labs.WinUI;

/// <summary>
/// Properties for <see cref="SizerBase"/>
/// </summary>
public partial class SizerBase : Control
{
    private CursorEnum _cursorToUse = CursorEnum.SizeWestEast;

    /// <summary>
    /// Gets or sets the cursor to use when hovering over the gripper bar. If left as <c>null</c>, the control will manage the cursor automatically based on the <see cref="Orientation"/> property value.
    /// </summary>
    public CursorEnum Cursor
    {
        get { return (CursorEnum)GetValue(CursorProperty); }
        set { SetValue(CursorProperty, value); }
    }

    /// <summary>
    /// Identifies the <see cref="Cursor"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CursorProperty =
        DependencyProperty.Register(nameof(Cursor), typeof(CursorEnum), typeof(SizerBase), new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the incremental amount of change for draging with the mouse or touch of a sizer control. Effectively a snapping increment for changes. The default is 1.
    /// </summary>
    /// <example>
    /// For instance, if the DragIncrement is set to 16. Then when a component is resized with the sizer, it will only increase or decrease in size in that increment. I.e. -16, 0, 16, 32, 48, etc...
    /// </example>
    /// <remarks>
    /// This value is indepedent of the <see cref="KeyboardIncrement"/> property. If you need to provide consistent snapping when moving regardless of input device, set these properties to the same value.
    /// </remarks>
    public double DragIncrement
    {
        get { return (double)GetValue(DragIncrementProperty); }
        set { SetValue(DragIncrementProperty, value); }
    }

    /// <summary>
    /// Identifies the <see cref="DragIncrement"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DragIncrementProperty =
        DependencyProperty.Register(nameof(DragIncrement), typeof(double), typeof(SizerBase), new PropertyMetadata(1d));

    /// <summary>
    /// Gets or sets the distance each press of an arrow key moves a sizer control. The default is 8.
    /// </summary>
    /// <remarks>
    /// This value is independent of the <see cref="DragIncrement"/> setting when using mouse/touch. If you want a consistent behavior regardless of input device, set them to the same value if snapping is required.
    /// </remarks>
    public double KeyboardIncrement
    {
        get { return (double)GetValue(KeyboardIncrementProperty); }
        set { SetValue(KeyboardIncrementProperty, value); }
    }

    /// <summary>
    /// Identifies the <see cref="KeyboardIncrement"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty KeyboardIncrementProperty =
        DependencyProperty.Register(nameof(KeyboardIncrement), typeof(double), typeof(SizerBase), new PropertyMetadata(8d));

    /// <summary>
    /// Gets or sets the orientation the sizer will be and how it will interact with other elements. Defaults to <see cref="Orientation.Vertical"/>.
    /// </summary>
    /// <remarks>
    /// Note if using <see cref="GridSplitter"/>, use the <see cref="GridSplitter.ResizeDirection"/> property instead.
    /// </remarks>
    public Orientation Orientation
    {
        get { return (Orientation)GetValue(OrientationProperty); }
        set { SetValue(OrientationProperty, value); }
    }

    /// <summary>
    /// Identifies the <see cref="Orientation"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OrientationProperty =
        DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(SizerBase), new PropertyMetadata(Orientation.Vertical, OnOrientationPropertyChanged));

    private static void OnOrientationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is SizerBase gripper)
        {
            gripper._cursorToUse = gripper.Orientation == Orientation.Vertical ? CursorEnum.SizeWestEast : CursorEnum.SizeNorthSouth;
#if WINAPPSDK
            var cursor = gripper.ReadLocalValue(CursorProperty);
            if (cursor == DependencyProperty.UnsetValue)
            {
                cursor = gripper._cursorToUse;
            }
            var scursor = InputSystemCursor.Create(gripper._cursorToUse);
            gripper.ProtectedCursor = scursor;
#else
            // On UWP, we use the extension in XAML to control this behavior, so we'll update it here.
            gripper.Cursor = gripper._cursorToUse;
#endif
        }
    }
}
