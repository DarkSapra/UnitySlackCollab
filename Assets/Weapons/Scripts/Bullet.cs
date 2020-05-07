namespace USC.CharacterController
{

    using UnityEngine;
    /// <summary>
    /// Component for controlling the bullets
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        public Gun starter;

        public float speed = 10;
        public float deathTime = 5;


        /// <summary>
        /// Move the bullet
        /// </summary>
        void FixedUpdate()
        {
            if(this.isActiveAndEnabled)
            {
                deathTime += Time.deltaTime;
                if(deathTime > 2 || Physics.Raycast(transform.position, transform.forward, speed*Time.deltaTime))
                    Destroy();        
                transform.position = transform.position+transform.forward*speed*Time.deltaTime;
            }

        }    
        void Destroy()
        {
            deathTime = 0;
            gameObject.SetActive(false);
            starter.activeBullets.Remove(this.gameObject);
            starter.disabledBullets.Add(this.gameObject);
        }
}
}
