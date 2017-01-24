using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Reflection;
using System;
using UnityEngine.EventSystems;

namespace PowerSystem.UI
{
	public class AddNewPowerDropdownManager : MonoBehaviour, ICancelHandler, ISubmitHandler
	{
		private AddNewPowerPanelManager addNewPowerPanelManager;
		public Dropdown dropdown;

		public void Initialize(AddNewPowerPanelManager addNewPowerPanelManager)
		{
			
			this.addNewPowerPanelManager = addNewPowerPanelManager;
			dropdown = GetComponent<Dropdown>();

			List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

			foreach (Type t in Manager.powerCreatorTypes)
			{
				Dropdown.OptionData od = new Dropdown.OptionData();
				FieldInfo fi = t.GetField("powerClassName");
				od.text = (string)fi.GetValue(null);

				options.Add(od);
			}

			dropdown.options = options;

		}

		public void OnCancel(BaseEventData eventData)
		{
			addNewPowerPanelManager.OnDropDownCancel();
		}

		public void OnSubmit(BaseEventData eventData)
		{
			//addNewPowerPanelManager.OnDropdownSubmit();
		}
	}
}