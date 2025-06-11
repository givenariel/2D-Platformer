using UnityEngine;

public class enemy : MonoBehaviour
{
    int hp = 3;

    void Update()
    {
        if (hp == 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("weapon"))
        {
            hp--;
        }
    }
}
