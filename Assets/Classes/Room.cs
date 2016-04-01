using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour
{
    public int nLeftDoor;
    public int nRightDoor;
    public int nUpDoor;
    public int nDownDoor;

    public Vector2[] leftDoorLocation;
    public Vector2[] rightDoorLocation;
    public Vector2[] upDoorLocation;
    public Vector2[] downDoorLocation;

    public Vector2[] addOnLeft;
    public Vector2[] addOnRight;
    public Vector2[] addOnUp;
    public Vector2[] addOnDown;

    //Se a Room for Referencia (ou seja, vai ter uma Room sendo adicionada a ela), os campos necessarios
    //sao a localização das portas.

    //Se a Room vai ser adicionada a uma referencia, os campos necessarios sao os campos addOnX.

    //--

    //Pelo modo como os GameObjects de cada Room foram criados, para uma Room ser adicionada à:
    //Esquerda:
    //addOnLeft.x = leftDoorLocation.x + 1;
    //addOnLeft.y = -leftDoorLocation.y;

    //Direita:
    //addOnRight.x = -(rightDoorLocation.x + 1);
    //addOnRight.y = -rightDoorLocation.y;

}
