using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace PowerSystem
{
	public abstract class PowerInfoGenerator<PowerInfoType> where PowerInfoType : PowerInfo
	{
		public static string powerClassName;
		public MetaParameter[] parameters;
		protected PowerInfoType powerInfo;
		public PowerInfoType Info { get { return powerInfo; } }
	}

	public class MetaParameter
	{
		public string name;
		public string description;
		public int rating;
		public Action<int> affectedParameterSetters;

		public MetaParameter(string name, Action<int> affectedParameterSetters, string description)
		{
			this.name = name;
			this.description = description;
			this.affectedParameterSetters = affectedParameterSetters;
		}

		public void SetParameters()
		{
			affectedParameterSetters(rating);
		}
	}
}