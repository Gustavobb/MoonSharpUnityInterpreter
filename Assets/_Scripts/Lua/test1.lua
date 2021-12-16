function Setup()
    Require("RaycastHit2D", "UnityEngine");
    Require("Rigidbody2D", "UnityEngine");
    Require("GameObject", "UnityEngine");
    Require("Physics2D", "UnityEngine");
    Require("Transform", "UnityEngine");
    Require("Vector3", "UnityEngine");
    Require("Vector2", "UnityEngine");
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
        mousePos = MousePos();
        vec2 = Vector2.zero;
        vec2.x = mousePos.x;
        vec2.y = mousePos.y;
        
        hit = Physics2D.Raycast(vec2, Vector2.zero);
            
        if hit.transform.gameObject.tag == "Player" then
            rigidbody.velocity = Vector2.zero;
            transform.position = mousePos;
        end
    end
end

function MousePos()
    vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    vector.z = 0;
    return vector;
end