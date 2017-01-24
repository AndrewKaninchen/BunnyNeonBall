using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;

using Utilities;

namespace PowerSystem.UI
{
	public class AddNewPowerPanelManager : MonoBehaviour, ISubmitHandler, ICancelHandler
	{
		private PowerListPanelManager powerListPanelManager;
		private ScrollRect powerListScrollRect;
		[HideInInspector]
		public MyEventSystem eventSystem;
		[HideInInspector]
		public CharacterPanelManager characterPanelManager;

		public GameObject textPanel;
		public AddNewPowerDropdownManager dropdownManager;
		private Dropdown dropdown;


		public void Initialize(PowerListPanelManager powerListPanelManager)
		{
			characterPanelManager = powerListPanelManager.characterPanelManager;
			eventSystem = powerListPanelManager.eventSystem;
			this.powerListPanelManager = powerListPanelManager;
			powerListScrollRect = GetComponentInParent<ScrollRect>();
			dropdown = dropdownManager.gameObject.GetComponent<Dropdown>();
			dropdownManager.Initialize(this);

			dropdown.onValueChanged.AddListener(OnDropdownChangedValue);
		}

		public void OnSubmit(BaseEventData eventData)
		{
			textPanel.SetActive(false);
			dropdownManager.gameObject.SetActive(true);
			eventSystem.SetSelectedGameObject(dropdownManager.gameObject);
			dropdown.OnSubmit(eventData);
		}

		public void OnDropdownChangedValue(int n)
		{
			Type pigT = Manager.powerCreatorTypes[dropdown.value];
			Type piT = Manager.powerTypes[dropdown.value];
									
			var addPowerMethod = typeof(PowerListPanelManager).GetMethod("AddPower");
			var addPowerMethodInstance = addPowerMethod.MakeGenericMethod(pigT, piT);
			addPowerMethodInstance.Invoke(powerListPanelManager, null);
			
			
			powerListScrollRect.verticalNormalizedPosition = 0;

			OnDropDownCancel();
		}

		public void OnDropDownCancel()
		{
			eventSystem.SetSelectedGameObject(gameObject);
			textPanel.SetActive(true);			
		}

		public void OnCancel(BaseEventData eventData)
		{
			eventSystem.SetSelectedGameObject(powerListPanelManager.gameObject);
			powerListScrollRect.verticalNormalizedPosition = 1;
		}
	}
}