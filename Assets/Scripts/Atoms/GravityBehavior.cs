using UnityEngine;
using System.Collections;

public class GravityBehavior : MonoBehaviour {

	public GameObject AttractionPoint;
	public float strength;
	private Rigidbody rb;
	private int settledFrames;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		settledFrames = 100;
	}
	
	void Update () {
		Vector3 offset = AttractionPoint.transform.position - transform.position;
		float sqrmag = offset.sqrMagnitude;
		if (sqrmag > 0.0001f)
			rb.AddForce (strength * offset.normalized / sqrmag, ForceMode.Force);
		if (settledFrames != 0) {
			settledFrames--;
		}
	}

	public void OnCollisionStay (Collision other)
	{
		FixedJoint fj = GetComponent<FixedJoint> ();
		if (settledFrames == 0 && fj == null) {
			fj = this.gameObject.AddComponent<FixedJoint> ();
			fj.anchor = other.contacts [0].point;
			fj.connectedBody = other.rigidbody;
			rb.isKinematic = true;
			other.rigidbody.isKinematic = true;
		}
	}
}
