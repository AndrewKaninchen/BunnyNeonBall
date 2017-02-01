using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PowerSystem
{
	public abstract class PowerController : MonoBehaviour
	{
		public abstract Power Power { get; set; }
	}

	public abstract class PowerController <PowerType> : PowerController where PowerType : Power
	{
		public override Power Power { get { return power; } set { power = (PowerType)value; } }
		public PowerType power;
	}
}