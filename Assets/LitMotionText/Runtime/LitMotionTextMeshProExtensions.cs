using System.Buffers;
using LitMotion;
using LitMotion.Extensions;
using TMPro;
using Unity.Collections;
using UnityEngine;

namespace amenone.litmotiontext
{
    /// <summary>
    /// Provides binding extension methods for TMP_Text
    /// </summary>
    public static class LitMotionTextMeshProExtensions
    {

        /// <summary>
        /// Create motion data and bind it to the character color.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <param name="charIndex">Target character index</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToTMPCharColorCustom<TOptions, TAdapter>(this MotionBuilder<Color, TOptions, TAdapter> builder, TMP_Text text, int charIndex, VertexPattern colorPattern = VertexPattern.Uniform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Color, TOptions>
        {
            Error.IsNull(text);

            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            animator.SetColorPattern(charIndex, colorPattern);

            var handle = builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].color = x;
                animator.SetDirty();
            });

            return handle;
        }

        public static MotionHandle BindToTMPCharColorCustom<TOptions, TAdapter>(this MotionBuilder<Color, TOptions, TAdapter> builder, TMP_Text text, int charIndex, Color initialValue, VertexPattern colorPattern = VertexPattern.Uniform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Color, TOptions>
        {
            Error.IsNull(text);

            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            animator.SetInitialCol(initialValue);
            animator.SetColorPattern(charIndex, colorPattern);

            var handle = builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].color = x;
                animator.SetDirty();
            });

            return handle;
        }

        public static MotionHandle BindToTMPCharColorCustomA<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text, int charIndex, float initialValue, VertexPattern colorPattern = VertexPattern.Uniform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);

            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            animator.SetInitialAlpha(initialValue);
            animator.SetColorPattern(charIndex, colorPattern);

            var handle = builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].color.a = x;
                animator.SetDirty();
            });

            return handle;
        }

        public static MotionHandle BindToTMPCharColorCustomA<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text, int charIndex, Color initialValue, VertexPattern colorPattern = VertexPattern.Uniform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);

            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            animator.SetInitialCol(initialValue);
            animator.SetColorPattern(charIndex, colorPattern);

            var handle = builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].color.a = x;
                animator.SetDirty();
            });

            return handle;
        }

#if LITMOTION_TMP_TANGENT_OVERRIDE
        public static MotionHandle BindToTMPCharTangentCustom<TOptions, TAdapter>(this MotionBuilder<Vector4, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector4, TOptions>
        {
            Error.IsNull(text);

            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);

            var handle = builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].tangent = x;
                animator.SetDirty();
            });

            return handle;
        }

        public static MotionHandle BindToTMPCharTangentCustom<TOptions, TAdapter>(this MotionBuilder<Vector4, TOptions, TAdapter> builder, TMP_Text text, int charIndex,Vector4 initialValue)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector4, TOptions>
        {
            Error.IsNull(text);

            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            animator.SetInitialTangent(initialValue);

            var handle = builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].tangent = x;
                animator.SetDirty();
            });

            return handle;
        }
#endif
        public static MotionHandle BindToTMPCharUv3Custom<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, TMP_Text text, int charIndex, VertexPattern uv3Pattern = VertexPattern.Uniform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(text);

            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            animator.SetUv3Pattern(charIndex, uv3Pattern);

            var handle = builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].uv3 = x;
                animator.SetDirty();
            });

            return handle;
        }

        public static MotionHandle BindToTMPCharUv3Custom<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, TMP_Text text, int charIndex, Vector2 defaultValue, VertexPattern uv3Pattern = VertexPattern.Uniform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(text);

            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            animator.SetInitialUV3(defaultValue);
            animator.SetUv3Pattern(charIndex, uv3Pattern);

            var handle = builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].uv3 = x;
                animator.SetDirty();
            });

            return handle;
        }

        public static MotionHandle BindToTMPCharPositionCustom<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, TMP_Text text, int charIndex,Vector3 initialValue)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(text);

            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            animator.SetInitialPosition(initialValue);

            var handle = builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].position = x;
                animator.SetDirty();
            });

            return handle;
        }

        /// <summary>
        /// Create motion data and bind it to the character position.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <param name="charIndex">Target character index</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToTMPCharPositionCustom<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(text);

            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);

            var handle = builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].position = x;
                animator.SetDirty();
            });

            return handle;
        }

        /// <summary>
        /// Create motion data and bind it to the character position.x.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <param name="charIndex">Target character index</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToTMPCharPositionCustomX<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);

            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            var handle = builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].position.x = x;
                animator.SetDirty();
            });

            return handle;
        }

        /// <summary>
        /// Create motion data and bind it to the character position.y.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <param name="charIndex">Target character index</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToTMPCharPositionCustomY<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);

            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);

            var handle = builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].position.y = x;
                animator.SetDirty();
            });

            return handle;
        }

        /// <summary>
        /// Create motion data and bind it to the character position.z.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <param name="charIndex">Target character index</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToTMPCharPositionCustomZ<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);

            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);

            var handle = builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].position.z = x;
                animator.SetDirty();
            });

            return handle;
        }

        /// <summary>
        /// Create motion data and bind it to the character rotation.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <param name="charIndex">Target character index</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToTMPCharRotationCustom<TOptions, TAdapter>(this MotionBuilder<Quaternion, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Quaternion, TOptions>
        {
            Error.IsNull(text);

            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);

            var handle = builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].rotation = x;
                animator.SetDirty();
            });

            return handle;
        }

        public static MotionHandle BindToTMPCharEulerAnglesCustom<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, TMP_Text text, int charIndex , Vector3 initialValue)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(text);

            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            animator.SetInitialRotation(Quaternion.Euler(initialValue));

            var handle = builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].rotation = Quaternion.Euler(x);
                animator.SetDirty();
            });

            return handle;
        }

        /// <summary>
        /// Create motion data and bind it to the character rotation (using euler angles).
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <param name="charIndex">Target character index</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToTMPCharEulerAnglesCustom<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(text);

            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);

            var handle = builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].rotation = Quaternion.Euler(x);
                animator.SetDirty();
            });

            return handle;
        }

        /// <summary>
        /// Create motion data and bind it to the character rotation (using euler angles).
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <param name="charIndex">Target character index</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToTMPCharEulerAnglesCustomX<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);

            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);

            var handle = builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                var eulerAngles = animator.charInfoArray[charIndex.Value].rotation.eulerAngles;
                eulerAngles.x = x;
                animator.charInfoArray[charIndex.Value].rotation = Quaternion.Euler(eulerAngles);
                animator.SetDirty();
            });

            return handle;
        }

        /// <summary>
        /// Create motion data and bind it to the character rotation (using euler angles).
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <param name="charIndex">Target character index</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToTMPCharEulerAnglesCustomY<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);

            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);

            var handle = builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                var eulerAngles = animator.charInfoArray[charIndex.Value].rotation.eulerAngles;
                eulerAngles.y = x;
                animator.charInfoArray[charIndex.Value].rotation = Quaternion.Euler(eulerAngles);
                animator.SetDirty();
            });

            return handle;
        }

        /// <summary>
        /// Create motion data and bind it to the character rotation (using euler angles).
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <param name="charIndex">Target character index</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToTMPCharEulerAnglesCustomZ<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);

            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);

            var handle = builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                var eulerAngles = animator.charInfoArray[charIndex.Value].rotation.eulerAngles;
                eulerAngles.z = x;
                animator.charInfoArray[charIndex.Value].rotation = Quaternion.Euler(eulerAngles);
                animator.SetDirty();
            });

            return handle;
        }

        public static MotionHandle BindToTMPCharScaleCustom<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, TMP_Text text, int charIndex , Vector3 initialValue)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(text);

            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);
            animator.SetInitialScale( initialValue);

            var handle = builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].scale = x;
                animator.SetDirty();
            });

            return handle;
        }

        /// <summary>
        /// Create motion data and bind it to the character scale.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <param name="charIndex">Target character index</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToTMPCharScaleCustom<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(text);

            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);

            var handle = builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].scale = x;
                animator.SetDirty();
            });

            return handle;
        }

        /// <summary>
        /// Create motion data and bind it to the character scale.x.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <param name="charIndex">Target character index</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToTMPCharScaleCustomX<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);

            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);

            var handle = builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].scale.x = x;
                animator.SetDirty();
            });

            return handle;
        }

        /// <summary>
        /// Create motion data and bind it to the character scale.y.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <param name="charIndex">Target character index</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToTMPCharScaleCustomY<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);

            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);

            var handle = builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].scale.y = x;
                animator.SetDirty();
            });

            return handle;
        }

        /// <summary>
        /// Create motion data and bind it to the character scale.z.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <param name="charIndex">Target character index</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToTMPCharScaleCustomZ<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text, int charIndex)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);

            var animator = TMPMotionAnimator.Get(text);
            animator.EnsureCapacity(charIndex + 1);

            var handle = builder.WithOnComplete(animator.completeAction).Bind(animator, Box.Create(charIndex), static (x, animator, charIndex) =>
            {
                animator.charInfoArray[charIndex.Value].scale.z = x;
                animator.SetDirty();
            });

            return handle;
        }
    }

}