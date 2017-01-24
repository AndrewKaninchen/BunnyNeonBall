using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PowerSystem
{	
	public abstract class Effect : ScriptableObject
	{
		public abstract void Apply(GameObject target, GameObject aplier, Vector2 direction);
		public abstract void Apply(GameObject target, GameObject aplier);
		public abstract void Apply(GameObject target);
		public abstract void Apply(GameObject target, Vector2 direction);
	}
}