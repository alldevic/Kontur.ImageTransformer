using System.ComponentModel;

namespace Kontur.ImageTransformer.Demo
{
    public enum Actions
    {
        [Description("grayscale")] grayscale,
        [Description("threshold")] threshold,
        [Description("sepia")] sepia,
        [Description("rotate-cw")] rotatecw,
        [Description("rotate-ccw")] rotateccw,
        [Description("flip-v")] flipv,
        [Description("flip-h")] fliph
    }
}