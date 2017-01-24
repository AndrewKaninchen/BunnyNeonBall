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
	public class AddNewPowerPanelManager2 : MonoBehaviour, ISubmitHandler, ICancelHandler
	{
		private PowerListPanelManager powerListPanelManager;
		private PowerClassListPanelManager powerClassListPanelManager;
		private ScrollRect powerListScrollRect;
		[HideInInspector]
		public CharacterPanelManager characterPanelManager;
		[HideInInspector]
		public MyEventSystem eventSystem;

		public void Initialize(PowerListPanelManager powerListPanelManager, CharacterPanelManager characterPanelManager)
		{
			this.characterPanelManager = characterPanelManager;
			eventSystem = characterPanelManager.eventSystem;
			this.powerListPanelManager = powerListPanelManager;
			powerClassListPanelManager = characterPanelManager.powerClassListPanel.GetComponent<PowerClassListPanelManager>();

			powerListScrollRect = GetComponentInParent<ScrollRect>();			
		}

		public void OnSubmit(BaseEventData eventData)
		{
			powerClassListPanelManager.gameObject.SetActive(true);
			eventSystem.SetSelectedGameObject(powerClassListPanelManager.powerClassPanels.First());
			powerClassListPanelManager.GetComponentInChildren<HierarchyNavigationGroup>().childrenExitTarget = GetComponent<Selectable>();
			powerListPanelManager.gameObject.SetActive(false);
			//AddPower(0);			
		}

		IEnumerator SetScrollRectPosition()
		{
			yield return null;
			powerListScrollRect.verticalNormalizedPosition = 0;
		}

		public void AddPower(int powerClassID)
		{
			Type pigT = Manager.powerCreatorTypes[powerClassID];
			Type piT = Manager.powerTypes[powerClassID];

			//begin	Suicídio mental
			var addPowerMethod = typeof(PowerListPanelManager).GetMethod("AddPower");
			var addPowerMethodInstance = addPowerMethod.MakeGenericMethod(pigT, piT);
			addPowerMethodInstance.Invoke(powerListPanelManager, null);
			StartCoroutine("SetScrollRectPosition");
		}

		public void OnCancel(BaseEventData eventData)
		{			
			eventSystem.SetSelectedGameObject(powerListPanelManager.gameObject);
			powerListScrollRect.verticalNormalizedPosition = 1;
		}		
	}
}