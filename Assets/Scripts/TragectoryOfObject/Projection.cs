using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;
using GameObject = UnityEngine.GameObject;

namespace TrajectoryObject
{

    /// <summary>
    /// This class creates a new scene as a copy of the current scene then it projects the
    /// player into the scene
    /// </summary>
    public class Projection : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        [SerializeField] private int maxPhysicsFrameIterations = 70;
        private Transform obstaclesParent;
        //[SerializeField] public GameObject ghostPlayer;
        private bool _projectionEnabled = false;
        private GameObject ghosty;
        private GameObjectUtilities gameObjectUtilities;
        private GameObject playerGameObject;
        
        private Scene _simulationScene;
        private PhysicsScene _physicsScene;
        
        private readonly Dictionary<Transform, Transform> _spawnedObjects = new Dictionary<Transform, Transform>();

        /// <summary>
        /// Gismo to show the developer if the object will not function correctly at run time
        /// </summary>
        private void OnDrawGizmos()
        {
            if ( (_lineRenderer.IsUnityNull()) || (_projectionEnabled && obstaclesParent == null ) )
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(playerGameObject.transform.position + Vector3.up * 2, 0.5f);
                Debug.LogWarning("Projection missing scene elements");
            }
        }

        /// <summary>
        /// This start creates a 'ghosty' object that is a copy of the game object being tracked so it can bounce around
        /// in some limited future in a simulated environment
        /// </summary>
        private void Start()
        {
            //CreatePhysicsScene();
            //_simBall = GameObject.FindObjectOfType<SimBall>();
            playerGameObject = GameObject.FindWithTag("Player");
            _lineRenderer = playerGameObject.GetComponent<LineRenderer>();
            gameObjectUtilities = gameObject.AddComponent<GameObjectUtilities>();
            ghosty = gameObjectUtilities.CreateNewInstanceOfGameObject(playerGameObject, "ghosty");
            try
            {
                obstaclesParent = GameObject.FindWithTag("SceneElements").transform;
            }
            catch (Exception e)
            {
                Debug.LogError("SceneElements tag not found in objects in scene : " + e);
            }
        }

        /// <summary>
        /// Create a new projection by creating a copy of the current scene
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
        /// Adds objects in the sim scene so the player movement can predict what would happen(minus effects)
        /// 
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
                    gameObjectUtilities.RemoveComponentsWithJustScripts(ghostObj);
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
        /// <param name="pos">The position of the object being tracked</param>
        /// <param name="velocity">The current velocity(and direction) of the object being tracked</param>
        public bool SimulateTrajectory( Vector3 pos, Vector3 velocity)
        {
            //GameObjectUtilities gameObjectUtilities = gameObject.GetComponent<GameObjectUtilities>();
            
            ghosty.SetActive(true);
            Debug.Log("ghosty : " + ghosty );
            //if (!_projectionEnabled || ballPrefab == null) return;
            bool landed = false;
            //
            var posTemp = pos;
            posTemp.x = 1000.0f;
            posTemp.y = 1000.0f;
            posTemp.z = 1000.0f;
            //ghostPlayer.SetActive(true);
            //GameObject ghostObj = Instantiate(ghostPlayer, posTemp, Quaternion.identity);
            GameObject ghostObj = Instantiate(ghosty, posTemp, Quaternion.identity);
           
            SceneManager.MoveGameObjectToScene(ghostObj.gameObject, _simulationScene);
            ghostObj.gameObject.GetComponent<Rigidbody>().position = pos;
            
            //ghostObj.Init(velocity);
            ghostObj.gameObject.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.Impulse);
            _lineRenderer.enabled = true;
            _lineRenderer.positionCount = maxPhysicsFrameIterations;

            for (var i = 0; i < maxPhysicsFrameIterations; i++)
            {
                _physicsScene.Simulate(Time.fixedDeltaTime);
                _lineRenderer.SetPosition(i, ghostObj.gameObject.transform.position);
                //Debug.Log("pos: " + i + " @ " + transform.position);
                /*Ray ray2 = new Ray(ghostObj.transform.position, Vector3.down);
                if (Physics.Raycast(ray2, out var hitInfo2, 1.0f))
                {
                    Debug.Log("hitInfo2 : i " + i + "gameobject(coll): " + hitInfo2.collider.gameObject.name);
                }*/
            }

            Ray ray = new Ray(ghostObj.gameObject.transform.position, Vector3.down);
            if ( Physics.Raycast(ray, out var hitInfo, 7.0f))
            {
                //Debug.Log("hitInfo : " + hitInfo);
                _lineRenderer.startColor = Color.green;
                _lineRenderer.endColor = Color.cyan;
                _lineRenderer.SetPosition(1, pos);
            }
            else
            {
                //Debug.Log("false hitInfo : " + hitInfo);
                _lineRenderer.startColor = Color.red;
                _lineRenderer.endColor = Color.black;
                //Vector3 pos = ghostObj.transform.position;
                //pos.y = pos.y - 15.0f; 
                _lineRenderer.SetPosition(1, pos);
            }
            Destroy(ghostObj.gameObject);
            //ghostPlayer.SetActive(false);
            ghosty.gameObject.SetActive(false);
            return landed;
        }

        /// <summary>
        /// Tidy up the simulation
        /// </summary>
        public void RemoveTrajectoryLine()
        {
            _lineRenderer.enabled = false;
        }
    }
}