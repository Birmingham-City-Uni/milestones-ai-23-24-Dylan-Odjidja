using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Rendering;

public class Node
{
    // Creates a set of nodes that are linked - these points back to actions based on preconditions and effects
    public Node parent;
    public float cost;
    public Dictionary<string, int> state; // Holds states, world state in root node
    public Action action; // Node points to this action

    public Node(Node parent, float cost, Dictionary<string, int> allstates, Action action)
    {
        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(allstates); // Copy constructor
        this.action = action;
    }
}

public class Planner
{
    public Queue<Action> plan(List<Action> actions, Dictionary<string, int> goal, WorldStates states)
    {
        List<Action> usableActions = new List<Action>();
        foreach (Action a in actions)
        {
            if (a.IsAchievable())
            {
                usableActions.Add(a);
            }
        }
        List<Node> leaves = new List<Node>();
        Node start = new Node(null, 0, World.Instance.GetWorld().GetStates(), null);

        bool success = BuildGraph(start, leaves, usableActions, goal);
        if (!success)
        {
            Debug.Log("No plan found, whoops");
            return null; // Return no plan if none found
        }
        // Have found a plan so find the cheapest one
        Node cheapest = null;
        foreach (Node leaf in leaves)
        {
            if (cheapest == null)
            {
                cheapest = leaf;
            }
            else
            {
                if (leaf.cost < cheapest.cost)
                {
                    cheapest = leaf;
                }
            }
            List<Action> result = new List<Action>();
            Node n = cheapest;
            while (n != null)
            {
                // Only null when hit starting node
                if (n.action != null)
                {
                    result.Insert(0, n.action);
                }
                n = n.parent;
            }
            Queue<Action> queue = new Queue<Action>();
            // Transition from list to queue to return
            foreach (Action a in result)
            {
                queue.Enqueue(a);
            }
            Debug.Log("The Plan is: ");
            foreach (Action a in queue)
            {
                Debug.Log("Q: " + a.actionName);
            }
            return queue;
        }
        return null;
    }

    private bool BuildGraph(Node parent, List<Node> leaves, List<Action> usableActions, Dictionary<string, int> goal)
    {
        bool foundPath = false;
        foreach (Action action in usableActions)
        {
            if (action.IsAchievableGiven(parent.state))
            {
                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);

                foreach (KeyValuePair<string, int> eff in action.effects)
                {
                    if (!currentState.ContainsKey(eff.Key))
                    {
                        currentState.Add(eff.Key, eff.Value);
                    }
                }

                Node node = new Node(parent, parent.cost + action.cost, currentState, action);

                if (GoalAchieved(goal, currentState))
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                else
                {
                    List<Action> subset = ActionSubset(usableActions, action);
                    bool found = BuildGraph(node, leaves, subset, goal);

                    if (found)
                    {
                        foundPath = true;
                    }
                }
            }
        }
        return foundPath;
    }

    private List<Action> ActionSubset(List<Action> actions, Action removeMe)
    {
        List<Action> subset = new List<Action>();

        foreach (Action a in actions)
        {
            if (!a.Equals(removeMe))
            {
                subset.Add(a);
            }
        }
        return subset;
    }

    private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state)
    {
        foreach (KeyValuePair<string, int> g in goal)
        {
            if (!state.ContainsKey(g.Key))
            {
                return false;
            }
        }
        return true;
    }
}
