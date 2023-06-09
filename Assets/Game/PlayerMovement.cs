using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private const string X_NAME = "Horizontal";
    private const string Y_NAME = "Vertical";

    [Header("References")]
    [SerializeField] private CharacterController _controller;

    [Space(10)]
    [Header("Characteristics")]
    [SerializeField] private float _defaultSpeed;
    [SerializeField] private float _stepSpeed;

    [Space(10)]
    [Header("Ground check")]
    [SerializeField] private Transform _groundPoint;
    [SerializeField] private LayerMask _groundMask;


    private Vector3 _velocity;
    private float _speed;

    private void Start()
    {
        _speed = _defaultSpeed;
    }

    private void Update()
    {
        Move();

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Step();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_groundPoint.position, _controller.radius);
    }

    public void Move()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw(X_NAME), Input.GetAxisRaw(Y_NAME));
        Vector3 direction = (transform.forward * input.y + transform.right * input.x).normalized;

        bool isGround = Physics.CheckSphere(_groundPoint.position, _controller.radius, _groundMask);

        if (isGround) _velocity.y = -2;
        else _velocity.y += 60 * -9.81f * Time.deltaTime;
        
        _controller.Move(direction * _speed * Time.deltaTime);
        _controller.Move(_velocity * Time.deltaTime);
    }

    public void Step()
    {
        StartCoroutine(StepRoutine(0.1f));
    }

    private IEnumerator StepRoutine(float time)
    {
        _speed = _stepSpeed;
        yield return new WaitForSeconds(time);
        _speed = _defaultSpeed;
    }
}
