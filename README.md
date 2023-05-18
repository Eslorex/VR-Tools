# VR-Tools


I will add more tools i've made and their visual representation soon.


Don't forget to remove Debug.Log lines which can cause huge memory leak in long-term.

# ColliderUpdater
ColliderUpdater adjusts the height and position of the player's Capsule Collider according to the height and position of the VR Camera. If no ground is detected (e.g., during flight), the height is set to the maximum collider height defined. This maximum height can be determined by the player's real-life height, which can be derived from the VR Device.

In various scenarios, this functionality proves beneficial for ensuring accurate physics and avoiding potential errors, particularly when navigating through tunnels or small holes.
