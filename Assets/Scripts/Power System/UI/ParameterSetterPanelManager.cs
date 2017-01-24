using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace PowerSystem.UI
{

	public class ParameterSetterPanelManager : MonoBehaviour
	{

		private int currentValue;
		private int maxValue;
		private int minValue;
		public Text valueText;

		public int MaxValue { get { return maxValue; } set { maxValue = value; UpdateText(); } }
		public int MinValue { get { return minValue; } set { minValue = value; UpdateText(); } }
		public int ParameterValue { get { return currentValue; } set { currentValue = Mathf.Clamp(value, minValue, maxValue); UpdateText(); } }

		public void Initialize(int minValue = 1, int maxValue = 3, int startingValue = 1)
		{
			this.minValue = minValue;
			this.maxValue = maxValue;
			ParameterValue = startingValue;
		}

		void UpdateText()
		{
			string right = (currentValue >= maxValue) ? "  " : " ▶";
			string left = (currentValue <= minValue) ? "  " : "◀ ";
			valueText.text = left + currentValue.ToString() + right;
		}
	}
}