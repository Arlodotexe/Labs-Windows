// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CommunityToolkit.Labs.Core.SourceGenerators.Attributes;

namespace CommunityToolkit.Labs.Core.SourceGenerators.Metadata;

/// <summary>
/// An INPC-enabled metadata container for data defined in an <see cref="ToolkitSampleTextOptionAttribute"/>.
/// </summary>
/// <remarks>
/// Instances of these are generated by the <see cref="ToolkitSampleMetadataGenerator"/> and
/// provided to the app alongside the sample registration.
/// </remarks>
public class ToolkitSampleTextOptionMetadataViewModel : IGeneratedToolkitSampleOptionViewModel
{
    private string? _title;
    private object _value;

    /// <summary>
    /// Creates a new instance of <see cref="ToolkitSampleTextOptionAttribute"/>.
    /// </summary>
    public ToolkitSampleTextOptionMetadataViewModel(string name, string placeholderText = "", string? title = null)
    {
        Name = name;
        _title = title;
        _value = placeholderText;
    }

    /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// A unique identifier for this option.
    /// </summary>
    /// <remarks>
    /// Used by the sample system to match up <see cref="ToolkitSampleTextOptionMetadataViewModel"/> to the original <see cref="ToolkitSampleTextOptionAttribute"/> and the control that declared it.
    /// </remarks>
    public string Name { get; }

    /// <summary>
    /// The current boolean value.
    /// </summary>
    /// <remarks>
    /// Provided to accomodate binding to a property that is a non-nullable <see cref="bool"/>.
    /// </remarks>
    public string PlaceholderText
    {
        get => (string)_value;
        set
        {
            _value = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }
    }

    /// <inheritdoc/>
    public object? Value
    {
        get => PlaceholderText;
        set
        {
            PlaceholderText = (string)(value ?? string.Empty);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }
    }

    /// <summary>
    /// A title to display on top of the boolean option.
    /// </summary>
    public string? Title
    {
        get => _title;
        set
        {
            _title = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
        }
    }
}
