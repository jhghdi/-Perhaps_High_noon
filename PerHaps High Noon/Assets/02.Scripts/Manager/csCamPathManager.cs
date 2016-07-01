using UnityEngine;
using System.Collections;

public class csCamPathManager : MonoBehaviour {
	public GameObject stageM;

	Transform[] paths;
	public GameObject player;

	int step_Num=1;


	void LoadInfo () {
		paths = GameObject.Find ("Map").GetComponentsInChildren<Transform> ();
		player = GameObject.FindWithTag ("Player");
		stageM = GameObject.Find ("StageManager");
	}

	void Move(){
		if (step_Num == 1)
			player.transform.position = paths [step_Num].GetComponent<iTweenPath> ().nodes [0];
		Debug.Log ("step_num"+step_Num);
		Debug.Log (paths [step_Num].name);
		Hashtable hash = new Hashtable ();

		hash.Add ("path", 
			paths [step_Num].GetComponent<iTweenPath> ().nodes.ToArray());
		//hash.Add ("movetopath", true);
		hash.Add ("orienttopath", true);
		//hash.Add ("looktime", 1.0f);
		//hash.Add ("time", 3.0f);
		hash.Add ("speed", 3.0f);
		hash.Add ("looptype", iTween.LoopType.none);
		//hash.Add ("easetype", iTween.EaseType.easeInExpo);
		hash.Add ("easetype", iTween.EaseType.linear);

		hash.Add ("oncomplete", "OnCameraEnded");
		hash.Add ("oncompletetarget", stageM);

		hash.Add ("ignoretimescale", true);

		iTween.MoveTo (player, hash);

		step_Num += paths[step_Num].childCount;
		step_Num++;
	}
}
