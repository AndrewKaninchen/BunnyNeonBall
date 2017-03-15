using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Utilities;

namespace PowerSystem.UI
{
	public class EffectParameterSetterPanelManager : ParameterSetterPanelManager, ISubmitHandler, ICancelHandler
	{
		private EffectClassListPanelManager effectClassListPanelManager;		
		private PowerListPanelManager powerListPanelManager;
		private PowerParameterPanelManager powerParameterPanelManager;
		private GameObject effectParametersListPanel;

		private EffectCreator effectCreator;
		private GameObject parameterPanelPrefab;

		public override int ParameterValue { get { return currentValue; } set { currentValue = Mathf.Clamp(value, minValue, maxValue); UpdateText(); UpdateCreatorStat(); } }

		public void Initialize(MyEventSystem eventSystem, Stat stat, PowerParameterPanelManager powerParameterPanelManager, 
			EffectClassListPanelManager effectClassListPanelManager, PowerListPanelManager powerListPanelManager, GameObject effectParametersListPanel, GameObject parameterPanelPrefab)
		{
			base.Initialize(eventSystem, stat);
			this.effectParametersListPanel = effectParametersListPanel;
			this.powerParameterPanelManager = powerParameterPanelManager;
			this.powerListPanelManager = powerListPanelManager;			
			this.effectClassListPanelManager = effectClassListPanelManager;
			this.parameterPanelPrefab = parameterPanelPrefab;
			minValue = 0;
			maxValue = Manager.effectTypes.Count-1;			
			ParameterValue = 0;
		}
				
		public new void UpdateText()
		{
			if (isInitialized)
			{				
				valueText.text = Manager.effectTypes[currentValue].GetField("name").GetValue(null) as string;
			}
		}

		public new void UpdateCreatorStat()
		{
			stat.GetType().GetField("value").SetValue(stat, ScriptableObject.CreateInstance(Manager.effectTypes[currentValue]));

			effectCreator = (EffectCreator) Activator.CreateInstance(Manager.effectCreatorTypes[currentValue]);

			for (int i = 0; i < effectParametersListPanel.transform.childCount; i++)
			{				
				Destroy(effectParametersListPanel.transform.GetChild(i).gameObject);				
			}
			effectParametersListPanel.transform.DetachChildren();
			foreach (Stat s in effectCreator.stats)
			{
				GameObject parameterPanel = Instantiate(parameterPanelPrefab, effectParametersListPanel.transform) as GameObject;				
				parameterPanel.GetComponent<PowerParameterPanelManager>().Initialize(eventSystem, null, s);
				//.Add(parameterPanel);
			}

			Navigation nav = new Navigation();
			Selectable firstParSel = effectParametersListPanel.transform.GetChild(0).GetComponent<Selectable>();
			nav.mode = Navigation.Mode.Explicit;
			nav.selectOnDown = firstParSel;
			GetComponent<Selectable>().navigation = nav;

			firstParSel.GetComponent<HierarchyNavigationElement>().overridePreviousTarget = true;
			firstParSel.GetComponent<HierarchyNavigationElement>().previousTarget = GetComponent<Selectable>();			
		}

		public void OnSubmit(BaseEventData eventData)
		{
			effectClassListPanelManager.gameObject.SetActive(true);
			eventSystem.SetSelectedGameObject(effectClassListPanelManager.effectClassPanels[0]);
			effectClassListPanelManager.GetComponentInChildren<HierarchyNavigationGroup>().childrenExitTarget = GetComponentInParent<Selectable>();
			powerListPanelManager.gameObject.SetActive(false);
		}		
	}
}