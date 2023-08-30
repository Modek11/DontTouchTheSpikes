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
    }

    private void UpdateVisuals(bool isFlyingRight)
    {
        bird.flipX = !isFlyingRight;
    }
}
