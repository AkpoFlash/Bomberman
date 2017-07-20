using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	void Start () {
        GenerateMap map = new GenerateMap();
        map.CreateMap();
    }

}
