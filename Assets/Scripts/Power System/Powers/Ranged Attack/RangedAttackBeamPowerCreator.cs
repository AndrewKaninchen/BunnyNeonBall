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
				new Stat("Range", rating => powerInfo.range = rating, "How far the beam goes."),				
			};
		}
	}
}


namespace PowerSystem.Powers
{
	public class RangedAttackBeamPowerCreator2 : PowerCreator2<RangedAttackBeamPower>
	{
		public static new string name = "Shoot (Beam)";
		public static new string description = "Shoots a Beam. Obviously.";
		public RangedAttackBeamPowerCreator2()
		{
			powerInfo = ScriptableObject.CreateInstance<RangedAttackBeamPower>();
			stats = new Stat[]
			{
				new Stat("Range", rating => powerInfo.range = rating, "How far the beam goes."),
			};
		}

		public override RangedAttackBeamPower CreatePower()
		{
			throw new NotImplementedException();
		}
	}
}
