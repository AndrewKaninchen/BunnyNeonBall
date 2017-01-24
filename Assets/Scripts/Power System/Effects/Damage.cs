using UnityEngine;
using System.Collections;
using System;

namespace PowerSystem.Effects
{
	[CreateAssetMenu(menuName = "Effects/_Damage")]
	public class Damage : Effect
	{
		public float amount;
		public override void Apply(GameObject gameObject)
		{
			Character obj;
			obj = gameObject.GetComponent<Character>();
			if(obj!=null)
				obj.Damage(amount);
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