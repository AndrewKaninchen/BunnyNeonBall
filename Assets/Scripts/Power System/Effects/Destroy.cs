using UnityEngine;
using System.Collections;
using System;

namespace PowerSystem.Effects
{
	[CreateAssetMenu(menuName = "Effects/_Destroy")]
	public class Destroy : Effect
	{
		public static new string name = "Destroy";
		public static new string description = "Utterly erases affected object from existence.";

		public override void Trigger(GameObject target = null, GameObject perpetrator = null, params object[] additionalParameters)
		{
			if (target.layer != LayerMask.NameToLayer("Scenario"))
				Destroy(target);
		}
	}
}