using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	public float speed;
	public float timeToChange;
	public bool horizontal;
	public AudioClip hitSound;
	public AudioClip fixedSound;
	public GameObject smokeParticleEffect;
	public ParticleSystem fixedParticleEffect;
	Animator animator;
	AudioSource audioSource;
	Rigidbody2D rigidbody2d;
	float remainingTimeToChange;
	Vector2 direction = Vector2.right;
	bool repaired = false;
	void Start()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();
		remainingTimeToChange = timeToChange;
		direction = horizontal ? Vector2.right : Vector2.down;
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		if (repaired)
			return;
		remainingTimeToChange -= Time.deltaTime;
		if (remainingTimeToChange <= 0)
		{
			remainingTimeToChange += timeToChange;
			direction *= -1;
		}

		animator.SetFloat("ForwardX", direction.x);
		animator.SetFloat("ForwardY", direction.y);
	}

	void FixedUpdate()
	{
		rigidbody2d.MovePosition(rigidbody2d.position + direction * speed * Time.deltaTime);
	}

	void OnCollisionStay2D(Collision2D other)
	{
		if (repaired)
			return;
		RubyController controller = other.collider.GetComponent<RubyController>();
		if (controller != null)
			controller.ChangeHealth(-4);
	}

	public void Fix()
	{
		animator.SetTrigger("Fixed");
		repaired = true;
		smokeParticleEffect.SetActive(false);
		Instantiate(fixedParticleEffect, transform.position + Vector3.up * 0.5f, Quaternion.identity);
		rigidbody2d.simulated = false;
		audioSource.Stop();
		audioSource.PlayOneShot(hitSound);
		audioSource.PlayOneShot(fixedSound);
	}
}