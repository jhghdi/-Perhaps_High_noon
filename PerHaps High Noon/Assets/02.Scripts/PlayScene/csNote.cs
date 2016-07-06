using UnityEngine;

public class csNote : MonoBehaviour {

    public GameObject value;
    public GameObject standard;
    public GameObject life;
    public GameObject fever;

    public Vector3 scaleSpeed;

    private GameObject defaultAim;
    private GameObject lifeAim;
    private GameObject feverAim;

    private float judgementStandard;
    // note의 종류
    public Common.ITEM_TYPE type;

	// Use this for initialization
	void Start () {
        scaleSpeed = new Vector3(1f, 1f, 1f);
        judgementStandard = standard.transform.localScale.x;

        defaultAim = Instantiate(standard, transform.position, Quaternion.identity) as GameObject;
        defaultAim.transform.parent = transform;
        defaultAim.SetActive(false);
        lifeAim = Instantiate(life, transform.position, Quaternion.identity) as GameObject;
        lifeAim.transform.parent = transform;
        lifeAim.SetActive(false);
        feverAim = Instantiate(fever, transform.position, Quaternion.identity) as GameObject;
        feverAim.transform.parent = transform;
        feverAim.SetActive(false);

        switch (type)
        {
            case Common.ITEM_TYPE.NONE:
                defaultAim.SetActive(true);
                break;
            case Common.ITEM_TYPE.LIFE:
                lifeAim.SetActive(true);
                break;
            case Common.ITEM_TYPE.FEVER:
                feverAim.SetActive(true);
                break;
        }
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
        if (value.transform.localScale.x > 0)
			value.transform.localScale -= scaleSpeed*Time.deltaTime;
        else
        {
			transform.parent.SendMessage ("OnHide");
            transform.parent.GetComponent<csEnemy>().ActiveItem(0);

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
