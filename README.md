# TopDown Shooter



## Description
Top-down shooter built in Unity with focus on clean architecture , modular systems , and scalable gameplay logic.
The project demonstrates event-driven design , decoupled systems , and extensible enemy / player mechanics.

## Features
- Player movement and  shooting system
- Wave-based enemy spawning system
- Enemy AI (melee / ranged)
- Projectile system with lifetime control
- UI system ( health , waves , game states)
- Game states (Menu / Gameplay / GameOver)
- Boss system with phases and abilities
- Economy system ( coins , rewards , exp)
- Upgrade system 

## Tech
- Unity (URP)
- C#
- ScriptableObjects
- Event-driven architecture

## Architecture
- **SRP (Single Responsibility)**
 - Each system has a clear responsibility ( Spawn , Attack , Health , UI , etc.)
- **Decoupled Architecture**
 - UI does not depend on gameplay logic
 - Systems communicate via events

- **Event-driven architecture**
 - Using C# Actions instead of Update polling
 - Example : 
  - `OnHealthChanged`
  - `OnWaveStarted`
  - `OnGameStateChanged`

#### 🔹 Game State System
- Centralized state management
- Enum-based states:
  - `Menu`
  - `Gameplay`
  - `Pause`
  - `GameOver`
- Systems subscribe to state changes

---

#### 🔹 Wave System
- Wave lifecycle:
  - Preparing → Running → Finished
- Config-driven waves (ScriptableObjects)
- Event-based transitions

---

#### 🔹 Enemy System
- Modular enemy architecture
- Supports:
  - Melee enemies
  - Ranged enemies
- FSM-based behavior:
  - Chase
  - Attack
  - Death

---

#### 🔹 Spawn System (Refactored)
- Originally monolithic → split into multiple components
- Responsibilities separated:
  - Spawn logic
  - Wave handling
  - Enemy tracking
- Coroutine-based spawning

---

#### 🔹 Combat System
- `IDamageable` interface
- Shared `Health` system
- Attack logic separated via configs:
  - Melee / Ranged

---

#### 🔹 Weapon System
- Data-driven via ScriptableObjects
- Weapon levels and stats scaling
- Events:
  - Fire
  - Reload
  - Stats changed

---

#### 🔹 Boss System
- Phase-based behavior (health thresholds)
- Ability system:
  - Slam (AoE attack)
  - Enemy spawn ability

---

#### 🔹 Economy System
- Centralized coin system
- Rewards from:
  - Enemies
  - Waves
- Upgrade cost scaling

---

#### 🔹 Upgrade System
- Between-wave upgrades
- Progressive cost scaling
- Integrated with player stats

---

## 🛠 Tech Stack

- Unity (URP)
- C#
- ScriptableObjects
- Coroutines
- Event-driven architecture

## How to run
Open project in Unity 2021.3+

## 🚀 Key Focus

This project focuses on:
- Clean architecture
- Scalability
- Decoupling systems
- Avoiding monolithic design

## Author
Temchenko
