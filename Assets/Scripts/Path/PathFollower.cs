using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public GameObject PowerSystem;

        //Car Variables
        public float speed = 5;
        public float fuel = 0;

        float distanceTravelled;

        void Start() {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
        }

        void Update()
        {
            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }

         private void OnTriggerStay(Collider target)
        {
            if(target.tag == "ElectricPump"){
                if(speed > 0 && fuel <= 100){
                    speed -= 1.0f;                  
                }  
                if(speed == 0){
                    if(fuel == 100){
                        speed = 5f;
                    }else{
                        if(PowerSystem.GetComponent<PowerSystemManager>().powerAvailable - 10.0f > 0.0f){
                        PowerSystem.GetComponent<PowerSystemManager>().powerUsed -= 10.0f;
                        fuel += 10;
                        }
                } 
                }
            }
        }

        private void OnTriggerEnter(Collider target)
        {
            if(target.tag == "Finish"){
                Destroy(gameObject);
            }
        }

    }
}