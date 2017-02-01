using UnityEngine;
using System.Collections;

namespace PowerSystem.Powers
{
	public class JumpPowerController : PowerController <JumpPower>
	{
		private int jumpsLeft;		
		private Rigidbody2D rb;
		private Character character;

		void OnEnable()
		{
			rb = GetComponent<Rigidbody2D>();
			character = GetComponent<Character>();
			jumpsLeft = 0;
		}
				
		void FixedUpdate()
		{
			if (character.IsGrounded)
				jumpsLeft = power.jumpAmount;
			else if (character.WasGrounded)
				jumpsLeft--;
			//Debug.Log("Jumps Left: " + jumpsLeft);	
		}

		void Update()
		{
			if (jumpsLeft > 0 && Input.GetButtonDown(power.activationKey + character.playerID))
			{
				Trigger();
			}
		}

		public void Trigger()
		{
			rb.velocity = new Vector2(rb.velocity.x, 0f);
			rb.AddForce(new Vector2(0, power.jumpForce));
			jumpsLeft--;
		}
	}
}