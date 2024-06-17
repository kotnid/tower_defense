using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    
    [Header("Attributes")]
    public float range = 3f;
    [SerializeField] float turnSpeed = 15f;

    [Header("Bullets (default)")]
    [SerializeField] float fireRate = 1f;
    private float fireCountDown = 0f;

    [Header("Laser")]
    [SerializeField] bool useLaser = false;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] ParticleSystem laserImpactEffect;
    [SerializeField] Light laserImpactLight;
    [SerializeField] int damageOverTime = 40;
    [SerializeField] float slowPct =.5f;

    [Header("Unity Setup Fields")]
    [SerializeField] Transform partToRotate;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    private Transform target;
    private string enemyTag = "Enemy";
    public int targetIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget",0f,0.5f);
    }

    // Update is called once per frame
    void Update()
    {   
        fireCountDown -= Time.deltaTime;

        if(target == null)
        {   
            if(useLaser)
            {
                if(lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    laserImpactEffect.Stop();
                    laserImpactLight.enabled = false;
                }
            }
            return ;
        }

       LockOnTarget();

       if(useLaser)
       {
        Laser();
       }
       else{
        if(fireCountDown <= 0f)
        {
            Shoot();
            fireCountDown = 1f/fireRate;
        }
       }   
    }

    private void LockOnTarget()
    {
        Vector3 dir = target.position-transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles; 
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Laser()
    {
        if(!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            laserImpactEffect.Play();
            laserImpactLight.enabled = true;
        }
       
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;
        laserImpactEffect.transform.rotation = Quaternion.LookRotation(dir);
        laserImpactEffect.transform.position = target.position + dir.normalized * 0.3f;

        target.GetComponent<Enemy>().TakeDamage(damageOverTime * Time.deltaTime);
        target.GetComponent<Enemy>().Slow(slowPct);
    }

    private void Shoot()
    {
        GameObject bulletGo = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGo.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.Seek(target);
        }    
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = new Color32(0, 128, 255, 128);
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject targetEnemy = null;

        if(targetIndex == 0) // nearest
        {
            float shortestDistance = Mathf.Infinity;
        
            foreach(GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                
                if(shortestDistance > distanceToEnemy && distanceToEnemy <= range)
                {
                    shortestDistance = distanceToEnemy;
                    targetEnemy = enemy;
                }
            }
        }
        else if(targetIndex == 1) // first
        {
            int smallestWaveIndex = 1000;
            float shortestDistance = Mathf.Infinity;

            foreach(GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                float distanceToTarget = enemy.GetComponent<EnemyMovement>().distanceToTarget();
                int waveIndex = enemy.GetComponent<EnemyMovement>().waveIndex;
              
                if(distanceToEnemy > range)
                {
                    continue;
                }

                if( (smallestWaveIndex > waveIndex) || (smallestWaveIndex == waveIndex && distanceToTarget > shortestDistance))
                {
                    smallestWaveIndex = waveIndex;
                    shortestDistance = distanceToTarget;
                    targetEnemy = enemy;
                }
            }
        }
        else if(targetIndex == 2) // last 
        {
            int largestWaveIndex = 0;
            float largestDistance = 0;

            foreach(GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                float distanceToTarget = enemy.GetComponent<EnemyMovement>().distanceToTarget();
                int waveIndex = enemy.GetComponent<EnemyMovement>().waveIndex;
              
                if(distanceToEnemy > range)
                {
                    continue;
                }

                if( (largestWaveIndex < waveIndex) || (largestWaveIndex == waveIndex && distanceToTarget < largestDistance))
                {
                    largestWaveIndex = waveIndex;
                    largestDistance = distanceToTarget;
                    targetEnemy = enemy;
                }
            }
        }
        else if(targetIndex == 3) // strongest
        {
            float largestHP = 0;
        
            foreach(GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                float HP = enemy.GetComponent<Enemy>().health;

                if(largestHP < HP && distanceToEnemy <= range)
                {
                    largestHP = distanceToEnemy;
                    targetEnemy = enemy;
                }
            }
        }
        else if(targetIndex == 4) // weakest
        {
            float smallestHP = Mathf.Infinity;
        
            foreach(GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                float HP = enemy.GetComponent<Enemy>().health;

                if(smallestHP > HP && distanceToEnemy <= range)
                {
                    smallestHP = distanceToEnemy;
                    targetEnemy = enemy;
                }
            }
        }

        
        if(targetEnemy != null)
        {
            target = targetEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
}
