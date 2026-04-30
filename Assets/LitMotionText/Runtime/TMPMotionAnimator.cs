using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LitMotion;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace amenone.litmotiontext
{
    internal sealed class TMPMotionAnimator
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        static void Init()
        {
#if UNITY_EDITOR
            var domainReloadDisabled = EditorSettings.enterPlayModeOptionsEnabled && EditorSettings.enterPlayModeOptions.HasFlag(EnterPlayModeOptions.DisableDomainReload);
            if (!domainReloadDisabled && initialized) return;
#else
            if (initialized) return;
#endif
            PlayerLoopHelper.OnUpdate += UpdateAnimators;
            initialized = true;
        }

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void InitEditor()
        {
            EditorApplication.update += UpdateAnimatorsEditor;
        }
#endif

        static bool initialized;
        static TMPMotionAnimator rootNode;

        internal static TMPMotionAnimator Get(TMP_Text text)
        {
            if (textToAnimator.TryGetValue(text, out var animator))
            {
                animator.Reset();
                animator.refCount++;
                return animator;
            }

            // get or create animator
            animator = rootNode ?? new();
            rootNode = animator.nextNode;
            animator.nextNode = null;

            // set target
            animator.target = text;
            animator.Reset();

            // increment ref count
            animator.refCount++;

            // add to array
            if (tail == animators.Length)
            {
                Array.Resize(ref animators, tail * 2);
            }
            animators[tail] = animator;
            tail++;

            // add to dictionary
            textToAnimator.Add(text, animator);

            return animator;
        }

        internal static void Return(TMPMotionAnimator animator)
        {
            animator.nextNode = rootNode;
            rootNode = animator;

            textToAnimator.Remove(animator.target);
            animator.target = null;
        }

        static readonly Dictionary<TMP_Text, TMPMotionAnimator> textToAnimator = new();
        static TMPMotionAnimator[] animators = new TMPMotionAnimator[8];
        static int tail;

        static void UpdateAnimators()
        {
            var j = tail - 1;

            for (int i = 0; i < animators.Length; i++)
            {
                var animator = animators[i];
                if (animator != null)
                {
                    if (!animator.TryUpdate())
                    {
                        Return(animator);
                        animators[i] = null;
                    }
                    else
                    {
                        continue;
                    }
                }

                while (i < j)
                {
                    var fromTail = animators[j];
                    if (fromTail != null)
                    {
                        if (!fromTail.TryUpdate())
                        {
                            Return(fromTail);
                            animators[j] = null;
                            j--;
                            continue;
                        }
                        else
                        {
                            animators[i] = fromTail;
                            animators[j] = null;
                            j--;
                            goto NEXT_LOOP;
                        }
                    }
                    else
                    {
                        j--;
                    }
                }

                tail = i;
                break;

            NEXT_LOOP:
                continue;
            }
        }

#if UNITY_EDITOR
        static void UpdateAnimatorsEditor()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isCompiling || EditorApplication.isUpdating)
            {
                return;
            }
            UpdateAnimators();
        }
#endif

        internal struct CharInfo
        {
            public Vector3 position;
            public Vector3 scale;
            public Quaternion rotation;
            public Color color;
            public VertexPattern colorPattern;
            public VertexPattern uv3Pattern;
            public Vector2 uv3;
#if LITMOTION_TMP_TANGENT_OVERRIDE
            public Vector4 tangent;
#endif
        }

        private Color initialColor = Color.white;
        private float initialAlpha = -1f;
        private Quaternion initialRotation = Quaternion.identity;
        private Vector3 initialScale = Vector3.one;
        private Vector3 initialPosition = Vector3.zero;
        private Vector2 initialUV3 = Vector2.zero;
#if LITMOTION_TMP_TANGENT_OVERRIDE
        private Vector4 initialTangent = Vector4.zero;
#endif

        public void SetInitialAlpha( float alpha)
        {
            initialColor = Color.white;
            initialAlpha = alpha;
            Reset();
        }

        public void SetInitialCol( Color col)
        {
            initialColor = col;
            initialAlpha = -1;
            Reset();
        }

        public void SetInitialRotation(Quaternion rot)
        {
            initialRotation = rot;
            Reset();
        }

        public void SetInitialScale( Vector3 scale)
        {
            initialScale = scale;
            Reset();
        }

        public void SetInitialPosition( Vector3 pos)
        {
            initialPosition = pos;
            Reset();
        }

        public void SetInitialUV3(Vector2 uv3)
        {
            initialUV3 = uv3;
            Reset();
        }
#if LITMOTION_TMP_TANGENT_OVERRIDE
        public void SetInitialTangent(Vector4 tangent)
        {
            initialTangent = tangent;
            Reset();
        }
#endif

        public TMPMotionAnimator()
        {
            charInfoArray = new CharInfo[32];
            for (int i = 0; i < charInfoArray.Length; i++)
            {
                SetInitialColor(i);
                charInfoArray[i].colorPattern = VertexPattern.Uniform;
                charInfoArray[i].uv3Pattern = VertexPattern.Uniform;
                charInfoArray[i].rotation = initialRotation;
                charInfoArray[i].scale = initialScale;
                charInfoArray[i].position = initialPosition;
                charInfoArray[i].uv3 = initialUV3;
#if LITMOTION_TMP_TANGENT_OVERRIDE
                charInfoArray[i].tangent = initialTangent;
#endif
            }

            updateAction = UpdateCore;
            completeAction = CompleteCore;
        }

        TMP_Text target;
        internal readonly Action updateAction;
        internal readonly Action completeAction;
        internal CharInfo[] charInfoArray;
        bool isDirty;
        int refCount;

        TMPMotionAnimator nextNode;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void EnsureCapacity(int length)
        {
            var prevLength = charInfoArray.Length;
            if (length > prevLength)
            {
                Array.Resize(ref charInfoArray, length);

                if (length > prevLength)
                {
                    for (int i = prevLength; i < length; i++)
                    {
                        SetInitialColor(i);
                        charInfoArray[i].colorPattern = VertexPattern.Uniform;
                        charInfoArray[i].uv3Pattern = VertexPattern.Uniform;
                        charInfoArray[i].rotation = initialRotation;
                        charInfoArray[i].scale = initialScale;
                        charInfoArray[i].position = initialPosition;
                        charInfoArray[i].uv3 = initialUV3;
#if LITMOTION_TMP_TANGENT_OVERRIDE
                        charInfoArray[i].tangent = initialTangent;
#endif
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update()
        {
            TryUpdate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetDirty()
        {
            isDirty = true;
        }

        public void Reset()
        {
            for (int i = 0; i < charInfoArray.Length; i++)
            {
                SetInitialColor(i);
                charInfoArray[i].colorPattern = VertexPattern.Uniform;
                charInfoArray[i].uv3Pattern = VertexPattern.Uniform;
                charInfoArray[i].rotation = initialRotation;
                charInfoArray[i].scale = initialScale;
                charInfoArray[i].position = initialPosition;
                charInfoArray[i].uv3 = initialUV3;
#if LITMOTION_TMP_TANGENT_OVERRIDE
                charInfoArray[i].tangent = initialTangent;
#endif
            }

            isDirty = false;
        }

        private void SetInitialColor(int i)
        {
            if( initialAlpha >= 0f )
            {
                var col = GetTextMeshColor(i);
                col.a = initialAlpha;
                charInfoArray[i].color = col;
            }
            else charInfoArray[i].color = initialColor;
        }

        internal void SetColorPattern(int charIndex, VertexPattern pattern)
        {
            charInfoArray[charIndex].colorPattern = pattern;
        }

        internal void SetUv3Pattern(int charIndex, VertexPattern pattern)
        {
            charInfoArray[charIndex].uv3Pattern = pattern;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        bool TryUpdate()
        {
            if (target == null || refCount <= 0) return false;

            if (isDirty)
            {
                UpdateCore();
                isDirty = false;
            }

            return true;
        }

        private Color GetTextMeshColor(int index)
        {
            var textInfo = target.textInfo;

            // Check if index is within bounds
            if (index < 0 || index >= textInfo.characterInfo.Length)
            {
                return Color.white;
            }

            var charInfo = textInfo.characterInfo[index];
            var materialIndex = charInfo.materialReferenceIndex;

            // Check if materialIndex is within bounds
            if (materialIndex < 0 || materialIndex >= textInfo.meshInfo.Length)
            {
                return Color.white;
            }

            var colors = textInfo.meshInfo[materialIndex].colors32;
            var vertexIndex = charInfo.vertexIndex;

            // Check if vertexIndex is within bounds
            if (vertexIndex < 0 || vertexIndex >= colors.Length)
            {
                return Color.white;
            }

            return colors[vertexIndex];
        }

        void UpdateCore()
        {
            target.ForceMeshUpdate();

            var textInfo = target.textInfo;
            EnsureCapacity(textInfo.characterCount);

            for (int i = 0; i < textInfo.characterCount; i++)
            {
                ref var charInfo = ref textInfo.characterInfo[i];
                if (!charInfo.isVisible) continue;

                var materialIndex = charInfo.materialReferenceIndex;
                var vertexIndex = charInfo.vertexIndex;

                ref var colors = ref textInfo.meshInfo[materialIndex].colors32;
                ref var uv3 = ref textInfo.meshInfo[materialIndex].uvs2;
                ref var motionCharInfo = ref charInfoArray[i];
#if LITMOTION_TMP_TANGENT_OVERRIDE
                ref var tangent = ref textInfo.meshInfo[materialIndex].tangents;
#endif
                VertexPatternHelper.ApplyColor(colors, vertexIndex, motionCharInfo.color, motionCharInfo.colorPattern);
                VertexPatternHelper.ApplyUv3(uv3, vertexIndex, motionCharInfo.uv3, motionCharInfo.uv3Pattern);
#if LITMOTION_TMP_TANGENT_OVERRIDE
                var charTangent = motionCharInfo.tangent;
                for (int n = 0; n < 4; n++)
                {
                    tangent[vertexIndex + n] = charTangent;
                }
#endif

                var verts = textInfo.meshInfo[materialIndex].vertices;
                var center = (verts[vertexIndex] + verts[vertexIndex + 2]) * 0.5f;

                var charRotation = motionCharInfo.rotation;
                var charScale = motionCharInfo.scale;
                var charOffset = motionCharInfo.position;
                for (int n = 0; n < 4; n++)
                {
                    var vert = verts[vertexIndex + n];
                    var dir = vert - center;
                    verts[vertexIndex + n] = center +
                        charRotation * new Vector3(dir.x * charScale.x, dir.y * charScale.y, dir.z * charScale.z) +
                        charOffset;
                }
            }

            for (int i = 0; i < textInfo.materialCount; i++)
            {
                if (textInfo.meshInfo[i].mesh == null) continue;
                textInfo.meshInfo[i].mesh.colors32 = textInfo.meshInfo[i].colors32;
                textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
                textInfo.meshInfo[i].mesh.uv4 = textInfo.meshInfo[i].uvs2;
#if LITMOTION_TMP_TANGENT_OVERRIDE
                textInfo.meshInfo[i].mesh.tangents = textInfo.meshInfo[i].tangents;
#endif
                target.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
            }
            isDirty = false;
        }
        void CompleteCore()
        {
            UpdateCore();
            refCount--;
        }
    }
}

