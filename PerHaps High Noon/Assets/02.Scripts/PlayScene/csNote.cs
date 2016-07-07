using UnityEngine;

public class csNote : MonoBehaviour {

    public GameObject value;
    public GameObject standard;

    public Vector3 scaleSpeed;

    // 판정기준 : standard의 크기
    private float judgementStandard;
  

	// Use this for initialization
	void Start () {
        scaleSpeed = new Vector3(1f, 1f, 1f);
        judgementStandard = standard.transform.localScale.x;
    }

    void onEnable()
    {

    }

    public void SetScaleSpeed(float speed)
    {
        scaleSpeed.x = speed;
        scaleSpeed.y = speed;
        scaleSpeed.z = speed;
    }
	
	// Update is called once per frame
	void Update () {
        if (!Common.isRunning)
            return;
<<<<<<< HEAD

=======
>>>>>>> Kim-Da-Hun
        if (value.transform.localScale.x > 0)
			value.transform.localScale -= scaleSpeed*Time.deltaTime;
        else
        {
			transform.parent.SendMessage ("OnHide");
            transform.parent.GetComponent<csEnemy>().ActiveItem(0);

            // 다시 활성화시 처음 크기로 보이도록
            value.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
        }
	}

    public void OnTrigger()
    {
        float judgementResult = Mathf.Abs( (value.transform.localScale.x - judgementStandard) / judgementStandard);

        // fever guage 증가량 및 miss 판별
        float amount=0;

        if (judgementResult < 0.1f)
            amount = 4.0f;
        else if (judgementResult < 0.4f)
            amount = 3.0f;
        else if (judgementResult < 0.8f)
            amount = 2.0f;
        else if (judgementResult < 0.99f)
            amount = 1.0f;
        else
            amount = 0;

        transform.parent.SendMessage("ActiveItem", amount);
        transform.parent.SendMessage ("OnHide");      
    }
}
