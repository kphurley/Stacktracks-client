# Stacktracks-client
A multiplayer top-down racing game made in Unity.  Created during Stackathon week at Fullstack Academy.

## Overview
Stacktracks is a racing game about two things:  First, finishing what amounts to an obstacle course for race cars, and second -
doing so faster than everyone else.  What is currently completed is a simple prototype of the idea.

## Tech Used
- Unity3D for all of the game engine goodies.  Thanks to Unity, I didn't have to write any code to handle the physics or 
control of the car, and all of my 3D models were more or less drag-and-drop.
- C# for all of the scripting. 
- Node/express/socket.io for the server.  
- A C# library that wraps socket.io client functionality in a Unity prefab:  
https://github.com/fpanettieri/unity-socket.io-DEPRECATED

## How to install
- Fork/clone
- Open with Unity, let it install all of the dependencies.
- To use with the server, make sure you grab it here:  https://github.com/kphurley/Stacktracks-server
- To see it working, make sure you find the SocketIoComponent on the Network game object and replace the ip with your
server ip.

