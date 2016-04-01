using UnityEngine;
using System.Collections;

public class AddRooms : MonoBehaviour {


    //[SerializeField] Transform roomStart;

    public GameObject roomStart;

    public GameObject[] rooms;
    
    public int nOfRooms;

    private Room refRoom;
    private Room roomToAdd;

    // Use this for initialization
    void Start () {

        int r = (int)Mathf.Round((rooms.Length - 1) * Random.value);        
        refRoom = roomStart.GetComponent<Room>();        
        roomToAdd = rooms[r].GetComponent<Room>();
        refRoom = (Room)addRoom(refRoom, roomToAdd);

        for (int i=0; i<nOfRooms-1; i++)
        {
            r = (int)Mathf.Round((rooms.Length - 1) * Random.value);
            roomToAdd = rooms[r].GetComponent<Room>();
            refRoom = (Room)addRoom(refRoom, roomToAdd);
        }        
    }


    public Object addRoom(Room refRoom, Room roomToAdd)
    {
        //Object[] obj = new Object(refRoom.nRightDoor);
        if (refRoom.nRightDoor > 0)
        {
            //for (int j=0; j<refRoom.nLeftDoor; j++)
            //{
            Debug.Log(refRoom.nRightDoor);
                for (int i = 0; i < refRoom.nRightDoor; i++)
                {
                    Vector3 posToAdd = refRoom.transform.position;
                    posToAdd.x = posToAdd.x + refRoom.rightDoorLocation[i].x + roomToAdd.addOnLeft[0].x;
                    posToAdd.y = posToAdd.y + refRoom.rightDoorLocation[i].y + +roomToAdd.addOnLeft[0].y;

                    //Debug.Log("RefRoom x=" + refRoom.transform.position.x + " / Ref Room Right dor x=" + refRoom.rightDoorLocation[i].x + " / Room to add left x=" + roomToAdd.addOnLeft[0].x);
                    //Debug.Log(posToAdd.x);                

                    //obj[i] = Instantiate(roomToAdd, posToAdd, refRoom.transform.rotation);
                }
            //}            
        }

        //return obj;
        return null;
    }
}
