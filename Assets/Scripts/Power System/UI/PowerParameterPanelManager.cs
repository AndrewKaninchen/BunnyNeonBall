using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

namespace PowerSystem.UI
{
	public class PowerParameterPanelManager : MonoBehaviour, IMoveHandler, ICancelHandler, ISubmitHandler, ISelectHandler
	{
		private MyEventSystem eventSystem;
		private EventTrigger eventTrigger;		
		public GameObject innerPanel;
		public GameObject namePanel;
		public GameObject effectParametersListPanel;
		private GameObject powerPanel;
		private ParameterSetterPanelManager parameterSetterPanelManager;
		
		public Stat stat;
		public GameObject parameterSetterPanelPrefab;
		public GameObject parameterPanelPrefab;		

		public void Initialize (MyEventSystem eventSystem, GameObject powerPanel, Stat stat)
		{			
			this.stat = stat;			
			this.eventSystem = eventSystem;
			this.powerPanel = powerPanel;

			namePanel.GetComponent<Text>().text = stat.name;
						
			Type genericStatType = stat.GetType().GetGenericArguments()[0];
			GameObject g = Instantiate(parameterSetterPanelPrefab, innerPanel.transform) as GameObject;

			if (genericStatType == typeof(int))
			{				
				parameterSetterPanelManager =  g.AddComponent<IntParameterSetterPanelManager>();				
				((IntParameterSetterPanelManager)parameterSetterPanelManager).Initialize(eventSystem, stat, 1, 10, 1);
			}
			else if (genericStatType == typeof(bool))
			{

			}
			else if (genericStatType.IsEnum)
			{				
				parameterSetterPanelManager = g.AddComponent<EnumParameterSetterPanelManager>();
				((EnumParameterSetterPanelManager)parameterSetterPanelManager).Initialize(eventSystem, stat, genericStatType);
			}
			else if (genericStatType == typeof(Effect))
			{			
				parameterSetterPanelManager = g.AddComponent<EffectParameterSetterPanelManager>();
				EffectClassListPanelManager effectClassListPanelManager = eventSystem.GetComponent<CharacterPanelManager>().effectClassListPanel.GetComponent<EffectClassListPanelManager>();
				PowerListPanelManager powerListPanelManager = eventSystem.GetComponent<CharacterPanelManager>().powerListPanel.GetComponent<PowerListPanelManager>();
				((EffectParameterSetterPanelManager)parameterSetterPanelManager).Initialize(
					eventSystem, stat, this, effectClassListPanelManager, powerListPanelManager, effectParametersListPanel, parameterPanelPrefab
				);
			}
		}

		public void OnCancel(BaseEventData eventData)
		{
			powerPanel.GetComponent<Toggle>().isOn = false;
			eventSystem.SetSelectedGameObject(powerPanel, eventData);
		}

		public void OnMove (AxisEventData eventData)
		{
			if (parameterSetterPanelManager.isActiveAndEnabled)
			{
				parameterSetterPanelManager.OnMove(eventData);						
			}
		}

		public void OnSubmit(BaseEventData eventData)
		{
			if (parameterSetterPanelManager.GetComponent<Selectable>()!= null)
			{
				eventSystem.SetSelectedGameObject(parameterSetterPanelManager.gameObject);
				effectParametersListPanel.SetActive(true);
			}
		}

		public void OnSelect(BaseEventData eventData)
		{
			effectParametersListPanel.SetActive(false);
		}
	}
}