using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using Utilities;

namespace PowerSystem.UI
{

	public class IntParameterSetterPanelManager : ParameterSetterPanelManager
	{
		public void Initialize(MyEventSystem eventSystem, Stat stat, int minValue = 1, int maxValue = 3, int startingValue = 1)
		{
			base.Initialize(eventSystem, stat);		
			this.minValue = minValue;
			this.maxValue = maxValue;
			this.stat = stat;			
			ParameterValue = startingValue;			
		}		
	}
}