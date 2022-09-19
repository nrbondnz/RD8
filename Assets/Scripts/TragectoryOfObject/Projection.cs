using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projection : MonoBehaviour {
    [SerializeField] private LineRenderer _line;
    [SerializeField] private int _maxPhysicsFrameIterations = 100;
    [SerializeField] private Transform _obstaclesParent;

    private Scene _simulationScene;
    private PhysicsScene _physicsScene;
    private readonly Dictionary<Transform, Transform> _spawnedObjects = new Dictionary<Transform, Transform>();
    private static Projection _instance;

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
    }
    private void Start() {
        CreatePhysicsScene();
    }

    private void CreatePhysicsScene() {
        _simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _simulationScene.GetPhysicsScene();

        AddGameObjects(_simulationScene, _obstaclesParent);
    }

    private void AddGameObjects(Scene mySimScene, Transform _obstaclesPar )
    {
        foreach (Transform obj in _obstaclesPar)
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

    private void Update() {
        foreach (var item in _spawnedObjects) {
            item.Value.position = item.Key.position;
            item.Value.rotation = item.Key.rotation;
        }
    }

    public void SimulateTrajectory(SimBall ballPrefab, Vector3 pos, Vector3 velocity)
    {
        var posTemp = pos;
        posTemp.x = 1000.0f;
        posTemp.y = 1000.0f;
        posTemp.z = 1000.0f;
        var ghostObj = Instantiate(ballPrefab, posTemp, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, _simulationScene);
        ghostObj.gameObject.GetComponent<Rigidbody>().position = pos;
        ghostObj.Init(velocity);

        _line.positionCount = _maxPhysicsFrameIterations;

        for (var i = 0; i < _maxPhysicsFrameIterations; i++) {
            _physicsScene.Simulate(Time.fixedDeltaTime);
            _line.SetPosition(i, ghostObj.transform.position);
        }

        Destroy(ghostObj.gameObject);
    }
}