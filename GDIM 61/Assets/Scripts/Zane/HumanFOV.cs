using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Zane
public class HumanFOV : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] [Range(1, 360)] private float angle;

    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask wallLayer;

    [SerializeField] GameObject Player;

    public bool detectPlayer { get; private set; }
    public bool inCircle { get; private set; }


    void Start()
    {
        StartCoroutine(FOVCheck());
    }

    private IEnumerator FOVCheck()
    {
        // checks if the player is in view every 0.2 seconds
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfView();
        }
    }

    private void FieldOfView()
    {
        // raycasts a circle around the AI
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, playerLayer);

        // checks if the player is within the viewing range of the AI
        if (rangeCheck.Length > 0)
        {
            Transform player = rangeCheck[0].transform;
            Vector2 directionToPlayer = (player.position - transform.position).normalized;

            // checks if the player is within the viewing angle of the AI
            if (Vector2.Angle(transform.right * transform.localScale.x, directionToPlayer) < angle / 2)
            {
                float distanceToPlayer = Vector2.Distance(transform.position * transform.localScale.x, player.position);

                // checks if the player is in the direct line of sight of the AI
                if (!Physics2D.Raycast(transform.position, directionToPlayer, distanceToPlayer, wallLayer))
                {
                    detectPlayer = true;
                }
                else
                {
                    detectPlayer = false;
                }
            }
            else
            {
                detectPlayer = false;
            }
        }
        else if (detectPlayer)
        {
            detectPlayer = false;
        }

    }

    private void OnDrawGizmos()
    {
        // creates a visible circle in the gizmos that represents the viewing range of the AI
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

        // positions the viewing angle
        Vector3 angle1 = DirectionFromAngle(-transform.eulerAngles.x, -angle / 2);
        Vector3 angle2 = DirectionFromAngle(-transform.eulerAngles.x, angle / 2);

        // creates visible lines in the gizmos that represents the viewing angle of the AI
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + angle1 * radius * transform.localScale.x);
        Gizmos.DrawLine(transform.position, transform.position + angle2 * radius * transform.localScale.x);

        // if the player is in the line of sight of the AI, then a visible red line is created in the gizmos that spans from the AI to the player
        if (detectPlayer)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, Player.transform.position);
        }
    }

    private Vector2 DirectionFromAngle(float eulerX, float angleInDegrees)
    {
        angleInDegrees += eulerX;

        return new Vector2(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
    }
}
