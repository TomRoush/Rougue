using UnityEngine;
using System.Collections;
using UnitySampleAssets.CrossPlatformInput;

public class PlayerInput : MonoBehaviour {
	public static bool isMovingUp() {
		return CrossPlatformInputManager.GetButton("Vertical") && CrossPlatformInputManager.GetAxis("Vertical") > 0;
	}

	public static bool isMovingDown() {
		return CrossPlatformInputManager.GetButton("Vertical") && CrossPlatformInputManager.GetAxis("Vertical") < 0;
	}

	public static bool isMovingRight() {
		return CrossPlatformInputManager.GetButton("Horizontal") && CrossPlatformInputManager.GetAxis("Horizontal") > 0;
	}

	public static bool isMovingLeft() {
		return CrossPlatformInputManager.GetButton("Horizontal") && CrossPlatformInputManager.GetAxis("Horizontal") < 0;
	}

	public static bool isMoving() {
		return (isMovingUp() || isMovingDown() || isMovingRight() || isMovingLeft());
	}
}