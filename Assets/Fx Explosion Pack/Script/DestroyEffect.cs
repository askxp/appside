using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class DestroyEffect : MonoBehaviour
{
	private SphereCollider _collider;
	[SerializeField] private float explosionRadius = 5;
	[SerializeField] private float explosionTime = 1;
	private bool _exploding = false;
	
	private void Awake()
	{
		_collider = GetComponent<SphereCollider>();
	}

	private void OnEnable()
	{
		StartCoroutine(nameof(DoExplosion));
	}
	

	private IEnumerator DoExplosion()
	{
		_exploding = true;	
		yield return new WaitForSeconds(explosionTime);
		_exploding = false;
		_collider.radius = 0;
		gameObject.SetActive(false);
	}

	private void Update()
	{
		if (!_exploding) return;
		_collider.radius += (explosionRadius / explosionTime) * Time.deltaTime;
	}
}
