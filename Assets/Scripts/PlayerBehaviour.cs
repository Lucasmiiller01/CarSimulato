using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

	private int rotateVelocity = 70;
	private int moveVelocity = 5;
	public WheelCollider roda1;
	public WheelCollider roda2;
	public WheelCollider roda3;
	public WheelCollider roda4;
	public Transform Rooda1;
	public Transform Rooda2;
	public Transform Rooda3;
	public Transform Rooda4;

	public Texture Medidor;
	public Texture Puntero;


	public float Velocidade;
	public int VelocidadeMax = 200;
	public int Aceleration = 100;
	public int DesAceleration = 80;
	int Angulo =0;
	public bool carro = false;



	private int valor = 0;
	// Use this for initialization
	void Start () {
		carro = false;
		transform.rigidbody.centerOfMass = new Vector3 (0, -1f, 0);
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag.Equals("Player"))carro = true;




	}
	void Update(){
		if (carro.Equals (true)) {
			Angulo ++;
			if (Angulo > 360)
				Angulo = 0;

			Rooda1.localEulerAngles = new Vector3 (0, roda1.steerAngle * 2, 0);
			Rooda2.localEulerAngles = new Vector3 (0, roda2.steerAngle * 2, 0);

			Velocidade = (2 * Mathf.PI * roda1.radius) * roda1.rpm * 60 / 1000;
			Velocidade = Mathf.Round (Velocidade);

			Rooda1.Rotate (new Vector3 ((Angulo * roda1.rpm / 60 * 360) * Time.deltaTime, 0, 0));
			Rooda2.Rotate (new Vector3 ((Angulo * roda1.rpm / 60 * 360) * Time.deltaTime, 0, 0));
			Rooda3.Rotate (new Vector3 (roda1.rpm / 60 * 360 * Time.deltaTime, 0, 0));
			Rooda4.Rotate (new Vector3 (roda1.rpm / 60 * 360 * Time.deltaTime, 0, 0));
		}

	}
	void FixedUpdate () {
		if (carro.Equals (true)) {
			if (Mathf.Abs (Velocidade) < VelocidadeMax) {
				roda1.motorTorque = Aceleration * NovosAxis ("Vertical");
				roda2.motorTorque = Aceleration * NovosAxis ("Vertical");
			} else {
				roda1.motorTorque = 0;
				roda2.motorTorque = 0;
			}
			if (Input.GetAxis ("Vertical") == 0) {
				roda1.brakeTorque = DesAceleration;
				roda2.brakeTorque = DesAceleration;
			} else {
				roda1.brakeTorque = 0;
				roda2.brakeTorque = 0;
			}
			roda1.steerAngle = 10 * NovosAxis ("Horizontal");
			roda2.steerAngle = 10 * NovosAxis ("Horizontal");
		
		}
	}
	float NovosAxis(string direcao){
		return(Input.GetAxis (direcao));	
	}

	void OnGUI(){
		GUI.DrawTexture (new Rect(Screen.width - 200,Screen.height-120,200,200),Medidor);
		float Angulo = Mathf.Abs(Velocidade) * 180 / 320;
		GUIUtility.RotateAroundPivot (Angulo, new Vector2 (Screen.width - 100, Screen.height - 20));
		GUI.DrawTexture (new Rect(Screen.width - 200,Screen.height-120,200,200),Puntero);
	}



}

	

