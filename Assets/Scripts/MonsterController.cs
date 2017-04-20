using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MonsterController : MonoBehaviour {
	public GameObject player;
	public GameObject deathCam;
	public Transform camPosition;
	private NavMeshAgent nav;
	private string state = "idle";
	private bool alive;
	private Animator anim;
	public Transform eyes;
	private float wait = 0f;
	private bool highAlert = false;
	private float alertness = 20f;
	private PlayerAlive playerAlive;
	private PlayController playController;

	

	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		nav.radius = 3f;
		nav.height = 7f;
		nav.baseOffset = -1f;
		nav.speed = 3.5f;
		anim.speed = 1.2f;
		player = GameObject.Find("Player");
		playerAlive = GameObject.Find("Player").GetComponent<PlayerAlive>();
		alive = playerAlive.alive;
	}

	public void checkSight() {
		if(alive) {
			RaycastHit rayHit;
			if(Physics.Linecast(eyes.position,player.transform.position, out rayHit)) {
				if(rayHit.collider.gameObject.name == "Player") {
					if (state != "kill") {
						state = "chase";
						nav.speed = 4.5f;
						anim.speed = 2.5f;
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(alive) {
			if(state == "idle") {
				Vector3 randomPos = Random.insideUnitSphere * alertness;
				NavMeshHit navHit;
				 NavMesh.SamplePosition(transform.position + randomPos, out navHit, 50f, NavMesh.AllAreas);
				 //go near the player//
				 if(highAlert) {
					 NavMesh.SamplePosition(player.transform.position + randomPos, out navHit, 50f, NavMesh.AllAreas);

					 alertness += 5f;
					 if (alertness > 20f) {
						 highAlert = false; 
						 nav.speed = 3.5f;
						 anim.speed = 1.2f;
					 }
				 }
				 nav.SetDestination(navHit.position);
				 state = "walk";
			}
			if(state == "walk") {
				if(nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending) {
					state = "search";
					wait = 3f;
				}
			}
			if(state == "search") {
				if(wait > 0f) {
					wait -= Time.deltaTime;
					transform.Rotate(0f, 120f * Time.deltaTime,0f);
				} else {
					state = "idle";
				}
			}
			if(state == "chase") {
				nav.destination = player.transform.position;
				
				//lose sight of player//
				float distance = Vector3.Distance(transform.position, player.transform.position);
				if(distance > 10f) {
					state = "hunt"; 
				}
				//kill the player//
				else if(nav.remainingDistance <= nav.stoppingDistance + 1f && !nav.pathPending) {
					if(playerAlive.GetComponent<PlayerAlive>().alive) {
						// StateKill();
					}
				}
			}
			if(state == "hunt") {
				if(nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending) {
					state = "search";
					wait = 3f;
					highAlert = true;
					alertness = 5f;
					checkSight();
				}
			}
			if(state == "kill") {
				deathCam.transform.position = Vector3.Slerp(deathCam.transform.position,camPosition.transform.position,10f*Time.deltaTime);
				deathCam.transform.rotation = Quaternion.Slerp(deathCam.transform.rotation,camPosition.transform.rotation,10f*Time.deltaTime);
				nav.SetDestination(deathCam.transform.position);
			}
		}
	}

	void reset() {
		// SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		state = "idle";
		playerAlive.GetComponent<PlayerAlive>().alive = true;
		playController.GetComponent<PlayController>().enabled = true;
		deathCam.SetActive(false);
		GameObject.Find("Player").transform.FindChild("Camera").gameObject.SetActive(true);
		
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.name == "Player") {
			playController = GameObject.Find("Player").GetComponent<PlayController>();
			// playController.Died();
			StateKill();
		}
	}

	private void StateKill() {
		state = "kill";
		playerAlive.GetComponent<PlayerAlive>().alive = false;
		playController.GetComponent<PlayController>().enabled = false;
		deathCam.SetActive(true);
		deathCam.transform.position = Camera.main.transform.position;
		deathCam.transform.rotation = Camera.main.transform.rotation;
		Camera.main.gameObject.SetActive(false);
		playController = GameObject.Find("Player").GetComponent<PlayController>();
		playController.Died();
		Invoke("reset",3f);
	}
}
