using UnityEngine;

public class projectile : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform bullet;

    void Update()
    {
        bullet.Translate(transform.right * bulletSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
