﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Aquired from Asset, slightly modified

public class Projectile : MonoBehaviour {

    public TurretAI.TurretType type = TurretAI.TurretType.Single;
    public Transform target;
    public EnemyControl enemyHit;
    public float bulletDamage;
    public bool lockOn;
    public float speed = 1;
    public float turnSpeed = 1;
    public bool catapult;

    public float knockBack = 0.1f;
    public float explosionTimer = 1;

    public ParticleSystem explosion;

    private void Start()
    {
        if (catapult)
        {
            lockOn = true;
        }

        if (type == TurretAI.TurretType.Single)
        {
            Vector3 dir = target.position - transform.position;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    private void Update()
    {
        if (target == null)
        {
            Explosion();
            return;
        }

        if (transform.position.y < -0.2F)
        {
            Explosion();
        }

        explosionTimer -= Time.deltaTime;
        if (explosionTimer < 0)
        {
            Explosion();
        }

        if (type == TurretAI.TurretType.Catapult)
        {
            if (lockOn)
            {
                Vector3 Vo = CalculateCatapult(target.transform.position, transform.position, 1);

                transform.GetComponent<Rigidbody>().velocity = Vo;
                lockOn = false;
            }
        }else if(type == TurretAI.TurretType.Dual)
        {
            Vector3 dir = target.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, dir, Time.deltaTime * turnSpeed, 0.0f);
            Debug.DrawRay(transform.position, newDirection, Color.red);

            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.LookRotation(newDirection);

        }else if (type == TurretAI.TurretType.Single)
        {
            float singleSpeed = speed * Time.deltaTime;
            transform.Translate(transform.forward * singleSpeed * 2, Space.World);
        }
    }

    Vector3 CalculateCatapult(Vector3 target, Vector3 origen, float time)
    {
        Vector3 distance = target - origen;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag == "Enemy")
        {
            
            enemyHit = collider.gameObject.GetComponent<EnemyControl>();

            enemyHit.TakeDamage(bulletDamage);

            Vector3 dir = collider.transform.position - transform.position;
            Vector3 knockBackPos = collider.transform.position + (dir.normalized * knockBack);
            knockBackPos.y = 1;
            collider.transform.position = knockBackPos;
            Explosion();
        }
    }

    public void Explosion()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
