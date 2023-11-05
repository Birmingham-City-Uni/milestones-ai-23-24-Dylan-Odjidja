using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public abstract class Action : MonoBehaviour // Pertains to the details of the action
{
    public string actionName = "Action";
    public float cost = 1.0f;
    public GameObject target; // Location where action will happen
    public string targetTag; // Piuckup game object using tag - if it exists in the hierachy
    public float duration = 0; // How long with this action take?
    public WorldState[] preConditions; // Get via inspect (serializable) - put into dictionaries
    public WorldState[] afterEffects;
    public NavMeshAgent agent; // Attached to agent for movement
    public Dictionary<string, int> preconditions;
    public Dictionary<string, int> effects;
    public WorldStates agentBeliefs;
    public bool running = false; // Default false - need to know uf we're running the action

    public Action()
    {
        preconditions = new Dictionary<string, int>();
        effects = new Dictionary<string, int>();
    }

    public void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        if (preConditions != null)
        {
            foreach (WorldState w in preConditions)
            {
                preconditions.Add(w.key, w.value);
            }
        }
        if (afterEffects != null)
        {
            foreach (WorldState w in afterEffects)
            {
                effects.Add(w.key, w.value);
            }
        }
    }

    public bool IsAchievable()
    {
        return true;
    }

    public bool IsAchievableGiven(Dictionary<string, int> conditions)
    {
        foreach (KeyValuePair<string, int> p in preconditions)
        {
            if (!conditions.ContainsKey(p.Key)) return false;
        }
        return true;
    }

    // Allow custom code to ensute things can be done before and after the acton
    public abstract bool PrePerform();
    public abstract bool PostPerform();
}
