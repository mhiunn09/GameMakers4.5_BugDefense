using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public static Road instance;

    public GameObject tile;
    public int col;
    public int row;
    public Vector2 firstTilePos;
    public SpriteRenderer[] nextSprites;
    private bool[,] grids;
    private Stack<GameObject> stack;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        grids = new bool[col, row];
        for (int i = 0; i < col; i++)
        {
            for (int k = 0; k < row; k++)
            {
                grids[i, k] = true;
            }
        }
        stack = new Stack<GameObject>();
        MakeRoad(firstTilePos);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int x = (int)Mathf.Round(pos.x);
        int y = (int)Mathf.Round(pos.y);
        pos = new Vector2(x, y);

        if (x < 0 || x >= col || y < 0 || y >= row)
           return;

        if (Input.GetMouseButtonDown(0))
        {
            MakeRoad(pos);
        }
    }

    //해당 좌표에 길 만들기.
    public void MakeRoad(Vector3 _pos)
    {
        int x = (int)_pos.x;
        int y = (int)_pos.y;

        //first road
        if (this.transform.childCount == 0)
        {
            grids[x, y] = false;
            GameObject go = Instantiate(tile, _pos, Quaternion.identity, this.transform);
            stack.Push(go);
            ShowNext(stack.Peek().transform.position);
            return;
        }
        //make road
        int rangeX = (int)Mathf.Abs(_pos.x - stack.Peek().transform.position.x);
        int rangeY = (int)Mathf.Abs(_pos.y - stack.Peek().transform.position.y);

        if(rangeY == 1 && rangeX == 1)
        {
            return;
        }
        if (grids[x, y] == true && rangeX < 2 && rangeY < 2)
        {
            grids[x , y] = false;
            GameObject go = Instantiate(tile, _pos, Quaternion.identity, this.transform);
            stack.Push(go);
            ChangeAdjoin();
            ShowNext(stack.Peek().transform.position);
        }
        else
        {
            return;
        }
    }

    //이전 블럭 만들수 있는곳 체크하는 곳.
    public void ChangeAdjoin()
    {

        int x = (int)this.transform.GetChild(this.transform.childCount - 2).transform.position.x;
        int y = (int)this.transform.GetChild(this.transform.childCount - 2).transform.position.y;
        if(x != 0)
        {
            grids[x - 1, y] = false;

        }
        if(y != 0)
        {
            grids[x, y - 1] = false;
        }
        if(x != grids.GetLength(0) - 1)
        {
            grids[x + 1, y] = false;

        }
        if( y != grids.GetLength(1) - 1)
        {
            grids[x, y + 1] = false;

        }
    }

    //다음 지역 보여주는거
    public void ShowNext(Vector2 _currentTile)
    {
        
        int x = (int)_currentTile.x;
        int y = (int)_currentTile.y;

        for(int i=0; i< nextSprites.Length; i++)
        {
            nextSprites[i].color = new Color(255, 255, 255, 0); //hide
        }

        //상
        if (y != grids.GetLength(1) - 1 && grids[x, y+1] == true )
        {
            nextSprites[0].color = new Color(255, 255, 255, 0.2f);
            nextSprites[0].transform.position = _currentTile + new Vector2(0, 1);
        }
        //하
        if (y != 0 && grids[x, y-1] == true)
        {
            nextSprites[1].color = new Color(255, 255, 255, 0.2f);
            nextSprites[1].transform.position = _currentTile + new Vector2(0, -1);
        }
        //좌
        if ( x != 0 && grids[x-1, y] == true )
        {
            nextSprites[2].color = new Color(255, 255, 255, 0.2f);
            nextSprites[2].transform.position = _currentTile + new Vector2(-1, 0);
        }
        //우
        if (x != grids.GetLength(0) - 1 && grids[x+1, y] == true )
        {
            nextSprites[3].color = new Color(255, 255, 255, 0.2f);
            nextSprites[3].transform.position = _currentTile + new Vector2(1, 0);
        }

    }

    public void DeleteTile()
    {
        if (this.transform.childCount == 0)
            return;

        //remove last road
        Destroy(stack.Pop().gameObject);
        CheckRoad();
        ShowNext(stack.Peek().transform.position);
    }

    //처음부터 끝까지 체크하는것.
    public void CheckRoad()
    {
        for (int i = 0; i < col; i++)
        {
            for (int k = 0; k < row; k++)
            {
                grids[i, k] = true;
            }
        }

        for (int i = 0; i < transform.childCount - 2; i++)
        {
            int x = (int)transform.GetChild(i).transform.position.x;
            int y = (int)transform.GetChild(i).transform.position.y;
            if (x != 0)
            {
                grids[x - 1, y] = false;

            }
            if (y != 0)
            {
                grids[x, y - 1] = false;
            }
            if (x != grids.GetLength(0) - 1)
            {
                grids[x + 1, y] = false;

            }
            if (y != grids.GetLength(1) - 1)
            {
                grids[x, y + 1] = false;

            }
        }
    }
}
