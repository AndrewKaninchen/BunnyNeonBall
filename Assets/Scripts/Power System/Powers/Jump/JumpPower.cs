using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PowerSystem.Powers
{
	[CreateAssetMenu(menuName = "Powers/Jump")]
	public class JumpPower : Power
	{
		public float jumpForce;
		public int jumpAmount;		
	}

	public class JumpPowerCreator : PowerCreator<JumpPower, JumpPowerController>
	{
		public static new string name = "Jump";
		public static new string description = "Makes the character hop in the air. Duh.";
		public static Dictionary<string, string> statDescriptions = new Dictionary<string, string>
		{
			{ "Magnitude", "How big is the jump."},
			{ "Amount", "How many times the character can jump before having to touch ground again."}
		};

		public JumpPowerCreator()
		{
            Instance = ScriptableObject.CreateInstance<JumpPower>();
            Debug.Log(Instance);
			stats = new Stat[]
			{
				new Stat<int>
				(
					"Magnitude",
					statDescriptions["Magnitude"],
					rating => { Instance.jumpForce = rating * 500; }
				),
				new Stat<int>
				(
					"Amount",
					statDescriptions["Amount"],
					rating => { Instance.jumpAmount = rating; }
				)
			};
		}
	}
}