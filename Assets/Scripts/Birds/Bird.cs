using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Bird : MonoBehaviour
{
    
    public Rigidbody2D rb2d;
    public PolygonCollider2D polygonCollider2D;


    public bool isLaunched = false;
    public bool faceDirection = false;


    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d.bodyType = RigidbodyType2D.Kinematic;
        polygonCollider2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLaunched && faceDirection && Input.GetKeyDown(KeyCode.Space))
        {
            SpecialAbility(rb2d.velocity.normalized, 10f);
        }
    }

    void FixedUpdate()
    {
        if (isLaunched && faceDirection) 
        {
            transform.right = rb2d.velocity;
        }

        
    }

    public virtual void LaunchBird(Vector2 direction, float force) 
    {
        Debug.Log("Activated");
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        polygonCollider2D.enabled = true;

        rb2d.AddForce(direction * force, ForceMode2D.Impulse);
        isLaunched = true;
        faceDirection = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        isLaunched = false;
        faceDirection = false;
        
        Destroy(gameObject, 5f);

        if (other.gameObject.CompareTag("Pigs")) 
        {
            Destroy(other.gameObject);
        }
    }

    public virtual void SpecialAbility(Vector2 direction, float force) 
    { 
        rb2d.velocity = direction * force * 3;
    }


}
