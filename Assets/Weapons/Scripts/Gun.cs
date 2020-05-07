namespace USC.CharacterController
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Component for controlling the gun
    /// </summary>
    public class Gun : MonoBehaviour
    {
        public List<GameObject> activeBullets;
        public List<GameObject> disabledBullets;
        private GameObject Bullets;

        public GunType equipedGun;
        private Animator anim;
        public Transform objectGun;
        public GameObject BulletPrefab;
        public InputBridge input;
        private float timeLastShot = 0;
        // Start is called before the first frame update
        void Start()
        {
            input = GetComponent<InputBridge>();
            anim = objectGun.GetComponent<Animator>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if(input.Shoot() && timeLastShot > equipedGun.rateOfFire)
            {
                Shoot();
                timeLastShot = 0;
            }
            timeLastShot+= Time.deltaTime;
        }

        /// <summary>
        /// Shoot a bullet
        /// </summary>
        void Shoot()
        {
            if(disabledBullets.Count <= 0)
            {
                if(Bullets== null)            
                    Bullets = new GameObject("bullets");            
                GameObject bulletInstance = Instantiate(BulletPrefab, objectGun.position ,objectGun.rotation, Bullets.transform);
                Bullet component = bulletInstance.GetComponent<Bullet>();
                component.speed = equipedGun.speed;
                component.deathTime = equipedGun.deathTime;
                component.starter = this;
                activeBullets.Add(bulletInstance);
            }
            else
            {
                GameObject bullet = disabledBullets[0];
                bullet.transform.position = objectGun.position;
                bullet.transform.rotation = objectGun.rotation;
                Bullet component = bullet.GetComponent<Bullet>();
                component.speed = equipedGun.speed;
                component.deathTime = equipedGun.deathTime;
                bullet.SetActive(true);
                activeBullets.Add(bullet);
                disabledBullets.Remove(bullet);
            }

            anim.PlayInFixedTime(equipedGun.clip.name, -1, 0);
        }
    }
}