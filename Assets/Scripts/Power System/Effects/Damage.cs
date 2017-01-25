using UnityEngine;
using System.Collections;
using System;

namespace PowerSystem.Effects
{
	[CreateAssetMenu(menuName = "Effects/_Damage")]
	public class Damage : Effect
	{
		public float amount;
		public override void Trigger(GameObject target = null, GameObject perpetrator = null, params object[] additionalParameters)
		{
			Character obj;
			obj = target.GetComponent<Character>();
			if(obj!=null)
				obj.Damage(amount);
		}
	}
}