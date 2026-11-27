using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Laast.Client.ViewModels.Common;

public partial class Field<T>(T value) : ObservableObject
{
    [ObservableProperty] private T _value = value;
    [ObservableProperty] private string _errorMsg = "";
    [ObservableProperty] private bool _isDirty;
    public event Action<T>? ValueChanged;

    partial void OnValueChanged(T value)
    {
        IsDirty = true;
        ValueChanged?.Invoke(value);
    }
}

public static class Field
{
    public static Field<T> Create<T>(T value) => new(value);
}