using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;

    [SerializeField]
    private float jumpPower = 5;

    [SerializeField]
    private float sensX = 10;

    [SerializeField]
    private float sensY = 10;

    [SerializeField]
    private Transform lookAt;

    [SerializeField]
    private Transform playerModel;

    private Rigidbody _rigidbody;
    private Vector3 movement = Vector3.zero;

    private float rotationX;
    private float rotationY;

    public Animator animator;
    private static readonly int IsRun = Animator.StringToHash("isRun");

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        var inputX = Input.GetAxis("Horizontal");
        var inputZ = Input.GetAxis("Vertical");

        var mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        var mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        movement = new Vector3(inputX, 0, inputZ).normalized;

        animator.SetBool(IsRun, movement.magnitude > 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump");
            Jump();
        }
    }
    
    private void FixedUpdate()
    {
        movement = lookAt.right * movement.x + lookAt.forward * movement.z;
        var newLinearVelocity = new Vector3(movement.x * speed, _rigidbody.velocity.y, movement.z * speed);
        _rigidbody.velocity = newLinearVelocity;
    }
    
    private void LateUpdate()
    {
        lookAt.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        if (movement.magnitude > 0)
        {
            var movementDirection = lookAt.right * movement.x + lookAt.forward * movement.z;
            var rotation = Quaternion.LookRotation(movementDirection).eulerAngles;
            rotation.x = 0;
            rotation.z = 0;
            playerModel.rotation = Quaternion.Euler(rotation);
        }
    }

    private void Jump()
    {
        _rigidbody.AddForce(transform.up * jumpPower, ForceMode.Impulse);
    }
}