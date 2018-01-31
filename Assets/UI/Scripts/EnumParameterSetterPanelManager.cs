using UnityEngine;
using UnityEngine.UI;
using System;
using Utilities;

namespace PowerSystem.UI
{
	public class EnumParameterSetterPanelManager : ParameterSetterPanelManager
	{		
		public Type enumType;
		public override int ParameterValue { get { return currentValue; } set { currentValue = Mathf.Clamp(value, minValue, maxValue); UpdateText(); UpdateCreatorStat(); } }

		public void Initialize(MyEventSystem eventSystem, Stat stat, Type enumType)
		{
			base.Initialize(eventSystem, stat);
			this.enumType = enumType;
			minValue = 0;
			maxValue = Enum.GetNames(enumType).GetLength(0)-1;
			ParameterValue = 0;
		}

		public new void UpdateText()
		{
			if (isInitialized)
			{
				string right = (currentValue >= maxValue) ? "  " : " ▶";
				string left = (currentValue <= minValue) ? "  " : "◀ ";
				valueText.text = left + Enum.GetName(enumType, currentValue) + right;
			}
		}
	}
}