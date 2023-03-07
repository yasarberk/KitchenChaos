using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking;

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        //Move direction changes
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius ,moveDirection, moveDistance);

        if(!canMove)
        {
            //Cannot move towards to moveDirection

            //Attempt only X movement
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);

            if(canMove)
            {
                //Can only move only on the X direction
                moveDirection = moveDirectionX;
            }
            else
            {
                //Cannot move only on the X direction

                //Attempt only on the Z movement
                Vector3 moveDirectionZ = new Vector3(0f, 0f, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);

                if(canMove)
                {
                    //Can only move only on the Z direction
                    moveDirection = moveDirectionZ;
                }
                else
                {
                    //Cannot move in any direction
                }
            }

        }

        if(canMove)
        {
            transform.position += moveDirection * moveDistance;
        }

        isWalking = moveDirection != Vector3.zero;

        //More smooth rotation
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
