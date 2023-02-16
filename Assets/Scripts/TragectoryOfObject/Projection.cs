using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projection : MonoBehaviour {
    [SerializeField] private LineRenderer line;
    [SerializeField] private int maxPhysicsFrameIterations = 100;
    [SerializeField] private Transform obstaclesParent;
    private bool _projectionEnabled = false;

    private Scene _simulationScene;
    private PhysicsScene _physicsScene;
    private readonly Dictionary<Transform, Transform> _spawnedObjects = new Dictionary<Transform, Transform>();
 
    private void Start() {
        CreatePhysicsScene();
    }

    private void CreatePhysicsScene()
    {
        if (obstaclesParent == null) return;
        _simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _simulationScene.GetPhysicsScene();

        AddGameObjects(_simulationScene, obstaclesParent);
    }

    private void AddGameObjects(Scene mySimScene, Transform obstaclesPar )
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

    private void Update()
    {
        if (!_projectionEnabled) return;
        foreach (var item in _spawnedObjects) {
            item.Value.position = item.Key.position;
            item.Value.rotation = item.Key.rotation;
        }
    }

    public void SimulateTrajectory(SimBall ballPrefab, Vector3 pos, Vector3 velocity)
    {
        if (!_projectionEnabled || ballPrefab == null) return;
        var posTemp = pos;
        posTemp.x = 1000.0f;
        posTemp.y = 1000.0f;
        posTemp.z = 1000.0f;
        var ghostObj = Instantiate(ballPrefab, posTemp, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, _simulationScene);
        ghostObj.gameObject.GetComponent<Rigidbody>().position = pos;
        ghostObj.Init(velocity);
        line.enabled = true;
        line.positionCount = maxPhysicsFrameIterations;

        for (var i = 0; i < maxPhysicsFrameIterations; i++) {
            _physicsScene.Simulate(Time.fixedDeltaTime);
            line.SetPosition(i, ghostObj.transform.position);
        }

        Destroy(ghostObj.gameObject);
    }

    public void RemoveTrajectoryLine()
    {
        line.enabled = false;
    }
}