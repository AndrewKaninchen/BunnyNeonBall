using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace PowerSystem
{
	public abstract class PowerCreator
	{
		public static string name;
		public static string description;
		public Stat[] stats;
		public abstract Power Power { get; }
		public abstract Power SetPowerStats();
	}

	public abstract class PowerCreator<PowerType> : PowerCreator where PowerType : Power
	{
		public static Type powerType = typeof(PowerType);
		protected PowerType power;
		public override Power Power { get { return power; } }

		public PowerType Info { get { return power; } }
				
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
}