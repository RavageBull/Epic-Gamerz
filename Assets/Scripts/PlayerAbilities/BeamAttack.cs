using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamAttack : MonoBehaviour
{
    //So basicalllyyyyyy, you want to do this particular ability the way the "Raycast Shooting" works

    //You will need to create variables that control: shoot damage, shooting range. (You can play with numbers to make it how you want)
    //A good video to watch on how to do this is < https://www.youtube.com/watch?v=THnivyG0Mvo >
    // Brackeys does tutorials for a lot of cool stuff, so you can look up other stuff on his channel as well

    // After getting raycast shooting working, you want a way to control the "agent" on the player character, for slowing down movement and stuff.
    // Also take a look at the movement script on the player and see what you can reference in this script and play with.
    // If this script is going to go straight on the character, then add "using UnityEngine.AI;" at the top. This lets you access the AI functions library, which is mostly everything to do with Nav Mesh.
    // After adding the library you want to create a variable that is written like this "NavMeshAgent agent;" in you go back onto unity and have a look on the players inspector you will see a component called NavMeshAgent
    // It has a bunch of different things you can play with. This is useful because you can mess with them in you're code for abilities. For Example, if you want to play with the speed, you reference it by writing "agent.speed".
    // Here is a link to information about using the Nav Mesh stuff from the unity website < https://docs.unity3d.com/2018.4/Documentation/ScriptReference/AI.NavMesh.html >
    // also there is a lot of stuff you can look up on there, there are different sections to click on, on the left

    //Just do things step by step and you'll be fine, focus on one thing at a time and google things you are confused about or want to know more about.

    // After you get the basic stuff working, look into particle effects and sounds.

    public float damage = 1f;
    public float range = 100f;
    public Transform firePoint;

    [SerializeField]
    [Range(0f, 1.5f)]
    private float fireRate = 5f;
    private float cooldown = 0f;
    private float abilityTime = 3f;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire2") && Time.time >= cooldown)
        {

            cooldown = Time.time + fireRate;
            Debug.Log("Cooldown time is " + cooldown);

            Debug.Log("Fired");
            Cast();

        }


    }


    void Cast()
    {

        RaycastHit hit;
        Debug.DrawLine(firePoint.position, (firePoint.forward * range), Color.red, abilityTime);

        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range))
        {
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range))

                Debug.Log("Raycast hit this thing " + hit.transform.name);

        }

        StartCoroutine("WaitAndExecute");
        Invoke("StopExecution", 3f);
    }


    void StopExecution()
    {
        Debug.Log("Ability pause done");
        StopCoroutine("WaitAndExecute");
    }

    IEnumerator WaitAndExecute()
    {
        print("Printed after wait time");
        yield return new WaitForSeconds(abilityTime);

       // StartCoroutine("WaitAndExecute");
    }


    /*private void InvokeRepeating()
    {
        InvokeRepeating("Cast", 0f, 0.2f);

        if (Input.GetButtonDown("Jump") && Time.time >= cooldown)
        {
            CancelInvoke();
            cooldown = Time.time + fireRate;
            Debug.Log("Cancel" );
            
        }
    }*/

}





