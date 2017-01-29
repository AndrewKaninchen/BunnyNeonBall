using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PowerSystem.Effects
{
	[CreateAssetMenu(menuName = "Effects/_AddForce")]
	public class Push : Effect
	{
		public static new string name = "Push/Pull";
		public static new string description = "Applies an amount of kinectic energy to the affected body.";
		public float magnitude;

		public override void Trigger(GameObject target = null, GameObject perpetrator = null, params object[] additionalParameters)
		{
			Vector2 direction = (Vector2)additionalParameters[0];
			Rigidbody2D obj;
			obj = target.GetComponent<Rigidbody2D>();
			if (obj != null)
				obj.AddForce(magnitude * direction);
		}
	}
}