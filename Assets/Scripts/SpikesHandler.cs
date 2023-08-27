using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpikesHandler : MonoBehaviour
{
    [SerializeField] private GameObject leftSpikes;
    [SerializeField] private GameObject rightSpikes;
    
    private GameObject activeSpikes;
    private List<GameObject> spikesToChangePosition = new List<GameObject>();
    
    private float countdown = 0;
    private int spikesToActivate = 5;
    
    void Update()
    {
        SpikeCounter();
    }

    private void ChangeSpikes()
    {
        while (spikesToChangePosition.Count < spikesToActivate)
        {
            var spike = activeSpikes.transform.GetChild(Random.Range(0, 12)).gameObject;
            if(!spikesToChangePosition.Contains(spike))
                spikesToChangePosition.Add(spike);
        }

        foreach (var spike in spikesToChangePosition)
        {
            //To add spikes movement when proper spikes are choosen
            //Vector2.MoveTowards()
        }
    }

    private void SidePicker()
    {
        activeSpikes = activeSpikes != rightSpikes ? rightSpikes : leftSpikes;
        ChangeSpikes();
    }

    private void SpikeCounter()
    {
        countdown -= Time.deltaTime;
        if (countdown < 0)
        {
            SidePicker();
            countdown = 3;
        }
    }
}
