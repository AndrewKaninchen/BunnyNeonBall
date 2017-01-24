using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PowerSystem.UI
{
	public class MainPanelManager : MonoBehaviour
	{
		[SerializeField]
		private GameObject panel12, panel34, characterPanelPrefab;
		
		private List<GameObject> characterPanels = new List<GameObject>();

		public int InitializedCharacterCount { get { return initializedCharacters; } }
		public int initializedCharacters = 0;
		

		void Start()
		{
			Manager.Initialize();
			characterPanels.Add(panel12.transform.GetChild(0).gameObject);
			AddPlayer();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Keypad0))
			{
				AddPlayer();
			}
		}

		public void AddPlayer()
		{
			if (initializedCharacters < 4)
			{
				characterPanels[characterPanels.Count - 1].GetComponent<CharacterPanelManager>().Initialize(characterPanels.Count);
				initializedCharacters++;

				if (initializedCharacters < 4)
				{
					GameObject c = Instantiate(characterPanelPrefab);
					characterPanels.Add(c);

					if (characterPanels.Count >= 3 && Camera.main.aspect < 16f/9f)
					{
						c.transform.SetParent(panel34.transform);
						panel34.SetActive(true);
					}
					else
						c.transform.SetParent(panel12.transform);
				}
			}
		}
	}
}
