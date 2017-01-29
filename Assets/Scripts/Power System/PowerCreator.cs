using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace PowerSystem
{
	public abstract class PowerCreator<PowerType> where PowerType : Power
	{
		public static string name;
		public static string description;
		public Stat[] stats;
		protected PowerType power;
		public PowerType Info { get { return power; } }

		public PowerType SetPowerStats()
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