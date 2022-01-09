# Multi agent pathfinding

Having multiple agents find their path to their goal while not colliding with eachother.

## Demo

Pathfinding of 2 agents without collision

![Demo - No Collision](./NoCol.gif)

Pathfinding of 2 agents with collision and path recalulation

![Demo - Collision](./Col.gif)




## How does it work

#### A* pathfinding

At first every agent calculates it's individual path 


#### Collision checks

After every agent has calculated its path then it checks each agents path with the other paths for collisions. (collisions only happen if they are at a point at the same time of course)


#### Recalculate path

When a collision occurs recalculate the path for an agent that collided while avoiding the collision point. Then check for collisions again and repeat this until no collisions are present.



## Implementation

This demo was implemented using Unity with C#.


#### Internal Structure

Each agent has its own list of Vectors which make a path, everytime we check for collisions the path lists of each agent gets compared to all the other agents path lists, a collision happens when a two path segments(consisting of 2 Vectors), the same amount of steps from their startpoint intersect eachother. That intersection gets stored in a list of vectors of points to avoid in the next path calculation.

#### Requirements

- Unity

## Results

The agents correctly recalculate paths but sometimes it keeps colliding which makes it keep calculating paths forever.

## Useful Links

#### Multi-Agent Pathfinding
Multi agent pathfinding:
https://ai.vub.ac.be/multi-agent-path-finding/?utm_source=www.google.com&utm_medium=organic&utm_campaign=Google&referrer-analytics=1
Conflict-based search for optimal multi-agent pathfinding: https://www.sciencedirect.com/science/article/pii/S0004370214001386