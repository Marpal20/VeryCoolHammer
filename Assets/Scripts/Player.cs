using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public Collider2D ground;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (rb.IsTouching(ground))
                rb.AddForce(Vector2.right * 2, ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (rb.IsTouching(ground))
                rb.AddForce(Vector2.left * 2, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (rb.IsTouching(ground))
                rb.AddForce(Vector2.up * 300, ForceMode2D.Impulse);
        }
    }
}
