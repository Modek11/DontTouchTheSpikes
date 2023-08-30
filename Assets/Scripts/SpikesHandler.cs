using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpikesHandler : MonoBehaviour
{
    [SerializeField] private GameObject leftSpikes;
    [SerializeField] private GameObject rightSpikes;
    
    private GameObject activeSpikes;
    private List<GameObject> spikesToChangePosition = new List<GameObject>();
    private float spikeShowValueX = -2.6f;
    private float spikeHideValueX = -3.1f;
    private float spikeShowHideSpeed = 0.1f;
    private int spikesToActivate = 5;


    private void Start()
    {
        GameHandler.Instance.OnPlayerHitWall += SidePicker;
    }

    private void ChangeSpikes()
    {
        while (spikesToChangePosition.Count < spikesToActivate)
        {
            var spike = activeSpikes.transform.GetChild(Random.Range(0, 11)).gameObject;
            if(!spikesToChangePosition.Contains(spike))
                spikesToChangePosition.Add(spike);
        }

        foreach (var spike in spikesToChangePosition)
        {
            StartCoroutine(ActivateSpike(spike));
        }
        spikesToChangePosition.Clear();
        
        //TODO: hide spikes 
    }

    private void SidePicker(bool isFlyingRight)
    {
        activeSpikes = isFlyingRight ? rightSpikes : leftSpikes;
        ChangeSpikes();
        
        Debug.Log(activeSpikes.name);
    }

    private IEnumerator ActivateSpike(GameObject spike)
    {
        while (Math.Abs(spike.transform.localPosition.x - spikeShowValueX) > 0.005f)
        {
            spike.transform.localPosition += Vector3.right * 0.01f;
            yield return new WaitForSeconds(.005f);
        }
    }


}
