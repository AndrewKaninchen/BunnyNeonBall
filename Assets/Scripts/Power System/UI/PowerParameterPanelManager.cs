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
			//stat.GetType().GetField("value").SetValue(stat, 2);
			this.eventSystem = eventSystem;
			this.powerPanel = powerPanel;

			transform.FindChild("Parameter Name").GetComponent<Text>().text = stat.name;
						
			Type genericStatType = stat.GetType().GetGenericArguments()[0];
			GameObject g = Instantiate(parameterSetterPanelPrefab, transform) as GameObject;

			if (genericStatType == typeof(int))
			{				
				parameterSetterPanelManager =  g.AddComponent<IntParameterSetterPanelManager>();				
				parameterSetterPanelManager.Initialize(stat, 1, 10, 1);
			}
			else if (genericStatType == typeof(bool))
			{

			}
			else if (genericStatType.IsEnum)
			{				
				parameterSetterPanelManager = g.AddComponent<EnumParameterSetterPanelManager>();
				((EnumParameterSetterPanelManager)parameterSetterPanelManager).Initialize(stat, genericStatType);
			}
			else if (genericStatType == typeof(Effect))
			{			
				parameterSetterPanelManager = g.AddComponent<EffectParameterSetterPanelManager>();
				((EffectParameterSetterPanelManager)parameterSetterPanelManager).Initialize(stat);
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
			}
		}	
	}
}