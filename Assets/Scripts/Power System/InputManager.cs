using UnityEngine;
using System.Collections.Generic;
using System;

namespace PowerSystem.InputManager
{
	public class InputManager : MonoBehaviour
	{
		//public static string 		
		public static List<InputAxis> axes = new List<InputAxis>
		{
			new InputAxis("A")
		};

		public void Update()
		{
			for (int i = 1; i <= 4; i++)
			{
				foreach (InputAxis axis in axes)
				{
					if (axis.axisMode == AxisType.GetButton)
					{
						if (Input.GetButton(axis.inputString + i))
							axis.del();
					}
					else if (axis.axisMode == AxisType.GetButtonDown)
					{
						if ((Input.GetButtonDown(axis.inputString + i)))
							axis.del();
					}
				}
			}
		}		
	}

	public enum AxisType
	{
		GetButton, GetButtonDown
	}

	public class InputAxis
	{
		public readonly string inputString;
		public readonly AxisType axisMode;
		public Action del;
		//bool inUse;

		public InputAxis (string inputString)
		{
			this.inputString = inputString;			
		}		
	}
}