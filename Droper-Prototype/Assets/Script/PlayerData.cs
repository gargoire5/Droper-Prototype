using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // Start is called before the first frame update

    enum Layer{
    
        Player = 6,
        Obs,
        Sol,
        Socle
    }
    
    public int Life = 10;

    public Vector3 firstTransform;

    void Start()
    {
        firstTransform = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if(collision.gameObject.layer == (int)Layer.Obs)
        {
            Damage();
            Debug.Log("collision");
        }
        else if(collision.gameObject.layer == (int)Layer.Socle)
        {
            Debug.Log("win");
        }
    }

    private void OnCollisionExit(UnityEngine.Collision collision)
    {
        
    }

    void Damage()
    {
        Life--;
        if (Life <= 0)
        {
            //Lose
        }
        this.gameObject.transform.position = firstTransform;
    }
}
