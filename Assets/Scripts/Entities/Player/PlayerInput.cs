using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	public bool isMovingUp()
	{
		return Input.GetButton("Vertical") && Input.GetAxis("Vertical")>0;
	}

	public bool isMovingDown()
	{
		return Input.GetButton("Vertical") && Input.GetAxis("Vertical")<0;
	}

	public bool isMovingRight()
	{
		return Input.GetButton("Horizontal") && Input.GetAxis("Horizontal")>0;
	}

	public bool isMovingLeft()
	{
		return Input.GetButton("Horizontal") && Input.GetAxis("Horizontal")<0;
	}

	public bool isMoving()
	{
		return (isMovingUp() || isMovingDown() || isMovingRight() || isMovingLeft());
	}
}