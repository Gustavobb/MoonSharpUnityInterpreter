function Setup()
    Require("GameObject", "UnityEngine");
    Require("Transform", "UnityEngine");
    Require("Vector3", "UnityEngine");
    Require("Camera", "UnityEngine");
    Require("Input", "UnityEngine");
end

function Start()
    transform = GameObject.FindWithTag("Player").transform;
end

function Update()
    transform.position = MousePos();
end

function MousePos()
    vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    vector.z = 0;
    return vector;
end