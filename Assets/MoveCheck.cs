using UnityEngine;
using System.Collections;

public class MoveCheck : MonoBehaviour {

	public static MoveCheck instance;
	public GameObject fire;

	void Awake(){
		fire = GameObject.Find ("Fire_Color");

		instance = this;
	}

	public static bool ControlMove(){

		int[][] steps = new int[2][];

		for (int i=0; i<2; i++) {
			steps[i] = new int[PlayCreator.selecetedNodes.Count];
		}

		int[] indexes = new int[PlayCreator.selecetedNodes.Count];

		for (int i=0; i<PlayCreator.selecetedNodes.Count; i++) {
			for(int j=0;j<PlayCreator.nodes.Count;j++){
				if(PlayCreator.selecetedNodes[i] == PlayCreator.nodes[j]){
					indexes[i] = j;
					break;
				}
			}
		}

		/*for(int i=0;i<indexes.Length;i++){
			Debug.Log("Indexes : " + indexes[i]);
		} */

		for (int i=0; i<steps[0].Length - 1; i++) {
			if(indexes[i] - indexes[i+1] >=0){
				steps[0][i] = indexes[i] - indexes[i+1];
				steps[1][i] = PlayCreator.nodes.Count - (indexes[i] - indexes[i+1]);

				//Debug.Log("Steps : " + steps[0][i]);
				//Debug.Log("Steps : " + steps[1][i]);

			}else{
				steps[0][i] = Mathf.Abs(indexes[i] - indexes[i+1]);
				steps[1][i] = PlayCreator.nodes.Count - Mathf.Abs(indexes[i] - indexes[i+1]);

				//Debug.Log("Steps : " + steps[0][i]);
				//Debug.Log("Steps : " + steps[1][i]);
			}
		}

		int[] playerIndexes = new int[InputController.Fuckers.Count];

		for (int i=0; i<InputController.Fuckers.Count; i++) {
			for(int j=0;j<FollowerSpawner.Followers.Count;j++){
				if(InputController.Fuckers[i] == FollowerSpawner.Followers[j]){
					playerIndexes[i] = j;
					break;
				}
			}
		}

		/*for (int i=0; i<playerIndexes.Length; i++) {
			Debug.Log("PlayerIndexes : " + playerIndexes[i]);
		}*/

		bool win = true;

		for (int i=0; i<playerIndexes.Length-1; i++) {
			if (Mathf.Abs(playerIndexes [i + 1] - playerIndexes [i]) != steps [0] [i]) {
				if (Mathf.Abs (playerIndexes [i + 1] - playerIndexes [i]) != steps [1] [i]) {
					if(Mathf.Abs (playerIndexes [playerIndexes.Length - i - 1] - playerIndexes [playerIndexes.Length - 1 - i]) != steps [1] [playerIndexes.Length-1 - i]){
						if(Mathf.Abs (playerIndexes [playerIndexes.Length - i - 1] - playerIndexes [playerIndexes.Length -1 - i]) != steps [0] [playerIndexes.Length-1 - i]){
							win = false;
						}
					}
				}
			}
		}

		if(InputController.Fuckers.Count == PlayCreator.selecetedNodes.Count && win){
			FollowerSpawner.success++;
			if(FollowerSpawner.success >=3){
				FollowerSpawner.success =0;
				FollowerSpawner.Amount++;
			}
			instance.GameWin();
		}

		if (!win) {
			instance.GameLose();
		}

		return win;
	}

	void GameWin(){
		Score.IncreaseScore(InputController.Fuckers.Count);
		StartCoroutine (MoveSacrifices());
	}

	IEnumerator MoveSacrifices(){
		while (Vector3.Distance(InputController.selectedFucker.transform.position,fire.transform.position) > 0.2f) {
			for (int i=0; i<InputController.Fuckers.Count; i++) {
				InputController.Fuckers[i].transform.Translate((fire.transform.position - InputController.Fuckers[i].transform.position).normalized * Time.deltaTime *5,Space.World);
			}
			yield return null;
		}

		ExplosionController.CallExplosion (fire.transform.position,Color.black);
		yield return new WaitForSeconds (0.2f);

		FollowerSpawner.instance.Refresh (FollowerSpawner.Amount);
		PlayCreator.instance.Refresh ();
		InputController.instance.Refresh ();
	}

	void GameLose(){
		FollowerSpawner.instance.Refresh (FollowerSpawner.Amount);
		PlayCreator.instance.Refresh ();
		InputController.instance.Refresh ();
	}

} // Class end
