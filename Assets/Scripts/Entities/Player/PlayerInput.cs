using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	public static bool isMovingUp()
	{
		return Input.GetButton("Vertical") && Input.GetAxis("Vertical")>0;
	}

	public static bool isMovingDown()
	{
		return Input.GetButton("Vertical") && Input.GetAxis("Vertical")<0;
	}

	public static bool isMovingRight()
	{
		return Input.GetButton("Horizontal") && Input.GetAxis("Horizontal")>0;
	}

	public static bool isMovingLeft()
	{
		return Input.GetButton("Horizontal") && Input.GetAxis("Horizontal")<0;
	}

	public static bool isMoving()
	{
		return (isMovingUp() || isMovingDown() || isMovingRight() || isMovingLeft());
	}
}