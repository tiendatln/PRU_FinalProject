using UnityEngine;

public class WaveAnimation : MonoBehaviour
{
    public float speed = 1.0f;

    public float offset = 0.0f;

    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * speed;

        spriteRenderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
