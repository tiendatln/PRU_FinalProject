using System.Collections;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private bool isPressed = false;
    private WheelJoint2D wheelJoint;
    private JointSuspension2D suspension;

    public GameObject block4;  // Chướng ngại vật cần di chuyển
    private Vector2 originalButtonPos; // Vị trí ban đầu của Button
    public float pressedThreshold = 0.2f; // Độ nén tối thiểu để kích hoạt

    void Start()
    {
        wheelJoint = GetComponent<WheelJoint2D>();
        suspension = wheelJoint.suspension;

        block4 = GameObject.Find("Block4");

        originalButtonPos = transform.position;
    }

    void Update()
    {
        // Kiểm tra nếu nút bị nén xuống đủ sâu
        if (!isPressed && transform.position.y < originalButtonPos.y - pressedThreshold)
        {
            isPressed = true;
            suspension.frequency = 1; // Làm nút bị nén xuống
            wheelJoint.suspension = suspension;

            StartCoroutine(MoveObstacle()); // Bắt đầu di chuyển block4 từ từ
        }

        // Khi nút trở lại vị trí ban đầu, đặt lại trạng thái
        if (isPressed && transform.position.y >= originalButtonPos.y - pressedThreshold)
        {
            isPressed = false;
            suspension.frequency = 3; // Đặt lại trạng thái đàn hồi
            wheelJoint.suspension = suspension;
        }
    }

    IEnumerator MoveObstacle()
    {
        if (block4 != null)
        {
            Rigidbody2D rb = block4.GetComponent<Rigidbody2D>();

            // Di chuyển block4 dần lên trên
            float targetY = block4.transform.position.y + 3f;
            while (block4.transform.position.y < targetY)
            {
                rb.linearVelocity = new Vector2(0f, 2f); // Điều chỉnh tốc độ di chuyển
                yield return new WaitForSeconds(0.02f);
            }

            rb.linearVelocity = Vector2.zero; // Dừng lại khi đạt vị trí mong muốn
        }
    }
}
