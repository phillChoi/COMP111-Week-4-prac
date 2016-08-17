using UnityEngine;
using System.Collections;

// make sure the object has a Rigidbody2D component
[RequireComponent (typeof (Rigidbody2D))]
public class DragObject2D : MonoBehaviour {

	private Rigidbody2D _rigidbody;
	private bool isDragging = false;
	private Plane plane;

	void Start () {
		// Check there is a tagged main camera available.

		if (Camera.main == null) {
			Debug.LogError(this.name + ": There is no camera with the MainCamera tag.");
		}

		// keep a reference to the rigidbody component
		_rigidbody = gameObject.GetComponent<Rigidbody2D>();

		// Sprites are assumed to be in the X-Y plane
		plane = new Plane(Vector3.forward, Vector3.zero);
	}
	
	void Update () {

		// if there is no main camera, do nothing, to avoid extra error messages
		if (Camera.main == null) return;

		// if we are dragging the object, move it to the mouse
		if (isDragging) {
			// compute new position by casting a ray from the mouse through the X-Y plane 
			Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
			float d = 0;
			if (plane.Raycast(r, out d)) {
				Vector3 pos = r.GetPoint(d);
				_rigidbody.MovePosition(pos);
			}
		}
	}

	void OnMouseDown() {
		// the object has been clicked, start dragging it
		isDragging = true;
	}

	void OnMouseUp() {
		// the object has been released, stop dragging it
		isDragging = false;
	}

}
