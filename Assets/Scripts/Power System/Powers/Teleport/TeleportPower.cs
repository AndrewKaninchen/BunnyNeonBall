using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PowerSystem.Powers
{
	//[CreateAssetMenu(menuName = "Powers/NewPower")]
	public class TeleportPower : Power
	{
		public float something;
		public int something2;
	}

	public class TeleportPowerCreator : PowerCreator<TeleportPower, TeleportPowerController>
	{
		public static new string name = "Teleport";
		public static new string description = "What the power does.";
		public static Dictionary<string, string> statDescriptions = new Dictionary<string, string>
		{
			{ "Stat1's Name", "What this stat's value does."},
			{ "Stat2's Name", "What this stat's value does."},
		};

		public TeleportPowerCreator()
		{
			power = ScriptableObject.CreateInstance<TeleportPower>();
			stats = new Stat[]
			{
				new Stat<int>("Stat1's Name", statDescriptions["Stat1's Name"], rating => {/* power.something = rating * something;*/}),
				new Stat<int>("Stat2's Name", statDescriptions["Stat2's Name"], rating => {/* power.something = rating * something;*/})
			};
		}
	}
}