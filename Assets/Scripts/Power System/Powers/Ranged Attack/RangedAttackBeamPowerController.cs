using UnityEngine;
using System.Collections;

namespace PowerSystem.Powers
{
	public class RangedAttackBeamPowerController : RangedAttackPowerController <RangedAttackBeamPower>
	{
		public GameObject beamPrefab;

		private bool locked;

		void OnEnable()
		{
			beamPrefab = Resources.Load("Prefabs/Beam", typeof(GameObject)) as GameObject;
			character = GetComponent<Character>();
			locked = false;			
		}

		void Update()
		{
			if (!locked && Input.GetButtonDown(powerInstance.activationKey + character.playerID))
			{
				Shoot();
				StartCoroutine(Lock());
			}
		}

		void Shoot()
		{
			Vector2 direction = GetFireDirection(powerInstance.direction);

			GameObject beamInstance = Instantiate(beamPrefab);
			beamInstance.transform.position = transform.position;
			beamInstance.transform.SetParent(transform);

			Beam beamController = beamInstance.GetComponent<Beam>();
			beamController.Initialize(character, this, powerInstance.range, powerInstance.effect, direction);
		}

		IEnumerator Lock()
		{
			locked = true;
			yield return new WaitForSeconds(powerInstance.rateOfFire);
			locked = false;
		}
	}
}