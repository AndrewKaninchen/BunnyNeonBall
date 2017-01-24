using UnityEngine;
using System.Collections;


namespace Utilities
{

	public class Spawner : MonoBehaviour
	{
		public GameObject[] prefabs;

		public void Spawn(int id)
		{
			GameObject instance = Instantiate(prefabs[id]);
			instance.transform.position = transform.position;
		}
	}
}