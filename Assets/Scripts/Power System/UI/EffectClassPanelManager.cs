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
	public class EffectClassPanelManager : MonoBehaviour, IMoveHandler, ISubmitHandler, ICancelHandler
	{
		MyEventSystem eventSystem;
		private ScrollRect effectClassListScrollRect;
		private RectTransform effectClassListPanelRectTransform;
		private GameObject effectClassListPanel;
		private EffectClassListPanelManager effectClassListPanelManager;
		private GameObject effectListPanel;
		private int classID;
		private HierarchyNavigationGroup hierarchyNavigationGroup;

		[SerializeField]	private Text nameText;
		[SerializeField]	private Text descriptionText;

		public void Initialize(CharacterPanelManager characterPanelManager, int classID, string className, string classDescription)
		{
			eventSystem = GetComponentInParent<MyEventSystem>();
			effectClassListScrollRect = GetComponentInParent<ScrollRect>();			
			effectClassListPanel = characterPanelManager.effectClassListPanel;
			effectClassListPanelManager = effectClassListPanel.GetComponent<EffectClassListPanelManager>();
			effectListPanel = characterPanelManager.powerListPanel;
			hierarchyNavigationGroup = GetComponentInParent<HierarchyNavigationGroup>();

			this.classID = classID;
			nameText.text = className;
			descriptionText.text = classDescription;			
		}

		public void OnMove(AxisEventData eventData)
		{
			float newPos = effectClassListScrollRect.normalizedPosition.y;

			newPos += Mathf.Round(Mathf.Clamp(eventData.moveVector.y, -1, 1)) / (effectClassListPanelManager.effectClassPanels.Count);
			newPos = Mathf.Clamp01(newPos);
			effectClassListScrollRect.verticalNormalizedPosition = newPos;
		}

		public void OnSubmit(BaseEventData eventData)
		{
			OnCancel(eventData);
			//hierarchyNavigationGroup.childrenExitTarget.GetComponent<AddNewPowerPanelManager2>().AddPower(classID);			
		}

		public void OnCancel(BaseEventData eventData)
		{
			effectListPanel.SetActive(true);
			effectClassListPanel.SetActive(false);
			eventSystem.SetSelectedGameObject(null);
			eventSystem.SetSelectedGameObject(hierarchyNavigationGroup.childrenExitTarget.gameObject);
		}
	}
}
