using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private Transform _camera;
    [SerializeField] private Material jetpack;
    [SerializeField] private float playerSpeed = 50f;
    [SerializeField] private float turnSmoothTime = 0.1f;

    private Rigidbody rb;
    private Transform playerTransform;
    private float turnSmoothVelocity;
    private float jetpackPower = 20f;
    private float boostPower = 0.01f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTransform = transform;
    }
    
    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("GameOver") == null)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            Destroy(gameObject.GetComponent<PlayerController>());
        }
    }

    void FixedUpdate()
    {
        Move();
        Jetpack();
    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        //float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, 0f).normalized;
        
        //rb.AddForce(direction * playerSpeed);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;// + _camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            
            playerTransform.rotation = Quaternion.Euler(0f,angle,0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            
            //playerTransform.position += moveDirection.normalized * playerSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + moveDirection.normalized * playerSpeed * Time.deltaTime);
        }
    }

    void Jetpack()
    {
        Vector3 boost = Vector3.Project(rb.velocity, transform.up);
        rb.AddForce(transform.up * boost.magnitude * boostPower);

        if(Input.GetKey(KeyCode.Space))
        {
            jetpack.color = Color.cyan;

            rb.AddForce(transform.up * jetpackPower);
            
            rb.drag = rb.velocity.magnitude * 0.01f;

            jetpackPower -= 3 * Time.deltaTime;
            if (jetpackPower < 0)
            {
                jetpackPower = 0;
            }
        }
        else
        {
            jetpack.color = Color.white;
            rb.drag = 0f;
            if (jetpackPower == 0)
            {
                jetpackPower = 10;
            }
            if (jetpackPower < 10)
            {
                jetpackPower += 3 * Time.deltaTime;
            }
            
            jetpackPower += 2 * Time.deltaTime;
            
            if (jetpackPower > 20)
            {
                jetpackPower = 20;
            }
        }
    }

}
