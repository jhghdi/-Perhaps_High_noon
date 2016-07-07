using UnityEngine;
using System.Collections;
using System.Linq;

public class csEnemyManager : MonoBehaviour {

    public GameObject stageM;
	public GameObject enemy1;
	public GameObject aim;
	public GameObject r_Aim;
	public GameObject aimLock;

	//start 초기 배열 초기화
	public int enemy1_Count;
	public int aimCount;

    // 각 객체에 대한 배열
	GameObject[] enemies_1;
	GameObject[] aims;
	GameObject[] r_Aims;
	GameObject[] aimLocks;

	int enemyCount=-11;
	int enemy1_Index = 0;
	int aim_Index = 0;
	public GameObject[] spawnPoints;
	int step_Count = 1;

	// Use this for initialization
	void Start () {
        enemies_1 = new GameObject[enemy1_Count];
		aims = new GameObject[aimCount];
		r_Aims = new GameObject[aimCount];
		aimLocks = new GameObject[aimCount];
	}

	void OnLevelWasLoaded(int level){
        step_Count = 1;

		if (level > 2) {
			
			for (int i = 0; i < enemy1_Count; i++) {
                enemies_1[i] = Instantiate (enemy1, Vector3.zero, Quaternion.identity) as GameObject;
                enemies_1[i].SetActive (false);
			}

			for (int i = 0; i < aimCount; i++) {
				aims [i] = Instantiate (aim, Vector3.zero, Quaternion.identity)as GameObject;
				aims [i].SetActive (false);

				r_Aims [i] = Instantiate (r_Aim, Vector3.zero, Quaternion.identity)as GameObject;
			    r_Aims [i].SetActive (false);

				aimLocks [i] = Instantiate (aimLock, Vector3.zero, Quaternion.identity)as GameObject;
				aimLocks [i].SetActive (false);
			}
		}
	}


	//enemy가 호출 예정
	void OnEnemyDead(){
		enemyCount--;
		if (enemyCount == 0) {
			stageM.SendMessage ("OnStepEnded");

			enemyCount = -11;
			step_Count++;
			return;
		}
	}

	void Spawn(SpawnInfo si){
		GameObject e;
		//종류 별로
		//switch (si.typeNum) {
		//case 1:
		e = enemies_1[enemy1_Index];
	
		enemy1_Index++;
		//break;
		//}

		Vector3 start = spawnPoints [si.spawnPos].transform.position;
		Vector3 end = spawnPoints [si.destinationPos].transform.position;

        // enemy의 노트 생성
        e.GetComponent<csEnemy>().itemType = si.item;
        e.GetComponent<csEnemy>().CreateNote();

        e.SetActive (true);
		e.transform.position = start;
		e.transform.rotation = Quaternion.Euler (end - start);

		aims [aim_Index].transform.parent = e.transform;
		r_Aims [aim_Index].transform.parent = e.transform;
		aimLocks [aim_Index].transform.parent = e.transform;
		aim_Index++;

		Hashtable hash = new Hashtable ();
		hash.Add ("path", new Vector3[2]{start,end});
		hash.Add ("orienttopath", true);
		hash.Add ("speed", 3.0f);
		hash.Add ("looptype", iTween.LoopType.none);
		hash.Add ("easetype", iTween.EaseType.linear);
		hash.Add ("ignoretimescale", false);
		hash.Add ("oncomplete", "OnMoveEnded");
		hash.Add ("oncompletetarget", e);

		iTween.MoveTo (e, hash);
	}

	//step별로 초기화 기능 있음.
	void setEnemyCount(int n){
		
		spawnPoints = GameObject.FindGameObjectsWithTag(string.Format("SpawnPoint{0}",step_Count)).OrderBy (g => g.transform.name).ToArray ();
		enemy1_Index = 0;
		aim_Index = 0;

		if (n == 0)
			stageM.SendMessage ("OnStepEnded");
		enemyCount = n;
	}

}
