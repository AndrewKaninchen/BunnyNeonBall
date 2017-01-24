﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using Utilities;

namespace PowerSystem.UI
{
	public class PowerPanelManager : MonoBehaviour, IMoveHandler, ICancelHandler, IDeleteHandler
	{
		List<GameObject> parameterPanels = new List<GameObject>();
		MyEventSystem eventSystem;

		public Sprite spriteAButton;
		public Sprite spriteBButton;

		public GameObject parameterPanelPrefab;

		private ScrollRect powerListScrollRect;
		private RectTransform powerListPanelRectTransform;
		private PowerListPanelManager powerListPanelManager;
		[SerializeField]
		private Text powerName;

		public void Initialize<PowerType> (PowerCreator<PowerType> powerCreator, PowerListPanelManager powerListPanelManager) where PowerType : Power
		{
			eventSystem = GetComponentInParent<MyEventSystem>();
			powerListScrollRect = GetComponentInParent<ScrollRect>();
			powerListPanelRectTransform = transform.parent.parent.GetComponent<RectTransform>();
			this.powerListPanelManager = powerListPanelManager;

			Toggle toggle = GetComponent<Toggle>();
			toggle.onValueChanged.AddListener((data) => { OnToggle(data); });

			powerName.text = powerCreator.GetType().GetField("name").GetValue(null) as string;

			foreach (Stat parameter in powerCreator.stats)
			{
				GameObject parameterPanel = Instantiate(parameterPanelPrefab, transform.FindChild("Parameters Panel")) as GameObject;
				parameterPanel.GetComponent<PowerParameterPanelManager>().Initialize(eventSystem, gameObject, parameter);

				parameterPanels.Add(parameterPanel);				
			}

		}	

		public void OnToggle(bool data)
		{
			GameObject p0 = transform.FindChild("Parameters Panel").GetChild(0).gameObject;
			eventSystem.SetSelectedGameObject(p0, null);
		}

		public void OnMove(AxisEventData eventData)
		{			
			float newPos = powerListScrollRect.normalizedPosition.y;

			newPos += Mathf.Round(Mathf.Clamp (eventData.moveVector.y, -1, 1)) / (powerListPanelManager.powerPanels.Count);
			newPos = Mathf.Clamp01(newPos);
			powerListScrollRect.verticalNormalizedPosition = newPos;
		}

		public void OnCancel(BaseEventData eventData)
		{
			eventSystem.SetSelectedGameObject(powerListPanelManager.gameObject);
			powerListScrollRect.verticalNormalizedPosition = 1;
		}

		public void OnDelete(BaseEventData eventData)
		{
			Debug.Log("oie");
			eventSystem.SetSelectedGameObject(powerListPanelManager.gameObject);
			Destroy(gameObject);
		}
	}
}