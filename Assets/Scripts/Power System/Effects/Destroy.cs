using UnityEngine;
using System.Collections;
using System;

namespace PowerSystem.Effects
{
	[CreateAssetMenu(menuName = "Effects/_Destroy")]
	public class Destroy : Effect
	{
		public override void Trigger(GameObject target = null, GameObject perpetrator = null, params object[] additionalParameters)
		{
			if (target.layer != LayerMask.NameToLayer("Scenario"))
				Destroy(target);
		}
	}
}