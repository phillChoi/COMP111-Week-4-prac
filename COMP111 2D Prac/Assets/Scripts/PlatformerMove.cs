using UnityEngine;
using System.Collections;

/**
 * A script that implements simple platformer controls: walking and jumping 
 */

// require that the object has a Rigidbody2D component
[RequireComponent (typeof (Rigidbody2D))]
public class PlatformerMove : MonoBehaviour {

	public float walkSpeed = 5f;
	public float jumpSpeed = 5f;
	public bool walkOnAir = false;
	private Rigidbody2D _rigidbody2D;

	private bool facingRight = true;
	private bool onGround;
	public Rect groundRect = new Rect(-0.32f, -0.72f, 0.56f, 0.1f);
	public LayerMask groundLayerMask = -1;

	void Start() {
		// record a reference to the rigidbody component to use later
		_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
	}
	
	void Update () {
	
		// test if the avatar is on the ground
		CheckOnGround();

		// get the current velocity
		Vector2 v = _rigidbody2D.velocity;

		// if we're on the ground or if in-air walking is allowed, set the horizontal velocity to match the "Horizontal" input axis (scaled by walkSpeed)
		if (onGround || walkOnAir) {
			v.x = walkSpeed * Input.GetAxis("Horizontal");
		}

		// if we're on the ground and the Jump button is pressed, set the vertical velocity to equal the jump speed
		if (onGround && Input.GetButtonDown("Jump")) {
			v.y = jumpSpeed;
		}

		// set the velocity to the new value
		_rigidbody2D.velocity = v;

		// if the horizontal velocity is opposite to the direction the sprite is facing, turn around
		if (v.x < 0 && facingRight || v.x > 0 && !facingRight) {
			facingRight = !facingRight;
			Vector3 s = transform.localScale;
			s.x = -s.x;
			transform.localScale = s;

			// adjust the groundRect position
			groundRect.x = - groundRect.max.x;
		}

	}

	private void CheckOnGround() {
		// check if we are on the group by testing if there is anything inside the rectangle given by the groundRect
		// only objects in the groundLayerMask are checked

		onGround = false;

		Vector2 min = new Vector2(transform.position.x, transform.position.y) + groundRect.min;
		Vector2 max = new Vector2(transform.position.x, transform.position.y) + groundRect.max;

		Collider2D collider = Physics2D.OverlapArea(min, max, groundLayerMask);

		onGround = collider != null;

	}

	void OnDrawGizmos() {
		// draw a box showing the groundRect. It is red if the avatar is on the ground and white otherwise.

		Vector3 centre = transform.position;
		centre.x += groundRect.center.x;
		centre.y += groundRect.center.y;

		Vector3 size = Vector3.zero;
		size.x += groundRect.width;
		size.y += groundRect.height;

		if (onGround) {
			Gizmos.color = Color.red;
		}
		else {
			Gizmos.color = Color.white;
		}
		Gizmos.DrawWireCube(centre, size);

	}

}
