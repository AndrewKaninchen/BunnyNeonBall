using UnityEngine;
using System.Collections;

namespace PowerSystem.Powers
{
	[CreateAssetMenu(menuName = "Powers/Beam")]
	public class RangedAttackProjectilePower : RangedAttackPower
	{
		public float speed;
		public Effect effect;		
	}
}