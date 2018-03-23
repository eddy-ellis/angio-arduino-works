using UnityEngine;
using System.Collections;
using Uniduino;

public class sphereMover : MonoBehaviour
{
	//Headers aren't scrictly neccesary, but they make life easier back in the Inspector.
	[Header("Arduino Variables")]

	//we need to declare the Arduino as a variable
	public Arduino arduino;

	public GameObject Object1;

	//we need to declare an integer for the pin number of our potentiometer,
	//making these variables public means we can change them in the editor later
	//if we change the layout of our arduino
	public int joyPinNumber;
	public int joyPinNumber2;
	public int joyPinNumber3;
	public int joyPinNumber4;
	public int joyPinNumber5;
	//a float variable to hold the potentiometer value (0 - 1023)
	public float joyValue;
	public float joyValue2;
	public float joyValue3;
	public float joyValue4;
	public float joyValue5;
	//we will later remap that potValue to the y position of our capsule and hold it in this variable
	public float mappedJoy;
	public float mappedJoy2;
	public float mappedJoy3;
	public float mappedJoy4;
	public float mappedJoy5;

	//public int for our button pin
	public int buttonPinNumber;

	[Header("Sphere Variables")]

	//variables to hold the values we noted earlier for the sides of our screen
	public float leftEdge;
	public float rightEdge;
	public float bottomEdge;
	public float topEdge;
	public float spinSpeed;

	// Use this for initialization
	void Start ()
	{//and initialize we shall, starting with the Arduino Variable. 
		//we are only using one arduino, so we can use Arduino.global to grab it.
		arduino = Arduino.global;
		arduino.Setup(ConfigurePins);
	}

	void ConfigurePins()
	{
		//configure the Arduino pin to be analog for our potentiometer
		arduino.pinMode(joyPinNumber, PinMode.ANALOG);
		arduino.pinMode(joyPinNumber2, PinMode.ANALOG);
		arduino.pinMode(joyPinNumber3, PinMode.ANALOG);
		arduino.pinMode(joyPinNumber4, PinMode.ANALOG);
		arduino.pinMode(joyPinNumber5, PinMode.ANALOG);
		//Tell the Arduino to report any changes in the value of our potentiometer
		arduino.reportAnalog(1, 1);  //up down
		arduino.reportAnalog (3, 1); //left right
		arduino.reportAnalog (7, 1); //rotation left right
		arduino.reportAnalog (15, 1); //rotation up down

		//configure our Button pin
		arduino.pinMode(buttonPinNumber, PinMode.INPUT);
		arduino.reportDigital((byte)(buttonPinNumber / 8), 1);
	}
	void Update ()
	{
		//We assign the value the arduino is reading from our potentionmeter to our potValue variable
		joyValue = arduino.analogRead(joyPinNumber); //joystick digital imput
		joyValue2 = arduino.analogRead(joyPinNumber2);
		joyValue3= arduino.analogRead(joyPinNumber3);
		joyValue4 = arduino.analogRead(joyPinNumber4);
		joyValue5 = arduino.analogRead(joyPinNumber5);

		mappedJoy = joyValue.Remap (1023, 0, -1, 1); //changed imput for unity
		mappedJoy2 = joyValue2.Remap (1023, 0, 1, -1);

		mappedJoy3 = joyValue3.Remap (1023, 0, 1, -1);
		float PosX = transform.position.x;
		float PosY = transform.position.y;



		if ((joyValue <=400) || (joyValue >=600)){
			PosX = PosX + mappedJoy;

		}


		if ((joyValue2 <= 400) || (joyValue2 >= 600)) {
			PosY = PosY + mappedJoy2;

			//transform.position = new Vector4 (transform.position.x, transform.position.y + mappedJoy2, transform.position.z);
		}
		//transform limits of "sphere"
		if (PosY > topEdge)	
			PosY = topEdge;
		if (PosY < bottomEdge) 
			PosY = bottomEdge;
		if (PosX < leftEdge)
			PosX = leftEdge;
		if (PosX > rightEdge)
			PosX = rightEdge;	
		
			
		transform.position = new Vector3(PosX, PosY, transform.position.z);


		if (joyValue3 <= 250) {
			transform.RotateAround (transform.position, Vector3.up, mappedJoy3 * spinSpeed);
		}
		if (joyValue3 >= 750) {
			transform.RotateAround (transform.position, Vector3.up, mappedJoy3 * spinSpeed);
		}




		//if Unity detects the button is being pressed, the time scale slows down
		if (arduino.digitalRead(buttonPinNumber) == 1){
			Time.timeScale = 0.4f;
		}
		else Time.timeScale = 1.0f;
	} 

}