using UnityEngine;

public class csEnemyManager : MonoBehaviour {

    public GameObject enemy;
    public GameObject toucnAim;
    public GameObject slideAim;
    public GameObject lifeItem;
    public GameObject feverItem;

    /// <summary>
    ///  Enemy 객체의 최대 갯수
    /// </summary>
    const int MAX_EMENY_COUNT = 20;

    /// <summary>
    /// 적 개체들
    /// </summary>
    GameObject[] enemies = new GameObject[MAX_EMENY_COUNT];

    /// <summary>
    ///  Aim 객체의 최대 갯수
    /// </summary>
    const int MAX_AIM_COUNT = 20;

    /// <summary>
    /// Aim 개체들
    /// </summary>
    GameObject[] aims = new GameObject[MAX_AIM_COUNT];

    /// <summary>
    /// 적을 활성화한다.
    /// </summary>
    /// <param name="index">활성화 할 Enemy의 index</param>
    /// <param name="position">Enemy의 시작 위치</param>
    public void ActiveEnemy(int index, Vector3 position)
    {

    }

    /// <summary>
    /// 해당 enemy를 비활성화 시킨다.
    /// </summary>
    /// <param name="index">비활성화 시킬 Enemy의 index</param>
    public void DeactiveEnemy(int index)
    {

    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
