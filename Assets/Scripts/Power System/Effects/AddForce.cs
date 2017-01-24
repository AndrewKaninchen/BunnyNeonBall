using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PowerSystem.Effects
{
	[CreateAssetMenu(menuName = "Effects/_AddForce")]
	public class AddForce : Effect
	{
		public float magnitude;

		public override void Apply(GameObject target, Vector2 direction)
		{
			Rigidbody2D obj;
			obj = target.GetComponent<Rigidbody2D>();
			if (obj != null)
				obj.AddForce(magnitude * direction);
		}

		public override void Apply(GameObject target)
		{
			Apply(target, Vector3.one);
		}

		public override void Apply(GameObject target, GameObject aplier)
		{
			Apply(target, Vector3.one);
		}

		public override void Apply(GameObject target, GameObject aplier, Vector2 direction)
		{
			Apply(target, direction);
		}
	}
}