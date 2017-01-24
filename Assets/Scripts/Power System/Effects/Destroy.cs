using UnityEngine;
using System.Collections;
using System;

namespace PowerSystem.Effects
{
	[CreateAssetMenu(menuName = "Effects/_Destroy")]
	public class Destroy : Effect
	{
		public override void Apply(GameObject target)
		{
			if (target.layer != LayerMask.NameToLayer("Scenario"))
				Destroy(target);
		}

		public override void Apply(GameObject target, Vector2 direction)
		{
			Apply(target);
		}

		public override void Apply(GameObject target, GameObject aplier)
		{
			Apply(target);
		}

		public override void Apply(GameObject target, GameObject aplier, Vector2 direction)
		{
			Apply(target);
		}
	}
}