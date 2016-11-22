using UnityEngine;
using System.Collections;
//using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;



public class Player : MonoBehaviour
{
    public int id;
    Rigidbody2D rb;
    Rigidbody2D rb2;
   
    public Vector2 playerPos;


    void Start ()
    {

        rb2 = GetComponent<Rigidbody2D>();

        playerPos = new Vector2();
        playerPos = GetComponent<Transform>().position;

    }

	void Update ()
    {
        playerPos = GetComponent<Transform>().position;
    }

    public void MoveUp()
    {
        rb2.AddForce(new Vector2(0, 50));
    }

    public void MoveDown()
    {
        rb2.AddForce(new Vector2(0, -50));
    }

    public void MoveLeft()
    {
        rb2.AddForce(new Vector2(-50, 0));
    }

    public void MoveRight()
    {
        rb2.AddForce(new Vector2(50, 0));
    }

    public void RemoteMove(string direction)
    {
        if (direction == "W")
        {
            rb = GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(0, 50));
        }
        if (direction == "D")
        {
            rb = GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(50, 0));
        }
        if (direction == "A")
        {
            rb = GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(-50, 0));
        }
        if (direction == "S")
        {
            rb = GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(0, -50));
        }
    }

    public void setPosition(Vector2 pos)
    {
        GetComponent<Rigidbody2D>().position = pos;
    }
}
