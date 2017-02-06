using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PowerSystem.Effects
{
	[CreateAssetMenu(menuName = "Effects/_AddForce")]
	public class AddForceEffect : Effect
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

	public class AddForceEffectCreator : EffectCreator<AddForceEffect>
	{
		public static new string name = "Jump";
		public static new string description = "Makes the character hop in the air. Duh.";		
		public static Dictionary<string, string> statDescriptions = new Dictionary<string, string>
		{
			{ "Magnitude", "How much force is applied at the target."},			
		};

		public AddForceEffectCreator()
		{
			effect = ScriptableObject.CreateInstance<AddForceEffect>();
			stats = new Stat[]
			{
				new Stat<int>("Magnitude", statDescriptions["Magnitude"], value => effect.magnitude = value * 500)
			};
		}
	}
}