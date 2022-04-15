using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private static string PIPE_TAG = "Pipe";
    private static string CHECKPOINT_TAG = "Checkpoint";
    private static string VOID_TAG = "Void";
    [SerializeField] float jumpForce = 1f;
    Rigidbody2D rigidBody;

    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip deathSound;

    public bool dead = false;

    private float deathForceMultiplier = 0.5f;

    public int score;

    private Vector2 savedVelocity;
    private AudioClip loadedJumpSound;
    // Start is called before the first frame update
    void Start()
    {
        // Fetch the object's Rigidbody.
        rigidBody = GetComponent<Rigidbody2D>();
        score = 0;
        Pause();
        jumpSound.LoadAudioData();
        deathSound.LoadAudioData();
    }

    // Update is called once per frame
    void Update()
    {
        // Jump pressed and player is not dead
        if (Input.GetButtonDown("Jump") && !dead)
        {
            Jump();
        }
    }

    void Jump()
    {
        GetComponent<AudioSource>().PlayOneShot(jumpSound);
        // Set velocity to zero so that the jump height is always the same.
        rigidBody.velocity = Vector2.zero;
        // Apply an upwards force when jumping
        rigidBody.AddForce(transform.up * jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == PIPE_TAG && !dead)
        {
            Die();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == CHECKPOINT_TAG && !dead)
        {
            score++;
        }
        else if (other.gameObject.tag == VOID_TAG && !dead)
        {
            Die();
        }
    }

    private void Die()
    {
        dead = true;
        GetComponent<AudioSource>().PlayOneShot(deathSound);
        rigidBody.velocity = Vector2.zero;
        rigidBody.AddForce(transform.up * jumpForce * deathForceMultiplier);
        rigidBody.AddForce(Vector2.left * jumpForce * deathForceMultiplier);
    }

    public void Pause()
    {
        savedVelocity = rigidBody.velocity;
        rigidBody.velocity = Vector2.zero;
        rigidBody.Sleep();
    }
    public void Resume()
    {
        rigidBody.WakeUp();
        rigidBody.velocity = savedVelocity;
    }
}
