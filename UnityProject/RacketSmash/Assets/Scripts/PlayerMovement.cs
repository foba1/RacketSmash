using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveControl { WASD, Arrows }
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1;
    [SerializeField] MoveControl controlType;
    void Update()
    {
        Vector3 moveDir = GetInputVector();
        transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);
    }
    public Vector3 GetInputVector()
    {
        Vector3 moveVec = Vector3.zero;
        if (controlType == MoveControl.WASD)
        {
            if (Input.GetKey(KeyCode.W))
                moveVec.z += 1;
            if (Input.GetKey(KeyCode.S))
                moveVec.z -= 1;
            if (Input.GetKey(KeyCode.D))
                moveVec.x += 1;
            if (Input.GetKey(KeyCode.A))
                moveVec.x -= 1;
        }
        else if (controlType == MoveControl.Arrows)
        {
            if (Input.GetKey(KeyCode.UpArrow))
                moveVec.z += 1;
            if (Input.GetKey(KeyCode.DownArrow))
                moveVec.z -= 1;
            if (Input.GetKey(KeyCode.RightArrow))
                moveVec.x += 1;
            if (Input.GetKey(KeyCode.LeftArrow))
                moveVec.x -= 1;
        }
        if (moveVec.sqrMagnitude > 1)
            moveVec.Normalize();
        return moveVec;
    }
}
