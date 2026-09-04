# Physics-Based Action Combat Prototype

A modular 3D gameplay engineering prototype developed in Unity to explore responsive combat feel, animation-driven melee combos, physics interactions, and custom shader feedback within a greybox testing environment.

---

## Core Systems & Gameplay Mechanics

- **Animation-Driven Melee Combat:** Input-buffered attack strings including light attacks, dynamic lunges, and rolling attack recovery windows.
- **Visual Feedback & Shaders:** Custom `HitFlash.shadergraph` implementation providing instantaneous visual hit reactions on damaged entities.
- **Physics & Audio Integration:** Physics-based weapon interactions, impulse-driven explosive forces, and dynamic slash/impact SFX triggers.
- **Enemy Combat AI:** Finite state machine (FSM) architecture handling basic target acquisition, windup, and hit-stun states.
- **New Input System:** Responsive key bindings and actions utilizing Unity's `Input.inputactions`.

---

## Tech Stack

| Layer        | Technology                                   |
|--------------|----------------------------------------------|
| Engine       | Unity                                        |
| Language     | C#                                           |
| Shaders      | Unity Shader Graph (`HitFlash.shadergraph`)  |
| Physics      | PhysX (Rigidbodies, Impulses)                |
| Input        | Unity New Input System                       |

---

## Project Structure

```text
prototype/
├── Assets/
│   ├── Animations/            # Attack combos, rolls & motion clips
│   ├── My Prefabs/            # Weapons, testing dummies & explosive props
│   ├── Scenes/                # Greybox combat testing arena
│   ├── Scripts/               # Combat controller, physics & AI logic
│   ├── Sounds/                # Slash audio, impacts & sound effects
│   ├── Settings/              # Render pipeline & project configurations
│   ├── TextMesh Pro/          # Debug & UI typography assets
│   ├── HitFlash.shadergraph   # Custom hit reaction shader
│   ├── HitMaterial.mat        # Shader material for damage feedback
│   └── Input.inputactions     # Input bindings for movement & combat
└── ProjectSettings/
```

---
