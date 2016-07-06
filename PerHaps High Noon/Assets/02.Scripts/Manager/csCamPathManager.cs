using UnityEngine;
using System.Collections;
using System.Linq;
public class csCamPathManager : MonoBehaviour {
	public GameObject stageM;

	GameObject[] paths;
	public GameObject player;

	int step_Num;


	public bool LoadInfo () {
		paths = GameObject.FindGameObjectsWithTag ("Step").OrderBy (g => g.transform.name).ToArray ();
        step_Num = 0;


        if (paths == null) {
			Debug.Log ("load failed");
			return false;
		}
		Debug.Log ("load succes");
        Destroy(player.GetComponent<iTween>());
            
		player.transform.position = paths [0].GetComponent<iTweenPath> ().nodes [0];
		Move ();
		return true;
	}

	void Move(){

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

		hash.Add ("ignoretimescale", false);

		hash.Add ("oncomplete", "OnCameraEnded");
		hash.Add ("oncompletetarget", stageM);



		iTween.MoveTo (player, hash);

		step_Num++;
	}
}
