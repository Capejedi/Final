using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(transform.position.magnitude > 1000.0f)
            Destroy(gameObject);
    }
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Enemy e = other.collider.GetComponent<Enemy>();
        if (e != null)
        {
            e.Fix();
        }
        Destroy(gameObject);

        EnemyHard h = other.collider.GetComponent<EnemyHard>();
        if (h != null)
        {
            h.Fix();
        }
        Destroy(gameObject);
        Boss b = other.collider.GetComponent<Boss>();
        if (b != null)
        {
            b.Fix();
        }
        Destroy(gameObject);
}

}
