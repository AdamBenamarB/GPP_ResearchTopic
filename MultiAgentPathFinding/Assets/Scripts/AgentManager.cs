using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager
{
    //public List<Agent> agents;
    public List<GameObject> agentObj;
    public bool processing = false;
    //public List<List<Vector3>> agentPaths;
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
    public AgentManager()
    {
        //agents = new List<Agent>();
        agentObj = new List<GameObject>();
        //agentPaths = new List<List<Vector3>>();
    }
    public void AddAgent(Vector3 startLoc,Grid<PathNode> grid)
    {
        //int length = agents.Count;
        //int length = agentObj.Count;
        //if (length>0)
        //{
        //    //agents[length] = new Agent(startLoc,agents[0].pathfinding.GetGrid());
        //    GameObject agent = new GameObject("Agent");
        //    agent.AddComponent<Agent>();
        //    agent.GetComponent<Agent>().SetLoc(startLoc);
        //    agent.GetComponent<Agent>().SetPathFinding(agentObj[0].GetComponent<Agent>().pathfinding.GetGrid());
        //    agentObj.Add(agent);
        //}
        //else
        //{
        //    //agents.Add( new Agent(startLoc));
        //    GameObject agent = new GameObject("Agent");
        //    agent.AddComponent<Agent>();
        //    agent.GetComponent<Agent>().SetLoc(startLoc);
        //    agent.GetComponent<Agent>().SetPathFinding();
        //    agentObj.Add(agent);

        //}
        GameObject agent = new GameObject("Agent");
        agent.AddComponent<Agent>();
        agent.GetComponent<Agent>().SetLoc(startLoc);
        agent.GetComponent<Agent>().SetPathFinding(grid);
        agent.GetComponent<Agent>().SetMgr(this);
        agentObj.Add(agent);
    }

    public void SetGoal(int idx,Vector3 goal)
    {
        //agents[idx].SetGoal(goal);
        agentObj[idx].GetComponent<Agent>().SetGoal(goal);
    }

    public void SetGoal(Vector3 goal)
    {
        //agents[idx].SetGoal(goal);
        int idx = 0;
        if(agentObj.Count>0)
        {
            idx = agentObj.Count - 1;
        }
        agentObj[idx].GetComponent<Agent>().SetGoal(goal);
    }

    public void FindPaths()
    {
        Vector3 avoidPoint=Vector3.zero;
        bool collided = false;
        bool unresolved = true;
        int collidedActorIdx0 = 0;
        //int collidedActorIdx1 = 1;
        int nrOfTries = 0;
        processing = true;
        //for (int x=0;x<agents.Count;x++)
        for(int x=0;x<agentObj.Count;x++)
        {
            //agents[x].FindPath();
            agentObj[x].GetComponent<Agent>().FindPath();
            //int lastCount = lastList.Count;
            //int newCount = newList.Count;
            //if(lastCount>newCount)
            //{
            //    lastCount = newCount;
            //}
            //for (int y=0;y<lastCount;y++)
            //{
            //    if(lastList[y]==newList[y])
            //    {
            //        avoidPoint = lastList[y];
            //        collided = true;
            //    }
            //}
        }
        while(unresolved&&nrOfTries<20)
        {
            List<Vector3> avoidPoints = new List<Vector3>();
            //for(int y=0;y<agents.Count-1;y++)
              for (int y = 0; y < agentObj.Count; y++)
                {
                    List<Vector3> path = agentObj[y].GetComponent<Agent>().pathList;
                    if (path != null)
                    {
                        for (int i = 0; i < path.Count - 1; i++)
                        {
                            // Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                            Debug.DrawLine(path[i], path[i + 1], Color.red, 5f);
                        }
                    }
                //for(int z=1;z<agents.Count;z++)
                for(int z=y+1;z<agentObj.Count;z++)
                {
                    List<Vector3> path2 = agentObj[z].GetComponent<Agent>().pathList;
                    if (path2 != null)
                    {
                        for (int i = 0; i < path2.Count - 1; i++)
                        {
                            // Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                            Debug.DrawLine(path2[i], path2[i + 1], Color.green, 5f);
                        }
                    }
                    int shortest = Mathf.Min(agentObj[y].GetComponent<Agent>().pathList.Count, agentObj[z].GetComponent<Agent>().pathList.Count);
                    for(int a=0;a<shortest-1;a++)
                    {
                        Vector3 intersect = Vector3.zero;
                        if (AreIntersecting(agentObj[y].GetComponent<Agent>().pathList[a], agentObj[y].GetComponent<Agent>().pathList[a + 1],
                            agentObj[z].GetComponent<Agent>().pathList[a], agentObj[z].GetComponent<Agent>().pathList[a + 1], out intersect))
                        {
                            if (!avoidPoints.Contains(intersect))
                            {

                                collided = true;
                                //avoidPoint = intersect;
                                avoidPoints.Add(intersect);
                                Debug.Log(intersect);
                                collidedActorIdx0 = y;
                                //collidedActorIdx1 = z;
                                GameObject target = new GameObject("Collision", typeof(Collision));
                                target.transform.position = intersect;
                            }
                        }
                        //if (agentObj[y].GetComponent<Agent>().pathList[a] == agentObj[z].GetComponent<Agent>().pathList[a])
                        //{
                        //    collided = true;
                        //    avoidPoints.Add(agentObj[y].GetComponent<Agent>().pathList[a]);
                        //    GameObject target = new GameObject("Target", typeof(Target));
                        //    target.transform.position = agentObj[y].GetComponent<Agent>().pathList[a];
                        //    collidedActorIdx0 = z;
                        //}
                    }
                }
            }
            if(!collided)
            {
                unresolved = false;
                for(int i=0;i<agentObj.Count;i++)
                {
                    agentObj[i].GetComponent<Agent>().SetMoving(true);
                }
                processing = false;
            }
            else
            {
                agentObj[collidedActorIdx0].GetComponent<Agent>().FindPath(avoidPoints);
                //agentObj[collidedActorIdx1].GetComponent<Agent>().FindPath(avoidPoint);
                collided =false;
                nrOfTries++;
             }

        }
    }

    public bool AreIntersecting(Vector3 a1, Vector3 a2, Vector3 b1, Vector3 b2,out Vector3 intersection)
    {
        //float det, gamma, lambda;
        //intersection = Vector3.zero;
        //det = (a2.x - a1.x) * (b2.y - b1.y) - (b2.x - b1.x) * (a2.y - a1.y);
        //if(det==0)
        //{
        //    return false;
        //}
        //else
        //{
        //    lambda = ((b2.y - b1.y) * (b2.x - a1.x) + (b1.x - b2.x) * (b2.y - a1.y)) / det;
        //    gamma = ((a1.y - a2.y) * (b2.x - a1.x) + (a2.x - a1.x) * (b2.y - a1.y)) / det;
        //    intersection = a1 + a2 * lambda;
        //    Debug.Log(intersection);
        //    return (-0.01 < lambda && lambda < 1.01) && (-0.01 < gamma && gamma < 1.01);
        //}
        intersection = Vector3.zero;
        Vector3 E = new Vector3(a2.x - a1.x, a2.y - a1.y);
        Vector3 F = new Vector3(b2.x - b1.x, b2.y - b1.y);
        Vector3 P = new Vector3(-E.y, E.x);
        float h = Vector3.Dot((a1-b1), P)/ Vector3.Dot(F,P);
        if(0<h&&h<1)
        {

        intersection = b1 + F * h;
        return true;
        }
        else
        {
            return false;
        }
        
    }
}
