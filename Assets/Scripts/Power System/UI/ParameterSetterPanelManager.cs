using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace PowerSystem.UI
{
	public class ParameterSetterPanelManager : MonoBehaviour
	{
		protected int currentValue;
		protected int maxValue;
		protected int minValue;
		public Text valueText;
		protected bool isInitialized = false;

		public int MaxValue { get { return maxValue; } set { maxValue = value; UpdateText(); } }
		public int MinValue { get { return minValue; } set { minValue = value; UpdateText(); } }
		public virtual int ParameterValue { get { return currentValue; } set { currentValue = Mathf.Clamp(value, minValue, maxValue); UpdateText(); } }

		public void Initialize(int minValue = 1, int maxValue = 3, int startingValue = 1)
		{
			this.minValue = minValue;
			this.maxValue = maxValue;			
			valueText = GetComponentInChildren<Text>();			
			isInitialized = true;
			ParameterValue = startingValue;
		}

		public void UpdateText()
		{
			if (isInitialized)
			{
				string right = (currentValue >= maxValue) ? "  " : " ▶";
				string left = (currentValue <= minValue) ? "  " : "◀ ";
				valueText.text = left + currentValue.ToString() + right;
			}
		}
	}
}