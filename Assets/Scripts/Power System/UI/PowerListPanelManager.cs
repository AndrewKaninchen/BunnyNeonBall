using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;
using Utilities;
using PowerSystem.Powers;

namespace PowerSystem.UI
{
	public class PowerListPanelManager : MonoBehaviour, ISubmitHandler
	{
		[HideInInspector]
		public CharacterPanelManager characterPanelManager;
		[HideInInspector]
		public MyEventSystem eventSystem;
		[SerializeField]
		private GameObject addNewPowerPanel;

		public List<GameObject> powerPanels;
		[SerializeField]private GameObject powerPanelPrefab;
		[SerializeField]private GameObject addNewPowerPanelPrefab;

		private ScrollRect scrollRect;

		public void Initialize(CharacterPanelManager characterPanelManager)
		{
			this.characterPanelManager = characterPanelManager;
			eventSystem = characterPanelManager.eventSystem;
			
			addNewPowerPanel = Instantiate(addNewPowerPanelPrefab);
			addNewPowerPanel.transform.SetParent(transform.GetChild(0)); //Bug maroto que faz o treco não aparecer me forçou a usar o jeito errado		

			addNewPowerPanel.GetComponent<AddNewPowerPanelManager2>().Initialize(this, characterPanelManager);
			powerPanels = new List<GameObject>();
			scrollRect = GetComponent<ScrollRect>();			
		}

		public GameObject AddPower<PowerInfoGeneratorType, PowerInfoType>() where PowerInfoType : Power where PowerInfoGeneratorType : PowerCreator<PowerInfoType>, new()
		{
			GameObject g = Instantiate(powerPanelPrefab);
			g.transform.SetParent(transform.GetChild(0)); //Bug maroto que faz o treco não aparecer me forçou a usar o jeito errado
			
			
			PowerInfoGeneratorType powerGenerator = new PowerInfoGeneratorType();
			g.GetComponent<PowerPanelManager>().Initialize(powerGenerator, this);
			addNewPowerPanel.transform.SetAsLastSibling();
			powerPanels.Add(g);
			g.SetActive(true);

			//Debug.Log(powerGenerator.GetType().Name);
					
			return g;
		}	

		public void OnSubmit(BaseEventData eventData)
		{
			eventSystem.SetSelectedGameObject(transform.GetChild(0).GetChild(0).gameObject);
			scrollRect.verticalNormalizedPosition = 1;
		}

		public void OnDelete()
		{

		}

		void Update()
		{
			//if(Input.GetButtonDown("Delete"))
			//{

			//}
		}
	}
}