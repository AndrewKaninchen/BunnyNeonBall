using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace PowerSystem.Effects
{
	[CreateAssetMenu(menuName = "Effects/_Damage")]
	public class DamageEffect : Effect
	{
		public static new string name = "Damage";
		public static new string description = "Applies an amount of damage to the affected object.";

		public float amount;
		public override void Trigger(GameObject target = null, GameObject perpetrator = null, params object[] additionalParameters)
		{
			Character obj;
			obj = target.GetComponent<Character>();
			if(obj!=null)
				obj.Damage(amount);
		}
	}

	public class DamageEffectCreator : EffectCreator<DamageEffect>
	{
		public static new string name = "Damage";
		public static new string description = "Applies an amount of damage to the affected object.";
		public static Dictionary<string, string> statDescriptions = new Dictionary<string, string>
		{
			{ "Magnitude", "How much damage is applied at the target."},
		};

		public DamageEffectCreator()
		{
			effect = ScriptableObject.CreateInstance<DamageEffect>();
			stats = new Stat[]
			{
				new Stat<int>("Amount", statDescriptions["Magnitude"], value => effect.amount = value * 500)
			};
		}
	}

}