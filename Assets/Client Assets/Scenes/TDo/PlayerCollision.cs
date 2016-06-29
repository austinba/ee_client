﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerCollision : MonoBehaviour {
  public GameObject particles;
  public Image retFill;
  // public GameObject gvrmain;
  public Camera cam;
  public GameObject zombieSpawner;
  private float charge = 0;
  private float maxCharge = 100;
  private bool boost = false;

  void Start() {
    retFill.type = Image.Type.Filled;
    retFill.fillClockwise = true;
  }

	void Update() {
    var ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    RaycastHit hit = new RaycastHit();
    if (Physics.Raycast(ray, out hit, 1000f)) {
      if (hit.collider.name == "TriggerSphere") {
        fillReticle();
      }
    }
    if (charge > 0) {
      charge--;
    }
    if (charge < 1) {
      boost = false;
      GetComponent<PlayerMovement>().speedMultiplier = 1f;
    }
    retFill.fillAmount = (charge)/maxCharge;
  }
  /////////////////////
  //COLLISION CHECKER//
  /////////////////////
	void OnTriggerEnter(Collider other) {
    particles.GetComponent<ParticleSystem>().Play();
    zombieSpawner.GetComponent<PlayerSpawner>().ZombieCollide(other.transform.parent.gameObject);
  }

  void fillReticle() {
    if (charge < maxCharge && boost == false) {
      charge += 2;
    } else {
      GetComponent<PlayerMovement>().speedMultiplier = 3f;
      boost = true;
    }
  }
}
