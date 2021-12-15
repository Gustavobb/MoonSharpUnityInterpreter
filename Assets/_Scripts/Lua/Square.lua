function Setup()
    Require("GameObject", "UnityEngine")
    Require("Transform", "UnityEngine")
    Require("Vector3", "UnityEngine")
    Require("Input", "UnityEngine")
    return;
end

function Start()
    transform = GameObject.FindWithTag("Player").transform;
end

function Update()
    transform.position = GetMousePos();
end