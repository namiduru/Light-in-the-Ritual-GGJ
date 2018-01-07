using UnityEngine;
using System.Collections.Generic;

public class PlayCreator : MonoBehaviour {

	GameObject playCenter;
	Vector3 refPos;

	public static List<GameObject> nodes;
	public static List<GameObject> selecetedNodes;

	public Material lineMat;

	public static PlayCreator instance; 

	List<GameObject> lines;

	void Awake(){

		lines = new List<GameObject> ();

		instance = this;

		playCenter = GameObject.Find ("PlayCreator_Center");
		refPos = GameObject.Find ("PlayCreator_Top").transform.position;

		nodes = new List<GameObject> ();
		selecetedNodes = new List<GameObject> ();
	}

	// Use this for initialization
	void Start () {
		CreatePlayNodes ();
		CreatePlayMoves ();
		CreateLines ();
	}



	void CreateLines(){
		for(int i=0;i<selecetedNodes.Count-1;i++){ 
			GameObject temp = new GameObject ("Line");
			temp.transform.LookAt(Camera.main.transform.position);
			LineRenderer p_LR = temp.AddComponent<LineRenderer>();
			p_LR.SetPosition(0,(selecetedNodes [i].transform.position));
			p_LR.SetPosition (1, (selecetedNodes [i+1].transform.position));
			p_LR.material = lineMat;
			p_LR.SetWidth(0.2f,0.2f);
			p_LR.sortingLayerName = "MovePoints";
			p_LR.SetColors(Color.red,new Color(1,1,1,1));
			lines.Add(temp);
		}
	}

	public void CreatePlayNodes(){
		int amount = FollowerSpawner.Amount;
		int space = 360 / amount;
		
		while (amount > 0) {
			// Create followers
			GameObject temp = Instantiate (Resources.Load("Debug_Play")as GameObject,refPos,playCenter.transform.rotation)as GameObject;
			temp.transform.RotateAround(playCenter.transform.position,playCenter.transform.forward,space * amount);


			nodes.Add(temp);

			amount--;
		}
	}

	public void CreatePlayMoves(){
		int edges = Random.Range (1,FollowerSpawner.Amount);

		List<GameObject> tempList = new List<GameObject> ();
		tempList.AddRange (nodes.ToArray());

		for (int i=0; i<50; i++) {
			int randomFirst = Random.Range(0,tempList.Count);
			int randomSecond = Random.Range(0,tempList.Count);
			GameObject temp = tempList[randomFirst];
			tempList[randomFirst] = tempList[randomSecond];
			tempList[randomSecond] = temp;
		}

		for(int i=0;i<edges+1;i++){
			selecetedNodes.Add (tempList[i]);
		}
	}

	public void Refresh(){
		for (int i=0; i<nodes.Count; i++) {
			Destroy(nodes[i]);
		}

		for (int i=0; i<selecetedNodes.Count; i++) {
			Destroy(selecetedNodes[i]);
		}

		for (int i=0; i<lines.Count; i++) {
			Destroy(lines[i]);
		}

		nodes.Clear ();
		selecetedNodes.Clear ();
		lines.Clear ();

		CreatePlayNodes ();
		CreatePlayMoves ();
		CreateLines ();
	}
}// Class End
