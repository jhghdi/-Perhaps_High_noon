using UnityEngine;
using System.Collections;

public class csSpawnManager : MonoBehaviour {
	public GameObject Aim;
	float coolTime = 0.0f;

	bool isHigh = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (!isHigh)
			return;

		coolTime += Time.deltaTime;
		if (coolTime > 1.0f) {
			coolTime -= 1.0f;


			Vector3 v = new Vector3 (Random.Range(-5,5),Random.Range(0.5f,5),Random.Range(-5,5));


			GameObject obj = Instantiate (Aim, v, Quaternion.identity ) as GameObject;
		}
	}

	public void OnHigh(){
		isHigh = !isHigh;
	}
}
