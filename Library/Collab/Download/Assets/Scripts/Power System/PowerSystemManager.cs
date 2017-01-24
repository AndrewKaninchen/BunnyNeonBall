using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class)]
public class PowerGeneratorAttribute : Attribute { }

public static class PowerSystemManager
{
	public static List<Type> powerInfoGeneratorTypes;

	public static void Initialize()
	{
		Assembly assembly = Assembly.GetExecutingAssembly();

		powerInfoGeneratorTypes = new List<Type>(assembly.GetTypes().Where(t => t.GetCustomAttributes(typeof(PowerGeneratorAttribute), false).Count() > 0));

		if (powerInfoGeneratorTypes == null)
			Debug.Log("oxe");
		else
		{
			foreach (Type t in powerInfoGeneratorTypes)
			{
				Debug.Log(t.Name);
			}
		}
	}
}