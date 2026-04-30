using LitMotion;
using TMPro;
using UnityEngine;

namespace amenone.litmotiontext
{
    // Extension methods on TMP_Text that create multiple LitMotion handles for per-vertex sweep patterns.
    // Each pattern divides `duration` evenly across vertices, staggering their start times with WithDelay.
    // Vertex order: BL=0, TL=1, TR=2, BR=3
    public static class LitMotionTMPCharPatternMotion
    {
        // BL+TL animate over first half, TR+BR animate over second half.
        public static MotionHandle[] BindCharColorFadeLeftToRight(
            this TMP_Text text, int charIndex, Color from, Color to, float duration, Ease ease = Ease.Linear)
        {
            var seg = duration * 0.5f;
            var animator = TMPMotionAnimator.GetForPattern(text, charIndex, 4, from);
            var boxed = Box.Create(charIndex);
            return new[]
            {
                LMotion.Create(from, to, seg).WithEase(ease)
                    .WithOnComplete(animator.completeAction)
                    .Bind(animator, boxed, static (x, a, idx) => { a.charInfoArray[idx.Value].colorBL = x; a.SetDirty(); }),
                LMotion.Create(from, to, seg).WithEase(ease)
                    .WithOnComplete(animator.completeAction)
                    .Bind(animator, boxed, static (x, a, idx) => { a.charInfoArray[idx.Value].colorTL = x; a.SetDirty(); }),
                LMotion.Create(from, to, seg).WithEase(ease).WithDelay(seg)
                    .WithOnComplete(animator.completeAction)
                    .Bind(animator, boxed, static (x, a, idx) => { a.charInfoArray[idx.Value].colorTR = x; a.SetDirty(); }),
                LMotion.Create(from, to, seg).WithEase(ease).WithDelay(seg)
                    .WithOnComplete(animator.completeAction)
                    .Bind(animator, boxed, static (x, a, idx) => { a.charInfoArray[idx.Value].colorBR = x; a.SetDirty(); }),
            };
        }

        // TL → TR → BR → BL, each vertex takes duration/4.
        public static MotionHandle[] BindCharColorClockwise(
            this TMP_Text text, int charIndex, Color from, Color to, float duration, Ease ease = Ease.Linear)
        {
            var seg = duration * 0.25f;
            var animator = TMPMotionAnimator.GetForPattern(text, charIndex, 4, from);
            var boxed = Box.Create(charIndex);
            return new[]
            {
                LMotion.Create(from, to, seg).WithEase(ease)
                    .WithOnComplete(animator.completeAction)
                    .Bind(animator, boxed, static (x, a, idx) => { a.charInfoArray[idx.Value].colorTL = x; a.SetDirty(); }),
                LMotion.Create(from, to, seg).WithEase(ease).WithDelay(seg)
                    .WithOnComplete(animator.completeAction)
                    .Bind(animator, boxed, static (x, a, idx) => { a.charInfoArray[idx.Value].colorTR = x; a.SetDirty(); }),
                LMotion.Create(from, to, seg).WithEase(ease).WithDelay(seg * 2f)
                    .WithOnComplete(animator.completeAction)
                    .Bind(animator, boxed, static (x, a, idx) => { a.charInfoArray[idx.Value].colorBR = x; a.SetDirty(); }),
                LMotion.Create(from, to, seg).WithEase(ease).WithDelay(seg * 3f)
                    .WithOnComplete(animator.completeAction)
                    .Bind(animator, boxed, static (x, a, idx) => { a.charInfoArray[idx.Value].colorBL = x; a.SetDirty(); }),
            };
        }

        // TL → BL → BR → TR, each vertex takes duration/4.
        public static MotionHandle[] BindCharColorCounterClockwise(
            this TMP_Text text, int charIndex, Color from, Color to, float duration, Ease ease = Ease.Linear)
        {
            var seg = duration * 0.25f;
            var animator = TMPMotionAnimator.GetForPattern(text, charIndex, 4, from);
            var boxed = Box.Create(charIndex);
            return new[]
            {
                LMotion.Create(from, to, seg).WithEase(ease)
                    .WithOnComplete(animator.completeAction)
                    .Bind(animator, boxed, static (x, a, idx) => { a.charInfoArray[idx.Value].colorTL = x; a.SetDirty(); }),
                LMotion.Create(from, to, seg).WithEase(ease).WithDelay(seg)
                    .WithOnComplete(animator.completeAction)
                    .Bind(animator, boxed, static (x, a, idx) => { a.charInfoArray[idx.Value].colorBL = x; a.SetDirty(); }),
                LMotion.Create(from, to, seg).WithEase(ease).WithDelay(seg * 2f)
                    .WithOnComplete(animator.completeAction)
                    .Bind(animator, boxed, static (x, a, idx) => { a.charInfoArray[idx.Value].colorBR = x; a.SetDirty(); }),
                LMotion.Create(from, to, seg).WithEase(ease).WithDelay(seg * 3f)
                    .WithOnComplete(animator.completeAction)
                    .Bind(animator, boxed, static (x, a, idx) => { a.charInfoArray[idx.Value].colorTR = x; a.SetDirty(); }),
            };
        }

        // BL+TL animate over first half, TR+BR animate over second half.
        public static MotionHandle[] BindCharUv3FadeLeftToRight(
            this TMP_Text text, int charIndex, Vector2 from, Vector2 to, float duration, Ease ease = Ease.Linear)
        {
            var seg = duration * 0.5f;
            var animator = TMPMotionAnimator.GetForUv3Pattern(text, charIndex, 4, from);
            var boxed = Box.Create(charIndex);
            return new[]
            {
                LMotion.Create(from, to, seg).WithEase(ease)
                    .WithOnComplete(animator.completeAction)
                    .Bind(animator, boxed, static (x, a, idx) => { a.charInfoArray[idx.Value].uv3BL = x; a.SetDirty(); }),
                LMotion.Create(from, to, seg).WithEase(ease)
                    .WithOnComplete(animator.completeAction)
                    .Bind(animator, boxed, static (x, a, idx) => { a.charInfoArray[idx.Value].uv3TL = x; a.SetDirty(); }),
                LMotion.Create(from, to, seg).WithEase(ease).WithDelay(seg)
                    .WithOnComplete(animator.completeAction)
                    .Bind(animator, boxed, static (x, a, idx) => { a.charInfoArray[idx.Value].uv3TR = x; a.SetDirty(); }),
                LMotion.Create(from, to, seg).WithEase(ease).WithDelay(seg)
                    .WithOnComplete(animator.completeAction)
                    .Bind(animator, boxed, static (x, a, idx) => { a.charInfoArray[idx.Value].uv3BR = x; a.SetDirty(); }),
            };
        }

        // TL → TR → BR → BL, each vertex takes duration/4.
        public static MotionHandle[] BindCharUv3Clockwise(
            this TMP_Text text, int charIndex, Vector2 from, Vector2 to, float duration, Ease ease = Ease.Linear)
        {
            var seg = duration * 0.25f;
            var animator = TMPMotionAnimator.GetForUv3Pattern(text, charIndex, 4, from);
            var boxed = Box.Create(charIndex);
            return new[]
            {
                LMotion.Create(from, to, seg).WithEase(ease)
                    .WithOnComplete(animator.completeAction)
                    .Bind(animator, boxed, static (x, a, idx) => { a.charInfoArray[idx.Value].uv3TL = x; a.SetDirty(); }),
                LMotion.Create(from, to, seg).WithEase(ease).WithDelay(seg)
                    .WithOnComplete(animator.completeAction)
                    .Bind(animator, boxed, static (x, a, idx) => { a.charInfoArray[idx.Value].uv3TR = x; a.SetDirty(); }),
                LMotion.Create(from, to, seg).WithEase(ease).WithDelay(seg * 2f)
                    .WithOnComplete(animator.completeAction)
                    .Bind(animator, boxed, static (x, a, idx) => { a.charInfoArray[idx.Value].uv3BR = x; a.SetDirty(); }),
                LMotion.Create(from, to, seg).WithEase(ease).WithDelay(seg * 3f)
                    .WithOnComplete(animator.completeAction)
                    .Bind(animator, boxed, static (x, a, idx) => { a.charInfoArray[idx.Value].uv3BL = x; a.SetDirty(); }),
            };
        }

        // TL → BL → BR → TR, each vertex takes duration/4.
        public static MotionHandle[] BindCharUv3CounterClockwise(
            this TMP_Text text, int charIndex, Vector2 from, Vector2 to, float duration, Ease ease = Ease.Linear)
        {
            var seg = duration * 0.25f;
            var animator = TMPMotionAnimator.GetForUv3Pattern(text, charIndex, 4, from);
            var boxed = Box.Create(charIndex);
            return new[]
            {
                LMotion.Create(from, to, seg).WithEase(ease)
                    .WithOnComplete(animator.completeAction)
                    .Bind(animator, boxed, static (x, a, idx) => { a.charInfoArray[idx.Value].uv3TL = x; a.SetDirty(); }),
                LMotion.Create(from, to, seg).WithEase(ease).WithDelay(seg)
                    .WithOnComplete(animator.completeAction)
                    .Bind(animator, boxed, static (x, a, idx) => { a.charInfoArray[idx.Value].uv3BL = x; a.SetDirty(); }),
                LMotion.Create(from, to, seg).WithEase(ease).WithDelay(seg * 2f)
                    .WithOnComplete(animator.completeAction)
                    .Bind(animator, boxed, static (x, a, idx) => { a.charInfoArray[idx.Value].uv3BR = x; a.SetDirty(); }),
                LMotion.Create(from, to, seg).WithEase(ease).WithDelay(seg * 3f)
                    .WithOnComplete(animator.completeAction)
                    .Bind(animator, boxed, static (x, a, idx) => { a.charInfoArray[idx.Value].uv3TR = x; a.SetDirty(); }),
            };
        }
    }
}
