using UnityEngine;
using System.Collections;

namespace PowerSystem.Powers
{
	public class Beam : MonoBehaviour
	{
		[HideInInspector]
		public Character owner;
		[HideInInspector]
		public RangedAttackBeamPowerController controller;
		[HideInInspector]
		public int playerID;
		[HideInInspector]
		public float range;
		[HideInInspector]
		public float maxWidth;
		[HideInInspector]
		public Vector2 direction;
		[HideInInspector]
		public Effect effect;
		[HideInInspector]
		public LineRenderer lineRenderer;
		public GameObject particles;

		private bool hasAppliedEffect;
		[HideInInspector]
		public float remainingTime;
		[HideInInspector]
		public float totalTime;

		public void Initialize(Character character, RangedAttackBeamPowerController controller, float range, Effect effect, Vector2 direction)
		{
			lineRenderer = GetComponent<LineRenderer>();
			lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

			totalTime = .2f;
			remainingTime = totalTime;
			this.range = range;
			this.effect = effect;
			this.direction = direction;
			this.controller = controller;

			owner = character;
			playerID = character.playerID;
			hasAppliedEffect = false;

			Vector2 point1 = transform.position;
			Vector2 point2 = GetTarget();

			lineRenderer.SetPosition(0, new Vector3(point1.x, point1.y, -5f));
			lineRenderer.SetPosition(1, new Vector3(point2.x, point2.y, -5f));

			lineRenderer.startWidth = maxWidth;
			lineRenderer.endWidth = maxWidth / 2;			

			playerID = character.playerID;
			lineRenderer.material = character.coreMaterial;
		}

		void Update()
		{

			direction = controller.GetFireDirection(controller.powerInfo.direction);
			Vector2 point1 = transform.position;
			Vector2 point2 = GetTarget();

			Debug.DrawLine(point1, point2, Color.blue);

			lineRenderer.SetPosition(0, new Vector3(point1.x, point1.y, -5f));
			lineRenderer.SetPosition(1, new Vector3(point2.x, point2.y, -5f));



			Destroy(Instantiate(particles, new Vector3(point2.x, point2.y, -5f), particles.transform.rotation), .5f);


			remainingTime -= Time.deltaTime;
			if (remainingTime > 0)
			{								
				float width = maxWidth * remainingTime / totalTime;
				lineRenderer.startWidth = width;
				lineRenderer.endWidth = width / 2;
			}
			else
				Destroy(gameObject);
		}

		Vector2 GetTarget()
		{
			RaycastHit2D[] hits;
			hits = Physics2D.RaycastAll(transform.position, direction, range);
			if (hits.Length > 0)
			{
				foreach (RaycastHit2D h in hits)
				{
					//Debug.Log(h.collider.gameObject);
					if (h.collider.tag == "Player")
					{
						Character character = h.collider.GetComponent<Character>();
						if (character != null)
						{
							if (character.playerID == playerID)
								continue;
							if (!hasAppliedEffect) { effect.Trigger(target: h.collider.gameObject, perpetrator: owner.gameObject, additionalParameters: direction); hasAppliedEffect = true; }

							return h.point;
						}
						else
						{
							continue;
						}
					}
					else
					{
						if (!hasAppliedEffect) { effect.Trigger(target : h.collider.gameObject, perpetrator: owner.gameObject, additionalParameters: direction); hasAppliedEffect = true; }
						return h.point;
					}
				}
			}
			hasAppliedEffect = true;
			return new Vector2(transform.position.x + direction.x * range, transform.position.y + direction.y * range);
		}
	}
}