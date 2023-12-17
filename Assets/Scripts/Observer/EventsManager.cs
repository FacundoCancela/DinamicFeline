using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager
{

    public static EventsManager Instance
    {
        get
        {

            if (instance == null)
                instance = new EventsManager();
            return instance;
        }
    }

    private static EventsManager instance;


    private Dictionary<string, List<Ilistener>> eventsList = new();

    public void AddListener(string eventID, Ilistener p_listener)
    {
        if (eventsList.TryGetValue(eventID, out var listeners) && !listeners.Contains(p_listener))
        {
            listeners.Add(p_listener);
        }
    }

    public void RemoveListener(string eventID, Ilistener p_listener)
    {
        if (eventsList.TryGetValue(eventID, out var listeners) &&
                 listeners.Contains(p_listener))
        {
            listeners.Remove(p_listener);
        }
    }

    public void DispatchEvents(string eventID)
    {
        if (eventsList.TryGetValue(eventID, out var listener))
        {
            for (int i = listener.Count - 1; i >= 0; i--)
            {
                listener[i].OnEventDispatch();
            }
        }
    }

    public void RegisterEvent(string eventID)
    {
        if (!eventsList.ContainsKey(eventID))
        {
            eventsList[eventID] = new List<Ilistener>();
        }
    }




}