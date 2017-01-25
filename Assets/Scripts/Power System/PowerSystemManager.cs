using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace PowerSystem
{
	public static class Manager
	{		
		public static List<Type> powerCreatorTypes;
		public static List<Type> powerTypes;
		public static List<Type> effectTypes;

		public static void Initialize()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();

			powerTypes = new List<Type>(assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Power))));
			effectTypes = new List<Type>(assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Effect))));
			powerCreatorTypes = new List<Type>
			(
				assembly.GetTypes().Where(
					t => t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(PowerCreator<>))
			);
		}
	}
}