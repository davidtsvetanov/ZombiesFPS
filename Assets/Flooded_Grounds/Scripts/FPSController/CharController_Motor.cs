using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class CharController_Motor : MonoBehaviour {
	static public CharController_Motor Instance { get; private set; }
	public float speed = 3.0f;
	public float sprintSpeed = 5.0f;
	public float sensitivity = 30.0f;
	public float WaterHeight = 15.5f;
	CharacterController character;
	public GameObject cam;
	float moveFB, moveLR;
	float rotX, rotY;
	public bool webGLRightClickRotation = true;
	float gravity = -9.8f;
	public float jumpHeight = 1;
	float startGravity = -9.8f;
	private Vector3 playerVelocity;
	public int healthPlayer = 100;
	public Image healthBar;
	public TextMeshProUGUI healthValue;
	public int maxHealth = 100;
	public Animator bloodSplash;
	public GameObject endScreen;
	public float stamina = 100;
	public float maxStamina;
	public int armor = 100;
	public int maxArmor;
	[SerializeField] TextMeshProUGUI staminaText;
	[SerializeField] TextMeshProUGUI moneyText;
	[SerializeField] Image staminaBar;
	public TextMeshProUGUI armorText;
	public Image armorBar;
	public int money;
	[SerializeField] GameObject pistol;
	public GameObject ak;
	public bool inMenu;
	[SerializeField] AudioSource steps;
	[SerializeField] AudioSource woodSteps;
	[SerializeField] Animator characterAnimator;
	[SerializeField] TextMeshProUGUI wavesSurvived;
	public float walkRate;
	private float currentWalkRate;
	public float sprintRate;
	public float minDownPos;
	public float maxUpPos;
	float rotationY = 0f;


	void Start(){
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}
		Cursor.lockState = CursorLockMode.Locked;
		character = GetComponent<CharacterController> ();
		if (Application.isEditor) {
			webGLRightClickRotation = false;
			sensitivity = sensitivity * 1.5f;
		}
		
		ChangeMoneyAmount(0);
		currentWalkRate = walkRate;
	}



    private void FixedUpdate()
    {
		UpdateAnimation();
		if (Input.GetKey(KeyCode.LeftShift))
		{
			if(stamina > 1)
            {
				stamina -= 0.18f;
			}
			
		}
		else
		{
			
			if (stamina < maxStamina)
			{
				stamina += 0.2f;
			}
		}
		if(stamina >= maxStamina)
        {
			stamina = maxStamina;
        }
		staminaText.text = $"STAMINA: {(int)stamina}";
		staminaBar.fillAmount = stamina / maxStamina;
	}
	public void ChangeMoneyAmount(int moneyAmount)
    {

		money += moneyAmount;
		moneyText.text = $"<color=green><b>$</b></color> {money}";
    }
    void LateUpdate(){

		if (Input.GetKey(KeyCode.LeftShift) && stamina > 1)
		{
			moveFB = Input.GetAxis("Horizontal") * sprintSpeed;
			moveLR = Input.GetAxis("Vertical") * sprintSpeed;
			
		}
        else
        {
			moveFB = Input.GetAxis("Horizontal") * speed;
			moveLR = Input.GetAxis("Vertical") * speed;
			
		}

		if (inMenu == false)
		{
			rotX += Input.GetAxis("Mouse X") * sensitivity;
			rotY += Input.GetAxis("Mouse Y") * sensitivity;	
		}
        else
        {
			return;
		}
		
		rotY = Mathf.Clamp(rotY, minDownPos, maxUpPos);
		var xQuat = Quaternion.AngleAxis(rotX, Vector3.up);
		var yQuat = Quaternion.AngleAxis(rotY, Vector3.left);

		transform.localRotation = xQuat;

			cam.transform.localRotation = yQuat;
			
		
			
        
		

		//rotX = Input.GetKey (KeyCode.Joystick1Button4);
		//rotY = Input.GetKey (KeyCode.Joystick1Button5);
		float currentJump = 0;
		if (character.isGrounded)
        {
			if (playerVelocity.y < -.5f)
			{
				playerVelocity.y = -.5f;
			}

		}
		if (Input.GetButtonDown("Jump") && character.isGrounded)
		{
			playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
			Debug.LogError("called");
		}

		playerVelocity.y += gravity * Time.deltaTime;

		
		Vector3 movement = new Vector3 (moveFB, playerVelocity.y, moveLR);

		if((moveFB != 0 || moveLR != 0) && character.isGrounded)
        {


			if (currentWalkRate <= 0)
			{
				RaycastHit hit = new RaycastHit();
				string floortag;
				if (Physics.Raycast(transform.position, Vector3.down, out hit))
				{

					floortag = hit.collider.gameObject.tag;
					if (floortag == "Terain")
					{
						Debug.Log("steps");
						steps.Play();
					}
					else
					{
						woodSteps.Play();
					}
					if (Input.GetKey(KeyCode.LeftShift) && stamina > 1)
					{
						currentWalkRate = sprintRate;
                    }
                    else
                    {
						currentWalkRate = walkRate;
					}
					

				}
			}
			else
			{
				currentWalkRate -= Time.deltaTime;
			}

			
        }

		
			//CameraRotation (cam, rotX, rotY);
		

		movement = transform.rotation * movement;
		character.Move (movement * Time.deltaTime);
	}
    private void Update()
    {
		
	}
    private void UpdateAnimation()
    {
        if (Input.GetKey(KeyCode.W))
        {
			if (Input.GetKey(KeyCode.A))
			{
				if (characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("UpLeft") == false)
				{
					Debug.LogError("calledUpLeft");
					ResetTriggers();
					characterAnimator.SetBool("EnterLeftD", true);
				}
			}
            else if (Input.GetKey(KeyCode.S))
			{
				if (characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") == false)
				{
					ResetTriggers();
					characterAnimator.SetBool("EnterIdle", true);
				}
			}
		    else if (Input.GetKey(KeyCode.D))
			{
				if (characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("UpRight") == false)
				{
					ResetTriggers();
					characterAnimator.SetBool("EnterRightD", true);
				}
			}
            else
            {
				if (characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("Up") == false)
				{
					ResetTriggers();
					characterAnimator.SetBool("EnterRun", true);
				}
			}
		}
		else if (Input.GetKey(KeyCode.A))
		{
			
		    if (Input.GetKey(KeyCode.D))
			{
				if (characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") == false)
				{
					ResetTriggers();
					characterAnimator.SetBool("EnterIdle", true);
				}
			}
			else if (Input.GetKey(KeyCode.S))
			{
				if (characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("DownLeft") == false)
				{
					ResetTriggers();
					characterAnimator.SetBool("EnterBLeftD", true);
				}
			}
            else
            {
				if (characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("Left") == false)
				{
					ResetTriggers();
					characterAnimator.SetBool("EnterLeft", true);
				}
			}
		}
		else if (Input.GetKey(KeyCode.S))
		{
			
		    if (Input.GetKey(KeyCode.D))
			{
				if (characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("DownRight") == false)
				{
					ResetTriggers();
					characterAnimator.SetBool("EnterBRightD", true);
				}
			}
            else
            {
				if (characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("Back") == false)
				{
					ResetTriggers();
					characterAnimator.SetBool("EnterBack", true);
				}
			}
		}
		else if (Input.GetKey(KeyCode.D))
		{
			if (characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("Right") == false)
			{
				ResetTriggers();
				characterAnimator.SetBool("EnterRight", true);
			}
		}
        else
        {
			if (characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") == false)
			{

				ResetTriggers();
				characterAnimator.SetBool("EnterIdle", true);
			}
		}
	}
	private void ResetTriggers()
    {
		characterAnimator.SetBool("EnterIdle", false);
		characterAnimator.SetBool("EnterRight", false);
		characterAnimator.SetBool("EnterBack", false);
		characterAnimator.SetBool("EnterBRightD", false);
		characterAnimator.SetBool("EnterLeft", false);
		characterAnimator.SetBool("EnterBLeftD", false);
		characterAnimator.SetBool("EnterRun", false);
		characterAnimator.SetBool("EnterRightD", false);
		characterAnimator.SetBool("EnterLeftD", false);
	}
	public void DecreaseHealth(int damage)
    {
		if(armor > damage/2)
        {
			healthPlayer -= damage / 2;
			armor -= damage;

        }
		
        else
        {
			healthPlayer -= damage;
			armor = 0;
		}
		if(armor < 0)
        {
			armor = 0;
        }
		armorText.text = armor.ToString();
		armorBar.fillAmount = (float)armor / (float)maxArmor;
		healthValue.text = healthPlayer.ToString();
		bloodSplash.SetTrigger("EnterBlood");
		healthBar.fillAmount = (float)healthPlayer / (float)maxHealth;
		if(healthPlayer <= 0)
        {
			endScreen.SetActive(true);
			wavesSurvived.text = "YOU SURVIVED " + (EnemySpawner.Instance.wave -2).ToString() + " WAVES!";
			Cursor.lockState = CursorLockMode.None;
		}
    }

	public void IncreaseMaxHealth(int increasedBy)
    {
		maxHealth += increasedBy;
		healthPlayer += increasedBy;
		healthValue.text = healthPlayer.ToString();
		healthBar.fillAmount = (float)healthPlayer / (float)maxHealth;
	}
	public void IncreaseMaxArmor(int increasedBy)
	{
		maxArmor += increasedBy;
		armor += increasedBy;
		armorText.text = armor.ToString();
		armorBar.fillAmount = (float)armor / (float)maxArmor;
	}
	public void IncreaseMaxStamina(int increasedBy)
	{
		maxStamina += increasedBy;
		staminaBar.fillAmount = (float)stamina / (float)maxStamina;
	}

	public void IncreaseMaxSpeed(float increasedBy)
	{
		speed += increasedBy;
		sprintSpeed += increasedBy;
	}
	public void RefillStats()
	{
		armor = maxArmor;
		healthPlayer = maxHealth;
		healthValue.text = healthPlayer.ToString();
		healthBar.fillAmount = (float)healthPlayer / (float)maxHealth;
		armorText.text = armor.ToString();
		armorBar.fillAmount = (float)armor / (float)maxArmor;

	}

	public void EquipWeapon()
    {
		pistol.SetActive(false);
		ak.SetActive(true);
    }
	public void RestartGame()
    {

		SceneManager.LoadScene("MainScene");
	}
	void CameraRotation(GameObject cam, float rotX, float rotY){
		Debug.Log(rotY);
		
		//transform.Rotate (0, rotX * Time.deltaTime, 0);
		/*if(cam.transform.eulerAngles.x - rotY < minDownPos)
        {
			Debug.Log("ho");
			rotY =  minDownPos- cam.transform.eulerAngles.x;
        }
		if (cam.transform.eulerAngles.x - rotY > maxUpPos)
		{
			Debug.Log("ha");
			rotY =  maxUpPos - cam.transform.eulerAngles.x;
		}
		*/
		Debug.Log(cam.transform.eulerAngles.x + "transformaars posledna vulna");
		//cam.transform.Rotate (-rotY * Time.deltaTime, 0, 0);
		//rotationY += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
		//rotationY = Mathf.Clamp(cam.transform.eulerAngles.x, minDownPos, maxUpPos);
		//transform.eulerAngles = new Vector3(rotationY, 0.0f, 0.0f);
	}




}
