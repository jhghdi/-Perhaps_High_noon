using UnityEngine;
using System.Collections;

public class csStage1Boss : csBossBase
{
    public GameObject missile;

    private GameObject[] missiles;

    private float upSpeed;
    private float downSpeed;

    public Vector3[] destinations;

    Vector3 speed;

    enum MoveStatus { UP =0, MOVE, DOWN };

    MoveStatus move;

    // Use this for initialization
    void Start()
    {
        upSpeed = 0.05f;
        downSpeed = 0.05f;
        destination = destinations[patternStep];

        missiles = new GameObject[12];

        for (int i = 0; i < missiles.Length; ++i)
        {
            missiles[i] = Instantiate(missile, Vector3.zero, Quaternion.identity) as GameObject;
            missiles[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Move()
    {
        switch(move)
        {
            case MoveStatus.UP:
                transform.position += Vector3.up * upSpeed;
                if (transform.position.y == 5.0f)
                    move = MoveStatus.MOVE;
                break;

            case MoveStatus.MOVE:
                transform.position += speed;
                if (transform.position.x == destination.x && transform.position.z == destination.z)
                    move = MoveStatus.DOWN;
                break;
         
            case MoveStatus.DOWN:
                transform.position += Vector3.down * upSpeed;
                if (transform.position == destination)
                {
                    move = MoveStatus.UP;
                    actionStatus = Common.BOSS_ACTION_TYPE.ATTACK;
                }
                break;
        }
    }

    void Attack()
    {
        switch (patternStep)
        {
            case 0:
            case 2:
            case 4:
                for (int i = 0; i < 4; ++i)
                {
                    missiles[i].GetComponent<csMissile>().player = player.transform;
                    missiles[i].GetComponent<csMissile>().isLeft = i < 2 ? true : false;
                    missiles[i].GetComponent<csMissile>().downSpeed = i % 2 > 0 ? 0.1f : 0.2f;
                    missiles[i].SetActive(true);
                }
                break;

            case 1:
            case 3:
            case 5:
                for (int i = 0; i < 6; ++i)
                {
                    missiles[i].GetComponent<csMissile>().player = player.transform;
                    missiles[i].SetActive(true);
                }
                break;

            case 6:
                for (int i = 0; i < 12; ++i)
                {
                    missiles[i].GetComponent<csMissile>().player = player.transform;
                    missiles[i].SetActive(true);
                }
                break;

            default:
                break;
        }
    }

    void RunDeadAnimation()
    {

    }
}
