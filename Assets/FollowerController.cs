using UnityEngine;
using System.Collections;

public class FollowerController : MonoBehaviour {

	// Reference for the FirePlace
	public GameObject fire;
	Animator animator;

	void Awake(){
		// Setting the fireplace reference
		fire = GameObject.Find ("Fire_Color");
		animator = GetComponent<Animator>();

		if (Random.Range (0, 10) > 5) {
			animator.SetTrigger ("Change");
		} else {
			animator.SetTrigger ("Go");
		}
	}

	// Update is called once per frame
	void Update () {

		// Rotation for followers
		transform.RotateAround (fire.transform.position,Vector3.up + Vector3.back / 2,Time.deltaTime * 95);

		// Angle adjustment for the followers
		transform.LookAt (new Vector3(transform.position.x,Camera.main.transform.position.y,Camera.main.transform.position.z));
	}

	void OnMouseDown(){
		// When clicked to the Follower, informs the InputController Class
		InputController.selectedFucker = this.gameObject;
	}

	public void ChangeAnimation(){
		if (Random.Range (0, 10) > 5) {
			animator.SetTrigger("Change");
		}
	}
}// Class End
