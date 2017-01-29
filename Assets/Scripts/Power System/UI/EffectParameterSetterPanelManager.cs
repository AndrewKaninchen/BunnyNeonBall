using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace PowerSystem.UI
{
	public class EffectParameterSetterPanelManager : ParameterSetterPanelManager
	{			
		public override int ParameterValue { get { return currentValue; } set { currentValue = Mathf.Clamp(value, minValue, maxValue); UpdateText(); UpdateCreatorStat(); } }

		public void Initialize(Stat stat)
		{
			this.stat = stat;
			minValue = 0;
			maxValue = Manager.effectTypes.Count-1;
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
				valueText.text = left + Manager.effectTypes[currentValue].GetField("name").GetValue(null) + right;
			}
		}

		public new void UpdateCreatorStat()
		{
			stat.GetType().GetField("value").SetValue(stat, ScriptableObject.CreateInstance(Manager.effectTypes[currentValue]));
		}
	}
}