﻿using UnityEngine;
using UnityEngine.UI;
using System;

namespace PowerSystem.UI
{
	public class EnumParameterSetterPanelManager : ParameterSetterPanelManager
	{		
		public Type enumType;
		public override int ParameterValue { get { return currentValue; } set { currentValue = Mathf.Clamp(value, minValue, maxValue); UpdateText(); UpdateCreatorStat(); } }

		public void Initialize(Stat stat, Type enumType)
		{
			this.stat = stat;
			this.enumType = enumType;
			minValue = 0;
			maxValue = Enum.GetNames(enumType).GetLength(0)-1;			
			valueText = GetComponentInChildren<Text>();			
			isInitialized = true;
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