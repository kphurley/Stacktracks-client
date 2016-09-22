using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject cube;
	public int GRID_X;
	public int GRID_Z;

	private int[,] map;

	void Start () {

		CreateBarricades ();
	
	}
	
	void CreateMap(){
		map = new int[GRID_X, GRID_Z];
		for (int x = 0; x < GRID_X; x++) {
			for (int z = 0; z < GRID_Z; z++) {
				map [x, z] = 1;
			}
		}

		for (int x = 10; x < GRID_X-10; x++) {
			for (int z = 10; z < GRID_Z-10; z++) {
				map [x, z] = 0;
			}
		}

		for (int x = 20; x < GRID_X-20; x++) {
			for (int z = 20; z < GRID_Z-20; z++) {
				map [x, z] = 1;
			}
		}
	}

	void CreateBarricades (){
		CreateMap ();
		for (int x=0;x<GRID_X;x++)
		{
			for (int z=0;z<GRID_Z;z++)
			{
				switch (map [x, z]) {
					case 0:
						break;
					case 1:
						Instantiate (cube, new Vector3 ((float)x, 0f, (float)z), Quaternion.identity);
						break;
					default:
						break;
				}
			}
		}
	}
}
