using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    // Use this for initialization
    Rigidbody2D rb;
    
    public KeyCode up, down, left, right;
    public bool remote;

    public Vector2 max;
    public Vector2 min;

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
    public void MoveBy(Vector2 pos)
    {
        rb.position = pos;
    }
    public void MoveByPosition(Vector2 pos)
    {
        max = pos;
        min = rb.position;
        t = 0.0f;
    }
    public void ManualUpdate(float FPS)
    {
        t += Time.deltaTime;

        //if (t > FPS)
        //{
        //    rb.position = new Vector2(max.x, max.y);
        //    min = max;
        //}
        //else
        //{
        //    float timeScaler = t / FPS;
        //    rb.position = new Vector2(Mathf.Lerp(min.x, max.x, timeScaler), Mathf.Lerp(min.y, max.y, timeScaler));
        //}

        float timeScaler = t / FPS;
        rb.transform.position = new Vector2(Mathf.Lerp(min.x, max.x, timeScaler), Mathf.Lerp(min.y, max.y, timeScaler));
    }
    public Vector2 GetPosition()
    {
        return rb.position;
    }
}
