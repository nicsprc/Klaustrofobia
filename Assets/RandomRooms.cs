using UnityEngine;
using System.Collections;

public class RandomRooms : MonoBehaviour {

	[SerializeField] Transform roomStart;
	private int random;
	private int numRooms;

	public GameObject[] conjuntos;
	//public GameObject[] conectionRooms;

	// Use this for initialization
	void Start () {
		numRooms = 5;
		//Rooms
		//1 = RoomConection1
		//2 = RoomConection2
		//ConectionRooms
		//1 = RoomSciFi1
		//2 = RoomSciFi2
		random = Random.Range (1,3);
		random = random - 1;

		//for(int i=0;i<numRooms;i++)
		//{
		//}

		Vector3 roomPos = roomStart.position;
		roomPos.x = roomPos.x + 42f;
		roomPos.y = roomPos.y + 8.5f;

		Instantiate (conjuntos[0], roomPos, roomStart.rotation);
	}
}
