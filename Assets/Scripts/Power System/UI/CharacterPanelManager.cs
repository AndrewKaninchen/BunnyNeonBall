using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using Utilities;

namespace PowerSystem.UI
{
	[RequireComponent (typeof (MyEventSystem))]
	[RequireComponent(typeof(HierarchyNavigationGroup))]
	[RequireComponent(typeof(StandaloneInputModule))]
	public class CharacterPanelManager : MonoBehaviour
	{
		[Range(1, 4)]
		public int playerID;

		public MyEventSystem eventSystem;
		private MyInputModule inputModule;
		private HierarchyNavigationGroup navGroup;

		[HideInInspector]	public bool isInitialized = false;

		[SerializeField]	private GameObject namePanel;
		[SerializeField]	private GameObject joinPanel;

		[SerializeField]	private GameObject previewPanelPrefab;
		[SerializeField]	private GameObject powerListPanelPrefab;
		[SerializeField]	private GameObject availableActionsPanelPrefab;
		[SerializeField]	private GameObject powerClassListPanelPrefab;

		[HideInInspector]	public GameObject previewPanel;
		[HideInInspector]	public GameObject powerListPanel;
		[HideInInspector]	public GameObject availableActionsPanel;		
		[HideInInspector]	public GameObject powerClassListPanel;

		public void Initialize(int playerID)
		{
			this.playerID = playerID;
			eventSystem = gameObject.GetComponent<MyEventSystem>();
			inputModule = gameObject.GetComponent<MyInputModule>();
			navGroup = gameObject.GetComponent<HierarchyNavigationGroup>();

			eventSystem.SetSelectedGameObject(namePanel);
			eventSystem.inputModule = inputModule;

			inputModule.verticalAxis = "D-Pad Vertical" + playerID;
			inputModule.horizontalAxis = "D-Pad Horizontal" + playerID;
			inputModule.cancelButton = "Cancel" + playerID;
			inputModule.submitButton = "Submit" + playerID;
			inputModule.deleteButton = "X" + playerID;
			inputModule.enabled = true;			

			navGroup.mode = HierarchyNavigationGroup.Mode.Vertical;
			navGroup.eventSystem = eventSystem;
			navGroup.childrenExitTarget = null;

			joinPanel.SetActive(false);

			previewPanel = Instantiate(previewPanelPrefab);
			previewPanel.transform.SetParent(transform);

			powerListPanel = Instantiate(powerListPanelPrefab);
			powerListPanel.transform.SetParent(transform);

			powerClassListPanel = Instantiate(powerClassListPanelPrefab);
			powerClassListPanel.transform.SetParent(transform);

			availableActionsPanel = Instantiate(availableActionsPanelPrefab);
			availableActionsPanel.transform.SetParent(transform);

			namePanel.GetComponent<NamePanelManager>().Initialize(this);
			previewPanel.GetComponent<PreviewPanelManager>().Initialize(this);
			powerListPanel.GetComponent<PowerListPanelManager>().Initialize(this);
			powerClassListPanel.GetComponent<PowerClassListPanelManager>().Initialize(this);			

			isInitialized = true;
		}		
	}
}