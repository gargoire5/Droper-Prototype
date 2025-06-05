using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerData : MonoBehaviour
{
    enum Layer
    {
        Player = 6,
        Obs,
        Sol,
        Safe,
        Bumper,
        Anneau
    }

    public int Life;

    public Vector3 firstTransform;

    public GameObject textLose;

    public TMP_Text textLife;

    public GenerateRamdom generate;

    public GameObject selectLv;

    public PlayerPowerUp PlayerPowerUp;

    int lv;

    void Start()
    {
        firstTransform = this.gameObject.transform.position;
        textLife.text = "";
        for (int i = 0; i < Life; i++)
        {
            textLife.text += "0";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == (int)Layer.Obs)
        {
            Damage();
            GetComponent<Score>().score = 0;
        }
        else if (collision.gameObject.layer == (int)Layer.Safe)
        {
            collision.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            Win();

            selectLv.SetActive(true);

            this.gameObject.transform.position = firstTransform;
            Life = 10;
            textLife.text = "";
            for (int i = 0; i < Life; i++)
            {
                textLife.text += "0";
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (int)Layer.Anneau)
        {
            GetComponent<Score>().AddPoint(500);
        }
    }
    public void selectlv(int lvselect)
    {
        lv = lvselect;
    }

    

    void Damage()
    {
        if(PlayerPowerUp.AbsorbDamage())
            return;
        Life--;
        if (Life <= 0)
        {
            textLife.text = "";
            textLose.SetActive(true);
        }
        else
        {
            this.gameObject.transform.position = firstTransform;
            textLife.text = "";
            for (int i = 0; i < Life; i++)
            {
                textLife.text += "0";
            }
        }
    }

    void Win()
    {
        for (int i = 0; i < Life; i++)
        {
            GetComponent<Score>().AddPoint(100);
        }
        GetComponent<Score>().AddPoint(2000*lv);
        GetComponent<Score>().Scoretext.gameObject.SetActive(true);        
    }
}
