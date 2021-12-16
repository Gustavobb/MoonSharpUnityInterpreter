function Setup()
    Require("GameObject", "UnityEngine");
    Require("Transform", "UnityEngine");
    Require("Vector3", "UnityEngine");
end

function Start()
    transform = GameObject.FindWithTag("Player").transform;
end

function Update()
    transform.position = Vector3.up;
end