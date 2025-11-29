using UnityEngine;

public class EnemyScore : MonoBehaviour
{
    public int points = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreScript.Instance.AddPoints(points);
            Destroy(gameObject);
        }
    }
}