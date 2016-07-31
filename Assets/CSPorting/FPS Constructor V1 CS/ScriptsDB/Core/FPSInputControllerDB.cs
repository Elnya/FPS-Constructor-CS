﻿using UnityEngine;
using System.Collections;
using ooparts.fpsctorcs;
namespace ooparts.fpsctorcs
{
	[RequireComponent(typeof(CharacterMotorDB))]
	public class FPSInputControllerDB : MonoBehaviour
	{
		private CharacterMotorDB motor;

		// Use this for initialization
		void Awake()
		{
			motor = GetComponent<CharacterMotorDB>();
		}

		// Update is called once per frame
		void Update()
		{
			// Get the input vector from kayboard or analog stick
			Vector3 directionVector = new Vector3(InputDB.GetAxis("Horizontal"), 0, InputDB.GetAxis("Vertical"));

			if (directionVector != Vector3.zero)
			{
				// Get the length of the directon vector and then normalize it
				// Dividing by the length is cheaper than normalizing when we already have the length anyway
				float directionLength = directionVector.magnitude;
				directionVector = directionVector / directionLength;

				// Make sure the length is no bigger than 1
				directionLength = Mathf.Min(1, directionLength);

				// Make the input vector more sensitive towards the extremes and less sensitive in the middle
				// This makes it easier to control slow speeds when using analog sticks
				directionLength = directionLength * directionLength;

				// Multiply the normalized direction vector by the modified length
				directionVector = directionVector * directionLength;
			}

			// Apply the direction to the CharacterMotorDB
			motor.inputMoveDirection = transform.rotation * directionVector;
			motor.inputJump = InputDB.GetButton("Jump");
		}
	}
}