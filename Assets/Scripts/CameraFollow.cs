using UnityEngine;

namespace ldjam_hellevator
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;
        private float yOffset = 3.9f;


        void Update()
        {
            if (target != null)
            {
                //Vector3 newPos = transform.position;
                //newPos.y = Mathf.Lerp(transform.position.y, target.position.y + yOffset, followSpeed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, target.transform.position.y - yOffset,
                    transform.position.z);
            }
        }
    }
}