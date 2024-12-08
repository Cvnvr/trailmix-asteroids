# **Trailmix-Asteroids**  
A clone of the classic arcade game **Asteroids (1979)**, developed as a take-home assignment for Trailmix.

Spec: 

> Implement for us its **primary game mode** and add a **new feature** of your own design. You do not need to implement any meta features like high scores, a title screen, sound, etc, just the core game mode when we start it up.

> You can use any plugins you like, and any publicly available art. Art will be ignored for evaluation purposes, unless you say you made it and then we'll be super impressed because we're programmers and those skills are mutually exclusive. You can use any recent version of Unity (we're currently on 2021.3.20f1 LTS).

---

## **Controls**

### **Movement**  
- **`W` / `Up Arrow`**: Forward thrust  
- **`A` / `Left Arrow`**: Rotate left  
- **`D` / `Right Arrow`**: Rotate right  
- **`S` / `Down Arrow`**: Hyperspace  

### **Firing**  
- **`Spacebar` / `Left Mouse Button (LMB)`**: Fire projectile  

---

## **Implementation details**  

### **Primary game mode**  
All core game mechanics from the original game mode have been implemented, including:  

- **Controllable player ship**: Movement, projectile firing, and hyperspace travel.  
- **Asteroids**: Three different sizes with movement and splitting behaviour after being destroyed.  
- **UFOs**: Two different sizes with dynamic movement and the ability to fire projectiles.
- **Lives system**: Displayed in the UI. If the player has any additional lives, they will respawn after being destroyed.

### **New feature**  

#### **Power-ups!**
Power-ups that change the player ship's projectile firing behaviour.

- After destroying a UFO, there’s a random chance that it will drop a power-up at its position.  
- To activate it, simply drive it with your ship.
- The power-up will disappear if the player fails to reach it in time.

- **Implemented power-ups**:

    - **Rapid fire**: Increases firing speed.
    - **Shotgun**: Fires multiple projectiles in a spread pattern.

### **Additional work**  

- **Content tests**: Comprehensive test coverage of all data objects (accessible via **Window > General > Test Runner**).  
- **Scoring system**: Points are awarded for destroying asteroids and UFOs, displayed in the UI.
- **Art assets**: All assets made in Figma.

---

## **Plugins & frameworks used**  

- **Zenject**: Third-party dependency injection framework.  
- **Unity Input System**: Handles player input for movement, shooting, and power-ups.  
- **TextMeshPro**: Manages UI elements like the score display.  
- **Unity Test Framework**: Enables automated Editor tests.

## **Unity version**
Unity **2021.3.20f1** LTS.