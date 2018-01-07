using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour {

	public static ExplosionController instance;

	public static GameObject explosionPrefab;
	public GameObject temp;

	void Awake(){
		if (instance == null)
			instance = this;

		explosionPrefab = Resources.Load ("Flash02")as GameObject;
	}

	public static void CallExplosion(Vector3 p_pos,Color p_color){
		if (instance.temp == null) {
			instance.temp = Instantiate(explosionPrefab)as GameObject;
		}

		instance.temp.transform.position = p_pos;
		instance.temp.GetComponent<ParticleSystem>().startColor = p_color;
		instance.temp.GetComponent<ParticleSystem>().Play();
	}
}
