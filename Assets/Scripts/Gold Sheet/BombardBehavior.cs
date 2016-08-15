using UnityEngine;
using System.Collections;

public class BombardBehavior : MonoBehaviour {

	Rigidbody rb;
	public float bounceForce;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.z > 1)
			Destroy (transform.gameObject);
	}

	public void OnCollisionEnter(Collision other){
		Debug.Log ("collision");
		if (other.gameObject.layer == 9) {
			Debug.Log ("Correct layer");
//			rb = gameObject.AddComponent<Rigidbody> ();
//			rb.useGravity = false;
//			transform.parent = transform.root;
			rb = GetComponent<Rigidbody> ();
			rb.velocity = Vector3.zero;
			rb.AddForce (Vector3.back * bounceForce);
		}
	}
}
