﻿using UnityEngine;
using System.Collections;

public class csGizmoView : MonoBehaviour {
	public Color _color = Color.red;
	public float _radius = 0.2f;

	void OnDrawGizmos(){
		Gizmos.color = _color;
		Gizmos.DrawSphere (transform.position, _radius);
	}

}
