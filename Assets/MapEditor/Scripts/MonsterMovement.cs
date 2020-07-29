using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float speed;
    private Road road;
    private Vector2 target;
    private int index;
    private bool moving;
    // Start is called before the first frame update
    void Start()
    {
        road = Road.instance;
        target = GetNextTarget();
        moving = true;
        this.transform.position = road.transform.GetChild(0).transform.position + new Vector3(-2, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving)
            return;
        // movement
        Vector2 dir = target - (Vector2)this.transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector2.Distance(this.transform.position, target) < 0.05f)
        {
             target = GetNextTarget();
        }
        // rotation Z
        if(dir.normalized.x > 0.5f)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if(dir.normalized.x < -0.5f)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        if(dir.normalized.y > 0.5f)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        if(dir.normalized.y < -0.5f)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 270);
        }
    }

    //next target
    private Vector2 GetNextTarget()
    {
        if(index == road.gameObject.transform.childCount)
        {
            moving = false;
            return Vector2.zero;
        }
        if(road.gameObject.transform.childCount == 0)
        {
            moving = false;
            return Vector2.zero;
        }
        return road.gameObject.transform.GetChild(index++).position;
    }
}
