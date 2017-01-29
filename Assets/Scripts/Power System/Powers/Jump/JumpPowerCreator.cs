using UnityEngine;

namespace PowerSystem.Powers
{
	public class JumpPowerCreator : PowerCreator<JumpPower>
	{
		public static new string name = "Jump";
		public static new string description = "Makes the character hop in the air. Duh.";

		public JumpPowerCreator()
		{
			power = ScriptableObject.CreateInstance<JumpPower>();
			stats = new Stat[]
			{
				new Stat<int>("Magnitude", "How big is the jump.", rating => power.jumpForce = rating * 500),
				new Stat<int>("Amount", "How many jumps the character can do before having to reach ground again.", rating => power.jumpAmount = rating)
			};
		}
	}
}