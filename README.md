# VR-Tools


I will add more tools i've made and their visual representation soon.


Don't forget to remove Debug.Log lines which can cause huge memory leak in long-term.

# ColliderUpdater
ColliderUpdater adjusts the height and position of the player's Capsule Collider according to the height and position of the VR Camera. If no ground is detected (e.g., during flight), the height is set to the maximum collider height defined. This maximum height can be determined by the player's real-life height, which can be derived from the VR Device (optional).

In various scenarios, this functionality proves beneficial for ensuring accurate physics and avoiding potential errors, particularly when navigating through tunnels or small holes.

# VRCamAngle
Calculates the angle between the VR camera's forward direction and the position of an target in the game. Gives you a value that explains how directly you're looking at the target. Which enables a lot of potential for VR gameplay.

# ContinuousMovement
Allows continuous movement for a character controller in a VR environment based on the input from the left hand controller's primary 2D axis. The movement direction is determined by the orientation of the player's head. You can also consider using XR System provided by Unity. This offers a simpler approach in case you want to make specific modifications.
