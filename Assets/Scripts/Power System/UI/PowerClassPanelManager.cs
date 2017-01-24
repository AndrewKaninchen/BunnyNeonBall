using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using Utilities;

namespace PowerSystem.UI
{
	public class PowerClassPanelManager : MonoBehaviour, IMoveHandler, ISubmitHandler, ICancelHandler
	{
		MyEventSystem eventSystem;
		private ScrollRect powerClassListScrollRect;
		private RectTransform powerClassListPanelRectTransform;
		private GameObject powerClassListPanel;
		private PowerClassListPanelManager powerClassListPanelManager;
		private GameObject powerListPanel;
		private int classID;
		private HierarchyNavigationGroup hierarchyNavigationGroup;

		[SerializeField]	private Text nameText;
		[SerializeField]	private Text descriptionText;

		public void Initialize(CharacterPanelManager characterPanelManager, int classID, string className, string classDescription)
		{
			eventSystem = GetComponentInParent<MyEventSystem>();
			powerClassListScrollRect = GetComponentInParent<ScrollRect>();			
			powerClassListPanel = characterPanelManager.powerClassListPanel;
			powerClassListPanelManager = powerClassListPanel.GetComponent<PowerClassListPanelManager>();
			powerListPanel = characterPanelManager.powerListPanel;
			hierarchyNavigationGroup = GetComponentInParent<HierarchyNavigationGroup>();

			this.classID = classID;
			nameText.text = className;
			descriptionText.text = classDescription;			
		}

		public void OnMove(AxisEventData eventData)
		{
			float newPos = powerClassListScrollRect.normalizedPosition.y;

			newPos += Mathf.Round(Mathf.Clamp(eventData.moveVector.y, -1, 1)) / (powerClassListPanelManager.powerClassPanels.Count);
			newPos = Mathf.Clamp01(newPos);
			powerClassListScrollRect.verticalNormalizedPosition = newPos;
		}

		public void OnSubmit(BaseEventData eventData)
		{
			OnCancel(eventData);
			hierarchyNavigationGroup.childrenExitTarget.GetComponent<AddNewPowerPanelManager2>().AddPower(classID);			
		}

		public void OnCancel(BaseEventData eventData)
		{
			powerListPanel.SetActive(true);
			powerClassListPanel.SetActive(false);
			eventSystem.SetSelectedGameObject(null);
			eventSystem.SetSelectedGameObject(hierarchyNavigationGroup.childrenExitTarget.gameObject);
		}
	}
}
