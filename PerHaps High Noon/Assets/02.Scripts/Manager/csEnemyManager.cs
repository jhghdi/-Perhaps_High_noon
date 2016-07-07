using UnityEngine;
using System.Collections;
using System.Linq;

public class csEnemyManager : MonoBehaviour {

    public GameObject stageM;
	public GameObject enemy1;
	public GameObject aimLock;
    public GameObject life;
    public GameObject fever;

    //start 초기 배열 초기화
    public int enemy1_Count;
	public int aimCount;

    GameObject lifeItem;
    GameObject feverItem;

    // 각 객체에 대한 배열
    GameObject[] enemies_1;
	GameObject[] aimLocks;

	int enemyCount=-11;
	int enemy1_Index = 0;
    int aim_Index = 0;
    public GameObject[] spawnPoints;
	int step_Count = 1;

	// Use this for initialization
	void Start () {
        enemies_1 = new GameObject[enemy1_Count];
		aimLocks = new GameObject[aimCount];


    }

	void OnLevelWasLoaded(int level){
        step_Count = 1;

		if (level > 2) {
			
			for (int i = 0; i < enemy1_Count; i++) {
                enemies_1[i] = Instantiate (enemy1, Vector3.zero, Quaternion.identity) as GameObject;
                enemies_1[i].transform.parent = transform;
                // enemy의 노트 생성
                enemies_1[i].GetComponent<csEnemy>().CreateNote();
                enemies_1[i].SetActive (false);
			}

			for (int i = 0; i < aimCount; i++) {
				aimLocks [i] = Instantiate (aimLock, Vector3.zero, Quaternion.identity)as GameObject;
				aimLocks [i].SetActive (false);
			}
		}
        
        // item 초기화
        if (lifeItem == null)
        {
            lifeItem = Instantiate(life, Vector3.zero, Quaternion.identity) as GameObject;
            lifeItem.SetActive(false);
        }
        if (feverItem == null)
        {
            feverItem = Instantiate(fever, Vector3.zero, Quaternion.AngleAxis(90.0f, Vector3.left)) as GameObject;
            feverItem.SetActive(false);
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

        // item 종류 지정
        e.GetComponent<csEnemy>().itemType = si.item;
      
        // aim대기시간 설정
        e.GetComponent<csEnemy>().SetAimCoolTime(si.aimTime);

        // 적 활성화
        e.SetActive (true);
		e.transform.position = start;
		e.transform.rotation = Quaternion.Euler (end - start);

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

    public void InitItem(GameObject obj)
    {
        if (obj.GetComponent<csEnemy>().itemType == Common.ITEM_TYPE.LIFE)
        { 
            lifeItem.transform.position = Common.GetAimPosition(obj.transform.position);
            lifeItem.SetActive(true);
        }
        else if(obj.GetComponent<csEnemy>().itemType == Common.ITEM_TYPE.FEVER)
        { 
            feverItem.transform.position = Common.GetAimPosition(obj.transform.position);
            feverItem.SetActive(true);
        }     
    }

    public void RemoveItem()
    {
        lifeItem.SetActive(false);
        feverItem.SetActive(false);
    }

}
