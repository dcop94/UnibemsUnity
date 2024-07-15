using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 10.0f;
    private Vector3 lastMousePosition;

    void Update()
    {
        // 마우스 왼쪽 버튼을 클릭한 상태에서만 카메라 회전
        if (Input.GetMouseButtonDown(0)) // 0 -> 왼쪽, 1 -> 오른쪽
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            {
                Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;

                
                float rotationY = -deltaMousePosition.y * rotationSpeed * Time.deltaTime; // 상하 회전
                float rotationX = deltaMousePosition.x * rotationSpeed * Time.deltaTime; // 좌우 이동

               
                transform.Rotate(Vector3.right, rotationY);
                transform.Rotate(Vector3.up, rotationX, Space.World);

                lastMousePosition = Input.mousePosition;
            }
        }
    }
}