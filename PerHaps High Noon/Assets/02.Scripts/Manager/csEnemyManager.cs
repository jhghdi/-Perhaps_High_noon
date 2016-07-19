using UnityEngine;
using System.Collections;
using System.Linq;

public class csEnemyManager : MonoBehaviour {

    public GameObject stageM;
	public GameObject enemy1;
    public GameObject enemyTanker;
    public GameObject drone;
    public GameObject life;
    public GameObject fever;

    //start 초기 배열 초기화
    public int enemy1_Count;
    public int enemyTanker_Count;
    public int drone_Count;

    GameObject lifeItem;
    GameObject feverItem;

    // 각 객체에 대한 배열
    GameObject[] enemies_1;
    GameObject[] enemies_tanker;
    GameObject[] drones;

    int enemyCount=-11;
	int enemy1_Index = 0;
    int enemyTanker_Index = 0;
    int drone_Index = 0;
    int turret_Index = 0;
    int aim_Index = 0;
    GameObject[] spawnPoints;
	int step_Count = 1;

    // turret을 찾는데 사용
    string turretName;

	// Use this for initialization
	void Start () {
        enemies_1 = new GameObject[enemy1_Count];
        enemies_tanker = new GameObject[enemyTanker_Count];
        drones = new GameObject[drone_Count];
    }

    // 각 객체들을 초기화한다
	void OnLevelWasLoaded(int level){
        step_Count = 1;

		if (level > 2) {

            for (int i = 0; i < enemy1_Count; i++)
            {
                // 기본 enemy
                enemies_1[i] = Instantiate(enemy1, Vector3.zero, Quaternion.identity) as GameObject;
                enemies_1[i].transform.parent = transform;
                enemies_1[i].SetActive(false);

                // 여러번 터치를 요구하는 enemy
                enemies_tanker[i] = Instantiate(enemyTanker, Vector3.zero, Quaternion.identity) as GameObject;
                enemies_tanker[i].transform.parent = transform;
                enemies_tanker[i].SetActive(false);

                // 드론
                drones[i] = Instantiate(drone, Vector3.zero, Quaternion.identity) as GameObject;
                drones[i].transform.parent = transform;
                drones[i].SetActive(false);

                // 이후 추가 요소 있을 경우 추가 할 것
            }
		}
        
        // item 초기화
        if (lifeItem == null)
            lifeItem = Instantiate(life, Vector3.zero, Quaternion.identity) as GameObject;
        if (feverItem == null)
            feverItem = Instantiate(fever, Vector3.zero, Quaternion.AngleAxis(90.0f, Vector3.left)) as GameObject;

        lifeItem.SetActive(false);
        feverItem.SetActive(false);
    }

	//enemy가 호출 예정
	public void OnEnemyDead(){
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

        // 시작과 도착지 지정
        Vector3 start = spawnPoints[si.spawnPos].transform.position;
        Vector3 end = spawnPoints[si.destinationPos].transform.position;

        // 적 유형 별로 생성
        switch (si.enemyType)
        {
            case Common.ENEMY_TYPE.NORMAL:
                e = enemies_1[enemy1_Index];
                enemy1_Index++;
                break;
            case Common.ENEMY_TYPE.TANKER:
                e = enemies_tanker[enemyTanker_Index];
                enemyTanker_Index++;
                break;
            case Common.ENEMY_TYPE.DRONE:
                e = drones[drone_Index];
                drone_Index++;
                break;
            case Common.ENEMY_TYPE.TURRET:
                turretName = string.Format("Turret{0}", turret_Index);
                e = GameObject.Find(turretName);
                e.transform.parent = transform;
                e.GetComponent<csEnemyObject>().itemType = si.item;
                e.GetComponent<csTurret>().SetMove(true);
                turret_Index++;
                return;
            default:
                e = new GameObject();
                break;
        }

        // item 종류 지정
        e.GetComponent<csEnemyObject>().itemType = si.item;

        // aim대기시간 설정
        e.GetComponent<csEnemyObject>().SetAimCoolTime(si.aimTime);

        // 적 활성화
        e.transform.position = start;
        e.transform.rotation = Quaternion.Euler(end - start);
        e.SetActive (true);
	
		aim_Index++;

        // 적 움직임 정의
        SetEnemyMove(e, si.enemyType, start, end);
    }

	// step별로 초기화 기능 있음.
	void setEnemyCount(int n){
		
		spawnPoints = GameObject.FindGameObjectsWithTag(string.Format("SpawnPoint{0}",step_Count)).OrderBy (g => g.transform.name).ToArray ();
		enemy1_Index = 0;
        enemyTanker_Index = 0;
        drone_Index = 0;
        aim_Index = 0;

		if (n == 0)
			stageM.SendMessage ("OnStepEnded");
        else
		    enemyCount = n;
    }

    // 적이 움직임이 끝나고 호출함
    public void InitItem(GameObject obj)
    {
        if (obj.GetComponent<csEnemyObject>().itemType == Common.ITEM_TYPE.LIFE)
        { 
            lifeItem.transform.position = Common.GetAimPosition(obj.transform.position);
            lifeItem.SetActive(true);
        }
        else if(obj.GetComponent<csEnemyObject>().itemType == Common.ITEM_TYPE.FEVER)
        { 
            feverItem.transform.position = Common.GetAimPosition(obj.transform.position);
            feverItem.SetActive(true);
        }     
    }

    // 아이템 비활성화
    public void RemoveItem()
    {
        lifeItem.SetActive(false);
        feverItem.SetActive(false);
    }

    // 적 움직임 정의
    private void SetEnemyMove(GameObject enemy, Common.ENEMY_TYPE enemyType, Vector3 start, Vector3 end)
    {
        Hashtable hash = new Hashtable();

        switch (enemyType)
        {
            case Common.ENEMY_TYPE.NORMAL:
            case Common.ENEMY_TYPE.TANKER:
                hash.Add("path", new Vector3[2] { start, end });
                hash.Add("orienttopath", true);
                hash.Add("speed", 9.0f);
                hash.Add("looptype", iTween.LoopType.none);
                hash.Add("easetype", iTween.EaseType.linear);
                hash.Add("ignoretimescale", false);
                hash.Add("oncomplete", "OnMoveEnd");
                hash.Add("oncompletetarget", enemy);
                enemy.SendMessage("OnMoveStart");
                iTween.MoveTo(enemy, hash);
                break;

            case Common.ENEMY_TYPE.DRONE:
                hash.Add("path", new Vector3[2] { start , end  });
                hash.Add("orienttopath", true);
                hash.Add("speed", 9.0f);
                hash.Add("looptype", iTween.LoopType.none);
                hash.Add("easetype", iTween.EaseType.linear);
                hash.Add("ignoretimescale", false);
                hash.Add("oncomplete", "OnMoveEnd");
                hash.Add("oncompletetarget", enemy);
                enemy.SendMessage("OnMoveStart");
                iTween.MoveTo(enemy, hash);
                break;

            case Common.ENEMY_TYPE.TURRET:
                break;
   
        }
    }

}
