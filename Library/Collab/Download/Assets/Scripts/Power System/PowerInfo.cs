using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PowerSystem
{
	public abstract class PowerInfo : ScriptableObject
	{
		public int baseCostPerGraduation;
		public int maxGraduations;
		public string activationKey;
	}
}