#pragma strict

public var moveSpeed : float = 1f;

function Start () {

}

function Update ()
{
	transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
}