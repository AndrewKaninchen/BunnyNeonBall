using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PowerSystem.UI
{
	public class PowerListPanelManager : MonoBehaviour, ISubmitHandler
	{
		MyEventSystem eventSystem;
		GameObject addNewPowerPanel;

		public List<GameObject> powerPanels;
		[SerializeField]private GameObject powerPanelPrefab;
		[SerializeField]private GameObject addNewPowerPanelPrefab;

		private ScrollRect scrollRect;

		public void Initialize(int playerID, MyEventSystem eventSystem)
		{
			this.eventSystem = eventSystem;

			addNewPowerPanel = Instantiate(addNewPowerPanelPrefab, transform.GetChild(0)) as GameObject;
			addNewPowerPanel.GetComponent<AddNewPowerPanelManager>().Initialize(this, eventSystem);

			powerPanels = new List<GameObject>();

			scrollRect = GetComponent<ScrollRect>();

			Navigation navi = new Navigation();
			navi.mode = Navigation.Mode.Explicit;
			addNewPowerPanel.GetComponent<Selectable>().navigation = navi;
		}

		public void AddPower()
		{
			JumpInfoGenerator jumpGen = new JumpInfoGenerator();
			AddPower(jumpGen);
		}

		public void AddPower<PowerInfoType>(PowerInfoGenerator<PowerInfoType> powerGenerator) where PowerInfoType : PowerInfo
		{
			GameObject g = Instantiate(powerPanelPrefab, transform.GetChild(0)) as GameObject;
			g.GetComponent<PowerPanelManager>().Initialize(powerGenerator, this);
			addNewPowerPanel.transform.SetAsLastSibling();
			powerPanels.Add(g);
			SetPowerNavigation(powerPanels.Count-1);		
		}

		public void SetPowerNavigation(int id)
		{
			Selectable sel = powerPanels[id].GetComponent<Selectable>();
			Navigation navi = new Navigation();
			navi.mode = Navigation.Mode.Explicit;

			if (id != 0)
			{
				Selectable sel2 = powerPanels[id - 1].GetComponent<Selectable>();
				Navigation navi2 = sel2.navigation;
				navi.selectOnUp = sel2;
				navi2.selectOnDown = sel;
				sel2.navigation = navi2;
			}

			Selectable selNewPower = addNewPowerPanel.GetComponent<Selectable>();
            Navigation naviNewPower = selNewPower.navigation;
			naviNewPower.selectOnUp = sel;
			selNewPower.navigation = naviNewPower;
			navi.selectOnDown = selNewPower;				
			sel.navigation = navi;
		}

		public void OnSubmit(BaseEventData eventData)
		{
			eventSystem.SetSelectedGameObject(transform.GetChild(0).GetChild(0).gameObject);
			scrollRect.verticalNormalizedPosition = 1;
		}
	}
}