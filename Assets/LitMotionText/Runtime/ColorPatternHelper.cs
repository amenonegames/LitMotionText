using UnityEngine;

namespace amenone.litmotiontext
{
    // TMP vertex order per quad: BL=0, TL=1, TR=2, BR=3
    internal static class ColorPatternHelper
    {
        internal static void Apply(Color32[] colors, int vertexIndex, Color c, ColorPattern pattern)
        {
            switch (pattern)
            {
                case ColorPattern.FadeLeftToRight:
                    ApplyFadeLeftToRight(colors, vertexIndex, c);
                    break;
                case ColorPattern.ClockwiseFromTopLeft:
                    ApplyClockwiseFromTopLeft(colors, vertexIndex, c);
                    break;
                case ColorPattern.CounterClockwiseFromTopLeft:
                    ApplyCounterClockwiseFromTopLeft(colors, vertexIndex, c);
                    break;
                default:
                    Color32 c32 = c;
                    colors[vertexIndex + 0] = c32;
                    colors[vertexIndex + 1] = c32;
                    colors[vertexIndex + 2] = c32;
                    colors[vertexIndex + 3] = c32;
                    break;
            }
        }

        static void ApplyFadeLeftToRight(Color32[] colors, int vertexIndex, Color c)
        {
            Color32 opaque = c;
            Color32 transparent = new Color(c.r, c.g, c.b, 0f);
            colors[vertexIndex + 0] = opaque;       // BL - left edge
            colors[vertexIndex + 1] = opaque;       // TL - left edge
            colors[vertexIndex + 2] = transparent;  // TR - right edge
            colors[vertexIndex + 3] = transparent;  // BR - right edge
        }

        // TL(1) → TR(2) → BR(3) → BL(0)
        static void ApplyClockwiseFromTopLeft(Color32[] colors, int vertexIndex, Color c)
        {
            colors[vertexIndex + 1] = c;                                           // TL
            colors[vertexIndex + 2] = new Color(c.r, c.g, c.b, c.a * (2f / 3f)); // TR
            colors[vertexIndex + 3] = new Color(c.r, c.g, c.b, c.a * (1f / 3f)); // BR
            colors[vertexIndex + 0] = new Color(c.r, c.g, c.b, 0f);              // BL
        }

        // TL(1) → BL(0) → BR(3) → TR(2)
        static void ApplyCounterClockwiseFromTopLeft(Color32[] colors, int vertexIndex, Color c)
        {
            colors[vertexIndex + 1] = c;                                           // TL
            colors[vertexIndex + 0] = new Color(c.r, c.g, c.b, c.a * (2f / 3f)); // BL
            colors[vertexIndex + 3] = new Color(c.r, c.g, c.b, c.a * (1f / 3f)); // BR
            colors[vertexIndex + 2] = new Color(c.r, c.g, c.b, 0f);              // TR
        }
    }
}
