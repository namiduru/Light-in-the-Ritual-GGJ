using UnityEngine;
using System.Collections.Generic;

public class InputController : MonoBehaviour {

	public static InputController instance;

	// List of selected followers
	public static List<GameObject> Fuckers;

	void Awake(){
		instance = this;

		Fuckers = new List<GameObject> ();
	}

	// Get Set for selected Follower
	public static GameObject selectedFucker {
		get {
			return p_selectedFucker;
		}
		
		set {
			if(Time.timeScale == 0)
				return;
			/*if(p_selectedFucker != null)
				p_selectedFucker.GetComponent<SpriteRenderer>().color = Color.white;*/
			
			bool exists = false;
			
			for (int i=0; i<Fuckers.Count; i++) {
				if (value == Fuckers [i]) {
					exists = true;
					break;
				}
			}


			
			if (!exists) {
				Fuckers.Add (value);

				if (p_selectedFucker != null) {
					if (p_selectedFucker.transform.childCount > 0) {
						Destroy (p_selectedFucker.transform.GetChild (0).gameObject);
					}
				}

				if (Fuckers.Count > 1)
					MoveCheck.ControlMove ();
				
				GameObject temp = Instantiate (Resources.Load ("ggj red"))as GameObject;
				temp.transform.SetParent (value.transform);
				temp.transform.localPosition = Vector3.zero;
				p_selectedFucker = value;
			}
		}
	}
	private static GameObject p_selectedFucker;


	public void Refresh(){
		for(int i =0;i<Fuckers.Count;i++){
			Destroy(Fuckers[i]);
		}

		Fuckers.Clear ();
		p_selectedFucker = null;
	}
}// Class End
