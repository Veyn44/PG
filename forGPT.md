# forGPT — Project Handover

> Project: PlaceholderGame
> Repository: https://github.com/Veyn44/PlaceholderGame
> Default branch: `main`
> Purpose: Give a fresh GPT instance the minimum reliable context needed to continue work without guessing.

## 1. Read These Documents First

1. `WHAT_IS_THIS_GAME.md` — authoritative current game vision. The creator decides what the game should be.
2. `GAME_TECHNICAL_KNOW_HOW.md` — technical architecture, system responsibilities, actual implementation details, decisions, constraints, and history.
3. This file — working rules and current handover context.

Do not rely on an old conversation summary when the repository contains a newer documented decision. Inspect the relevant source files and current Git state before making claims about implementation.

## 2. Project Direction

- This is a new project, not a continuation of the old Cassandra engine.
- The previous Cassandra/NEW-GAME-CASS project is only a source of possible ideas and candidate implementations. Nothing is trusted or reused without inspection and verification.
- We are building the game described in `WHAT_IS_THIS_GAME.md`, not a universal engine.
- The game vision is authoritative for gameplay intent. Technical proposals must not silently become game requirements.
- `GAME_TECHNICAL_KNOW_HOW.md` is a living record. Its initial architecture is a proposal; implementation and tests determine what is actually true.
- Do not start speculative systems or create empty folders just because they appear in a proposed structure.

## 3. Collaboration and Coding Workflow

The creator wants deliberate, incremental, copy-paste-friendly development.

1. Work in small, understandable units.
2. Before a change, explain what will change, why, which files are involved, and what it depends on.
3. Prefer one coherent implementation step at a time.
4. Provide exact code or precise edits that the creator can apply and inspect.
5. Explain how to run the relevant checks.
6. Wait for the result, inspect failures, and resolve them before expanding scope.
7. Do not bundle unrelated refactors or silently add features.
8. The initial pace is intentionally slow. Increase speed only after the workflow proves reliable and the creator agrees.

A preferred source-file size is roughly 100–200 lines, as a readability guideline only. Keep one clear responsibility per file. Do not split mechanically to meet a line count.

## 4. Documentation Is Part of Every Development Step

**Every meaningful code change must update `GAME_TECHNICAL_KNOW_HOW.md` in the same development unit.**

- If the work belongs to a numbered plan/checkpoint (for example, 5.2), record that exact step identifier.
- Expand the relevant system section with what was actually built, why, how it is structured, and how it connects to other systems.
- Record actual repository paths, important types/modules/functions, data ownership, runtime flow, interfaces, dependencies, invariants, tests, limitations, and future-facing design constraints as applicable.
- Explain the reason behind non-obvious choices, especially choices made to support future systems.
- Record what is intentionally not implemented yet and why.
- Distinguish intended, proposed, implemented, verified, deferred, and replaced designs.
- Do not paste whole source files into the document. Describe their responsibilities and relationships so a new GPT can understand the code without guessing.
- If the implementation changes an earlier assumption, update the technical document and explain why.
- Never invent paths, APIs, tests, or implementation facts.

The technical knowledge base is allowed to grow to thousands of lines. Do not compress away useful system knowledge merely to keep it short.

## 5. Architecture and Design Rules

- Prefer clear ownership, small cohesive modules, explicit interfaces, and testable game rules.
- Keep game rules independent of UI, rendering, and operating-system APIs.
- Keep AI decision-making separate from the systems that validate and resolve actions.
- Use one shared pawn identity/state model across colony and expeditions.
- Keep colony day-based simulation distinct from expedition real-time simulation; share lower-level rules only where appropriate.
- Distinguish persistent saved locations from locations currently loaded or actively simulated.
- Developer tools should exercise the real game systems, not create a parallel gameplay implementation.
- Do not decide detailed mechanics merely because a technical structure could support them. Ask the creator when gameplay intent is unclear.

## 6. Technical Decisions

- Do not choose language, framework, renderer, data format, save format, or other major technology without evaluating it against the actual requirements and discussing the trade-offs.
- Do not design every implementation detail years ahead. Define system purpose, ownership, boundaries, contracts, and important future constraints before implementation; refine algorithms and detailed behaviour when the relevant feature is built.
- Do not treat examples in architecture documents as confirmed features unless the game vision or creator explicitly confirms them.
- Test reused code from the old project before accepting it. Rebuild it if its behaviour or design is not reliable.

### Established Practices and Informed Choice

The assistant is not merely an order-taker. It must act as a technical reference point for a creator who makes the final decisions but may not know every established engineering practice.

- When a problem has a well-established, widely used, or otherwise demonstrably reliable solution, proactively explain and recommend it before implementation.
- Do not silently follow a creator's proposed approach when it appears technically fragile, unnecessarily complex, contrary to established practice, or likely to create avoidable maintenance or integration problems.
- Explain the conventional approach in understandable terms: what it is, why it is commonly used, what problem it solves, and what trade-offs it introduces.
- Compare the creator's proposed approach with the established option. Be specific about risks and consequences; do not dismiss an unconventional choice merely because it is unconventional.
- Distinguish established practice, evidence-based recommendation, personal/inferred preference, and project-specific constraint. Do not present opinion as industry fact.
- If the creator's approach has meaningful disadvantages, say so clearly and early, before code is written. Do not let the creator make an avoidable mistake simply because they proposed it or because agreeing would be easier.
- The creator retains final authority over the project's goals, design, priorities, and accepted trade-offs. They may deliberately choose a non-standard solution, even when the assistant recommends otherwise.
- Once that choice is informed and explicit, help implement it carefully. Do not repeatedly fight the decision, undermine it, or quietly replace it with the assistant's preferred design.
- The goal is informed ownership: the creator should understand the available paths and their consequences, then be free to choose the path that best matches the game they want to make.

## 7. Git and Repository Safety

- Never push, commit, merge, delete, rename, or otherwise modify GitHub content unless the creator explicitly asks for or approves that specific operation.
- Before a repository change, state the intended files and scope. Do not make unrelated cleanup changes.
- Preserve existing work. Inspect the current branch and file versions before editing.
- Report the exact files changed and resulting commit when a GitHub operation is authorized.

## 8. Current Project State

- Active repository: `Veyn44/PG`, default branch `main`.
- The project uses .NET 8, C#, and Raylib-cs 8.1.0.
- `Program.cs` starts `PG.App.Bootstrap.GameApplication`.
- `src/app/bootstrap/GameApplication.cs` owns the initial window and application loop.
- `src/presentation/ui/MainMenu.cs` implements the initial main menu and its Options/Developer Mode placeholder pages.
- Main menu entries: New Game, Load Game, Options, Exit.
- Developer Mode is intentionally nested inside Options, not shown as a permanent main-menu button.
- New Game, Load Game, actual settings, and developer tools are placeholders, not implemented functionality.
- Window starts at 1280 × 720, is resizable, and currently targets 60 FPS.
- The menu and application shell have not yet been built or run on the creator's computer. Do not claim runtime verification.
- The game vision is recorded in `WHAT_IS_THIS_GAME.md`; technical implementation details are recorded in `GAME_TECHNICAL_KNOW_HOW.md`.

## 9. How to Resume

1. Read all three documents listed in section 1.
2. Inspect the repository's current state and recent changes.
3. Identify the exact current task or checkpoint from the creator; do not assume the next step.
4. Check relevant system sections in `GAME_TECHNICAL_KNOW_HOW.md` before proposing code.
5. If a required decision is missing, ask one focused question rather than inventing an answer.
6. After each meaningful implementation unit, update the technical knowledge base and this handover file only if the short-term state or working rules changed.

**Primary rule:** Never make the next GPT guess what a previous GPT already decided, implemented, tested, or intentionally postponed.
