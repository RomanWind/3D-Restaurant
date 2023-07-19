using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    private bool isWalking;

    private void Update()
    {
        Vector2 inputVector = new Vector2(0,0);
        if(Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1;
        }        
        if(Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }        
        if(Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }        
        if(Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;
        }

        inputVector = inputVector.normalized;
        Vector3 movementDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += movementDirection * movementSpeed * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, movementDirection, rotationSpeed * Time.deltaTime);
        isWalking = movementDirection != Vector3.zero;
    }

    public bool IsWalking() => isWalking;
}
