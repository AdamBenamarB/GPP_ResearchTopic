using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Grid<TGridObject>
{
    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    private int width;
    private int height;
    private float cellSize;
    private TGridObject[,] gridArray;
    //private TextMesh[,] debugTextArray;
    private Vector3 originPos;

    public Grid(int width, int height,float cellSize,Vector3 originPos, Func<Grid<TGridObject>,int, int, TGridObject> createGridObject)
    {
   

        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPos = originPos;
        gridArray = new TGridObject[width, height];
        //debugTextArray = new TextMesh[width, height];

        for (int x=0;x<gridArray.GetLength(0);x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                // Debug.Log(x + ", " + y);
                gridArray[x, y] = createGridObject(this,x,y);
                //debugTextArray[x,y]=CreateWorldText(null,gridArray[x, y].ToString(), GetWorldPosition(x, y)+new Vector3(cellSize,cellSize)*.5f, 20, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x+1, y), Color.white, 100f);
            }
        }

        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width,height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width,height), Color.white, 100f);

        OnGridObjectChanged += (object sender, OnGridObjectChangedEventArgs eventArgs) =>
        {
            //debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
        };

 }



    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize+originPos;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition-originPos).x / cellSize);
        y = Mathf.FloorToInt((worldPosition-originPos).y / cellSize);
    }

    public void SetGridObject(int x,int y, TGridObject value)
    {
        if(x>=0&&y>=0&&x<width&&y<height)
        {
            gridArray[x, y] = value;
            //debugTextArray[x, y].text = gridArray[x, y].ToString();
            if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
        }
    }


    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x, y, value);

    }
    public void TriggerGridObjectChanged(int x, int y)
    {
        if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
        Debug.Log("Test");
    }
    
    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y]; 
        }
        else
        {
            return default(TGridObject);
        }
    }

    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);

    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public float GetCellSize()
    {
        return cellSize;
    }

    //HELPER
    private TextMesh CreateWorldText(Transform parent, string txt, Vector3 localPos, int fontSize, Color color, TextAnchor txtAnch)
    {
        GameObject gameObj = new GameObject("World Text", typeof(TextMesh));
        Transform transform = gameObj.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPos;
        TextMesh textMesh = gameObj.GetComponent<TextMesh>();
        textMesh.anchor = txtAnch;
        //textMesh.alignment = txtAlign;
        textMesh.text = txt;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        // textMesh.GetComponent<MeshRenderer>().sortingOrder = sortOrder;
        return textMesh;
    }
}