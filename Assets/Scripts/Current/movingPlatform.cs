using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    [SerializeField] private Transform platform;
    private Vector3 direction;
    [SerializeField] private bool changeDirection;
    [SerializeField] private float speed = 2;

    void Start()
    {
        direction = transform.right;
    }
    void Update()
    {
        if (changeDirection)
        {
            direction = -transform.right;
        }
        else
        {
            direction = transform.right;
        }

        platform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        changeDirection = true;
    }
}
