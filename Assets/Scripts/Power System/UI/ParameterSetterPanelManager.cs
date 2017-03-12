using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using Utilities;
using System;

namespace PowerSystem.UI
{
	public class ParameterSetterPanelManager : MonoBehaviour, IMoveHandler, ICancelHandler, ISelectHandler
	{
		protected int currentValue;
		protected int maxValue;
		protected int minValue;
		protected Stat stat;
		public Text valueText;
		protected bool isInitialized = false;
		protected MyEventSystem eventSystem;
		protected Image panelImage;

		public int MaxValue { get { return maxValue; } set { maxValue = value; UpdateText(); } }
		public int MinValue { get { return minValue; } set { minValue = value; UpdateText(); } }
		public virtual int ParameterValue { get { return currentValue; } set { currentValue = Mathf.Clamp(value, minValue, maxValue); UpdateText(); UpdateCreatorStat(); } }

		public void Initialize(MyEventSystem eventSystem, Stat stat)
		{
			this.eventSystem = eventSystem;			
			this.stat = stat;
			valueText = GetComponentInChildren<Text>();			
			isInitialized = true;			
			panelImage = GetComponent<Image>();
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

		public void UpdateCreatorStat()
		{
			stat.GetType().GetField("value").SetValue(stat, currentValue);
		}

		public virtual void OnMove(AxisEventData eventData)
		{
			ParameterValue +=
				(
					eventData.moveDir == MoveDirection.Right ? 1 :
					eventData.moveDir == MoveDirection.Left ? -1 :
					0
				);
		}

		public void OnCancel(BaseEventData eventData)
		{
			eventSystem.SetSelectedGameObject(transform.parent.parent.gameObject);
			panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, 0);
		}

		public void OnSelect(BaseEventData eventData)
		{
			panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, 1);
		}
	}
}