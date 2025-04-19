using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    private float _speed = 5f;
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
    }
    
    private void MoveLeft()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }
    
    private void MoveRight()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }
}
