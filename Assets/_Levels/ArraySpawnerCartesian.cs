using UnityEngine;
using System.Collections;

public class ArraySpawnerCartesian : MonoBehaviour {
    [SerializeField]
    Transform thing;
	public float xmin;
	public float xmax;
	public float xIncrementor;
	public float ymin;
	public float ymax;
	public float yIncrementor;
	public float zmin;
	public float zmax;
	public float zIncrementor;




    // Use this for initialization
    void Start () {
	//	Debug.Log ("I started_arr");
	//	for (int radInc = 0; radInc < radii.Length; radInc++) {
	//		for (float z = zmin; z < zmax; z+=zIncrementor) {
	//			for (int phi=0; phi < 360; phi+=phiIncrementor) {
	//				Instantiate (thing, new Vector3 (radii[radInc] * Mathf.Cos (phi * Mathf.Deg2Rad), z, radii[radInc] * Mathf.Sin (phi * Mathf.Deg2Rad)), Quaternion.identity);
		Debug.Log ("I started_arr");
		for (float x = xmin; x <= xmax; x = x+xIncrementor) {
			for (float y = ymin; y <= ymax; y = y+yIncrementor) {
				for (float z = zmin; z <= zmax; z = z+zIncrementor) {
					Instantiate (thing, new Vector3 (x, y, z), Quaternion.identity);
				}
			}
		}
	}
}