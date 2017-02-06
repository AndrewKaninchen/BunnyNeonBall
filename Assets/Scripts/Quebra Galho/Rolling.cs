using UnityEngine;
using System.Collections;

namespace Utilities
{
	public class Rolling : MonoBehaviour
	{
		public float
			rollSpeed;

		private Rigidbody2D rb;

		// Use this for initialization
		void Start()
		{
			rb = GetComponent<Rigidbody2D>();
		}

		// Update is called once per frame
		void Update()
		{
			float h = Input.GetAxis("Horizontal1");
			rb.AddForce(new Vector2(h * rollSpeed * Time.deltaTime, 0));
		}
	}
}