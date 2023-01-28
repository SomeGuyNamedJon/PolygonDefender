using UnityEngine;

//Aquired from Asset, slightly modified

public class TurretAI : MonoBehaviour {

    public enum TurretType
    {
        Single = 1,
        Dual = 2,
        Catapult = 3,
    }
    
    public GameObject currentTarget;
    public Transform turreyHead;

    public float range = 10.0f;
    public float coolDown;
    private float timer;
    public float lockSpeed;
    public float health = 100f;

    public Vector3 randomRot;
    public Animator animator;

    [Header("[Turret Type]")]
    public TurretType turretType = TurretType.Single;
    
    public Transform muzzleMain;
    public Transform muzzleSub;
    public GameObject muzzleEff;
    public GameObject bullet;
    private bool shootLeft = true;

    private Transform lockOnPos;

    void Start () {
        InvokeRepeating("CheckForTarget", 0, 0.5f);

        if (transform.GetChild(0).GetComponent<Animator>())
        {
            animator = transform.GetChild(0).GetComponent<Animator>();
        }

        randomRot = new Vector3(0, Random.Range(0, 359), 0);
    }
	
	void Update () {
        if (currentTarget != null)
        {
            FollowTarget();

            float currentTargetDist = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (currentTargetDist > range)
            {
                currentTarget = null;
            }
        }
        else
        {
            IdleRitate();
        }

        timer += Time.deltaTime;
        if (timer >= coolDown)
        {
            if (currentTarget != null)
            {
                timer = 0;
                
                if (animator != null)
                {
                    animator.SetTrigger("Fire");
                    ShootTrigger();
                }
                else
                {
                    ShootTrigger();
                }
            }
        }
	}

    private void CheckForTarget()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, range);
        float distAway = Mathf.Infinity;

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].tag == "Enemy")
            {
                float dist = Vector3.Distance(transform.position, colls[i].transform.position);
                if (dist < distAway)
                {
                    currentTarget = colls[i].gameObject;
                    distAway = dist;
                }
            }
        }
    }

    private void FollowTarget()
    {
        Vector3 targetDir = currentTarget.transform.position - turreyHead.position;
        targetDir.y = 0;
        if (turretType == TurretType.Catapult)
        {
            Vector3 away = transform.position - targetDir;
            turreyHead.transform.rotation = Quaternion.RotateTowards(turreyHead.rotation, Quaternion.LookRotation(away), lockSpeed * Time.deltaTime);
        }
        else{
            turreyHead.transform.rotation = Quaternion.RotateTowards(turreyHead.rotation, Quaternion.LookRotation(targetDir), lockSpeed * Time.deltaTime);
        }
    }

    private void ShootTrigger()
    {
        Shoot(currentTarget);
    }
    
    Vector3 CalculateVelocity(Vector3 target, Vector3 origen, float time)
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void IdleRitate()
    {
        bool refreshRandom = false;
        
        if (turreyHead.rotation != Quaternion.Euler(randomRot))
        {
            turreyHead.rotation = Quaternion.RotateTowards(turreyHead.transform.rotation, Quaternion.Euler(randomRot), lockSpeed * Time.deltaTime * 0.2f);
        }
        else
        {
            refreshRandom = true;

            if (refreshRandom)
            {

                int randomAngle = Random.Range(0, 359);
                randomRot = new Vector3(0, randomAngle, 0);
                refreshRandom = false;
            }
        }
    }

    public void Shoot(GameObject go)
    {
        if (turretType == TurretType.Catapult)
        {
            lockOnPos = go.transform;

            Instantiate(muzzleEff, muzzleMain.transform.position, muzzleMain.rotation);
            GameObject missleGo = Instantiate(bullet, muzzleMain.transform.position, muzzleMain.rotation);
            Projectile projectile = missleGo.GetComponent<Projectile>();
            projectile.target = lockOnPos;
        }
        else if(turretType == TurretType.Dual)
        {
            if (shootLeft)
            {
                Instantiate(muzzleEff, muzzleMain.transform.position, muzzleMain.rotation);
                GameObject missleGo = Instantiate(bullet, muzzleMain.transform.position, muzzleMain.rotation);
                Projectile projectile = missleGo.GetComponent<Projectile>();
                projectile.target = transform.GetComponent<TurretAI>().currentTarget.transform;
            }
            else
            {
                Instantiate(muzzleEff, muzzleSub.transform.position, muzzleSub.rotation);
                GameObject missleGo = Instantiate(bullet, muzzleSub.transform.position, muzzleSub.rotation);
                Projectile projectile = missleGo.GetComponent<Projectile>();
                projectile.target = transform.GetComponent<TurretAI>().currentTarget.transform;
            }

            shootLeft = !shootLeft;
        }
        else
        {
            Instantiate(muzzleEff, muzzleMain.transform.position, muzzleMain.rotation);
            GameObject missleGo = Instantiate(bullet, muzzleMain.transform.position, muzzleMain.rotation);
            Projectile projectile = missleGo.GetComponent<Projectile>();
            projectile.target = currentTarget.transform;
        }
    }
}
