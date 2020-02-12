using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerDelegateExample : MonoBehaviour
{
      // delegate 
    delegate void PlayerAbility();
    private PlayerAbility _playerAbility;
    
    Material m_Material; 
    public Color noAbilityColour = Color.white;
    public Color basicColour = Color.black;
    public Color fastColour = Color.red;
    public Color shieldColour = Color.yellow;
    [SerializeField]
    private float speed = 0f;
    public float speedNormal = 5.0f;
    public float speedFast = 40.0f;
    public float rotationSpeed = 100.0f;
    
    Vector3 m_EulerAngleVelocity;
    private Rigidbody rb;
    private IEnumerator coroutinePW;
    void Start()
    {
        //Fetch the Material from the Renderer of the GameObject
        m_Material = GetComponent<Renderer>().material;
        //Set the axis the Rigidbody rotates in
        m_EulerAngleVelocity = Vector3.forward;
        rb = GetComponent<Rigidbody>();
        _playerAbility += NoAbility; // adds the base state - no ability
        coroutinePW = PlayerWaking();
        StartCoroutine(coroutinePW); // start with player cant even move!
        
    }

    void FixedUpdate ()
    {
       
        if (_playerAbility != null)
        {
            _playerAbility(); // call multicast delegate
        } 
       
    }

    private void PlayerMoving()
    {
        // this is a standard player controller
        // Instead of always being called, or thru a state machine, it is added as a delegate
        m_Material.color = basicColour;
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        m_EulerAngleVelocity.y += moveHorizontal * rotationSpeed;
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity);
        rb.MoveRotation(deltaRotation);
        rb.AddForce (transform.forward * speed * moveVertical);
        if (rb.velocity.magnitude > speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerAbility += PoweringUp; // add a simple method for boosting speed
        }

      /*  if (Input.GetKeyUp(KeyCode.Space))
        {
            _playerAbility -= PoweringUp; // possible to remove a delegate on key up with -=
        }
        */
        if (Input.GetKeyDown(KeyCode.C))
        {
            _playerAbility += ShieldOn;
        }

    
    }

    
    void NoAbility()
    {
        speed = speedNormal;
        
    }

    public void GoHaywire()
    {
        _playerAbility += GoHaywireAbility;
        
       
    }

    public void GoHaywireAbility()
    {
        
        speed = 50;
        rb.velocity = rb.velocity.normalized * speed;
        // remove the ability
        _playerAbility -= PlayerMoving;

        StartCoroutine(PlayerWaking()); 
        _playerAbility -= GoHaywireAbility;
        
        
    }
    void PoweringUp()
    {
        // add logic - has a power pack?
        speed = speedFast;
        m_Material.color = (basicColour + fastColour) / 2;

        StartCoroutine(FadeIt(fastColour));
        StartCoroutine(ShieldFade(PoweringUp, 1.5f));
    }

    void ShieldOn()
    {
        // add logic ? has a shield orb?
        speed = speedNormal / 2;
        m_Material.color = shieldColour;
        _playerAbility -= PoweringUp;
        StartCoroutine(ShieldFade(ShieldOn, 3f));
    }

    IEnumerator FadeIt(Color _color)
    {
        // choose a color to fade out
        for (float ft = 1f; ft >= 0; ft -= 0.1f)
        {
            _color.a = ft;
           m_Material.color = _color;
            yield return new WaitForSeconds(.1f);
        }
    }

   
    IEnumerator ShieldFade(PlayerAbility _ability, float _t)
    {
        yield return new WaitForSeconds(_t);
        _playerAbility -= _ability;
        //After we have waited 1 seconds remove ability.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    IEnumerator PlayerWaking()
    {
        basicColour.a = 0f; 
        m_Material.SetColor("_Color", basicColour); // custom shader method
        // or use also m_Material.color = basicColour;
        Debug.Log("Player waking started at : " + Time.time);
        for (float ft = 0f; ft <= 1f; ft += 0.05f)
        {
    
            basicColour.a = ft;
            //m_Material.color = basicColour;
            m_Material.SetColor("_Color", basicColour); 
            yield return new WaitForSeconds(.1f);
        }
        
      
        m_Material.SetColor("_Color", basicColour);
        yield return new WaitForSeconds(1.5f);
        _playerAbility += PlayerMoving;
        Debug.Log("Player started at timestamp : " + Time.time);
        
        //StopCoroutine(coroutinePW);
    }
}
