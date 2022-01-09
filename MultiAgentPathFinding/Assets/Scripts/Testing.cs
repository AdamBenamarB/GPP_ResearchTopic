using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private PathVisuals pathVisual;
    private Pathfinding pathfinding;
    private AgentManager agentMgr;
    private int AgentIdx=0;
    private bool placing = true;
    private bool started = false;
    // Start is called before the first frame update
    private void Start()
    {
        //pathVisual.SetGrid(pathfinding.GetGrid());
        pathfinding = new Pathfinding(20, 10);
        agentMgr = new AgentManager();
        //agentMgr.AddAgent(Vector3.zero);
    }

    //private void Update()
    //{
    //    //switch()
    //    if (Input.GetMouseButtonDown(0))
    //    {

    //        Vector3 mouseWorldPosition = GetMouseWorldPos();
    //        // pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
    //        //List<PathNode> path = pathfinding.FindPath(0, 0, x, y);

    //        List<Vector3> path = agentMgr.agentObj[0].GetComponent<Agent>().pathList;
    //        if (path != null)
    //        {
    //            for (int i = 0; i < path.Count - 1; i++)
    //            {
    //               // Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
    //                Debug.DrawLine(path[i], path[i + 1],Color.green,5f);
    //            }
    //        }
    //    }
    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        Vector3 mouseWorldPosition = GetMouseWorldPos();
    //        agentMgr.SetGoal(0, mouseWorldPosition);
    //        agentMgr.FindPaths();
    //        //Vector3 mouseWorldPosition = GetMouseWorldPos();
    //        //pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
    //        //pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
    //    }
    //}
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(placing)
            {
                agentMgr.AddAgent(GetMouseWorldPos(), pathfinding.GetGrid());
                placing = false;
            }
            else
            {
                agentMgr.SetGoal(GetMouseWorldPos());
                //AgentIdx++;
                placing = true;
            GameObject target = new GameObject("Target",typeof(Target));
            target.transform.position = GetMouseWorldPos();
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPosition = GetMouseWorldPos();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
        }
        if (Input.GetKeyDown("space"))
        {
            agentMgr.FindPaths();
            //started = true;
            //for (int x = 0; x < agentMgr.agentObj.Count; x++)
            //{
            //    List<Vector3> path = agentMgr.agentObj[x].GetComponent<Agent>().pathList;
            //    if (path != null)
            //    {
            //        for (int i = 0; i < path.Count - 1; i++)
            //        {
            //            // Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
            //            Debug.DrawLine(path[i], path[i + 1], Color.green, 5f);
            //        }
            //    }
            //}


        }
        //if(started)
        //{
        //    if(!agentMgr.processing)
        //    {
        //    for(int x=0;x<agentMgr.agentObj.Count;x++)
        //    {
        //        List<Vector3> path = agentMgr.agentObj[x].GetComponent<Agent>().pathList;
        //        if (path != null)
        //        {
        //            for (int i = 0; i < path.Count - 1; i++)
        //            {
        //                // Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
        //                Debug.DrawLine(path[i], path[i + 1], Color.green, 5f);
        //            }
        //        }
        //    }
        //        started = false;

        //    }

        //}
    }
    private Vector3 GetMouseWorldPos()
    {
        Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vec.z = 0f;
        return vec;
    }

    
}
