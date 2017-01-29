using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Utilities;
using System;

namespace PowerSystem.UI
{
	public class PowerParameterPanelManager : MonoBehaviour, IMoveHandler, ICancelHandler
	{
		private MyEventSystem eventSystem;
		private EventTrigger eventTrigger;
		private GameObject powerPanel;
		private ParameterSetterPanelManager parameterSetterPanelManager;
		public Stat stat;

		public GameObject parameterSetterPanelPrefab;
        //private IntParameterSetterPanelManager parameterSetterPanelManager;

		public void Initialize (MyEventSystem eventSystem, GameObject powerPanel, Stat stat)
		{
			this.stat = stat;
			stat.GetType().GetField("value").SetValue(stat, 2);
			this.eventSystem = eventSystem;
			this.powerPanel = powerPanel;

			transform.FindChild("Parameter Name").GetComponent<Text>().text = stat.name;
						
			Type genericStatType = stat.GetType().GetGenericArguments()[0];			

			if (genericStatType == typeof(int))
			{
				GameObject g = Instantiate(parameterSetterPanelPrefab, transform) as GameObject;
				parameterSetterPanelManager =  g.AddComponent<IntParameterSetterPanelManager>();				
				parameterSetterPanelManager.Initialize(1, 10, 1);
			}
			else if (genericStatType == typeof(bool))
			{

			}
			else if (genericStatType.IsEnum)
			{
				GameObject g = Instantiate(parameterSetterPanelPrefab, transform) as GameObject;
				parameterSetterPanelManager = g.AddComponent<EnumParameterSetterPanelManager>();
				((EnumParameterSetterPanelManager)parameterSetterPanelManager).Initialize(genericStatType);
			}
			else if (genericStatType == typeof(Effect))
			{
				GameObject g = Instantiate(parameterSetterPanelPrefab, transform) as GameObject;
				parameterSetterPanelManager = g.AddComponent<EffectParameterSetterPanelManager>();
				((EffectParameterSetterPanelManager)parameterSetterPanelManager).Initialize();
			}
		}

		public void OnCancel(BaseEventData eventData)
		{
			//Debug.Log("oi");
			powerPanel.GetComponent<Toggle>().isOn = false;
			eventSystem.SetSelectedGameObject(powerPanel, eventData);
		}

		public void OnMove (AxisEventData eventData)
		{
			if (parameterSetterPanelManager.isActiveAndEnabled)
			{		
				parameterSetterPanelManager.ParameterValue +=
				(
					eventData.moveDir == MoveDirection.Right ? 1 :
					eventData.moveDir == MoveDirection.Left ? -1 :
					0
				);
				//Consertar essa porcaria depois porque tem coisa que não é, well, int. Basicamente isso tem que ser feito nas classes que herdam de ParameterSetterPanelManager.
				stat.GetType().GetField("value").SetValue(stat, parameterSetterPanelManager.ParameterValue);
			}
		}	
	}
}