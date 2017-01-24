using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PowerSystem.Powers
{
	public class JumpPowerCreator : PowerCreator<JumpPower>
	{
		public static new string name = "Jump";
		public static new string description = "Makes the character hop in the air. Duh.";
		public JumpPowerCreator()
		{
			powerInfo = ScriptableObject.CreateInstance<JumpPower>();
			stats = new Stat[]
			{
				new Stat<int>("Magnitude", "How big is the jump.", SetJumpForce /*ou*/ /* rating => powerInfo.jumpForce = rating * 500 */),
				new Stat<int>("Amount", "How many jumps the character can do before having to reach ground again.", SetJumpAmount /*ou*/ /* rating => powerInfo.jumpAmount = rating */)
			};
		}

		void SetJumpForce(int rating)
		{
			powerInfo.jumpForce = 500 * rating;
		}

		void SetJumpAmount(int rating)
		{
			powerInfo.jumpAmount = rating;
		}
	}
}