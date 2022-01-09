using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Grid<bool> grid;
    private void Start()
    {
        //grid = new Grid<bool>(4, 2,10f,new Vector3(20,0),()=>new bool());
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            grid.SetGridObject(GetMouseWorldPos(), true);
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vec.z = 0f;
        return vec;
    }
}
