using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TrajectoryObject
{

    /// <summary>
    ///     This class creates a new scene as a copy of the current scene then it projects the
    /// player into the scene
    /// </summary>
    public class Projection : MonoBehaviour
    {
        [SerializeField] LineRenderer lineRenderer;
        [SerializeField] private int maxPhysicsFrameIterations = 100;
        [SerializeField] private Transform obstaclesParent;
        [SerializeField] SimBall _simBall;
        private bool _projectionEnabled = false;

        private Scene _simulationScene;
        private PhysicsScene _physicsScene;
        
        private readonly Dictionary<Transform, Transform> _spawnedObjects = new Dictionary<Transform, Transform>();

        private void OnDrawGizmos()
        {
            if ( (_projectionEnabled && obstaclesParent == null ))
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(transform.position + Vector3.up * 2, 0.5f);
            }
        }
        
        /**private static Projection _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Debug.Log("Projection Trying second Awake");
                Destroy(gameObject);
                return;
            }
            Debug.Log("Projection Awake");
            _instance = this as Projection;

            DontDestroyOnLoad(gameObject);
        }
        
        public static Projection GetInstance()
        {
            return _instance;
        }**/
        
        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {
            //CreatePhysicsScene();
            //_simBall = GameObject.FindObjectOfType<SimBall>();
            //lineRenderer = gameObject.GetComponent<LineRenderer>();
        }

        /// <summary>
        ///    Create a new projection by creating a copy of the current scene
        /// </summary>
        public void CreatePhysicsScene()
        {
            if (obstaclesParent == null) return;
            if (SceneManager.GetSceneByName("Simulation").isLoaded)
            {
                SceneManager.UnloadSceneAsync("Simulation");
            }

            _simulationScene =
                SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
            _physicsScene = _simulationScene.GetPhysicsScene();

            AddGameObjects(_simulationScene, obstaclesParent);
        }

        /// <summary>
        /// Adds a object in the sim scene so the player movement can interact with it
        /// </summary>
        /// <param name="mySimScene"></param>
        /// <param name="obstaclesPar"></param>
        private void AddGameObjects(Scene mySimScene, Transform obstaclesPar)
        {
            foreach (Transform obj in obstaclesPar)
            {
                var ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
                Debug.Log("gameObject : " + ghostObj);
                try
                {
                    ghostObj.GetComponent<Renderer>().enabled = false;
                    SceneManager.MoveGameObjectToScene(ghostObj, mySimScene);
                    if (!ghostObj.isStatic) _spawnedObjects.Add(obj, ghostObj.transform);
                }
                catch (Exception e)
                {

                    // assume its a parent
                    AddGameObjects(mySimScene, obj);
                }
            }

            Debug.Log("Finish sim setup");
        }

        /// <summary>
        /// simulation update by making the destination of the transforms the source
        /// </summary>
        private void Update()
        {
            if (!_projectionEnabled) return;
            foreach (var item in _spawnedObjects)
            {
                item.Value.position = item.Key.position;
                item.Value.rotation = item.Key.rotation;
            }
        }

        /// <summary>
        /// Simulates the sim balls movement given its position and velocity
        /// </summary>
        /// <param name="realGameObject"></param>
        /// <param name="pos"></param>
        /// <param name="velocity"></param>
        public bool SimulateTrajectory(GameObject realGameObject, Vector3 pos, Vector3 velocity)
        {
            //if (!_projectionEnabled || ballPrefab == null) return;
            bool landed = false;
            //
            var posTemp = pos;
            posTemp.x = 1000.0f;
            posTemp.y = 1000.0f;
            posTemp.z = 1000.0f;
            GameObject ghostObj = Instantiate(gameObject, posTemp, Quaternion.identity);
            SceneManager.MoveGameObjectToScene(ghostObj.gameObject, _simulationScene);
            ghostObj.GetComponent<Rigidbody>().position = pos;
            
            //ghostObj.Init(velocity);
            ghostObj.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.Impulse);
            lineRenderer.enabled = true;
            lineRenderer.positionCount = maxPhysicsFrameIterations;

            for (var i = 0; i < maxPhysicsFrameIterations; i++)
            {
                _physicsScene.Simulate(Time.fixedDeltaTime);
                lineRenderer.SetPosition(i, ghostObj.transform.position);
                //Debug.Log("pos: " + i + " @ " + transform.position);
                /*Ray ray2 = new Ray(ghostObj.transform.position, Vector3.down);
                if (Physics.Raycast(ray2, out var hitInfo2, 1.0f))
                {
                    Debug.Log("hitInfo2 : i " + i + "gameobject(coll): " + hitInfo2.collider.gameObject.name);
                }*/
            }

            Ray ray = new Ray(ghostObj.transform.position, Vector3.down);
            if ( Physics.Raycast(ray, out var hitInfo, 4.0f))
            {
                Debug.Log("hitInfo : " + hitInfo);
                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.cyan;
                lineRenderer.SetPosition(1, pos);
            }
            else
            {
                Debug.Log("false hitInfo : " + hitInfo);
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.black;
                //Vector3 pos = ghostObj.transform.position;
                //pos.y = pos.y - 15.0f; 
                lineRenderer.SetPosition(1, pos);
            }
            Destroy(ghostObj.gameObject);
            return landed;
        }

        /// <summary>
        /// Tidy up the simulation
        /// </summary>
        public void RemoveTrajectoryLine()
        {
            lineRenderer.enabled = false;
        }
    }
}