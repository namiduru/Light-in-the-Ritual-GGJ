using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowerSpawner : MonoBehaviour {

	// FirePlace gameObject
	public GameObject fire;
	// Amount of followers to spawn
	public static int Amount;
	public static int success;

	// Reference position that is found from Scene
	public static Vector3 refPos = new Vector3(-6f,0f,0f);

	public static List<GameObject> Followers;

	public static FollowerSpawner instance;

	void Awake(){

		instance = this;

		Followers = new List<GameObject> ();

		// Setting the amount
		Amount = 3;
		success = 0;

		// Setting the reference for the fire object
		fire = GameObject.Find ("Fire_Color");

		// Function creating the followers
		CreateFollowers (Amount);
		StartCoroutine (MoveFollowers());
		refPos = new Vector3(-2f,0f,0f);
	}

	IEnumerator MoveFollowers(){
		while (Vector3.Distance(Followers[0].transform.position,fire.transform.position) > 3f) {
			for (int i=0; i<Followers.Count; i++) {
				Followers [i].transform.Translate ((fire.transform.position - Followers [i].transform.position).normalized * Time.deltaTime * 2,Space.World);
			}

			yield return null;
		}
	}

	public void CreateFollowers(int p_amount){
		// Calculation the space needed between each follower
		int space = 360 / p_amount;

		while (p_amount > 0) {
			// Create followers
			GameObject temp = Instantiate (Resources.Load("Follower")as GameObject,refPos,Quaternion.identity)as GameObject;
			temp.transform.RotateAround(fire.transform.position,Vector3.up + Vector3.back / 2,space * p_amount);
			Followers.Add(temp);
			p_amount--;
		}
	}

	public void Refresh(int p_int){
		Amount = p_int;
		for (int i=0; i<Followers.Count; i++) {
			Destroy(Followers[i]);
		}

		Followers.Clear ();

		CreateFollowers (Amount);
	}
}// Class End
