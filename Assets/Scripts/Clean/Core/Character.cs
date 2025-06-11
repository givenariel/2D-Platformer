using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected float moveSpeed;
    protected int health;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Move(float direction)
    {
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocityY);
    }
}
