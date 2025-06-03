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
        Safe
    }

    public int Life;

    public Vector3 firstTransform;

    public GameObject textLose;

    public TMP_Text textLife;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int)Layer.Obs)
        {
            Damage();
            Debug.Log("DEAD");
        }
        else if (collision.gameObject.layer == (int)Layer.Safe)
        {
            collision.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            Win();
            Debug.Log("WIN");
        }
    }

    void Damage()
    {
        Life--;
        if (Life <= 0)
        {
            textLife.text = "";
            Debug.Log("LOSE");
            textLose.SetActive(true);
        }
        else
        {
            this.gameObject.transform.position = firstTransform;
            textLife.text = "";
            for (int i = 0;i < Life; i++)
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
        GetComponent<Score>().AddPoint(2000);
        GetComponent<Score>().Scoretext.gameObject.SetActive(true);        
    }
}
