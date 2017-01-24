using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace PowerSystem
{
	public abstract class PowerCreator<PowerInfoType> where PowerInfoType : Power
	{
		public static string name;
		public static string description;
		public Stat[] stats;
		protected PowerInfoType powerInfo;
		public PowerInfoType Info { get { return powerInfo; } }
	}

	public abstract class Stat
	{
		public string name;
		public string description;
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

		public void SetParameters()
		{
			affectedFieldSetters(value);
		}
	}
}