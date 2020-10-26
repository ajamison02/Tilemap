using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text lives;
    public Text winText;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    Animator anim;
    
    private int scoreValue = 0;
    private int livesValue = 3;

    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        SetScoreText ();
        SetLivesText ();
        winText.text = "";
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
        
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            SetScoreText ();
            Destroy(collision.collider.gameObject);
        }
        
        if(collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            SetLivesText ();
            Destroy(collision.collider.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }

        }
    }
    void SetScoreText ()
    {
        score.text = scoreValue.ToString();
        if (scoreValue >= 8)
        {
            winText.text = "You Win! Game created by Aja Jamison"; 
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = false;
            Destroy(this);
        }

        if (scoreValue == 4)
        {
            transform.position = new Vector2(102.0f, 3.0f);
        }
    }

    void SetLivesText ()
    {
        lives.text = livesValue.ToString();
        if (livesValue <= 0)
        {
            winText.text = "You Lose!";
            Destroy(this);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }

        
    }

    void Flip()
    {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
    }
}
