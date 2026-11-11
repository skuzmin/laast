using System;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Laast.Client.Enums;

namespace Laast.Client.Models;

public class ActionTypeOption(PilotActionType id, string title, string iconName)
{
    public PilotActionType Id { get; set; } = id;
    public string Title { get; set; } = title;
    public Bitmap IconPath { get; set; } = new(AssetLoader.Open(new Uri($"avares://Laast.Client/Assets/Icons/{iconName}")));
}