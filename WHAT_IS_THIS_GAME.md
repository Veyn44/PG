# What Is This Game About?

> Working title: PlaceholderGame  
> Status: Initial vision draft  
> Purpose: Describe the game the creator wants to make. This document records the game vision, not a technical implementation plan.

## 1. Core Concept

The game combines colony simulation, direct character control, tactical squad combat, exploration, and resource management.

The player is physically present in the game world and directly controls one character at a time. The player also manages a permanent underground colony and sends expeditions into other locations beneath the planet's surface.

The game has two primary ways of playing:

- **The colony/base:** A persistent home where the player develops an underground settlement, expands its rooms, gathers and processes resources, builds, produces, maintains, farms, and manages the people living there.
- **Expeditions:** Separate locations that the player visits with one or more companions. These are played as real-time top-down action/shooter environments with pause and tactical orders.

These modes are part of the same game and share the same characters, identities, inventories, attributes, and other relevant character data.

## 2. Setting and Premise

The game takes place on a planet with extremely harsh surface temperatures.

Humanity has colonised this planet, but most of its civilisation exists underground. This is not merely a small mining outpost: there are enormous underground cities and extensive subterranean complexes. The extraction of rare minerals and rocks is one of the reasons humanity is present on the planet.

The player was sent to the planet in response to an emergency signal — a call for help.

A rescue cruiser arrived, but the player wakes up inside an evacuation module on the planet's surface. The exact events surrounding the arrival and the player's landing are not fully fixed.

One possible version of the opening is that the cruiser crashes. Because it is enormous, it crashes directly onto the player's position. This is an optional narrative idea, not a required part of the game concept.

## 3. Starting Location

The game begins at a small, abandoned military outpost that serves as an entrance to an underground complex.

The starting location includes:

- A decontamination room.
- A small military station/outpost.
- An entrance into the underground complex.
- A collapsed or blocked entrance shaft.
- An elevator/transport system at the edge of the map, leading deeper into the underground world.

The player may begin in the first underground level rather than spending significant time on the planet's surface.

The initial outpost is abandoned and small. It is the starting point from which the player begins to establish a functioning base.

## 4. The Colony and Base Mode

The colony is the player's **one permanent home base**.

The base is experienced directly from the perspective of the currently controlled character. The player moves through the physical space using **WASD** and interacts with objects in the world.

Examples of interactions include:

- Using terminals.
- Using workbenches and other workstations.
- Mining and excavating underground space.
- Expanding the rooms and accessible areas of the base.
- Building.
- Crafting and production.
- Maintenance.
- Farming.

The base mode is not intended to be continuously time-pressured.

### 4.1 Action Points / Stamina

Actions such as crafting, mining, excavation, construction, maintenance, and farming consume a limited daily resource. Its working name is **Pawn Points** or **Stamina**; the final name and exact rules are undecided.

The player can spend this resource on available activities during the day.

When the resource is exhausted, the player can end the day. The day-end transition resolves the relevant simulation:

- Production and gains.
- Resource consumption and losses.
- Effects and ongoing processes.
- Healing and other character changes.

Time therefore behaves more like a turn-based strategy or a day-based colony simulation than a continuously running real-time simulation while the player works around the base.

### 4.2 Colony Leader and Base Control

The colony has a designated **Colony Leader**. This is the character selected as the player's current protagonist and the pawn that can be directly controlled by the player.

In the colony, the Colony Leader can be drafted or undrafted:

- **Drafted:** The player directly controls the leader.
- **Undrafted:** The leader is controlled by the normal character AI and can act autonomously.

This allows the player to spend time watching the colony and its characters behave without constantly controlling the protagonist — a more cinematic or observational way to play.

The leader remains a normal pawn using the same character and AI systems as other pawns when undrafted.

## 5. Underground Locations and Expeditions

The elevator/transport system provides access to different locations within the underground cities and complexes.

These locations are separate maps from the permanent home base. They are used for exploration, missions, combat, resource gathering, and other expedition activities.

### 5.1 Persistence of Locations

Visited locations should be persistent: the player should be able to leave a location and return later, with its relevant state and changes preserved.

The game does not necessarily need to keep every possible location fully loaded or active at all times. It may limit how many maps are kept open/loaded simultaneously.

The distinction between a location being **saved/persistent** and being **currently loaded/active** is important. A location may be stored and unloaded, then restored when the player returns.

The exact persistence, unloading, and map-capacity rules are not yet defined.

### 5.2 Expedition Gameplay

Expeditions are played as a top-down action/shooter.

The player can:

- Go alone.
- Take a group of companion pawns.

The player directly controls the designated Colony Leader during an expedition. In this mode the leader must remain drafted: companions using the **Follow** behaviour need the protagonist to act as their moving reference point.

The player cannot switch direct control to another pawn during an expedition. Other pawns act autonomously under AI control and follow assigned behaviour presets and situational decisions.

The expedition simulation can be paused. While paused, the player can issue orders to companions, including during dangerous situations.

### 5.3 Player Incapacitation, Death, and Mission Withdrawal

If the player-controlled Colony Leader becomes incapacitated during an expedition, the companions should recognise the situation and attempt to initiate a retreat.

The squad should attempt to:

- Break off or disengage from the mission.
- Recover the incapacitated leader if possible.
- Carry or otherwise transport the leader back.
- Return to safety rather than continuing the mission as if nothing happened.

This is intended to work like a **retreat / abandon mission** behaviour. The AI should attempt this when it judges that withdrawal is possible; success is not guaranteed.

If the Colony Leader dies during an expedition:

- The player cannot take direct control of another squad member in the field.
- The remaining AI-controlled companions should attempt to withdraw from the mission if they can.
- The player chooses a new Colony Leader / player-controlled pawn after returning to the colony.

The death of the current leader does not automatically end the entire game. The colony and wider game continue, and another available pawn can become the new Colony Leader.

The exact conditions, risks, and possible outcomes of a retreat, as well as the detailed leader-selection rules, are not yet defined.

## 6. Characters and Shared Pawn System

Characters use one shared pawn/character system across the colony and expedition modes.

A pawn does not become a different kind of entity when moving between modes. The same character identity and relevant data follow the pawn between the base and expeditions.

This includes, as applicable:

- Character identity.
- Attributes and abilities.
- Health and condition.
- Inventory and equipment.
- Other character state relevant to gameplay.

The difference between modes is how the pawn is controlled and how the surrounding simulation operates — not a separate character implementation.

## 7. Squad Control and Companion AI

Only the designated Colony Leader can be directly controlled by the player. Other squad members are not directly draftable by the player, including during expeditions.

Other members of the expedition group are autonomous. The player gives them higher-level behaviour presets/orders rather than directly controlling every movement and action.

Examples of behaviour presets include:

- **Follow** — stay with and accompany the player.
- **Scout** — move ahead and gather information while responding to danger.
- **Hold Position** — maintain a position.
- **Carry Load** — focus on carrying assigned or relevant loads.

These are examples of the intended command style, not a final or exhaustive list.

### 7.1 Autonomous Combat and Situational Decisions

Companion AI is an accepted core feature of the game.

Companions should be capable of acting and fighting without the player continuously directing every action. Depending on their orders and the situation, they should be able to make reasonable decisions such as:

- Reacting to enemies and danger.
- Choosing targets.
- Seeking cover or safer positions.
- Fighting and supporting other squad members.
- Responding to injuries and changing conditions.
- Following the squad's overall intent.

The goal is to avoid requiring the player to micromanage every companion as in a system where the player directly drafts and commands every pawn throughout combat.

The AI does not need to be infinitely complex. It needs to be reliable and capable enough to make sensible decisions within the scope of this game.

### 7.2 Protagonist AI and Undrafting

The Colony Leader is not permanently under direct player control.

In the colony, the player can undraft the leader. The leader then uses the same basic character AI as other pawns and behaves autonomously. This supports an observational or cinematic play style in which the player can watch the protagonist and colony act without directly steering every movement.

During expeditions, however, the Colony Leader must remain drafted and directly controlled by the player. This gives the Follow behaviour a clear, moving protagonist to follow and preserves the intended expedition control model.

## 8. Core Design Decisions Established So Far

The following points are accepted parts of the current vision:

1. The game has one permanent home base.
2. The home base is an underground colony on a planet with extreme surface temperatures.
3. The player directly controls the designated Colony Leader using WASD when that pawn is drafted.
4. The Colony Leader can be undrafted in the colony and then acts under normal character AI.
5. Base activities use a limited daily action resource, currently called Pawn Points or Stamina.
6. The base day is resolved when the player ends the day; it is not intended to be a continuously time-pressured real-time experience.
7. The player travels from the base to separate underground locations using an elevator/transport system.
8. Expedition locations should preserve their state so the player can return later.
9. Not every persistent location needs to remain loaded/active simultaneously.
10. Expeditions use top-down action/shooter gameplay.
11. The player can travel alone or with companions.
12. The Colony Leader must remain drafted and directly controlled during expeditions.
13. The player cannot switch direct control to another pawn during an expedition.
14. Other pawns are controlled by autonomous AI and higher-level behaviour presets; the player does not directly draft them.
15. The expedition can be paused so the player can issue orders.
16. Companion AI should be capable of autonomous combat and situational decisions.
17. If the leader is incapacitated, companions should attempt to retreat and recover the leader if possible.
18. If the leader dies, companions should attempt to withdraw if possible; the player selects a replacement leader after returning to the colony.
19. The death of the current leader does not automatically end the game.
20. Characters use the same shared pawn system and retain their identity and relevant data across the base and expedition modes.

## 9. Ideas That Are Not Yet Fixed

These ideas have been mentioned but should not be treated as mandatory or fully specified:

- The rescue cruiser's crash and its impact on the evacuation module.
- The exact sequence of events before the player wakes up.
- The final name and rules of the daily action resource (Pawn Points / Stamina).
- The maximum number of persistent or simultaneously loaded expedition maps.
- The exact mechanics, risks, and limits of retreat and leader recovery.
- The detailed rules for selecting a replacement Colony Leader.
- The exact set of behaviours available to the Colony Leader when undrafted.

## 10. Purpose and Status of This Document

This document records the game vision as currently described by its creator.

It is not a technical architecture, implementation specification, production schedule, or final list of all game features.

The technical architecture and project structure will be derived from this vision in separate documents. Technical proposals must not silently become game requirements. If a technical decision requires a change to the intended gameplay, that change must be discussed and accepted explicitly.

The document is expected to evolve as the creator explains more of the game. New details should be added without inventing features or treating unresolved ideas as settled decisions.
