using UnityEngine;
using System.Collections;

namespace PowerSystem.Powers
{
	[CreateAssetMenu(menuName = "Powers/Beam")]
	public class RangedAttackBeamPower : RangedAttackPower
	{
		public float range;
		public Effect effect;
	}
}