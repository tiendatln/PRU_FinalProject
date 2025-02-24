using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int HP = 100;
    public GameObject ememy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ememy = GameObject.Find("e");
        // Khởi tạo hoặc kiểm tra gameObject đã được gán chưa
    }

    public virtual void Hp()
    {
        HP -= 5;
        float at = 5f / 200f; // Sửa lại để phép chia chính xác

        // Kiểm tra gameObject có được gán chưa
        if (ememy != null)
        {
            // Điều chỉnh kích thước gameObject
            ememy.transform.localScale = new Vector2(ememy.transform.localScale.x - at, ememy.transform.localScale.y);
        }

        Debug.Log(HP);
    }
}
