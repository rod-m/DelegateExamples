using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerDelegateExample : MonoBehaviour
{
    Material m_Material;    
    delegate void PlayerAbility();
    public Color noAbilityColour = Color.white;
    public Color basicColour = Color.black;
    public Color fastColour = Color.red;
    public Color shieldColour = Color.yellow;
    [SerializeField]
    private float speed = 0f;
    public float speedNormal = 5.0f;
    public float speedFast = 40.0f;
    public float rotationSpeed = 100.0f;
    private PlayerAbility _playerAbility;
    Vector3 m_EulerAngleVelocity;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Material from the Renderer of the GameObject
        m_Material = GetComponent<Renderer>().material;
        //Set the axis the Rigidbody rotates in (100 in the y axis)
        m_EulerAngleVelocity = Vector3.forward;
        rb = GetComponent<Rigidbody>();
        _playerAbility += NoAbility;
        
        StartCoroutine("PlayerWaking");
    }

    void FixedUpdate ()
    {
       
        if (_playerAbility != null)
        {
            _playerAbility();
        } 
       
    }

    private void PlayerMoving()
    {
        m_Material.color = basicColour;
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        m_EulerAngleVelocity.y += moveHorizontal * rotationSpeed;
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity);
        rb.MoveRotation(deltaRotation);
        rb.AddForce (transform.forward * speed * moveVertical); 
    }

    private void Update()
    {
        
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerAbility += PoweringUp;
        }

      /*  if (Input.GetKeyUp(KeyCode.Space))
        {
            _playerAbility -= PoweringUp;
        }
        */
        if (Input.GetKeyDown(KeyCode.C))
        {
            _playerAbility += ShieldOn;
        }

    
    }

    
    void NoAbility()
    {
        //m_Material.color = noAbilityColour;
        speed = speedNormal;
        
    }

    void PoweringUp()
    {
        // add logic - has a power pack?
        speed = speedFast;
        m_Material.color = (basicColour + fastColour) / 2;
        //m_Material.SetColor("_Color", (basicColour + fastColour) / 2);
        StartCoroutine(FadeIt(fastColour));
        StartCoroutine(ShieldFade(PoweringUp, 1.5f));
    }

    void ShieldOn()
    {
        // add logic ? has a shield orb?
        speed = speedNormal / 2;
        m_Material.color = shieldColour;
 
        StartCoroutine(ShieldFade(ShieldOn, 3f));
    }

    IEnumerator FadeIt(Color _color)
    {
       // m_Material.color = _color;
        for (float ft = 1f; ft >= 0; ft -= 0.1f)
        {
            _color.a = ft;
           m_Material.color = _color;
            yield return new WaitForSeconds(.1f);
        }
       // _playerAbility -= _ability;
  
    }
    
    IEnumerator ShieldFade(PlayerAbility _ability, float _t)
    {
        yield return new WaitForSeconds(_t);
        _playerAbility -= _ability;
        //After we have waited 1 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    IEnumerator PlayerWaking()
    {
        basicColour.a = 0f;
      //  m_Material.color = basicColour;

        
        m_Material.SetColor("_Color", basicColour);
        Debug.Log("Player waking started at : " + Time.time);
        for (float ft = 0f; ft <= 1f; ft += 0.05f)
        {
    
            basicColour.a = ft;
            m_Material.color = basicColour;
            yield return new WaitForSeconds(.1f);
        }
        Debug.Log("Player started at timestamp : " + Time.time);
        //m_Material.color = basicColour;
        m_Material.SetColor("_Color", basicColour);
        _playerAbility += PlayerMoving;
    }
}
