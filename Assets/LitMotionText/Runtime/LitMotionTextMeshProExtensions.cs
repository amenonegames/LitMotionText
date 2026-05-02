using LitMotion;
using LitMotion.Extensions;
using TMPro;
using UnityEngine;

namespace amenone.litmotiontext
{
    public static class LitMotionTextMeshProExtensions
    {
        public static MotionHandle BindToTMPCharColorCustom<TOptions, TAdapter>(this MotionBuilder<Color, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Color, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                ref var info = ref animator.charInfoArray[charIndex.Value];
                info.colorBL = x; info.colorTL = x; info.colorTR = x; info.colorBR = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharColorCustom<TOptions, TAdapter>(this MotionBuilder<Color, TOptions, TAdapter> builder, TMP_Text text, int charIndex, Color initialValue)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Color, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            animator.SetInitialCol(initialValue);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                ref var info = ref animator.charInfoArray[charIndex.Value];
                info.colorBL = x; info.colorTL = x; info.colorTR = x; info.colorBR = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharColorCustomA<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text, int charIndex, float initialValue)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            animator.SetInitialAlpha(initialValue);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                ref var info = ref animator.charInfoArray[charIndex.Value];
                info.colorBL.a = x; info.colorTL.a = x; info.colorTR.a = x; info.colorBR.a = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharColorCustomA<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text, int charIndex, Color initialValue)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            animator.SetInitialCol(initialValue);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                ref var info = ref animator.charInfoArray[charIndex.Value];
                info.colorBL.a = x; info.colorTL.a = x; info.colorTR.a = x; info.colorBR.a = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharColorBL<TOptions, TAdapter>(this MotionBuilder<Color, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Color, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].colorBL = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharColorBL<TOptions, TAdapter>(this MotionBuilder<Color, TOptions, TAdapter> builder, TMP_Text text, int charIndex, Color initialValue)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Color, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            animator.SetInitialColBL(initialValue);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].colorBL = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharColorTL<TOptions, TAdapter>(this MotionBuilder<Color, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Color, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].colorTL = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharColorTL<TOptions, TAdapter>(this MotionBuilder<Color, TOptions, TAdapter> builder, TMP_Text text, int charIndex, Color initialValue)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Color, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            animator.SetInitialColTL(initialValue);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].colorTL = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharColorTR<TOptions, TAdapter>(this MotionBuilder<Color, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Color, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].colorTR = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharColorTR<TOptions, TAdapter>(this MotionBuilder<Color, TOptions, TAdapter> builder, TMP_Text text, int charIndex, Color initialValue)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Color, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            animator.SetInitialColTR(initialValue);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].colorTR = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharColorBR<TOptions, TAdapter>(this MotionBuilder<Color, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Color, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].colorBR = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharColorBR<TOptions, TAdapter>(this MotionBuilder<Color, TOptions, TAdapter> builder, TMP_Text text, int charIndex, Color initialValue)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Color, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            animator.SetInitialColBR(initialValue);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].colorBR = x;
                animator.SetDirty();
            });
        }

#if LITMOTION_TMP_TANGENT_OVERRIDE
        public static MotionHandle BindToTMPCharTangentCustom<TOptions, TAdapter>(this MotionBuilder<Vector4, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector4, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].tangent = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharTangentCustom<TOptions, TAdapter>(this MotionBuilder<Vector4, TOptions, TAdapter> builder, TMP_Text text, int charIndex, Vector4 initialValue)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector4, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            animator.SetInitialTangent(initialValue);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].tangent = x;
                animator.SetDirty();
            });
        }
#endif

        public static MotionHandle BindToTMPCharUv3Custom<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                ref var info = ref animator.charInfoArray[charIndex.Value];
                info.uv3BL = x; info.uv3TL = x; info.uv3TR = x; info.uv3BR = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharUv3Custom<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, TMP_Text text, int charIndex, Vector2 defaultValue)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            animator.SetInitialUV3(defaultValue);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                ref var info = ref animator.charInfoArray[charIndex.Value];
                info.uv3BL = x; info.uv3TL = x; info.uv3TR = x; info.uv3BR = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharUv3CustomBL<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].uv3BL = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharUv3CustomTL<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].uv3TL = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharUv3CustomTR<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].uv3TR = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharUv3CustomBR<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].uv3BR = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharPositionCustom<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].position = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharPositionCustom<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, TMP_Text text, int charIndex, Vector3 initialValue)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            animator.SetInitialPosition(initialValue);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].position = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharPositionCustomX<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].position.x = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharPositionCustomY<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].position.y = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharPositionCustomZ<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].position.z = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharRotationCustom<TOptions, TAdapter>(this MotionBuilder<Quaternion, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Quaternion, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].rotation = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharEulerAnglesCustom<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].rotation = Quaternion.Euler(x);
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharEulerAnglesCustom<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, TMP_Text text, int charIndex, Vector3 initialValue)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            animator.SetInitialRotation(Quaternion.Euler(initialValue));
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].rotation = Quaternion.Euler(x);
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharEulerAnglesCustomX<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                var e = animator.charInfoArray[charIndex.Value].rotation.eulerAngles;
                e.x = x;
                animator.charInfoArray[charIndex.Value].rotation = Quaternion.Euler(e);
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharEulerAnglesCustomY<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                var e = animator.charInfoArray[charIndex.Value].rotation.eulerAngles;
                e.y = x;
                animator.charInfoArray[charIndex.Value].rotation = Quaternion.Euler(e);
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharEulerAnglesCustomZ<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                var e = animator.charInfoArray[charIndex.Value].rotation.eulerAngles;
                e.z = x;
                animator.charInfoArray[charIndex.Value].rotation = Quaternion.Euler(e);
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharScaleCustom<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].scale = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharScaleCustom<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, TMP_Text text, int charIndex, Vector3 initialValue)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            animator.SetInitialScale(initialValue);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].scale = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharScaleCustomX<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].scale.x = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharScaleCustomY<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].scale.y = x;
                animator.SetDirty();
            });
        }

        public static MotionHandle BindToTMPCharScaleCustomZ<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);
            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            return builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].scale.z = x;
                animator.SetDirty();
            });
        }
    }
}
