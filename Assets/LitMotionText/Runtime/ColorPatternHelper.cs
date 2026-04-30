using UnityEngine;

namespace amenone.litmotiontext
{
    // TMP vertex order per quad: BL=0, TL=1, TR=2, BR=3
    internal static class VertexPatternHelper
    {
        internal static void ApplyColor(Color32[] colors, int vertexIndex, Color c, VertexPattern pattern)
        {
            switch (pattern)
            {
                case VertexPattern.FadeLeftToRight:
                    ApplyColorFadeLeftToRight(colors, vertexIndex, c);
                    break;
                case VertexPattern.ClockwiseFromTopLeft:
                    ApplyColorClockwiseFromTopLeft(colors, vertexIndex, c);
                    break;
                case VertexPattern.CounterClockwiseFromTopLeft:
                    ApplyColorCounterClockwiseFromTopLeft(colors, vertexIndex, c);
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

        internal static void ApplyUv3(Vector2[] uv3, int vertexIndex, Vector2 v, VertexPattern pattern)
        {
            switch (pattern)
            {
                case VertexPattern.FadeLeftToRight:
                    ApplyUv3FadeLeftToRight(uv3, vertexIndex, v);
                    break;
                case VertexPattern.ClockwiseFromTopLeft:
                    ApplyUv3ClockwiseFromTopLeft(uv3, vertexIndex, v);
                    break;
                case VertexPattern.CounterClockwiseFromTopLeft:
                    ApplyUv3CounterClockwiseFromTopLeft(uv3, vertexIndex, v);
                    break;
                default:
                    uv3[vertexIndex + 0] = v;
                    uv3[vertexIndex + 1] = v;
                    uv3[vertexIndex + 2] = v;
                    uv3[vertexIndex + 3] = v;
                    break;
            }
        }

        static void ApplyColorFadeLeftToRight(Color32[] colors, int vertexIndex, Color c)
        {
            Color32 opaque = c;
            Color32 transparent = new Color(c.r, c.g, c.b, 0f);
            colors[vertexIndex + 0] = opaque;       // BL - left edge
            colors[vertexIndex + 1] = opaque;       // TL - left edge
            colors[vertexIndex + 2] = transparent;  // TR - right edge
            colors[vertexIndex + 3] = transparent;  // BR - right edge
        }

        // TL(1) → TR(2) → BR(3) → BL(0)
        static void ApplyColorClockwiseFromTopLeft(Color32[] colors, int vertexIndex, Color c)
        {
            colors[vertexIndex + 1] = c;                                           // TL
            colors[vertexIndex + 2] = new Color(c.r, c.g, c.b, c.a * (2f / 3f)); // TR
            colors[vertexIndex + 3] = new Color(c.r, c.g, c.b, c.a * (1f / 3f)); // BR
            colors[vertexIndex + 0] = new Color(c.r, c.g, c.b, 0f);              // BL
        }

        // TL(1) → BL(0) → BR(3) → TR(2)
        static void ApplyColorCounterClockwiseFromTopLeft(Color32[] colors, int vertexIndex, Color c)
        {
            colors[vertexIndex + 1] = c;                                           // TL
            colors[vertexIndex + 0] = new Color(c.r, c.g, c.b, c.a * (2f / 3f)); // BL
            colors[vertexIndex + 3] = new Color(c.r, c.g, c.b, c.a * (1f / 3f)); // BR
            colors[vertexIndex + 2] = new Color(c.r, c.g, c.b, 0f);              // TR
        }

        static void ApplyUv3FadeLeftToRight(Vector2[] uv3, int vertexIndex, Vector2 v)
        {
            uv3[vertexIndex + 0] = v;             // BL - left edge
            uv3[vertexIndex + 1] = v;             // TL - left edge
            uv3[vertexIndex + 2] = Vector2.zero;  // TR - right edge
            uv3[vertexIndex + 3] = Vector2.zero;  // BR - right edge
        }

        // TL(1) → TR(2) → BR(3) → BL(0)
        static void ApplyUv3ClockwiseFromTopLeft(Vector2[] uv3, int vertexIndex, Vector2 v)
        {
            uv3[vertexIndex + 1] = v;              // TL
            uv3[vertexIndex + 2] = v * (2f / 3f); // TR
            uv3[vertexIndex + 3] = v * (1f / 3f); // BR
            uv3[vertexIndex + 0] = Vector2.zero;   // BL
        }

        // TL(1) → BL(0) → BR(3) → TR(2)
        static void ApplyUv3CounterClockwiseFromTopLeft(Vector2[] uv3, int vertexIndex, Vector2 v)
        {
            uv3[vertexIndex + 1] = v;              // TL
            uv3[vertexIndex + 0] = v * (2f / 3f); // BL
            uv3[vertexIndex + 3] = v * (1f / 3f); // BR
            uv3[vertexIndex + 2] = Vector2.zero;   // TR
        }
    }
}
