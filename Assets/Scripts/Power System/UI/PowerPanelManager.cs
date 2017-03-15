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
	public class PowerPanelManager : MonoBehaviour, IMoveHandler, ICancelHandler, IDeleteHandler
	{
		private MyEventSystem eventSystem;

		public Sprite spriteAButton;
		public Sprite spriteBButton;

		public GameObject parameterPanelPrefab;

		private ScrollRect powerListScrollRect;
		private PowerListPanelManager powerListPanelManager;
		[SerializeField]
		private Text powerName;

		public PowerCreator powerCreator;
		public Power power; 
		
		
		public void Initialize (PowerCreator powerCreator, PowerListPanelManager powerListPanelManager)
		{
			this.powerCreator = powerCreator;
			eventSystem = GetComponentInParent<MyEventSystem>();
			powerListScrollRect = GetComponentInParent<ScrollRect>();			
			this.powerListPanelManager = powerListPanelManager;

			Toggle toggle = GetComponent<Toggle>();
			toggle.onValueChanged.AddListener((data) => { OnToggle(data); });

			powerName.text = powerCreator.GetType().GetField("name").GetValue(null) as string;

			foreach (Stat stat in powerCreator.stats)
			{				
				GameObject parameterPanel = Instantiate(parameterPanelPrefab, transform.FindChild("Parameters List Panel")) as GameObject;
				parameterPanel.GetComponent<PowerParameterPanelManager>().Initialize(eventSystem, gameObject, stat);				
			}
		}	

		public void OnToggle(bool data)
		{
			GameObject p0 = transform.FindChild("Parameters List Panel").GetChild(0).gameObject;
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
			eventSystem.SetSelectedGameObject(powerListPanelManager.gameObject);
			Destroy(gameObject);
		}

		public Power UpdatePower()
		{
			return powerCreator.SetPowerStats();
		}	

		public void Update()
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				power = UpdatePower();
			}
		}
	}
}