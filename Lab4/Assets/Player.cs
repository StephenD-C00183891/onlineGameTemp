using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    // Use this for initialization
    Rigidbody2D rb;
    
    public KeyCode up, down, left, right;
    public bool remote;

    void Start (){
        rb = GetComponent<Rigidbody2D>();
    }
    public void Move(KeyCode key){
        if (key == up)
        {
            rb.AddForce(new Vector2(0, 5));
        }
        if (key == down)
        {
            rb.AddForce(new Vector2(0, -5));
        }
        if (key == left)
        {
            rb.AddForce(new Vector2(-5, 0));
        }
        if (key == right)
        {
            rb.AddForce(new Vector2(5, 0));
        }
    }
    public void MoveByPosition(Vector2 pos)
    {
        rb.position = pos;
    }
    public Vector2 GetPosition()
    {
        return rb.position;
    }
}
