using UnityEngine;
using System.Collections;
using System;

public class Score : MonoBehaviour {

	public static int score;
	public static GameObject textMesh;
		
	void Awake(){
		score = PlayerPrefs.GetInt ("FuckYou",0);
			
		textMesh = GameObject.Find ("Score");
			
		ChangeScore ();
	}
		
	static void ChangeScore(){
		textMesh.GetComponent<TextMesh>().text = score.ToString();
	}


    public static void IncreaseScore(int numberOfSpawner) {
        score += numberOfSpawner * 20;
		ChangeScore ();
    }
}// Class End
