using UnityEngine;

public class TilemapFloating : MonoBehaviour
{
    public Transform tilemap; // Kéo Tilemap vào đây
    public float floatSpeed = 2f; // Tốc độ bay
    public float floatHeight = 0.5f; // Độ cao bay

    private Vector3 startPos;

    void Start()
    {
        if (tilemap != null)
        {
            startPos = tilemap.position; // Lưu vị trí ban đầu
        }
    }

    void Update()
    {
        if (tilemap != null)
        {
            // Tạo hiệu ứng bay lên xuống bằng Sin()
            float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            tilemap.position = new Vector3(startPos.x, newY, startPos.z);
        }
    }
}
