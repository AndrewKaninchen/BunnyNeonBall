using UnityEngine;
using System.Collections;

namespace PowerSystem.Powers
{
	public class RangedAttackBeamPowerController : RangedAttackPowerController
	{
		public RangedAttackBeamPower powerInfo;
		public GameObject beamPrefab;

		private bool locked;

		// Use this for initialization
		void Start()
		{
			character = GetComponent<Character>();
			locked = false;
		}

		// Update is called once per frame
		void Update()
		{
			if (!locked && Input.GetButtonDown(powerInfo.activationKey + character.playerID))
			{
				Shoot();
				StartCoroutine(Lock());
			}
		}

		void Shoot()
		{
			Vector2 direction = GetFireDirection(powerInfo.direction);

			GameObject beamInstance = Instantiate(beamPrefab);
			beamInstance.transform.position = transform.position;
			beamInstance.transform.SetParent(transform);

			Beam beamController = beamInstance.GetComponent<Beam>();
			beamController.Initialize(character, this, powerInfo.range, powerInfo.effect, direction);
		}

		IEnumerator Lock()
		{
			locked = true;
			yield return new WaitForSeconds(powerInfo.rateOfFire);
			locked = false;
		}
	}
}