using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _body;

    private float _xRotation;

    private void LateUpdate()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        _xRotation -= input.y;
        _xRotation = Mathf.Clamp(_xRotation, -60, 60);

        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        _body.Rotate(Vector3.up * input.x);
    }
}
