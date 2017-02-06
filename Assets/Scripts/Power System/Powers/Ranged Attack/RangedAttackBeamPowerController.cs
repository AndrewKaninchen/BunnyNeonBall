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
			if (!locked && Input.GetButtonDown(power.activationKey + character.playerID))
			{
				Shoot();
				StartCoroutine(Lock());
			}
		}

		void Shoot()
		{
			Vector2 direction = GetFireDirection(power.direction);

			GameObject beamInstance = Instantiate(beamPrefab);
			beamInstance.transform.position = transform.position;
			beamInstance.transform.SetParent(transform);

			Beam beamController = beamInstance.GetComponent<Beam>();
			beamController.Initialize(character, this, power.range, power.effect, direction);
		}

		IEnumerator Lock()
		{
			locked = true;
			yield return new WaitForSeconds(power.rateOfFire);
			locked = false;
		}
	}
}