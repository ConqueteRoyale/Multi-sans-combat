using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//2018-13-21
//Kevin Langlois
//Script qui permet la synchronisation de data entre les clients du serveur
public class NetworkCharacterMULTITEST : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		if(stream.isWriting){
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			stream.SendNext(anim.GetBool("Start"));
		} else{
			transform.position = (Vector3) stream.ReceiveNext();
			transform.rotation = (Quaternion) stream.ReceiveNext();
			anim.SetBool("Start", (bool)stream.ReceiveNext());
		}
	}

}
