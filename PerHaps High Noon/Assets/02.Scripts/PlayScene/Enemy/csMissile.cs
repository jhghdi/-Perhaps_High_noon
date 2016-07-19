using UnityEngine;
using System.Collections;

public class csMissile : csEnemyObject
{
    /// <summary>
    /// player의 position을 가져오기 위한 객체
    /// </summary>
    public Transform player;

    /// <summary>
    /// player와의 거리
    /// </summary>
    Vector3 distance;

    /// <summary>
    /// missile의 속도
    /// </summary>
    Vector3 speed;

    /// <summary>
    /// 생성 후 누적시간
    /// </summary>
    float time;

    /// <summary>
    /// 왼쪽, 혹은 오른쪽으로 움직일지 지정
    /// </summary>
    public bool isLeft;

    /// <summary>
    /// 아래로 내려가는 속도
    /// </summary>
    public float downSpeed;

    /// <summary>
    /// 좌, 혹은 우측으로 퍼지는 속도
    /// </summary>
    Vector3 wideSpeed;

	// Use this for initialization
	void Start () {
        distance = transform.position - player.position;
        speed = new Vector3(distance.x / 60, distance.y / 60, distance.z / 60);
        wideSpeed = (isLeft ? Vector3.left : Vector3.right) * 0.2f;
    }
	
	// Update is called once per frame
	new void Update () {
      
        //일시정지 시 리턴
        if (!Common.isRunning)
            return;

        //아직 이동중이여서 에임 활성 X
        if (!value.activeSelf)
            return;

        // 이동
        if (time < 0.7f)
            transform.position += wideSpeed + (Vector3.down * downSpeed);
        else
            transform.position += speed;

        // 아이템이 있을경우 같이 이동시킨다. - TODO

        //조준중
        if (value.transform.localScale.x > -0.6)
            value.transform.localScale -= scaleSpeed * Time.deltaTime;
        else
        {
            //공격단계 수정요망
            OnHide();
            ActiveItem(0);

            // 다시 활성화시 처음 크기로 보이도록
            value.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
        }

        time += Time.deltaTime;
    }
}
