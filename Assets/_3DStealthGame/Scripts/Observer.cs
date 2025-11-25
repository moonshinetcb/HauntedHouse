using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;
    private bool m_IsPlayerInRange;
    private CapsuleCollider col;

    void Start()
    {
        col = GetComponent<CapsuleCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
            m_IsPlayerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
            m_IsPlayerInRange = false;
    }

    void Update()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 origin = transform.TransformPoint(col.center);
            Vector3 target = player.position + Vector3.up;

            Ray ray = new Ray(origin, target - origin);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == player)
                    gameEnding.CaughtPlayer();
            }
        }
    }
}