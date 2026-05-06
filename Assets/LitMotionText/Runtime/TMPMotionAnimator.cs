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
                if(animator.refCount<=0) animator.Reset();
                animator.refCount++;
                return animator;
            }

            animator = rootNode ?? new();
            rootNode = animator.nextNode;
            animator.nextNode = null;

            animator.target = text;
            animator.Reset();
            animator.refCount++;

            if (tail == animators.Length)
            {
                Array.Resize(ref animators, tail * 2);
            }
            animators[tail] = animator;
            tail++;

            textToAnimator.Add(text, animator);

            return animator;
        }

        // Initializes all vertex colors to `from`, reserves refCount for motionCount motions.
        internal static TMPMotionAnimator GetForPattern(TMP_Text text, int charIndex, int motionCount, Color from)
        {
            var animator = Get(text);
            animator.EnsureCapacity(charIndex + 1);
            animator.refCount += motionCount - 1;

            ref var info = ref animator.charInfoArray[charIndex];
            info.colorBL = from;
            info.colorTL = from;
            info.colorTR = from;
            info.colorBR = from;
            animator.SetDirty();

            return animator;
        }

        // Initializes all vertex UV3s to `from`, reserves refCount for motionCount motions.
        internal static TMPMotionAnimator GetForUv3Pattern(TMP_Text text, int charIndex, int motionCount, Vector2 from)
        {
            var animator = Get(text);
            animator.EnsureCapacity(charIndex + 1);
            animator.refCount += motionCount - 1;

            ref var info = ref animator.charInfoArray[charIndex];
            info.uv3BL = from;
            info.uv3TL = from;
            info.uv3TR = from;
            info.uv3BR = from;
            animator.SetDirty();

            return animator;
        }

        internal static void Return(TMPMotionAnimator animator)
        {
            Array.Clear(animator.vertexCallbackArray, 0, animator.vertexCallbackArray.Length);

            animator.nextNode = rootNode;
            rootNode = animator;

            textToAnimator.Remove(animator.target);
            animator.target = null;
        }

        internal static bool TryGetExisting(TMP_Text text, out TMPMotionAnimator animator)
            => textToAnimator.TryGetValue(text, out animator);

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

        // TMP vertex order per quad: BL=0, TL=1, TR=2, BR=3
        internal struct CharInfo
        {
            public Vector3 position;
            public Vector3 scale;
            public Quaternion rotation;
            public Color colorBL;
            public Color colorTL;
            public Color colorTR;
            public Color colorBR;
            public Vector2 uv3BL;
            public Vector2 uv3TL;
            public Vector2 uv3TR;
            public Vector2 uv3BR;
#if LITMOTION_TMP_TANGENT_OVERRIDE
            public Vector4 tangent;
#endif
        }

        public void SetInitialAlpha(int charIndex, float alpha)
        {
            var col = GetTextMeshColor(charIndex);
            col.a = alpha;
            charInfoArray[charIndex].colorBL = col;
            charInfoArray[charIndex].colorTL = col;
            charInfoArray[charIndex].colorTR = col;
            charInfoArray[charIndex].colorBR = col;
            SetDirty();
        }

        public void SetInitialCol(int charIndex, Color col)
        {
            charInfoArray[charIndex].colorBL = col;
            charInfoArray[charIndex].colorTL = col;
            charInfoArray[charIndex].colorTR = col;
            charInfoArray[charIndex].colorBR = col;
            SetDirty();
        }

        public void SetInitialColBL(int charIndex, Color col) { charInfoArray[charIndex].colorBL = col; SetDirty(); }
        public void SetInitialColTL(int charIndex, Color col) { charInfoArray[charIndex].colorTL = col; SetDirty(); }
        public void SetInitialColTR(int charIndex, Color col) { charInfoArray[charIndex].colorTR = col; SetDirty(); }
        public void SetInitialColBR(int charIndex, Color col) { charInfoArray[charIndex].colorBR = col; SetDirty(); }

        public void SetInitialRotation(int charIndex, Quaternion rot)
        {
            charInfoArray[charIndex].rotation = rot;
            SetDirty();
        }

        public void SetInitialScale(int charIndex, Vector3 scale)
        {
            charInfoArray[charIndex].scale = scale;
            SetDirty();
        }

        public void SetInitialPosition(int charIndex, Vector3 pos)
        {
            charInfoArray[charIndex].position = pos;
            SetDirty();
        }

        public void SetInitialUV3(int charIndex, Vector2 uv3)
        {
            charInfoArray[charIndex].uv3BL = uv3;
            charInfoArray[charIndex].uv3TL = uv3;
            charInfoArray[charIndex].uv3TR = uv3;
            charInfoArray[charIndex].uv3BR = uv3;
            SetDirty();
        }

        public void SetInitialUV3BL(int charIndex, Vector2 uv3) { charInfoArray[charIndex].uv3BL = uv3; SetDirty(); }
        public void SetInitialUV3TL(int charIndex, Vector2 uv3) { charInfoArray[charIndex].uv3TL = uv3; SetDirty(); }
        public void SetInitialUV3TR(int charIndex, Vector2 uv3) { charInfoArray[charIndex].uv3TR = uv3; SetDirty(); }
        public void SetInitialUV3BR(int charIndex, Vector2 uv3) { charInfoArray[charIndex].uv3BR = uv3; SetDirty(); }

#if LITMOTION_TMP_TANGENT_OVERRIDE
        public void SetInitialTangent(int charIndex, Vector4 tangent)
        {
            charInfoArray[charIndex].tangent = tangent;
            SetDirty();
        }
#endif

        public TMPMotionAnimator()
        {
            charInfoArray = new CharInfo[32];
            vertexCallbackArray = new Action<Color, Vector3>[charInfoArray.Length * 4];
            for (int i = 0; i < charInfoArray.Length; i++)
                InitializeCharInfo(i);

            updateAction = UpdateCore;
            completeAction = CompleteCore;
        }

        TMP_Text target;
        internal readonly Action updateAction;
        internal readonly Action completeAction;
        internal CharInfo[] charInfoArray;
        internal Action<Color, Vector3>[] vertexCallbackArray;
        bool isDirty;
        int refCount;

        TMPMotionAnimator nextNode;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void EnsureCapacity(int length)
        {
            var prevLength = charInfoArray.Length;
            if (length <= prevLength) return;

            Array.Resize(ref charInfoArray, length);
            Array.Resize(ref vertexCallbackArray, length * 4);
            for (int i = prevLength; i < length; i++)
                InitializeCharInfo(i);
        }

        public void SetVertexCallback(int charIndex, int vertexOffset, Action<Color, Vector3> callback)
        {
            EnsureCapacity(charIndex + 1);
            vertexCallbackArray[charIndex * 4 + vertexOffset] = callback;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update() => TryUpdate();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetDirty() => isDirty = true;

        public void Reset()
        {
            for (int i = 0; i < charInfoArray.Length; i++)
                InitializeCharInfo(i);

            isDirty = false;
        }

        private void InitializeCharInfo(int i)
        {
            charInfoArray[i].colorBL = Color.white;
            charInfoArray[i].colorTL = Color.white;
            charInfoArray[i].colorTR = Color.white;
            charInfoArray[i].colorBR = Color.white;
            charInfoArray[i].rotation = Quaternion.identity;
            charInfoArray[i].scale = Vector3.one;
            charInfoArray[i].position = Vector3.zero;
            charInfoArray[i].uv3BL = Vector2.zero;
            charInfoArray[i].uv3TL = Vector2.zero;
            charInfoArray[i].uv3TR = Vector2.zero;
            charInfoArray[i].uv3BR = Vector2.zero;
#if LITMOTION_TMP_TANGENT_OVERRIDE
            charInfoArray[i].tangent = Vector4.zero;
#endif
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

            if (index < 0 || index >= textInfo.characterInfo.Length)
                return Color.white;

            var charInfo = textInfo.characterInfo[index];
            var materialIndex = charInfo.materialReferenceIndex;

            if (materialIndex < 0 || materialIndex >= textInfo.meshInfo.Length)
                return Color.white;

            var colors = textInfo.meshInfo[materialIndex].colors32;
            var vertexIndex = charInfo.vertexIndex;

            if (vertexIndex < 0 || vertexIndex >= colors.Length)
                return Color.white;

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
                colors[vertexIndex + 0] = motionCharInfo.colorBL;
                colors[vertexIndex + 1] = motionCharInfo.colorTL;
                colors[vertexIndex + 2] = motionCharInfo.colorTR;
                colors[vertexIndex + 3] = motionCharInfo.colorBR;

                uv3[vertexIndex + 0] = motionCharInfo.uv3BL;
                uv3[vertexIndex + 1] = motionCharInfo.uv3TL;
                uv3[vertexIndex + 2] = motionCharInfo.uv3TR;
                uv3[vertexIndex + 3] = motionCharInfo.uv3BR;

#if LITMOTION_TMP_TANGENT_OVERRIDE
                var charTangent = motionCharInfo.tangent;
                for (int n = 0; n < 4; n++)
                    tangent[vertexIndex + n] = charTangent;
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

                var callbackBase = i * 4;
                vertexCallbackArray[callbackBase]?.Invoke(motionCharInfo.colorBL, verts[vertexIndex]);
                vertexCallbackArray[callbackBase + 1]?.Invoke(motionCharInfo.colorTL, verts[vertexIndex + 1]);
                vertexCallbackArray[callbackBase + 2]?.Invoke(motionCharInfo.colorTR, verts[vertexIndex + 2]);
                vertexCallbackArray[callbackBase + 3]?.Invoke(motionCharInfo.colorBR, verts[vertexIndex + 3]);
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
