using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 3f;
    //[SerializeField] private float currentSpeed;
    private Rigidbody rb;
    private float rotationSpeed = 100f;
    public Animator animator;
    private static readonly int IsRun = Animator.StringToHash("IsRun");

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        
        Vector3 direction = new Vector3(horizontal, 0f, vertical);
        direction = Camera.main.transform.TransformDirection(direction);
        direction.y = 0f;

        animator.SetBool(IsRun, direction.magnitude > 0);
        if (direction.magnitude >= 0.1f)
        { 
            transform.Translate(direction * baseSpeed * Time.deltaTime, Space.World);
        }
    }
    void RotatePlayer()
    {
        Vector3 viewDirection = Camera.main.transform.forward;
        viewDirection.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(viewDirection);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
    /*public void ApplySpeedBuff(float multiplier)
    {
        currentSpeed = baseSpeed * multiplier;
    }*/
}
