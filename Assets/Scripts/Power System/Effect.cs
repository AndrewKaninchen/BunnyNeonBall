using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PowerSystem
{
	public abstract class Effect : ScriptableObject
	{
		public static new string name;
		public static string description;
		/// <param name="target">Direct target of the Effect.</param>
		/// <param name="perpetrator">GameObject responsible for the triggering of the Effect.</param>		
	    /// <param name="additionalParamenters">For other parameters that depend on the circunstance in which the Effect is triggered rather than the Effect's stats.</param>
		public abstract void Trigger
		(
			GameObject target = null,
			GameObject perpetrator = null,
			params object[] additionalParameters
		);
	}
}