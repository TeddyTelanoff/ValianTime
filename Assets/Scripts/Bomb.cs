using System;
using System.Collections;

using UnityEngine;

public class Bomb: MonoBehaviour
{
	public float tickTime;
	public float force;
	public float radius;
	
	void Start() {
		StartCoroutine(Tick());

		IEnumerator Tick() {
			yield return new WaitForSeconds(tickTime);

			Explode();
		}
	}

	void OnCollisionEnter2D(Collision2D hit) {
		if (hit.collider.CompareTag("Player"))
			Explode();
	}

	public void Explode() {
		var impacts = Physics2D.OverlapCircleAll(transform.position, radius);

		foreach (Collider2D impact in impacts)
		{
			var rb = impact.GetComponentInParent<Rigidbody2D>();
			if (rb)
				rb.AddExplosionForce(force, transform.position, radius);
		}

		Destroy(gameObject);
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
}
