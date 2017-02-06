using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;


namespace PowerSystem
{
	public abstract class EffectCreator
	{
		public static string name;
		public static string description;
		public Stat[] stats;
		public abstract Effect Effect { get; }
		public abstract Effect SetEffectStats();
	}

	public abstract class EffectCreator<EffectType> : EffectCreator where EffectType : Effect
	{
		protected EffectType effect;
		public override Effect Effect { get { return effect; } }
		public override Effect SetEffectStats()
		{
			foreach (Stat stat in stats)
			{
				stat.SetFields();
				Debug.Log(stat.name + ": " + stat.GetType().GetField("value").GetValue(stat));
			}
			return effect;
		}
	}
}