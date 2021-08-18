using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    [SerializeField]
    private GameObject Base1, Base2, Mid1, Mid2, Mid3, Mid4, Mid5, High1, High2, High3, High4, High5, Top1, Top2, Top3;
    [SerializeField]
    private Transform Pos0, Pos1, Pos2, Pos3, Pos4, Pos5, Pos6, Pos7, Pos8, Pos9, Pos10;
    private GameObject player;
    [SerializeField]
    private GameObject level;


    List<GameObject> Base = new List <GameObject>();
    List<GameObject> Mid = new List<GameObject>();
    List<GameObject> High = new List<GameObject>();
    List<GameObject> Top = new List<GameObject>();

    private float baseHeight = 2.5f;
    private int midHeight = 5;
    private int highHeight = 4;
    private int topHeight = 8;
    private int groundHeight = 6;

    private bool active = true;

    private ObjectPoolScript objectPool;
    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPoolScript>();
        player = GameObject.FindWithTag("Player");
        Base.Add(Base1);
        Base.Add(Base2);

        Mid.Add(Mid1);
        Mid.Add(Mid2);
        Mid.Add(Mid3);
        Mid.Add(Mid4);
        Mid.Add(Mid5);

        High.Add(High1);
        High.Add(High2);
        High.Add(High3);
        High.Add(High4);
        High.Add(High5);

        Top.Add(Top1);
        Top.Add(Top2);
        Top.Add(Top3);

        

        InstantiateBuildings();


    }

    private void FixedUpdate()
    {
        if (active)
        {
            Vector3 dist = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z);
            if (dist.magnitude < 200)
            {
                InstantiateLevel();
                //objectPool.GetLevel();
            }
            if (dist.magnitude > 600)
            {
                objectPool.ReturnLevel(this.gameObject);
                //Destroy(gameObject);
            }
        }
    }
    private void InstantiateLevel()
    {
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;

        Vector3 pos1 = new Vector3(xPos + 200, yPos,zPos);
        Vector3 pos2 = new Vector3(xPos + 200, yPos, zPos+ 200);
        Vector3 pos3 = new Vector3(xPos + 200, yPos, zPos- 200);
                                                                  
        Vector3 pos4 = new Vector3(xPos, yPos, zPos + 200);
        Vector3 pos5 = new Vector3(xPos, yPos, zPos - 200);
                                                              
        Vector3 pos6 = new Vector3(xPos - 200, yPos, zPos);
        Vector3 pos7 = new Vector3(xPos - 200, yPos, zPos + 200);
        Vector3 pos8 = new Vector3(xPos - 200, yPos, zPos - 200);

        //Vector3 here = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        CheckIfEmpty(pos1);
        CheckIfEmpty(pos2);
        CheckIfEmpty(pos3);
        CheckIfEmpty(pos4);
        CheckIfEmpty(pos5);
        CheckIfEmpty(pos6);
        CheckIfEmpty(pos7);
        CheckIfEmpty(pos8);
        //active = false;

    }
    private void CheckIfEmpty(Vector3 pos)
    {
        RaycastHit hit;
        if (!(Physics.SphereCast(pos - new Vector3(0,5,0), 1, transform.up, out hit, 10)))
        {
            //Instantiate(level, pos, Quaternion.identity);
            GameObject newLevel = objectPool.GetLevel();
            newLevel.transform.position = pos;
        }
        else
        {
            //print("hit: " + hit.transform.gameObject.name);
        }

    }

    private void InstantiateBuildings()
    {
        SetBuildingAt(Pos1);
        SetBuildingAt(Pos2);
        SetBuildingAt(Pos3);
        SetBuildingAt(Pos4);
        SetBuildingAt(Pos5);
        SetBuildingAt(Pos6);
        SetBuildingAt(Pos7);
        SetBuildingAt(Pos8);
        SetBuildingAt(Pos9);
        SetBuildingAt(Pos10);
    }

    private void SetBuildingAt(Transform pos)
    {
        int baseRandom = Random.Range(1, (Base.Count));
        int midRandom = Random.Range(1, (Mid.Count));
        int highRandom = Random.Range(1, (High.Count));
        int topRandom = Random.Range(1, (Top.Count));

        int midLength = Random.Range(2, 6);
        int highLength = Random.Range(2, 5);


        Color32 rndomColor = colorRandom();
        GameObject b1 = Instantiate(Base[baseRandom], pos.position + new Vector3(1, groundHeight, 0), Base[baseRandom].transform.rotation);
        b1.transform.SetParent(pos);
        b1.GetComponent<Renderer>().material.color= rndomColor;

        for (int i = 0; i < midLength; i++)
        {
            GameObject m1 = Instantiate(Mid[midRandom], pos.position + new Vector3(0, (midHeight * ((2 * i) + 1)) + groundHeight, 0), Mid[midRandom].transform.rotation);
            m1.transform.SetParent(pos);
            m1.GetComponent<Renderer>().material.color = rndomColor;
            if (i == (midLength - 1))
            {
                for (int k = 0; k < highLength; k++)
                {
                    GameObject h1 = Instantiate(High[highRandom], pos.position + new Vector3(0, (midHeight * ((2 * i) + 1)) + midHeight + groundHeight + (highHeight * ((2 * k) + 1)), 0), High[highRandom].transform.rotation);
                    h1.transform.SetParent(pos);
                    h1.GetComponent<Renderer>().material.color = rndomColor;
                    /*if (k == (highLength - 1))
                    {
                        GameObject t1 = Instantiate(Top[topRandom], pos.position + new Vector3(0, (midHeight * ((2 * i) + 1)) + midHeight + groundHeight + (highHeight * ((2 * k) + 1)) + highHeight + topHeight, 0), Top[topRandom].transform.rotation);
                        t1.transform.SetParent(pos);
                        t1.GetComponent<Renderer>().material.color = colorRandom;
                    }*/
                }
            }
        }

    }
    private Color32 colorRandom()
    {
        if (Random.value > 0.5)
        {
            Color32 clr = new Color32((byte)Random.Range(205, 255), (byte)Random.Range(205, 255), (byte)Random.Range(205, 255), 255);
            return clr;
        }
        else
        {
            Color32 clr = new Color32((byte)Random.Range(0, 50), (byte)Random.Range(0, 50), (byte)Random.Range(0, 50), 255);
            return clr;
        }
       
    }

}
