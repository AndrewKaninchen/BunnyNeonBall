using UnityEngine;
using System.Collections;

namespace PowerSystem.Powers
{
	[CreateAssetMenu(menuName = "Powers/Beam")]
	public abstract class RangedAttackPower : Power
	{
		public float rateOfFire;
		//public int amount;
		public Direction direction;

		public enum SpreadType
		{
			Cone,
			Aura
		}
		
	}

	public enum Direction
	{
		Right,
		Left,
		Up,
		Down,
		UpRight,
		UpLeft,
		DownRight,
		DownLeft,
		Analog1,
		Analog2,
		DPad,
		Velocity,
		Crosshair
	}

	public abstract class RangedAttackPowerController <RangedAttackPowerType> : PowerController <RangedAttackPowerType> where RangedAttackPowerType : Power
	{
		protected Character character;	

		public Vector2 GetFireDirection(Direction direction)
		{
			Vector2 actualDirection;
			switch (direction)
			{
				case Direction.Right:
					actualDirection = Vector2.right;
					break;
				case Direction.Left:
					actualDirection = Vector2.left;
					break;
				case Direction.Up:
					actualDirection = Vector2.up;
					break;
				case Direction.Down:
					actualDirection = Vector2.down;
					break;
				case Direction.UpRight:
					actualDirection = Vector2.up + Vector2.right;
					actualDirection.Normalize();
					break;
				case Direction.UpLeft:
					actualDirection = Vector2.up + Vector2.left;
					actualDirection.Normalize();
					break;
				case Direction.DownRight:
					actualDirection = Vector2.down + Vector2.right;
					actualDirection.Normalize();
					break;
				case Direction.DownLeft:
					actualDirection = Vector2.down + Vector2.left;
					actualDirection.Normalize();
					break;
				case Direction.Velocity:
					actualDirection = character.GetComponent<Rigidbody2D>().velocity;
					actualDirection.Normalize();
					break;
				case Direction.Analog1:
					//Debug.Log("Horizontal" + character.playerID + " = "  + Input.GetAxis("Horizontal1"));
					actualDirection =

						Input.GetAxis("Horizontal" + character.playerID) * Vector2.right
						+ Input.GetAxis("Vertical" + character.playerID) * Vector2.down;
					actualDirection.Normalize();
					//Debug.Log("Direction = " + actualDirection);
					break;
				case Direction.Crosshair:
					actualDirection = character.transform.right;
					break;
				default:
					actualDirection = Vector2.zero;
					break;
			}
			return actualDirection;
		}

	}
}