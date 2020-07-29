using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject monsterPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DeleteRoadBtn()
    {
        Road.instance.DeleteTile();
    }

    public void MakeMonster()
    {
        Instantiate(monsterPrefab);
    }
}
