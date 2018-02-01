using UnityEngine;

namespace PowerSystem.Powers
{	
	public class RangedAttackBeamPowerCreator : PowerCreator<RangedAttackBeamPower, RangedAttackBeamPowerController>
	{
		public static new string name = "Shoot (Beam)";
		public static new string description = "Shoots a Beam. Obviously.";

		public RangedAttackBeamPowerCreator()
		{
            Instance = ScriptableObject.CreateInstance<RangedAttackBeamPower>();
            stats = new Stat[]
			{
				new Stat<int>("Range", "How far the beam goes.", value => Instance.range = value),
				new Stat<int>("Rate of Fire", "How fast you can fire the next beam.", value => Instance.rateOfFire = 1f/value),
				new Stat<Direction>("Direction", "Direction of fire.", value => Instance.direction = value),
				new Stat<Effect>("Effect", "Effect to be aplied to what is hit by the beam.", value => Instance.effect = value)
			};
		}
	}
}