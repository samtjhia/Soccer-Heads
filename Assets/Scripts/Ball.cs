using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    GameManager gm;
    Vector2 startPos;
    Rigidbody2D rb;

    public GameObject player1;
    public GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    private void ResetPlayers()
    {
        // reset player positions to the start
        player1.transform.position = new Vector2(-4.32f, -2.14f);
        player2.transform.position = new Vector2(4.32f, -2.14f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "GoalLeft")
        {

            gm.aiScore++;
            rb.velocity = Vector2.zero;
            transform.position = startPos;
            ResetPlayers();
        }
        else if (collision.gameObject.name == "GoalRight")
        {
            gm.playerScore++;
            rb.velocity = Vector2.zero;
            transform.position = startPos;
            ResetPlayers();
        }
    }
}
