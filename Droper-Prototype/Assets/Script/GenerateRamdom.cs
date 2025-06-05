using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRamdom : MonoBehaviour
{
    public List<GameObject> prefabOBAHV;

    public float MaxX;
    public float MinX;

    public float MaxY;
    public float MinY;

    public float MaxZ;
    public float MinZ;

    public PlayerController playerController;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    float RandomPos(float minmin, float minmax, float maxmin, float maxmax)
    {
        int DG = Random.Range(0, 2);
        if (DG == 0)
        {
            return Random.Range(minmin, minmax+1);
        }
        else
        {
            return Random.Range(maxmin, maxmax+1);
        }
    }

    public void generateRamdom(int numObs)
    {
        int bouc = this.transform.childCount;
        for (int i = 0; i < bouc; i++)
        {
            Destroy(this.transform.GetChild(this.transform.childCount-1).gameObject);
        }
        for (int i = 0; i < numObs; i++)
        {

            float positionX = Random.Range(-50,50);
            float positionY = RandomPos(-50, -10, 10, 50);
            float positionZ = Random.Range(-50, 50);



            Vector3 position = new Vector3(positionX, positionY, positionZ);
            GameObject prefab = Instantiate(prefabOBAHV[0]);
            prefab.transform.parent = transform;
            prefab.transform.localPosition = position;
            prefab.transform.localRotation = Random.rotation;
        }
        for (int i = 0;i < numObs / 16; i++)
        {
            float positionX = Random.Range(-50, 50);
            float positionY = RandomPos(-50, -10, 10, 50);
            float positionZ = Random.Range(-50, 50);
            Vector3 position = new Vector3(positionX, positionY, positionZ);
            GameObject prefab = Instantiate(prefabOBAHV[1]);
            prefab.transform.parent = transform;
            prefab.transform.localPosition = position;
        }
        playerController.isselect = true;
    }
}
