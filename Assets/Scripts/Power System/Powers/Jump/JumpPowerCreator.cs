using UnityEngine;
using System.Collections.Generic;

namespace PowerSystem.Powers
{
	public class JumpPowerCreator : PowerCreator<JumpPower, JumpPowerController>
	{
		public static new string name = "Jump";
		public static new string description = "Makes the character hop in the air. Duh.";
		JumpPowerStats powerStats;
		public static Dictionary<string, string> statDescriptions = new Dictionary<string, string>
		{
			{ "Magnitude", "How big is the jump."},
			{ "Amount", "How many jumps the character can do before having to reach ground again."}
		};

		public JumpPowerCreator()
		{
			power = ScriptableObject.CreateInstance<JumpPower>();
			powerStats = ScriptableObject.CreateInstance<JumpPowerStats>();
			stats = new Stat[]
			{
				new Stat<int>
				(
					"Magnitude", 
					statDescriptions["Magnitude"], 
					rating => 
					{
						power.jumpForce = rating * 500;
						powerStats.magnitude = rating;
					} 
				),
				new Stat<int>
				(
					"Amount", 
					statDescriptions["Amount"], 
					rating => 
					{
						power.jumpAmount = rating;
						powerStats.amount = rating;
					}
				)
			};
		}
	}

	//Tentar dar um jeito de fazer o powerStats linkar melhor com o Stat. Ou sumir completamente com um deles. Sei lá. Estou confuso.

	public class JumpPowerStats : ScriptableObject
	{
		public int magnitude;
		public int amount;
	}
}