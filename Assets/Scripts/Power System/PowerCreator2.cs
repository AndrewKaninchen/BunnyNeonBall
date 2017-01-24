using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace PowerSystem
{
	public abstract class PowerCreator2<PowerInfoType> where PowerInfoType : Power
	{
		public static string name;
		public static string description;
		public Stat[] stats;
		protected PowerInfoType powerInfo;
		public PowerInfoType Info { get { return powerInfo; } }
		public abstract PowerInfoType CreatePower();
	}

	//public class Stat<T> : Stat
	//{
	//	public string name;
	//	public string description;
	//	public T rating;
	//	public Action<T> affectedFieldSetters;

	//	public Stat(string name, string description, Action<T> affectedFieldSetters)
	//	{
	//		this.name = name;
	//		this.description = description;
	//		this.affectedFieldSetters = affectedFieldSetters;
	//	}

	//	public void SetParameters()
	//	{
	//		affectedFieldSetters(rating);
	//	}
	//}


	//<summary>
	//Use this attribute to set maximum and minimum values to a Power's Stat of integer type.
	//</summary>
	//[AttributeUsage(AttributeTargets.Class)]
	//public class Range : Attribute
	//{
	//	int min, max;
	//	//<summary>
	//	//Use this attribute to set maximum and minimum values to a Power's Stat of integer type.
	//	//</summary>
	//	public Range (int min, int max) { this.min = min; this.max = max; }
	//}
}