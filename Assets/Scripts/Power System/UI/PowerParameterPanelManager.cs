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


		public GameObject parameterSetterPanelPrefab;
        //private IntParameterSetterPanelManager parameterSetterPanelManager;

		public void Initialize (MyEventSystem eventSystem, GameObject powerPanel, Stat parameter)
		{
			this.eventSystem = eventSystem;
			this.powerPanel = powerPanel;

			transform.FindChild("Parameter Name").GetComponent<Text>().text = parameter.name;
						
			Type genericParameterType = parameter.GetType().GetGenericArguments()[0];			

			if (genericParameterType == typeof(int))
			{
				GameObject g = Instantiate(parameterSetterPanelPrefab, transform) as GameObject;
				parameterSetterPanelManager =  g.AddComponent<IntParameterSetterPanelManager>();				
				parameterSetterPanelManager.Initialize(1, 10, 1);
			}
			else if (genericParameterType == typeof(bool))
			{

			}
			else if (genericParameterType.IsEnum)
			{
				GameObject g = Instantiate(parameterSetterPanelPrefab, transform) as GameObject;
				parameterSetterPanelManager = g.AddComponent<EnumParameterSetterPanelManager>();
				((EnumParameterSetterPanelManager)parameterSetterPanelManager).Initialize(genericParameterType);
			}
			else if (genericParameterType == typeof(Effect))
			{

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