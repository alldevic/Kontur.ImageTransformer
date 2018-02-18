using System.ComponentModel;

namespace Kontur.ImageTransformerDemo
{
    public enum Actions
    {
        [Description("Grayscale filter")] grayscale,
        [Description("Threshold filter")] threshold,
        [Description("Sepia filter")] sepia,
        [Description("Rotate 90 degree")] rotatecw,
        [Description("Rotate 270 degree")] rotateccw,
        [Description("Flip vertically")] flipv,
        [Description("Flip horizontally")] fliph
    }
}