# Game Technical Know-How

> Project: PlaceholderGame  
> Status: Living technical knowledge base — initial architecture is still a proposal until verified by implementation  
> Based on: `WHAT_IS_THIS_GAME.md`  
> Purpose: Record the game's technical structure and accumulated implementation knowledge. It begins as an architectural prediction and grows into a detailed, system-by-system record of what we build, why we build it, where it lives, how it works, and what other systems rely on it. It is not a promise that every proposed folder or module must exist exactly as shown.

## 1. Architectural Intent

The project should be built around the needs of this specific game.

We are **not** starting by building a universal game engine. Reusable technical foundations may emerge naturally, but the architecture should first make PlaceholderGame understandable, testable, and playable.

The code should be divided by clear responsibilities. A module should own one coherent area of behaviour and expose a small, understandable interface to other modules.

A rough target of **100–200 lines per source file** is a preferred readability guideline, not a hard limit. A file may be shorter or longer when its responsibility remains cohesive. We should split files when they contain multiple distinct responsibilities, not merely to satisfy a line count.

The architecture should favour:
- Small, cohesive modules.
- Explicit ownership of game state.
- Clear dependencies between systems.
- Shared character and world data across game modes.
- Separate simulation rules for colony days and real-time expeditions.
- Automated tests for important rules and state transitions.
- Visible, incremental implementation rather than large speculative frameworks.

## 2. High-Level Application Shape

The game is expected to have the following broad layers:

1. **Application / Composition** — starts the game, creates systems, connects dependencies, and controls application-level flow.
2. **Game Domain** — owns the rules and state of the colony, characters, expeditions, maps, resources, combat, and AI.
3. **Simulation** — advances the different kinds of game time and resolves their effects.
4. **Presentation** — renders the game world and user interface, and translates player input into application commands.
5. **Platform** — provides access to files, window/input devices, audio, and other operating-system or runtime capabilities.
6. **Content and Assets** — stores game definitions and visual/audio resources.
7. **Developer Tools** — in-game inspection and controlled testing tools, built on the same game systems rather than a separate game runtime.
8. **Tests** — verifies domain rules, simulation, AI decisions, persistence, and integration between systems.

This is a logical separation. It does not require each layer to become a large framework.

## 3. Proposed Repository Layout

The following is a first proposed directory structure. It is intentionally a map of responsibilities, not a complete list of source files.

```text
PlaceholderGame/
│
├── docs/
│   ├── WHAT_IS_THIS_GAME.md
│   ├── GAME_TECHNICAL_KNOW_HOW.md
│   └── ...future design and technical documents
│
├── src/
│   ├── app/
│   │   ├── bootstrap/
│   │   ├── composition/
│   │   └── session/
│   │
│   ├── game/
│   │   ├── colony/
│   │   ├── characters/
│   │   ├── player/
│   │   ├── squads/
│   │   ├── expeditions/
│   │   ├── locations/
│   │   ├── world/
│   │   ├── resources/
│   │   ├── inventory/
│   │   ├── construction/
│   │   ├── crafting/
│   │   ├── mining/
│   │   ├── farming/
│   │   ├── combat/
│   │   ├── ai/
│   │   └── progression/
│   │
│   ├── simulation/
│   │   ├── colony-day/
│   │   ├── expedition-realtime/
│   │   ├── time/
│   │   └── effects/
│   │
│   ├── presentation/
│   │   ├── rendering/
│   │   ├── camera/
│   │   ├── input/
│   │   ├── animation/
│   │   ├── audio/
│   │   └── ui/
│   │
│   ├── platform/
│   │   ├── files/
│   │   ├── window/
│   │   └── runtime/
│   │
│   └── developer/
│       ├── inspectors/
│       ├── tools/
│       └── overlays/
│
├── data/
│   ├── characters/
│   ├── items/
│   ├── equipment/
│   ├── buildings/
│   ├── resources/
│   ├── locations/
│   └── behaviours/
│
├── assets/
│   ├── sprites/
│   ├── tiles/
│   ├── animations/
│   ├── effects/
│   ├── audio/
│   ├── fonts/
│   └── ui/
│
├── saves/
│
└── tests/
    ├── unit/
    ├── integration/
    └── scenarios/
```

Some directories may later be combined, renamed, or omitted. We should only create a directory when there is actual code or content that needs its responsibility.

## 4. Main Game Systems and Responsibilities

### 4.1 Application and Session

**Likely responsibility:** Coordinate the running game without owning the rules of individual gameplay systems.

Expected areas:
- Application startup and shutdown.
- Construction and connection of game services.
- Current game/session lifecycle.
- Loading a saved session and returning to the main menu.
- Switching between the colony and an expedition.

The application layer should coordinate systems rather than contain their gameplay implementation.

### 4.2 World, Maps, and Locations

**Likely responsibility:** Represent the planet's underground world and the individual maps that make it up.

Expected areas:
- World and location identity.
- Map dimensions and spatial data.
- Map contents and entity placement.
- Connections between locations, including elevator/transport links.
- Active/loaded map lifecycle.
- Persistent storage of locations that are not currently loaded.

A persistent location must not need to remain continuously active in memory. The architecture should distinguish:
- A location's saved state.
- A location currently loaded into the running game.
- A location currently being simulated.

The exact number of simultaneously loaded maps and the rules for background simulation remain undecided.

### 4.3 Characters and Pawn State

**Likely responsibility:** Own the identity and state of every character.

Expected areas:
- Character identity.
- Attributes and abilities.
- Health, injuries, incapacitation, and death.
- Inventory and equipment.
- Character needs or other state as later defined.
- Current activity and orders.

The colony and expedition modes must use the same pawn representation. A pawn should not need a separate implementation or identity when it moves between these modes.

### 4.4 Player Control and Colony Leader

**Likely responsibility:** Decide which pawn is the current protagonist and how direct player control is applied.

Expected areas:
- Designated Colony Leader.
- Selecting or replacing the Colony Leader while at the colony.
- Drafted and undrafted states for the leader.
- Sending direct movement/action input to the drafted leader.
- Preventing direct control from switching to another squad pawn during an expedition.

The leader's undrafted behaviour should use the normal character AI rather than a separate simplified implementation.

The control rules differ by mode:
- In the colony, the leader may be drafted or undrafted.
- During an expedition, the leader remains drafted and directly controlled by the player.
- If the leader dies during an expedition, the player does not take control of another pawn in the field. The squad AI attempts to withdraw, and a replacement leader is selected after returning to the colony.

### 4.5 Colony Management

**Likely responsibility:** Own the persistent home-base activities and colony-level state.

Expected areas:
- Colony identity and state.
- Residents and their assignments.
- Base rooms and facilities.
- Workstations and interactions.
- Construction and expansion.
- Production and resource management.
- Maintenance and farming.
- Colony-level status and summaries.

The colony is the one permanent home base. Other underground locations are not additional permanent player colonies under the current vision.

### 4.6 Colony-Day Simulation

**Likely responsibility:** Resolve the colony's day-based simulation.

Expected areas:
- Daily action resource for the player-controlled character (working name: Pawn Points / Stamina).
- Spending action points on supported activities.
- Ending the day.
- Resolving production, consumption, gains, losses, healing, and ongoing effects.
- Advancing day-based timers and processes.

This simulation should not be implemented as the same continuous real-time loop used by expeditions. It may share lower-level rules and effect calculations, but its time-advance model is different.

### 4.7 Expeditions and Real-Time Simulation

**Likely responsibility:** Manage a temporary expedition session taking place on a persistent underground location.

Expected areas:
- Starting and ending an expedition.
- Selecting participating pawns.
- Entering and leaving a location.
- Pausing and resuming the expedition.
- Real-time movement and action updates.
- Expedition objectives and outcome state, as later defined.
- Returning the group and its state to the colony.

The expedition uses real-time simulation with pause and player-issued orders. It should not be implemented as a colony-day simulation.

### 4.8 Squad and Orders

**Likely responsibility:** Manage the group accompanying the Colony Leader and the high-level instructions given to companions.

Expected areas:
- Squad membership.
- Companion assignment.
- Behaviour presets such as Follow, Scout, Hold Position, and Carry Load.
- Order state and order changes.
- Squad cohesion and relationship to the leader.
- Communication of the leader's current state to companions.

Companion pawns are not directly draftable by the player. They execute orders through AI.

### 4.9 AI and Behaviour

**Likely responsibility:** Make autonomous decisions for pawns when they are not directly controlled.

Expected areas:
- Basic character autonomy.
- Behaviour preset execution.
- Situational evaluation.
- Threat response.
- Target selection.
- Movement and positioning requests.
- Combat decisions.
- Support for injured allies.
- Following and regrouping.
- Retreat and recovery decisions.

The AI should be composed of understandable parts rather than one monolithic decision system.

The AI needs to support at least two important uses:
- The Colony Leader acting autonomously when undrafted in the colony.
- Companions acting autonomously during expeditions.

The same underlying character AI concepts should be reused where appropriate, while mode-specific behaviours may differ.

### 4.10 Incapacitation, Death, and Retreat

**Likely responsibility:** Coordinate character incapacitation/death with squad and expedition behaviour.

Expected areas:
- Incapacitated character state.
- Death state.
- Detection that the Colony Leader can no longer continue.
- Squad decision to attempt withdrawal.
- Attempt to recover and transport an incapacitated leader.
- Retreat/abandon-mission outcome.
- Handling the death of the leader during an expedition.
- Returning to the colony and selecting a replacement leader.

Retreat is an AI-driven attempt, not a guaranteed teleport or guaranteed rescue. The exact success conditions and consequences remain to be designed.

### 4.11 Combat

**Likely responsibility:** Resolve combat actions independently of who decided to perform them.

Expected areas:
- Attacks and attack execution.
- Hit and damage resolution.
- Health and injury changes.
- Combat interactions with equipment and character attributes.
- Combat events that AI can observe and react to.

Combat should not contain the AI's decision-making. The AI decides whether and how to attempt an action; the combat system resolves the action according to game rules. Player-issued actions and AI-issued actions should use the same combat rules.

### 4.12 Navigation and Movement

**Likely responsibility:** Provide movement and pathfinding capabilities to both direct player control and AI.

Expected areas:
- Grid or world-space movement representation (exact choice to be confirmed).
- Collision and movement constraints.
- Pathfinding.
- Navigation around obstacles.
- Movement requests from AI.
- Direct movement input for the Colony Leader.
- Navigation in colony and expedition maps.

The same underlying movement/navigation foundation should serve the player and AI where their requirements overlap.

### 4.13 Resources, Inventory, Construction, Crafting, Mining, and Farming

These are likely separate gameplay modules or cohesive groups of modules, depending on their eventual size.

Their responsibilities will include:
- Resource definitions and quantities.
- Character and storage inventories.
- Equipment and item handling.
- Construction and room expansion.
- Workbench-based crafting and production.
- Excavation and mining.
- Farming and maintenance.

The precise boundaries should be decided when their actual rules are described. We should avoid creating empty modules for every noun in the design document.

### 4.14 Presentation, Input, and Audio

**Likely responsibility:** Display the game and translate user input into game commands.

Expected areas:
- World rendering.
- Camera behaviour.
- Character and object visuals.
- Animation.
- Effects and lighting.
- UI and menus.
- Direct movement input.
- Interaction input.
- Pause and order controls.
- Audio playback.

Presentation should display game state rather than becoming the owner of gameplay rules.

### 4.15 Data and Content

**Likely responsibility:** Store authored game content separately from the code that interprets it.

Likely content includes:
- Character definitions.
- Items and equipment.
- Buildings and workstations.
- Resources.
- Location definitions.
- Behaviour presets.
- Visual and audio asset references.

The exact data format is not decided. We should choose it based on actual authoring and validation needs rather than selecting a format prematurely.

### 4.16 Save and Load

**Likely responsibility:** Persist the game so that the player can resume it without losing the state of the colony, characters, or visited locations.

Expected data includes:
- Colony state.
- Colony Leader selection.
- Pawn identities and state.
- Inventories and equipment.
- Persistent location state.
- Relevant expedition state and outcomes.
- Day and simulation state.

Save/load should be designed around stable game identities and explicit state ownership. It should not rely on serializing arbitrary runtime objects wholesale.

### 4.17 Developer Mode

**Likely responsibility:** Provide in-game tools for inspecting and testing the actual game.

Expected areas may include:
- Inspecting pawns, maps, objects, and game state.
- Spawning or removing test entities.
- Triggering controlled test situations.
- Inspecting AI orders and decisions.
- Testing map transitions and persistence.

Developer Mode should use the same game systems as normal gameplay. It should not become a separate game runtime or implement alternative versions of gameplay mechanics.

The exact tools and interface are not yet defined.

## 5. Important Architectural Boundaries

These boundaries are more important than the exact folder names.

### 5.1 Game Rules vs. Presentation

Game rules should not depend on UI panels, rendering code, or input devices.

The UI may request an action and display the result, but it should not independently implement the action's rules.

### 5.2 AI vs. Action Resolution

AI chooses and requests actions. Dedicated gameplay systems validate and resolve those actions.

For example:
- AI chooses a target and requests an attack.
- Combat validates and resolves the attack.
- The resulting state change is visible to the AI and other systems.

### 5.3 Shared Pawn Model Across Modes

There should be one character identity and state model. Colony and expedition systems should not maintain incompatible copies of the same pawn.

### 5.4 Separate Time Models

Colony days and real-time expeditions have different time-advance rules.

They may share effects, production calculations, character rules, and other domain logic, but should not be forced into one identical simulation loop.

### 5.5 Persistent State vs. Loaded Runtime State

A map can exist as saved persistent data without being loaded into active memory. Loading, unloading, and simulation activity should be explicit concepts.

### 5.6 One Colony, Many Visitable Locations

The current vision has one permanent home colony and multiple persistent or revisitable underground locations. The architecture should not assume that every location is a separate player-managed colony.

### 5.7 Developer Tools Use Real Game Systems

Developer Mode may expose privileged controls, but it should call the same gameplay operations as the player-facing game wherever possible.

## 6. Likely Dependency Direction

A first expected dependency direction is:

```text
Platform
   ↑
Presentation
   ↑
Application / Composition
   ↓
Game Domain and Simulation
```

This sketch is conceptual, not a complete dependency graph. The intended rule is:

- Game rules should not import UI or rendering modules.
- Game rules should not directly depend on operating-system APIs.
- Presentation should call application/game interfaces rather than modify internal state arbitrarily.
- Application composition connects implementations and services.
- Tests should be able to exercise game rules without launching the full presentation layer.

The final dependency graph will be refined when the language, runtime, rendering technology, and concrete interfaces are selected.

## 7. Testing Strategy Implied by the Game

The architecture must make the most failure-prone rules testable without relying only on manual gameplay.

Important test areas include:

- Pawn identity and state remain consistent when moving between colony and expedition.
- Drafting and undrafting the Colony Leader behaves correctly in the colony.
- The Colony Leader remains the only player-controlled pawn during expeditions.
- The player cannot switch to another pawn after the leader dies in the field.
- Companion AI follows behaviour presets and reacts to danger.
- Companions attempt retreat when the leader is incapacitated or dead.
- Retreat and leader recovery handle success and failure cases.
- Colony day-end calculations resolve the expected production, consumption, healing, and effects.
- Persistent locations restore their saved state after being unloaded and loaded again.
- Save/load preserves colony, pawn, and location identities and state.
- Pausing an expedition stops the appropriate simulation while still allowing orders.

Tests should be added alongside the systems they protect. We should not build a large testing framework before there is real game logic to test.

## 8. File and Module Size Guideline

A preferred initial target is approximately **100–200 lines per source file**.

This is a readability guideline, not a strict maximum or a requirement to split every file.

Use these rules:
- One file should have one clear primary responsibility.
- Related small functions may live together.
- Split a file when it starts owning multiple independent responsibilities.
- Do not create dozens of tiny files with no meaningful separation.
- Avoid giant application shells that contain game rules, input handling, persistence, and UI coordination together.
- Review file size as a signal, not as a substitute for understanding the code.

## 9. Decisions Still Open

This proposal intentionally does not decide:

- Programming language.
- Game framework or rendering technology.
- Exact coordinate/grid representation.
- Exact folder names or final module boundaries.
- Exact data definition format.
- Exact save-file format.
- Whether every proposed system needs its own directory.
- Detailed simulation scheduling and background simulation rules.
- Detailed AI architecture and algorithms.
- Exact map-loading limits.
- Exact UI framework and presentation architecture.

These choices should be made when they can be evaluated against the actual game requirements.

## 10. Relationship to the Previous Project

The previous Cassandra/engine project is a source of possible implementations and lessons, not a required foundation.

No old subsystem should be accepted solely because it already exists.

When a requirement reaches implementation:
1. Identify the actual requirement in `WHAT_IS_THIS_GAME.md`.
2. Identify the proposed responsibility in this architecture document.
3. Inspect the relevant old implementation, if one exists.
4. Verify its behaviour with focused tests or a small working prototype.
5. Decide whether to reuse, adapt, or replace it.
6. Keep only code that demonstrably serves the new game.

The new project should not inherit unverified abstractions, old application shells, or architecture merely to avoid rewriting code.

## 11. Status and Change Policy

This is a predicted structure based on the current game vision. It is expected to change.

It is not a binding law and does not authorize implementation of every listed system.

Before adding a major new module or changing an architectural boundary:
- Explain the responsibility it will own.
- Explain which existing systems it depends on.
- Explain why it cannot reasonably belong to an existing module.
- Update this proposal if the structural change is meaningful.

The game vision document remains authoritative for what the game should do. This document is a technical interpretation of that vision and must be revised when the vision changes.


## 12. How This Document Evolves During Development

This document is intended to grow with the implementation. It may become very long. Completeness and usefulness are more important than keeping it short.

The initial sections above describe predicted architecture. A proposed system must not be described as implemented until code and tests verify it. As implementation progresses, update the relevant system sections with the actual design and behaviour.

### 12.1 Required Record for Every Meaningful Implementation

Whenever a meaningful code change implements, extends, or constrains a system, record the result here. If the work belongs to a numbered development step or checkpoint (for example, step 5.2), include that exact identifier and link the implementation record to it.

For each meaningful change, record as applicable:

1. **Status:** Proposed, designed, implemented, tested, partial, deprecated, or replaced.
2. **Purpose:** What problem or game requirement the system addresses.
3. **Responsibility and boundaries:** What it owns and what it deliberately does not own.
4. **Location in the repository:** Actual file and directory paths, not guessed future paths.
5. **Structure:** Important modules, classes, functions, data types, and their relationships. Explain their roles; do not copy entire source files.
6. **Runtime flow:** What calls or triggers what, in what order, and how state changes.
7. **Data and identity:** Important fields, identifiers, ownership, persistence, and serialization expectations.
8. **Interfaces and dependencies:** What it consumes, what it exposes, and which systems depend on it.
9. **Invariants and constraints:** Conditions that must remain true, including architectural decisions made to support future work.
10. **Reasoning:** Why this design was chosen, what alternatives were considered when relevant, and what trade-offs it makes.
11. **Tests and verification:** Which tests cover it, what was manually verified, and what remains unverified.
12. **Known limitations and next steps:** Missing behaviour, edge cases, risks, and planned extensions.

Not every entry needs all twelve headings if some are genuinely irrelevant. Do not omit important context merely to keep an entry short.

### 12.2 System-Level Detail

When a system becomes real, expand its own section rather than creating disconnected notes elsewhere. Record its actual implementation in enough detail that a new developer can understand the design without reverse-engineering every file.

Examples of the expected depth:

- **Pawn / Character:** identity model, state ownership, attributes, health representation, component or body-part model if adopted, lifecycle, and relationships to inventory, AI, control, combat, and persistence.
- **Lighting:** what lighting means in this game, supported light types and why those types exist, light data and configuration, rendering responsibilities, blending/shadow approach if implemented, performance constraints, asset requirements, and the actual source paths that implement it.
- **Rendering:** world and UI responsibilities, coordinate conventions, render layers/passes, camera relationship, asset handling, and boundaries between visual effects and game rules.
- **AI:** decision ownership, behaviour/order representation, action requests, movement/combat integration, state transitions, and how decisions are tested.
- **Persistence:** stable identifiers, serialized state, load/unload lifecycle, compatibility/versioning decisions, and recovery/error handling.

These examples illustrate documentation depth, not a requirement to implement every listed feature immediately.

### 12.3 Proposed vs. Verified Knowledge

Keep these categories distinct:

- **Intended:** A requirement or design intention from the game vision.
- **Proposed:** A technical approach that has not yet been implemented and verified.
- **Implemented:** Present in the code, but not necessarily fully tested.
- **Verified:** Implemented and supported by identified tests or explicit manual verification.
- **Deferred:** Deliberately postponed; include why and what future decision will trigger it.
- **Replaced:** Superseded by another design; preserve a short explanation and point to the replacement.

Never turn an assumption into a fact by writing it in a confident tone. Mark uncertainty explicitly.

### 12.4 Decision and Change History

For meaningful architectural changes, record:

- Date or development checkpoint.
- Affected system and repository paths.
- Previous approach and new approach.
- Reason for the change.
- Consequences for dependent systems and documentation.

When a decision is reversed, do not silently erase the old reasoning if it could help explain existing code or prevent repeating a failed approach.

### 12.5 Documentation Maintenance Rules

- Update this document in the same development unit as the code change, not many steps later.
- Document the final result after testing, not merely the original plan.
- If implementation differs from the proposal, update the proposal to match reality and explain the reason.
- Record future-facing constraints when code is deliberately structured to support a later feature, even if that feature is not implemented yet.
- Record actual paths only after files exist.
- Do not duplicate source code. Explain structure, contracts, flow, and intent.
- Keep the game vision in `WHAT_IS_THIS_GAME.md`; this document explains the technical response to that vision.
- Keep handover instructions and the current short-term working state in `forGPT.md`; this document is the deeper technical reference.
- If a system is substantially redesigned, update its current description and preserve the important historical reason for the change.

## 13. Initial Implementation Record

No game implementation has started yet. The systems and folders described in this document are architectural predictions, not verified code.

At the beginning of implementation, create or expand system records as each system is designed and built. Do not pre-fill fictitious file paths, classes, APIs, tests, or implementation details.
