# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a Unity package that extends **LitMotion** (a high-performance animation library) with TextMeshPro character-level animation capabilities. The package provides extension methods to animate individual character properties in TMP_Text components, enabling per-character effects like color, position, rotation, scale, and UV animations.

**Key Dependencies:**
- Unity 6000.2.6f1
- LitMotion (from GitHub: annulusgames/LitMotion)
- TextMeshPro (Unity package)
- Universal Render Pipeline 17.2.0

## Architecture

### Core Components

1. **TMPMotionAnimator** (`Assets/LitMotionText/Runtime/TMPMotionAnimator.cs`)
   - Internal pooled animator class that manages per-character animation state
   - Uses object pooling pattern via linked list (`nextNode`) to avoid allocations
   - Maintains a `CharInfo` struct array containing position, scale, rotation, color, and UV3 data for each character
   - Updates character mesh data during Unity's PlayerLoop
   - Static dictionary (`textToAnimator`) maps TMP_Text components to their animators
   - Automatically returns animators to pool when all motions complete

2. **LitMotionTextMeshProExtensions** (`Assets/LitMotionText/Runtime/LitMotionTextMeshProExtensions.cs`)
   - Public API providing `BindToTMPChar*` extension methods on LitMotion's `MotionBuilder`
   - Each method binds a motion to a specific character property (color, position, rotation, scale, UV3)
   - Methods follow pattern: `BindToTMPChar{Property}<TOptions, TAdapter>(builder, text, charIndex, [initialValue])`
   - Uses `Box.Create(charIndex)` to pass character index to the motion callback without allocations

### Update Loop Architecture

The animator integrates into Unity's PlayerLoop via `PlayerLoopHelper.OnUpdate`:
- Static array (`animators`) tracks all active animators
- Update loop iterates through active animators, calling `TryUpdate()` on each
- Animators with `activeMotionCount > 0` remain active; others are returned to pool
- Array compaction occurs during iteration to maintain dense packing

### CharInfo Struct

Contains per-character state:
- `position`: Vector3 offset from original position
- `scale`: Vector3 scale multiplier
- `rotation`: Quaternion rotation
- `color`: Color32 tint
- `uv3`: Vector2 custom UV coordinates (mapped to uv4 on mesh)
- `tangent`: Vector4 tangent override (only if `LITMOTION_TMP_TANGENT_OVERRIDE` defined)

### Mesh Update Process

`UpdateCore()` method flow:
1. Force TMP_Text mesh update to get latest character positions
2. Ensure `charInfoArray` capacity matches character count
3. For each visible character:
   - Calculate character center point
   - Apply rotation, scale, and position offset to all 4 vertices
   - Update color32 array for all 4 vertices
   - Update UV3 (mapped to mesh.uv4) for all 4 vertices
   - Optionally update tangents if enabled
4. Upload modified arrays back to TMP meshInfo
5. Call `target.UpdateGeometry()` for each material

## Development Commands

### Building
This is a Unity package project. Build through Unity Editor:
- Open project in Unity 6000.2.6f1 or later
- Use Unity's standard build pipeline

### Testing
No test framework is currently set up, though test assemblies exist in dependencies.

### Assembly Definitions
- Main assembly references `LitMotion.Extensions` (via asmref)
- Located at: `Assets/LitMotionText/Runtime/anenome.litmotiontext.asmref`

## Implementation Patterns

### Adding New Character Property Bindings

When adding a new character property animation:

1. Add property to `CharInfo` struct in `TMPMotionAnimator.cs`
2. Initialize property in `TMPMotionAnimator` constructor and `EnsureCapacity()`
3. Update property in `UpdateCore()` mesh update loop
4. Create extension methods in `LitMotionTextMeshProExtensions.cs`:
   - One without `initialValue` (uses current/default values)
   - One with `initialValue` parameter (sets initial state via `SetInitial*()`)
5. Follow naming: `BindToTMPChar{PropertyName}`

### Initial Value Pattern

Methods with `initialValue` parameters:
- Call `animator.SetInitial{Property}(initialValue)`
- The `SetInitial*()` method updates private fields and calls `Reset()`
- `Reset()` re-initializes all `charInfoArray` elements with new initial values
- This ensures consistent state for all characters before animation starts

### Memory Management

- Animators are pooled and reused to avoid GC allocations
- `charInfoArray` is resized only when needed (grows but never shrinks)
- Static array of animators grows by 2x when capacity exceeded
- Use `Box.Create()` for passing primitive state to avoid boxing allocations

## Conditional Compilation

- `LITMOTION_TMP_TANGENT_OVERRIDE`: Enable tangent override support in shaders
- `UNITY_EDITOR`: Editor-specific code for domain reload handling and edit-mode updates

## Important Constraints

- Character indices are 0-based
- Animators automatically clean up when target TMP_Text is destroyed or all motions complete
- UV3 data is mapped to mesh.uv4 (TMP convention)
- Mesh updates occur every frame while any motion is active
- Editor mode updates via `EditorApplication.update` (does not run during play mode, compilation, or domain reload)