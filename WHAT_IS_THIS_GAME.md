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

The player directly controls one pawn at a time using direct character control. Other pawns act autonomously under AI control and follow assigned behaviour presets and situational decisions.

The expedition simulation can be paused. While paused, the player can issue orders to companions, including during dangerous situations.

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

Only one pawn can be directly controlled by the player at a time.

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

### 7.2 Incapacitated Player and Retreat

If the player-controlled pawn becomes incapacitated during an expedition, the companions should recognise the situation and initiate a retreat.

The squad should attempt to:

- Break off or disengage from the mission.
- Recover the incapacitated leader if possible.
- Carry or otherwise transport the leader back.
- Return to safety rather than continuing the mission as if nothing happened.

This is intended to work like a **retreat / abandon mission** behaviour.

The exact conditions, risks, and possible outcomes of a retreat are not yet defined.

## 8. Death of the Player-Controlled Pawn

The death of the currently player-controlled pawn should not automatically end the entire game.

If that pawn dies, the player can take control of another available pawn.

The colony and the wider game continue. The player is not permanently identical to one individual character; the player controls the colony and takes direct control of one of its characters at a time.

The detailed rules for selecting the replacement pawn and handling the death and its consequences are not yet defined.

## 9. Core Design Decisions Established So Far

The following points are accepted parts of the current vision:

1. The game has one permanent home base.
2. The home base is an underground colony on a planet with extreme surface temperatures.
3. The player directly controls one pawn using WASD while moving around the base.
4. Base activities use a limited daily action resource, currently called Pawn Points or Stamina.
5. The base day is resolved when the player ends the day; it is not intended to be a continuously time-pressured real-time experience.
6. The player travels from the base to separate underground locations using an elevator/transport system.
7. Expedition locations should preserve their state so the player can return later.
8. Not every persistent location needs to remain loaded/active simultaneously.
9. Expeditions use top-down action/shooter gameplay.
10. The player can travel alone or with companions.
11. Only one pawn is directly controlled by the player at a time.
12. Companions use autonomous AI and higher-level behaviour presets.
13. The expedition can be paused so the player can issue orders.
14. Companion AI should be capable of autonomous combat and situational decisions.
15. If the controlled pawn is incapacitated, companions should attempt to retreat and recover the leader.
16. If the controlled pawn dies, the player can continue by taking control of another pawn.
17. Characters use the same shared pawn system and retain their identity and relevant data across the base and expedition modes.

## 10. Ideas That Are Not Yet Fixed

These ideas have been mentioned but should not be treated as mandatory or fully specified:

- The rescue cruiser's crash and its impact on the evacuation module.
- The exact sequence of events before the player wakes up.
- The final name and rules of the daily action resource (Pawn Points / Stamina).
- The maximum number of persistent or simultaneously loaded expedition maps.
- The exact mechanics and limits of retreat and leader recovery.
- The detailed rules for choosing a replacement pawn after the controlled pawn dies.

## 11. Purpose and Status of This Document

This document records the game vision as currently described by its creator.

It is not a technical architecture, implementation specification, production schedule, or final list of all game features.

The technical architecture and project structure will be derived from this vision in separate documents. Technical proposals must not silently become game requirements. If a technical decision requires a change to the intended gameplay, that change must be discussed and accepted explicitly.

The document is expected to evolve as the creator explains more of the game. New details should be added without inventing features or treating unresolved ideas as settled decisions.
