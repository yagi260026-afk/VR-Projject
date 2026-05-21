using UnityEngine;
 
public class FPSController : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 2f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
 
    private CharacterController controller;
 
    // カメラ
    public Transform cameraTransform;
 
    // カメラ上下回転用
    private float xRotation = 0f;
 
    // 重力用
    private Vector3 velocity;
 
    void Start()
    {
        controller = GetComponent<CharacterController>();
 
        // マウスカーソルを中央固定
        Cursor.lockState = CursorLockMode.Locked;
    }
 
    void Update()
    {
        Move();
        Look();
    }
 
    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
 
        Vector3 move =
            transform.right * x +
            transform.forward * z;
 
        controller.Move(move * speed * Time.deltaTime);
 
        // 接地中なら少し下方向へ
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
 
        // ジャンプ
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
 
        // 重力
        velocity.y += gravity * Time.deltaTime;
 
        controller.Move(velocity * Time.deltaTime);
    }
 
    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
 
        // 左右回転
        transform.Rotate(Vector3.up * mouseX);
 
        // 上下回転
        xRotation -= mouseY;
 
        // 上向き制限
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
 
        cameraTransform.localRotation =
            Quaternion.Euler(xRotation, 0f, 0f);
    }
}