function Setup()
    Require("Rigidbody2D", "UnityEngine");
    Require("GameObject", "UnityEngine");
    Require("RaycastHit", "UnityEngine");
    Require("Transform", "UnityEngine");
    Require("Vector3", "UnityEngine");
    Require("Vector2", "UnityEngine");
    Require("Physics", "UnityEngine");
    Require("Camera", "UnityEngine");
    Require("Input", "UnityEngine");
    Require("Ray", "UnityEngine");
end

function Start()
    transform = GameObject.FindWithTag("Player").transform;
    rigidbody = transform.gameObject.GetComponent("Rigidbody2D");
end

function Update()
    if Input.GetMouseButton(0) then
        rigidbody.velocity = Vector2.zero;
        transform.position = MousePos();
    end
end

function MousePos()
    vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    vector.z = 0;
    return vector;
end