using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 10.0f;
    private Vector3 lastMousePosition;

    void Update()
    {
        // ���콺 ���� ��ư�� Ŭ���� ���¿����� ī�޶� ȸ��
        if (Input.GetMouseButtonDown(0)) // 0 -> ����, 1 -> ������
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            {
                Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;

                
                float rotationY = -deltaMousePosition.y * rotationSpeed * Time.deltaTime; // ���� ȸ��
                float rotationX = deltaMousePosition.x * rotationSpeed * Time.deltaTime; // �¿� �̵�

               
                transform.Rotate(Vector3.right, rotationY);
                transform.Rotate(Vector3.up, rotationX, Space.World);

                lastMousePosition = Input.mousePosition;
            }
        }
    }
}