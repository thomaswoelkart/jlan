using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolWall : MonoBehaviour
{
    // Start is called before the first frame update
    public float mMovementSpeed = 3.0f;
    public bool bIsGoingRight = true;

    private SpriteRenderer _mSpriteRenderer;

    public float mRaycastingDistance = 1f;
    // Start is called before the first frame update
    void Start()
    {
        _mSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _mSpriteRenderer.flipX = bIsGoingRight;
    }


    void Update()
    {
        // if the ennemy is going right, get the vector pointing to its right
        Vector3 directionTranslation = (bIsGoingRight) ? transform.right : -transform.right;
        directionTranslation *= Time.deltaTime * mMovementSpeed;

        transform.Translate(directionTranslation);


        CheckForWalls();
    }

    private void CheckForWalls()
    {
        Vector3 raycastDirection = (bIsGoingRight) ? Vector3.right : Vector3.left;

        // Raycasting takes as parameters a Vector3 which is the point of origin, another Vector3 which gives the direction, and finally a float for the maximum distance of the raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position + raycastDirection * mRaycastingDistance - new Vector3(0f, 0.25f, 0f), raycastDirection, 0.075f);

        // if we hit something, check its tag and act accordingly
        if (hit.collider != null)
        {
            if (hit.transform.tag == "Terrain")
            {
                bIsGoingRight = !bIsGoingRight;
                _mSpriteRenderer.flipX = bIsGoingRight;

            }
        }
    }
}