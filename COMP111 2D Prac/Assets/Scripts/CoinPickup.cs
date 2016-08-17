using UnityEngine;
using System.Collections;

public class CoinPickup : MonoBehaviour {

	public LayerMask playerLayer;
	private AudioSource _audio;
	private bool isGone = false;

	public void Start() {
		// store a reference to the AudioSource component (if there is one)
		_audio = gameObject.GetComponent<AudioSource>();
	}

	public void OnTriggerEnter2D(Collider2D collider) {	
		// something has collided with the coin check if it is the player

		// if the coin has already been collected, do nothing
		if (isGone) return;

		// check if the colliding object is in the playerLayer
		if (((1 << collider.gameObject.layer) & playerLayer.value) != 0) {
			// play a sound if there is one
			if (_audio != null) {
				_audio.Play();
			}

			// hide the sprite and disable this script
			SpriteRenderer _renderer = gameObject.GetComponent<SpriteRenderer>();
			_renderer.enabled = false;
			isGone = true;
		}
	}

}
