﻿using UnityEngine;
using System.Collections;

public class collideWithSphere : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		Destroy(other.gameObject);
	}
}