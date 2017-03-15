using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace PowerSystem
{
	public abstract class Stat
	{
		public string name;
		public string description;
		public abstract void SetFields();
	}
	public class Stat<T> : Stat
	{
		public T value;
		public Action<T> affectedFieldSetters;

		public Stat(string name, string description, Action<T> affectedFieldSetters)
		{
			this.name = name;
			this.description = description;
			this.affectedFieldSetters = affectedFieldSetters;
		}

		public override void SetFields()
		{
			affectedFieldSetters(value);
			Debug.Log("setando uns field");
		}
	}	

	public abstract class PowerCreator
	{
		public static string name;
		public static string description;				
		public Stat[] stats;
		public abstract Power Power { get; }
		public abstract Power SetPowerStats();		
	}

	public abstract class PowerCreator<PowerType, PowerControllerType> : PowerCreator where PowerType : Power where PowerControllerType : PowerController
	{		
		protected PowerType power;
		public override Power Power { get { return power; } }
		public override Power SetPowerStats()
		{
			foreach (Stat stat in stats)
			{
				stat.SetFields();
				Debug.Log(stat.name + ": " + stat.GetType().GetField("value").GetValue(stat));
			}
			return power;
		}
	}

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