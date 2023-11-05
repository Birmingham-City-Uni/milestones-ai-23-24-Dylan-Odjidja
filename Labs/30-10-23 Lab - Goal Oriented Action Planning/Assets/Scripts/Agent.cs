using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // For sorting

public class SubGoal // Helper class for agent
{
    public Dictionary<string, int> sgoals; // Nurse resting
    public bool remove; // Some agents need to be given a goal; when satisfied remove from thier intentions

    public SubGoal(string s, int i, bool r)
    {
        sgoals = new Dictionary<string, int>();
        sgoals.Add(s, i);
        remove = r;
    }
}

public class Agent : MonoBehaviour
{
    public List<Action> actions = new List<Action>(); // List of actions to perform
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
    Planner planner; // Returns a queue of actions
    Queue<Action> actionQueue;
    public Action currentAction;
    SubGoal currentGoal;

    public void Start()
    {
        Action[] acts = this.GetComponents<Action>(); // Can drag actions onto the agent
        foreach (Action a in acts)
        {
            actions.Add(a);
        }
    }
    bool invoked = false;

    void CompleteAction()
    {
        currentAction.running = false;
        currentAction.PostPerform();
        invoked = false;
    }

    void LastUpdate()
    {
        if (currentAction != null && currentAction.running)
        {
            // Navmesh code 
            if (currentAction.agent.hasPath && currentAction.agent.remainingDistance < 1f)
            {
                if (!invoked)
                {
                    Invoke("CompleteAction", currentAction.duration);
                    invoked = true;
                }
            }
            return;
        }

        if (planner == null || actionQueue == null)
        {
            planner = new Planner();
            var sortedGoals = from entry in goals orderby entry.Value descending select entry;
            foreach (KeyValuePair<SubGoal, int> sg in sortedGoals)
            {
                actionQueue = planner.plan(actions, sg.Key.sgoals, null);
                if (actionQueue != null)
                {
                    currentGoal = sg.Key;
                    break;
                }
            }
        }

        // Have we done all actions?
        if (actionQueue != null && actionQueue.Count == 0)
        {
            // If goal working on is removable
            if (currentGoal.remove)
            {
                goals.Remove(currentGoal);
            }
            planner = null;
        }

        // If action queue and we still have actions to do
        if (actionQueue != null && actionQueue.Count > 0)
        {
            currentAction = actionQueue.Dequeue();
            // Check cubicles availabe etc
            if (currentAction.PrePerform())
            {
                if (currentAction.target == null && currentAction.targetTag != "")
                {
                    currentAction.target = GameObject.FindWithTag(currentAction.targetTag);
                }
                if (currentAction.target != null)
                {
                    // Actions starts to take place
                    currentAction.running = true;
                    currentAction.agent.SetDestination(currentAction.target.transform.position);
                }
                // If preperform fails
                else
                {
                    actionQueue = null;
                }
            }
        }
    }
}
