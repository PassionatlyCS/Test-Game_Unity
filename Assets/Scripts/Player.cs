using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Player : MonoBehaviour
{
    //public string Name; //the name of our player
    //public int Attack;  //an int of our attack power
    //public float AttackSpeed; //a float of how fast we attack

    public int health; //integar of health
    private bool dead; //true of false
    public float speed = 10;
    public int points = 0;
    public float gravity = 10;
    public float maxVelocityChange = 10;
    public float jumpHeaight = 2;
    private bool grounded;

    private Rigidbody _rigidbody;

    private Transform PlayerTransform; 

    private GameObject Enemy;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _rigidbody.freezeRotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerTransform.Rotate(0, Input.GetAxis("Horizontal"), 0); //trial and error for vlaues
        
        Vector3 targetVelocity = new Vector3(0, 0, Input.GetAxis("Vertical"));
        targetVelocity = PlayerTransform.TransformDirection(targetVelocity);
        targetVelocity = targetVelocity * speed;


        Vector3 velocity = _rigidbody.velocity;
        Vector3 velocityChange = targetVelocity - velocity;

        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);

        velocityChange.y = 0;

        _rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

        if(Input.GetButton("Jump") && grounded == true)
        {
            _rigidbody.velocity = new Vector3(velocity.x, calculateJump(), velocity.z);
        }

        _rigidbody.AddForce(new Vector3 (0,-gravity* _rigidbody.mass,0));
        grounded = false;


    }

    float calculateJump()
    {
        return Mathf.Sqrt(2 * jumpHeaight * gravity); 
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            Destroy(other.gameObject);
            points = points + 1;
        }
    }
    void Update()
    {
        if(health < 1)
        {
           // SceneManager.LoadScene(string scenePath)
            Application.LoadLevel("test");
        }
    }

}
