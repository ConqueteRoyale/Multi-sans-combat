using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkCharacterMULTITEST : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		if(stream.isWriting){
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		} else{
			transform.position = (Vector3) stream.ReceiveNext();
			transform.rotation = (Quaternion) stream.ReceiveNext();
		}
	}

}
