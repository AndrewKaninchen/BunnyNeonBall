using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace PowerSystem
{
	public class Character : MonoBehaviour
	{
		public Material coreMaterial;
		public Material ringMaterial;
		public Sprite coreShape;


		public static List<Material> neonMaterials = new List<Material>();
		public int CoreMaterial { get { return neonMaterials.IndexOf(coreMaterial); } set { coreMaterial = neonMaterials[value]; core.GetComponent<Renderer>().material = coreMaterial; } }
		public int RingMaterial { get { return neonMaterials.IndexOf(ringMaterial); } set { ringMaterial = neonMaterials[value]; ring.GetComponent<Renderer>().material = ringMaterial; } }


		public static List<Sprite> coreShapes = new List<Sprite>();
		public int CoreShape { get { return coreShapes.IndexOf(coreShape); } set { coreShape = coreShapes[value]; core.GetComponent<SpriteRenderer>().sprite = coreShape; } }

		public float maxHealth;
		public float curHealth;
		public int playerID;

		public GameObject coreDeath;
		public GameObject ringDeath;

		private bool grounded;
		private bool wasGrounded;

		public bool IsGrounded { get { return grounded; } }
		public bool WasGrounded { get { return wasGrounded; } }

		private GameObject core;
		private GameObject ring;

		void Awake()
		{
			if (neonMaterials.Count == 0)
			{
				neonMaterials.Add((Material)Resources.Load("Materials/Glowing/Red", typeof(Material)));
				neonMaterials.Add((Material)Resources.Load("Materials/Glowing/Green", typeof(Material)));
				neonMaterials.Add((Material)Resources.Load("Materials/Glowing/Blue", typeof(Material)));
				neonMaterials.Add((Material)Resources.Load("Materials/Glowing/Magenta", typeof(Material)));
				neonMaterials.Add((Material)Resources.Load("Materials/Glowing/Cyan", typeof(Material)));
				neonMaterials.Add((Material)Resources.Load("Materials/Glowing/Yellow", typeof(Material)));
				neonMaterials.Add((Material)Resources.Load("Materials/Glowing/White", typeof(Material)));

			}

			if (coreShapes.Count == 0)
			{
				coreShapes.Add((Sprite)Resources.Load("Sprites/Diamond", typeof(Sprite)));
				coreShapes.Add((Sprite)Resources.Load("Sprites/Hexagon", typeof(Sprite)));
				coreShapes.Add((Sprite)Resources.Load("Sprites/Triangle", typeof(Sprite)));
				coreShapes.Add((Sprite)Resources.Load("Sprites/Circle", typeof(Sprite)));
				//coreShapes.Add((Sprite)Resources.Load("Sprites/Bunny.png", typeof(Sprite)));
			}
			//Debug.Log(neonMaterials);
			//Debug.Log(neonMaterials.Count);
			//foreach (object o in neonMaterials)
			//{
			//	Debug.Log(o);
			//}
		}

		// Use this for initialization
		void Start()
		{
			core = transform.Find("Core").gameObject;
			ring = transform.Find("Ring").gameObject;
			SpriteRenderer coreRenderer = core.GetComponent<SpriteRenderer>();
			SpriteRenderer ringRenderer = ring.GetComponent<SpriteRenderer>();
			coreRenderer.material = coreMaterial;
			coreRenderer.sprite = coreShape;
			ringRenderer.material = ringMaterial;

			grounded = CheckForGround();

		}

		void FixedUpdate()
		{
			wasGrounded = grounded;
			grounded = CheckForGround();
		}

		// Update is called once per frame
		void Update()
		{

		}

		public bool CheckForGround()
		{
			Vector2 start = new Vector2(transform.position.x, transform.position.y - 0.51f);

			if (Physics2D.Raycast(start, Vector3.down, 0.2f))
			{
				Debug.DrawRay(start, Vector2.down * 0.2f, Color.blue);
				return true;
			}
			Debug.DrawRay(start, Vector2.down * 0.2f, Color.red);
			return false;
		}

		public void Damage(float damage)
		{
			curHealth -= damage;

			if (curHealth <= 0)
			{
				Die();
			}
			else
				Debug.Log("Player " + playerID + " HP = " + curHealth);
		}

		public void Die()
		{
			coreDeath.transform.parent = null;
			ringDeath.transform.parent = null;
			coreDeath.SetActive(true);
			ringDeath.SetActive(true);
			Destroy(coreDeath, 2f);
			Destroy(ringDeath, 2f);

			Debug.Log("Player " + playerID + " is DED!");
			Destroy(gameObject);
		}
	}
}