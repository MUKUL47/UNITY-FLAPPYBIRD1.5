using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BirdJump : MonoBehaviour {
    Rigidbody2D bird,p1b;
    public GameObject Bird,P1B,p1T,P2B,P2T;
    float y = 0,p1=0,p2=0,p3=0,p4=0;
    public Text start, scoreT, GO;
    int score = 0;
    bool gameOver = false, P1 = false, P2 = false, P3 = false, P4 = false;
    public AudioClip jumpSound;
    private AudioSource sound;
    Scene currentScene;
    //speed
    float birdSpeed = 2, gravitySpeed = 1, pillarSpeed = 1;
    void Start () {
        bird = GetComponent<Rigidbody2D>();
        start.text = "Space to start";
        currentScene = SceneManager.GetActiveScene();
    }
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space) && !gameOver){
            
            start.text = "";
            scoreT.text = "Score " + score;
            bird.gravityScale = gravitySpeed;
            bird.velocity = new Vector2(0, y+(float)birdSpeed)*2;
            movePillars();
            incrementScore();
            sound.PlayOneShot(jumpSound, 1f);
        }

        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("FlappyBird");

            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name=="T" || collision.gameObject.name == "B"||
            collision.gameObject.name == "P1B" || collision.gameObject.name == "P1T"||
            collision.gameObject.name == "P2B" || collision.gameObject.name == "P2T")
        {           
            gameOver = true; bird.gravityScale = 0;GO.text = "GAMEOVER!!\n   Score "+score;
            
            scoreT.text = "Press Enter to restart";
           
        }
        
    }
    public void movePillars()
    {
        P1B.GetComponent<Rigidbody2D>().velocity = new Vector2((p1 - pillarSpeed), 0);
        p1T.GetComponent<Rigidbody2D>().velocity = new Vector2((p2 - pillarSpeed), 0);

        P2B.GetComponent<Rigidbody2D>().velocity = new Vector2((p3 - pillarSpeed), 0);
        P2T.GetComponent<Rigidbody2D>().velocity = new Vector2((p4 - pillarSpeed), 0);

        if (P1B.transform.position.x <= -6.35 && p1T.transform.position.x <= -6.35)
        {
            P1 = P2 = false;     
            P1B.transform.position = new Vector3(6.31f, (int)UnityEngine.Random.RandomRange(-2f, -5f), -2.1f);
            p1T.transform.position = new Vector3(P1B.transform.position.x+4f, (int)UnityEngine.Random.RandomRange(3f, 6f), -2.1f);
        }
        if (P2B.transform.position.x <= -6.35 && P2T.transform.position.x <= -6.35)
        {
            P3 = P4 = false;
            P2B.transform.position = new Vector3(6.31f, (int)UnityEngine.Random.RandomRange(-2f, -5f), -2.1f);
            P2T.transform.position = new Vector3(P2B.transform.position.x + 4f, (int)UnityEngine.Random.RandomRange(3f, 6f), -2.1f);
        }

    }
    public void incrementScore()
    {
        if (bird.transform.position.x > P1B.transform.position.x && !P1)
        {
            score += 1; P1 = true;
        }
        if (bird.transform.position.x > p1T.transform.position.x && !P2)
        {
            score += 1; P2 = true;
        }
        if (bird.transform.position.x > P2B.transform.position.x && !P3)
        {
            score += 1; P3 = true;
        }
        if (bird.transform.position.x > P2T.transform.position.x && !P4)
        {
            score += 1; P4 = true;
            increaseDifficulty();
        }
    }
    public void increaseDifficulty()
    {
        pillarSpeed += .25f;
        gravitySpeed += .7f;
        birdSpeed += .7f;
    }
}
