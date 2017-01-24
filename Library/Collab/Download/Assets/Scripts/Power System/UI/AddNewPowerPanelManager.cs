using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;

namespace PowerSystem.UI
{
	public class AddNewPowerPanelManager : MonoBehaviour, ISubmitHandler, ICancelHandler
	{
		private PowerListPanelManager powerListPanelManager;
		private ScrollRect powerListScrollRect;
		private MyEventSystem eventSystem;

		public GameObject textPanel;
		public AddNewPowerDropdownManager dropdownManager;
		private Dropdown dropdown;


		public void Initialize(PowerListPanelManager powerListPanelManager, MyEventSystem eventSystem)
		{
			this.eventSystem = eventSystem;
			this.powerListPanelManager = powerListPanelManager;
			powerListScrollRect = GetComponentInParent<ScrollRect>();
			dropdown = dropdownManager.gameObject.GetComponent<Dropdown>();
			dropdownManager.Initialize(this);
		}

		public void OnSubmit(BaseEventData eventData)
		{
			textPanel.SetActive(false);
			dropdownManager.gameObject.SetActive(true);
			eventSystem.SetSelectedGameObject(dropdownManager.gameObject);
			dropdown.OnSubmit(eventData);		
		}

		public void OnDropdownSubmit()
		{
			powerListPanelManager.AddPower();
			eventSystem.SetSelectedGameObject(powerListPanelManager.powerPanels[powerListPanelManager.powerPanels.Count - 1]);
			powerListScrollRect.verticalNormalizedPosition = 0;
		}

		public void OnDropDownCancel()
		{
			textPanel.SetActive(true);
			dropdownManager.gameObject.SetActive(false);
			eventSystem.SetSelectedGameObject(gameObject);
		}

		public void OnCancel(BaseEventData eventData)
		{
			eventSystem.SetSelectedGameObject(powerListPanelManager.gameObject);
			powerListScrollRect.verticalNormalizedPosition = 1;
		}
	}
}