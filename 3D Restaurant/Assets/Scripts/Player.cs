using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 movementDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += movementDirection * movementSpeed * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, movementDirection, rotationSpeed * Time.deltaTime);
        isWalking = movementDirection != Vector3.zero;
    }

    public bool IsWalking() => isWalking;
}
