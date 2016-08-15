using UnityEngine;
using System.Collections;

public class BasicUnit : MonoBehaviour {

	Rigidbody rb;
	Camera cam;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = 10;
			Debug.Log (cam.ScreenToWorldPoint (mousePos));
			Vector3 heading = (cam.ScreenToWorldPoint (mousePos) - transform.position).normalized;
			rb.velocity = heading * 10f;
		}	
	}
}
