using System.ComponentModel;

namespace Kontur.ImageTransformer.Demo
{
    public enum Actions
    {
        [Description("grayscale")] Grayscale,
        [Description("threshold")] Threshold,
        [Description("sepia")] Sepia,
        [Description("rotate-cw")] Rotatecw,
        [Description("rotate-ccw")] Rotateccw,
        [Description("flip-v")] Flipv,
        [Description("flip-h")] Fliph
    }
}