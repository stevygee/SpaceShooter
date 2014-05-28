#pragma strict

public var moveSpeed : float = 10f;

function Start () {

}

function Update ()
{
	// Movement
	if(Input.GetKey(KeyCode.W))
		transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

	if(Input.GetKey(KeyCode.S))
		transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

	if(Input.GetKey(KeyCode.A))
		transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

	if(Input.GetKey(KeyCode.D))
		transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

    // TODO: Weapons
    //if(Input.GetKey(KeyCode.Space))
}