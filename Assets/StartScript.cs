using UnityEngine;
using System.Collections;

public class StartScript : MonoBehaviour {

	int clickAmount;

	void Awake(){
		clickAmount = Random.Range (4,7);
	}

	void Start(){
		StartCoroutine (LoadLevel());
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			ExplosionController.CallExplosion(GameObject.Find("ggj odun").transform.position,Color.yellow);
			clickAmount--;
		}

		if (clickAmount <= 0)
			op.allowSceneActivation = true;
	}

	AsyncOperation op;

	IEnumerator LoadLevel(){
		op = Application.LoadLevelAsync ("Test(Murat)");
		op.allowSceneActivation = false;

		while (!op.isDone)
			yield return op;
	}
}
