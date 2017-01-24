using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Utilities;

namespace PowerSystem.UI
{
	public class PowerParameterPanelManager : MonoBehaviour, IMoveHandler, ICancelHandler
	{
		private MyEventSystem eventSystem;
		private EventTrigger eventTrigger;
		private GameObject powerPanel;

		public GameObject parameterSetterPanelPrefab;
        private ParameterSetterPanelManager parameterSetterPanelManager;

		public void Initialize (MyEventSystem eventSystem, GameObject powerPanel, Stat parameter)
		{
			this.eventSystem = eventSystem;
			this.powerPanel = powerPanel;

			transform.FindChild("Parameter Name").GetComponent<Text>().text = parameter.name;
			parameterSetterPanelManager = (Instantiate(parameterSetterPanelPrefab, transform) as GameObject).GetComponent<ParameterSetterPanelManager>();
			
			parameterSetterPanelManager.Initialize(1, 10, 1);
		}

		public void OnCancel(BaseEventData eventData)
		{
			//Debug.Log("oi");
			powerPanel.GetComponent<Toggle>().isOn = false;
			eventSystem.SetSelectedGameObject(powerPanel, eventData);
		}

		public void OnMove (AxisEventData eventData)
		{
			Debug.Log(this + "  OnMove");
			parameterSetterPanelManager.ParameterValue += 
			(
				eventData.moveDir == MoveDirection.Right? 1:
				eventData.moveDir == MoveDirection.Left? -1:
				0
			);
		}
	}
}