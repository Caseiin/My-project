Highlighted systems:
===============================================
1) Cooldown-system
   This Unity tool manages the on-screen cooldown indicators for a set of player abilities. Each indicator is pooled using Unity's ObjectPool<T> API so the HUD can scale to any number of abilities without allocating at runtime.
   Cooldown state is tracked in a Dictionary<AbilitySO, AbilityCooldownState> so each ability's timer is fully independent. A nullable Color? parameter on each pool entry lets designers override the fill colour per ability without touching code.

2)Boss Fight Prototype
  This prototype focuses on the feel of a boss fight — the moment-to-moment feedback that makes hits satisfying and the boss's phase transitions readable. The boss runs a finite state machine with a dedicated vulnerable state triggered by specific player actions.
  Key engineering work included a full-screen URP vignette that persists cleanly across scene loads, material flashing driven entirely through the FSM without separate coroutine chains, and fixing lambda subscription bugs that caused the vignette to stack on repeated scene entry.

  features of enemies which use GOAP AI logic include
  -FSM boss behaviour — idle, attack, vulnerable, and death states with clean transitions
  - since it uses GOAP logic the AI decides what to do next based on its beliefs and environment. Making it more dynamic and less hardcoded.
