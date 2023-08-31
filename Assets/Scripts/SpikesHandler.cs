using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesHandler : MonoBehaviour
{
    [SerializeField] private GameObject leftSpikes;
    [SerializeField] private GameObject rightSpikes;
    
    private GameObject activeSpikes;
    private List<GameObject> spikesToChangePosition = new List<GameObject>();
    private float spikeShowValueX = -2.6f;
    private float spikeHideValueX = -3.1f;
    private float spikeShowHideSpeed = 0.01f;
    private int spikesToActivate = 5;


    private void Start()
    {
        GameHandler.Instance.OnPlayerHitWall += SidePicker;
        SidePicker(true);
    }

    private void ChangeSpikes()
    {
        //Deactivate all spikes
        foreach (var spike in spikesToChangePosition)
        {
            StartCoroutine(DeactivateSpike(spike));
        }
        spikesToChangePosition.Clear();
        
        //Choose choose spikes to show
        while (spikesToChangePosition.Count < spikesToActivate)
        {
            var spike = activeSpikes.transform.GetChild(Random.Range(0, 11)).gameObject;
            if(!spikesToChangePosition.Contains(spike))
                spikesToChangePosition.Add(spike);
        }

        //Activate all chosen spikes
        foreach (var spike in spikesToChangePosition)
        {
            StartCoroutine(ActivateSpike(spike));
        } 
    }

    private void SidePicker(bool isFlyingRight)
    {
        activeSpikes = isFlyingRight ? rightSpikes : leftSpikes;
        ChangeSpikes();
    }

    private IEnumerator ActivateSpike(GameObject spike)
    {
        while (Mathf.Abs(spike.transform.localPosition.x - spikeShowValueX) > 0.005f)
        {
            spike.transform.localPosition += Vector3.right * spikeShowHideSpeed;
            yield return new WaitForSeconds(.005f);
        }
    }
    
    private IEnumerator DeactivateSpike(GameObject spike)
    {
        while (Mathf.Abs(spike.transform.localPosition.x - spikeHideValueX) > 0.005f)
        {
            spike.transform.localPosition += Vector3.left * spikeShowHideSpeed;
            yield return new WaitForSeconds(.005f);
        }
    }


}
