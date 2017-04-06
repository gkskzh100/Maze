using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour {
	public GameObject player;
	private NavMeshAgent nav;
	private string state = "idle";
	private bool alive = true;
	private Animator anim;
	public Transform eyes;
	private float wait = 0f;
	private bool highAlert = false;
	private float alertness = 20f;

	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		nav.radius = 3f;
		nav.height = 7f;
		nav.baseOffset = -1f;
		nav.speed = 3.5f;
		anim.speed = 1.2f;
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
		}
		// nav.SetDestination(player.transform.position);
	}
}
