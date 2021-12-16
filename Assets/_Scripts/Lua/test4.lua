function Setup()
    Require("RaycastHit2D", "UnityEngine");
    Require("Rigidbody2D", "UnityEngine");
    Require("GameObject", "UnityEngine");
    Require("Physics2D", "UnityEngine");
    Require("BoxCollider2D", "UnityEngine");
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

    code = [[ 
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
    end]];

    form = {}
    form["code"] = code;

    Post("https://test-lua-api.herokuapp.com/v1/post-code", form, "Callback");
end

function Callback(response)
    Print("OK");
end

function Update()
    if Input.GetMouseButton(0) then
        mousePos = MousePos();
        vec2 = Vector2.zero;
        vec2.x = mousePos.x;
        vec2.y = mousePos.y;
        
        hit = Physics2D.Raycast(vec2, Vector2.zero);

        if hit.collider ~= nil and hit.transform.gameObject.tag == "Player" then
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