using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PowerSystem
{
	public abstract class PowerController : MonoBehaviour
	{
		public abstract Power PowerInstance { get; set; }
	}

	public abstract class PowerController <PowerType> : PowerController where PowerType : Power
	{
		public override Power PowerInstance { get { return powerInstance; } set { powerInstance = (PowerType)value; } }
        public PowerType powerInstance;
	}
}