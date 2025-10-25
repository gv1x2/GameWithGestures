# Hand Gesture Controlled Game

A game controlled entirely by hand gestures, integrating **Python** and **Unity**.

---

## Overview

The system uses **Python** to capture and process real-time camera input and **Unity** to render and control the game world.  
Hand gestures detected via **MediaPipe** and **OpenCV** are transmitted as control signals over **UDP sockets**, allowing intuitive, touch-free gameplay.

---

## Architecture

### Python side
- Captures video stream from the webcam  
- Detects hand positions and gestures using **MediaPipe**  
- Processes images with **OpenCV**  
- Sends corresponding control commands (e.g., *move left*, *jump*, *attack*) through **UDP** to Unity  

### Unity side
- Listens for UDP messages from Python  
- Maps each command to in-game character actions  
- Runs the main gameplay logic and visuals  

---

## Requirements

### Python
- Python 3.9+  
- OpenCV  
- MediaPipe  
- socket (standard library)

Install dependencies:
```bash
pip install opencv-python mediapipe
```


### Unity
- Unity 2021+  
- A C# UDP listener script integrated with player controller logic  

---

## How It Works
1. Run the Python script to start the gesture detection server  
2. Launch the Unity project and start the scene  
3. Move your hand in front of the camera  
4. The detected gestures will control the character in real time  

---

## Communication Protocol
UDP socket connection between:
- **Python:** Sender  
- **Unity:** Receiver
