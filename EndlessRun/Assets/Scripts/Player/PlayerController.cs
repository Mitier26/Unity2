using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    private Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed;

    private int desiredLane = 1;
    public float laneDistance = 4;

    public float jumpForce;
    public float gravity = -20f;

    public Animator animator;

    private bool isSliding = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!PlayerManager.isGameStarted) return;

        if(forwardSpeed < maxSpeed)
            forwardSpeed += 0.1f * Time.deltaTime;

        animator.SetBool("isGameStarted", true);

        direction.z = forwardSpeed;

        animator.SetBool("isGrounded", transform.position.y > 1.5 ? false : true);

        if(controller.isGrounded)
        {
            //if (Input.GetKeyDown(KeyCode.UpArrow))
            if(SwipeManager.swipeUp)
            {
                Jump();
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }
        
        if(SwipeManager.swipeDown && !isSliding)
        {
            StartCoroutine(Slide());
        }

        //if(Input.GetKeyDown(KeyCode.RightArrow))
        if(SwipeManager.swipeRight)
        {
            desiredLane++;
            if(desiredLane == 3)
            {
                desiredLane = 2;
            }
        }

        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        if(SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if(desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if(desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        //transform.position = targetPosition;
        //transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.fixedDeltaTime);
        //controller.center = controller.center;

        if (transform.position == targetPosition) return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
    }

    private void FixedUpdate()
    {
        if (!PlayerManager.isGameStarted) return;

        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.CompareTag("Obstacle"))
        {
            PlayerManager.gameover = true;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);

        controller.center = new Vector3(0, 0.5f, 0);
        controller.height = 1;

        yield return new WaitForSeconds(1.3f);

        controller.center = new Vector3(0, 0f, 0);
        controller.height = 2;
        animator.SetBool("isSliding", false);
        isSliding = false;
    }
}
