# Cafe Simulator

## Overview

Welcome to the **Cafe Tycoon Mini-Game**!  
This project is a 2.5D simulation game built in Unity where you manage and grow a bustling cafe. The core gameplay loop involves customers arriving, ordering from a dynamic menu, and being served by AI-controlled cashiers who use machines to prepare orders. Your goal is to earn money by efficiently serving customers, allowing you to hire more staff and buy new, more advanced machines to expand your business.

This project is designed for scalability and maintainability, making heavy use of professional design patterns to ensure it can be easily expanded in the future.

## How to Play
- **Automated Gameplay:** The cafe runs mostly on its own; you act as the high-level manager.
- **Start:** The game begins with "Coffee" unlocked and your first Coffee Machine ready to use.
- **Customers:** Customers automatically spawn and queue at the counter to place orders.
- **Hire Cashiers:** Press the <kbd>C</kbd> key to hire a new cashier (costs money).
- **Buy Machines:** Press the <kbd>M</kbd> key to purchase an additional Coffee Machine (costs money).
- **Expand:** As you earn money, hire more staff and buy different types of machines to serve more customers efficiently.
- **Goal:** Keep growing your cafe by managing resources and optimizing your workforce to serve as many customers as possible.

---

## Core Architecture & Design Patterns

This project uses several professional design patterns for clean, scalable, and maintainable code:

### 1. State Machine Pattern

- **Usage:** All AI for Customers and Cashiers is driven by robust state machines.
- **How:** Each character has a StateMachine component that cycles through states like `MovingToMachine`, `ServingCustomer`, or `Exiting`. Each state is a class that defines `Enter()`, `Execute()`, and `Exit()` methods.
- **Benefit:** Avoids large `if-else` blocks, making character logic modular, clean, and easy to debug.

### 2. Singleton Pattern

- **Usage:** `CafeManager` is a Singleton.
- **How:** Accessed via `CafeManager.Instance`, providing a single, globally-accessible authoritative hub.
- **Benefit:** Centralizes logic, eliminates the need for `FindObjectOfType()`, and simplifies cross-system communication.

### 3. Manager/Orchestrator Pattern

- **Usage:** High-level managers coordinate specialized sub-managers:
  - **CustomerManager:** Handles spawning and assigning customers.
  - **CashierManager:** Handles hiring and tasking cashiers.
  - **MachineManager:** Handles machine spawning and management.
  - **PlayerWallet:** Manages all money transactions.
- **Benefit:** Promotes separation of concerns and modularity.

### 4. Observer Pattern (C# Events)

- **Usage:** Scripts communicate via C# events (`Action<>`).
- **How:** For example, when a customer is ready for service, it fires `OnCustomerReadyForService`. Systems can subscribe and react without direct references.
- **Key Events:** `OnCustomerReadyForService`, `OnCashierBecameIdle`.
- **Benefit:** Highly decoupled, flexible architecture.

### 5. Strategy Pattern (via Abstract Classes)

- **Usage:** Abstract base classes define contracts for characters and machines.
  - **BaseCharacter:** For Customers and Cashiers (shared logic like movement, animations, UI progress).
  - **BaseMachine:** For all machines (shared logic like type, cost, operation).
- **Benefit:** Easily extendable—add a new character or machine by inheriting and customizing.

### 6. ScriptableObjects for Data Management

- **Usage:** Menu items and game data are managed with ScriptableObjects.
  - **MenuItemData:** Stores product info (name, price, creation time, required machine type).
- **Benefit:** Separate data from logic—add or balance items in the Unity editor without code changes.

## Future Scope & Potential Improvements

The architecture is designed for expansion. Some next steps:

- **UI Improvements:**
  - Real-time money display.
  - UI buttons for hiring/buying instead of keyboard shortcuts.
- **Content Expansion:**
  - Add new menu items (e.g., Fries, Milkshakes) and machines (Fryer, Blender).
  - Add customer types with special behaviors/preferences.
- **Upgrade Systems:**
  - Machine upgrades (faster production).
  - Train cashiers for better performance.
- **Persistence:**
  - Save/load system for money, unlocked items, and layout.
- **Visual & Audio Polish:**
  - Particle effects, sound effects, more detailed idle behaviors.
- **Optimization:**
  - Object pooling for customers/products.
  - Timer-based AI checks for efficiency.
  - Optimize UI canvases in large cafes.

---

## Optimization Strategies

- **Object Pooling:** Reuse customer and product objects instead of instantiating/destroying for better performance.
- **Efficient AI:** State checks are event-driven or timer-based, not frame-based.
- **UI Management:** For many objects, centralize UI with a manager to avoid performance issues with many canvases.

---

## Getting Started

1. **Clone the repository:**  
   `git clone https://github.com/Anonymous6503/Cafe.git`
2. **Open in Unity Hub.**
3. **Install dependencies** (from `Packages/` if prompted).
4. **Open the main scene** from `Assets/Scenes/`.
5. **Press Play** in the Unity Editor.

---

## Contributing
Pull requests and suggestions are welcome!  
If you'd like to contribute, please fork the repo and submit a PR.

---

## License

This project is currently unlicensed. Please contact the repository owner for usage or distribution questions.

---
