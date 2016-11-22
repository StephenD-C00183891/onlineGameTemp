using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    // Use this for initialization
    Rigidbody2D rb;
    
    public KeyCode up, down, left, right;
    public bool remote;

    public float max = 1.0f;
    public float min = -1.0f;

    static float t = 0.0f;

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
    public void MoveByPosition(Vector2 pos, bool recieved)
    {
        if (recieved == true)
        {
            rb.position = pos;
        }
        else if (recieved == false) 
        {
            rb.position = new Vector2(Mathf.Lerp(min, max, t), 0);

            t += 0.05f * Time.deltaTime;

            if(t > 1.0f)
            {
                float temp = max;
                max = min;
                min = temp;
                t = 0.0f;
            }
        }
    }
    public Vector2 GetPosition()
    {
        return rb.position;
    }
}
