using UnityEngine;
using System.Collections;
using System;

namespace PowerSystem.Powers
{	
	public class RangedAttackBeamPowerCreator : PowerCreator<RangedAttackBeamPower>
	{
		public static new string name = "Shoot (Beam)";
		public static new string description = "Shoots a Beam. Obviously.";
		
		public RangedAttackBeamPowerCreator()
		{
			powerInfo = ScriptableObject.CreateInstance<RangedAttackBeamPower>();
			stats = new Stat[]
			{
				new Stat<int>("Range", "How far the beam goes.", value => powerInfo.range = value),
				new Stat<Direction>("Direction", "Direction of fire.", value => powerInfo.direction = value),
				new Stat<Effect>("Effect", "Effect to be aplied to what is hit by the beam.", value => powerInfo.effect = value)
			};
		}
	}
}