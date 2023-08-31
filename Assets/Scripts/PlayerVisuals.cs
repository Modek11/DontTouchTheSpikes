using System.Collections;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    private SpriteRenderer bird;
    

    private void Awake()
    {
        bird = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        GameHandler.Instance.OnPlayerHitWall += UpdateVisuals;
        GameHandler.Instance.OnPlayerDie += FadeOutPlayerStarter;
    }

    private void UpdateVisuals(bool isFlyingRight)
    {
        bird.flipX = !isFlyingRight;
    }
    
    private void FadeOutPlayerStarter()
    {
        StartCoroutine(FadeOutPlayer());
    }

    private IEnumerator FadeOutPlayer()
    {
        yield return new WaitForSeconds(1f);
        float alpha = 1;
        while (bird.color.a > 0)
        {
            Debug.Log(alpha);
            alpha -= 0.01f;
            bird.color = new Color(1, 1, 1, alpha);
            yield return new WaitForSeconds(.001f);
        }
    }
}
