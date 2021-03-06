﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class csValueManager : MonoBehaviour {

    public Slider revengeGuage;
    public Button btnRevenge;
    public GameObject player;
    public GameObject life;

    public Text comboText;

    // fever 상태 유무
    int fever;
    const int TRUE = 2;
    const int FALSE = 1;

    int combo_count = 0;

    private csPlayer playerMethod;

    GameObject[] lifeImages;

    /// <summary>
    /// player life 감소 sound
    /// </summary>
    public AudioClip sound_ReduceLife;


    /// <summary>
    /// player life 획득 sound
    /// </summary>
    public AudioClip sound_GainLife;

    /// <summary>
    /// Player 사망시 나오는 sound
    /// </summary>
    public AudioClip deadSound;

    /// <summary>
    /// fever 모드 발동시 나오는 sound
    /// </summary>
    public AudioClip feverSound;

    // Use this for initialization
    void Start () {
        revengeGuage = GameObject.Find("RevengeGuage").GetComponent<Slider>();
        btnRevenge = GameObject.Find("BtnHigh").GetComponent<Button>();
        playerMethod = player.GetComponent<csPlayer>();

        fever = FALSE;

        life.transform.GetChild(0).gameObject.SetActive(true);
        life.transform.GetChild(1).gameObject.SetActive(true);
        life.transform.GetChild(2).gameObject.SetActive(true);
    }

    public void ActiveRevenge()
    {
        if ( (GetRevengeGuage() >= 30.0f ) || 
             ((GetRevengeGuage() < 30.0f && playerMethod.isHighNoon)) )
            playerMethod.OnHighNoon();
        else
            return;
    }

    /// <summary>
    /// Revenge Guage의 값을 변경한다
    /// </summary>
    /// <param name="amount">guage가 증가, 감소하는 값</param>
    public void AddRevengeGuage(float amount)
    {
        if (amount >= 0)
            revengeGuage.value += (fever * amount+ combo_count);
        else 
            revengeGuage.value = revengeGuage.value < amount ? 0 : revengeGuage.value + amount;
    }

    /// <summary>
    ///  Revenge Guage를 반환한다
    /// </summary>
    public float GetRevengeGuage()
    {
        return revengeGuage.value;
    }

    public void GainLife()
    {
        if (playerMethod.life >= 5)
            return;

        csSoundManager.Instance().PlaySfx(sound_GainLife);

        life.transform.GetChild(playerMethod.life).gameObject.SetActive(true);
        
        ++playerMethod.life;
    }

    public void ReduceLife()
    {
        //피가 1이면 -> 0 = 게임 종료
        if (playerMethod.life == 1) {
            //무적 모드
            //return; 
            Common.isRunning = true;
            Time.timeScale = 1.0f;

            csSoundManager.Instance().Stop();
            csSoundManager.Instance().PlaySfx(deadSound);

            //모든 객체 제거
            //신전환해도 DonDestroy때문에 제거 안됨.
            GameObject[] g = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject gg in g)
            {
                if (gg.gameObject.name == "SoundManager")
                    continue;
                else
                    Destroy(gg);
            }
            
            SceneManager.LoadScene("StageScene");
         }
        else
        {
            csSoundManager.Instance().PlaySfx(sound_ReduceLife);
            --playerMethod.life;
            life.transform.GetChild(playerMethod.life).gameObject.SetActive(false);
            SetFeverMode(false);
       }

        //피격시 콤보 초기화
        combo_count = 0;
        updateCombo();
    }

    public void SetFeverMode(bool isActive)
    {
        fever = isActive ? TRUE : FALSE;

        if (isActive)
            csSoundManager.Instance().PlaySfx(feverSound);
    }

    public void Combo(int count)
    {
        combo_count += count;
        updateCombo();
    }

    void updateCombo()
    {
        comboText.text = string.Format("{0} Combo!", combo_count);
    }
}
