using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public int waveIndex = 0;
    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.points[0];
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * enemy.speed);

        if(transform.position == target.position)
        {
            waveIndex++;
            if(waveIndex >= Waypoints.points.Length)
            {
                EndPath();
                return ;
            }
            target = Waypoints.points[waveIndex];
        }
        enemy.speed = enemy.startSpeed;
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        Destroy(gameObject);
        WaveSpawner.EnemiesAlive--;
    }

    public float distanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }
}
