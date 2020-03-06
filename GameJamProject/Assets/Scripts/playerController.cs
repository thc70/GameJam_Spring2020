using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour {
	public float moveSpeed = 5f;
	public float jumpHeight = 5f;
	public float jumpForce = 700f;
	private bool facingRight = true;
	private float moveInput;
	private float countJumps = 2;
	private int nextScene;

	void Start () {
		Scene currentScene = SceneManager.GetActiveScene();
		int buildIndex = currentScene.buildIndex;
		nextScene = buildIndex + 1;

	}

	void FixedUpdate () {
		moveInput = Input.GetAxis("Horizontal");
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
		transform.position += movement *Time.deltaTime * moveSpeed;
		if(facingRight == false && moveInput > 0){
			Flip();
		}
		else if(facingRight == true && moveInput < 0){
			Flip();
		}
	}
	void Update() {
		if(Input.GetKeyDown("space") && countJumps > 0) {
			gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
			countJumps -=1;
		}
	}

	/*void Jump() {
	}*/

	void Flip() {
		facingRight = !facingRight;
		Vector3 Scaler = transform.localScale;
		Scaler.x *= -1;
		transform.localScale = Scaler;
	}

	void OnCollisionEnter2D(Collision2D Col) {
		if(Col.gameObject.tag == "Platform" )
			countJumps = 2;
		if(Col.gameObject.tag == "Bound")
			transform.position = new Vector3(-5f,-1.76f,0);
			// cheat for the first two levels if jump to the bottom
			// transform.position = new Vector3(80f,7f,0);
		if(Col.gameObject.tag == "Checkpoint"){
			SceneManager.LoadScene(nextScene);
		}
		else if(Col.gameObject.tag == "Finish")
			SceneManager.LoadScene("YouWin");
	}
}
