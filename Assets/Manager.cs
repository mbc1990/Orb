using UnityEngine;
using System.Collections;
using System.IO;


public class Manager : MonoBehaviour {
	
	/***********************************************************************************************/
	/*********************IF YOU WANT TO CHANGE CONSTANTS, CHANGE THEM HERE*************************/
	/***********************************************************************************************/
	public static void ResetConstants() {
		/*GAMEPLAY CONTROLS */
		//Larger the error, the wider legal orbit radius 
		RADIAL_ERROR = 50;
		//larger the tan error, the easier it is to enter a star at a legal radius
		TAN_ERROR = 8;
		//the larger this number is, the sharper bends are
		BEND_FACTOR = 4;
		//larger the number, the faster the learth moves overall
		MOVEMENT_SPEED = 0.72f;
		//speed you move at without energy
		CONSTANT_SPEED = 5f;
		//larger the number, the faster learth moves when orbiting (doesn't affect speed, but makes aiming easier)
		ORBIT_SPEED_FACTOR = .5f;
		
		/*CAMERA CONTROLS */
		//the larger this number is, the more closely the camera follows learth while in orbit
		ORBIT_LERP = 3f;
		//the larger this number is, the more closely the camera follows learth while not in orbit
		TRAVEL_LERP = 5f;
		//How far the player is allowed to move the camera
		CAM_MAX_DIST = 5000;
		//How close the player is allowed to move the camera
		CAM_MIN_DIST = 50;
		//how fast the player can zoom in/out
		CAM_MOVE_SPEED = 10;
		//Camera orthographic size at start, higher = see more
		CAM_START_HEIGHT = 600;
		
		/*ENERGY CONTROLS */	
		//starting energy
		STARTING_ENERGY = 35f;
		//How much energy is reduced each frame while bending
		BEND_COST = 0;
		//How much energy is reduced each frame while invincible
		INVINC_COST = .2f;
		//this much energy is subtracted each frame the learth is not in orbit
		FLYING_COST = .027f;
		//this much energy is subtracted each frame the learth is in orbit
		ORBITING_COST = .00025f;
		//this much energy is subtracted when they player hits the space bar to launch from a star
		LEAVING_COST = 0;
		//cost of a directional shift
		DIR_SHIFT_COST = 15;
		//determines whether shield is activeable
		SHIELD = false;
	    //determines whether boost is activatable
	    BOOST = false;
	    //lets you shift directions
	    DIRECTION_SHIFT = false;	
	 
	 	/*BLACK HOLE CONSTANTS*/
	 	//how fast black holes suck you into them when you are trapped--LOWER VALUES ARE SUCKIER
		BLACK_HOLE_SUCKINESS = 5f;	
		//energy it takes to escape a black hole on each press of space bar
		BH_ESCAPE_ENERGY = .2f;
		//distance you travel when you press space to escape a black hole
		BH_ESCAPE_DISTANCE = 300f;
		
		/*ALIEN CONSTANTS*/
		//when aliens are within distance, they start to suck your energy
		ALIEN_SUCKING_DISTANCE = 40f;
		//this much energy is sucked from player when alien is within alien_sucking_distance
		ALIEN_SUCKS_ENERGY = .025f;	
		   
		//black hole helper
		BLACK_HOLE_HELPER = false;
		
	}
	
	
	
	//Constants
	/*DON'T CHANGE ANY OF THESE. IT WILL HAVE NO EFFECT. TO ADD A NEW CONSTANT, DECLARE IT HERE AND INITIALIZE IN ResetConstants()*/
	//Larger the error, the wider legal orbit radius 
	public static int RADIAL_ERROR = 10;
	//larger the tan error, the easier it is to enter a star at a legal radius
	public static float TAN_ERROR = 8;
	//the larger this number is, the sharper bends are
	public static float BEND_FACTOR = 4;
	//larger the number, the faster the learth moves overall
	public static float MOVEMENT_SPEED = 0.72f;
	//speed you move at without energy
	public static float CONSTANT_SPEED = 1f;
	//larger the number, the faster learth moves when orbiting (doesn't affect speed, but makes aiming easier)
	private static float ORBIT_SPEED_FACTOR = .55f;
	
	/*CAMERA CONTROLS */
	//the larger this number is, the more closely the camera follows learth while in orbit
	public static float ORBIT_LERP = 3f;
	//the larger this number is, the more closely the camera follows learth while not in orbit
	public static float TRAVEL_LERP = 5f;
	//How far the player is allowed to move the camera
	public static float CAM_MAX_DIST = 5000;
	//How close the player is allowed to move the camera
	public static float CAM_MIN_DIST = 50;
	//how fast the player can zoom in/out
	public static float CAM_MOVE_SPEED = 10;
	//Camera orthographic size at start, higher = see more
	public static float CAM_START_HEIGHT = 600;
	
	/*ENERGY CONTROLS */	
	//starting energy
	public static float STARTING_ENERGY = 35f;
	//How much energy is reduced each frame while bending
	public static float BEND_COST = 0;
	//How much energy is reduced each frame while invincible
	private static float INVINC_COST = .2f;
	//this much energy is subtracted each frame the learth is not in orbit
	private static float FLYING_COST = .027f;
	//this much energy is subtracted each frame the learth is in orbit
	private static float ORBITING_COST = .00025f;
	//this much energy is subtracted when they player hits the space bar to launch from a star
	private static float LEAVING_COST = 0;
	//cost of a directional shift
	public static float DIR_SHIFT_COST = 15;
	//determines whether shield is activeable
	public static bool SHIELD = false;
    //determines whether boost is activatable
    public static bool BOOST = false;
    //lets you shift directions
    public static bool DIRECTION_SHIFT = false;	
 
 	/*BLACK HOLE CONSTANTS*/
 	//how fast black holes suck you into them when you are trapped--LOWER VALUES ARE SUCKIER
	private static float BLACK_HOLE_SUCKINESS = 5f;	
	//energy it takes to escape a black hole on each press of space bar
	private static float BH_ESCAPE_ENERGY = .2f;
	//distance you travel when you press space to escape a black hole
	private static float BH_ESCAPE_DISTANCE = 300f;
	
	/*ALIEN CONSTANTS*/
	//when aliens are within distance, they start to suck your energy
	public static float ALIEN_SUCKING_DISTANCE = 40f;
	//this much energy is sucked from player when alien is within alien_sucking_distance
	public static float ALIEN_SUCKS_ENERGY = .025f;	

	//Hook into unity
	public GameObject learth;
	public GameObject star;		
	public GameObject rip;
	public GameObject coin;
	public GameObject plane;
	public GameObject alien;
	public GameObject bomb;
		
	public static GameObject cur_star;
	
	//actual objects used in script
	public static GameObject l, s, e, p;
	public GameObject[] star_arr;
	public GameObject[] rip_arr;
	public GameObject[] coin_arr;
	public GameObject[] alien_arr;
	public int numStars = 0;
	
	//positions past which learth will die. levels are always rectangles
	float LEVEL_X_MAX = 100000;
	float LEVEL_X_MIN = -100000;
	float LEVEL_Y_MAX = 100000;
	float LEVEL_Y_MIN = -100000;
	
	//learth-related variables
	public static float speed = 0;
	public static float energy;
	public static GameObject lastStar;
	public static Vector3 tangent;
	public static bool clockwise = false;
	public static bool escaping_black_hole = false;
	public static Vector3 point_of_escape;
	public static int num_deaths = 0;
	
	//star colors and textures
	public Color orange = new Color(1f, .6f, 0f, 1f);
	public Color dgray = new Color(.1f, .1f, .1f, 1f);
	public Texture tred;
	public Texture torange;
	public Texture tyellow;
	public Texture twhite;
	public Texture tgray;
	public Texture tblue;
	
    //energy gauge
    public Texture gaugeTexture;

	
	//timer
	public float start_time;
	
	//performance tools
	public float updateInterval = 0.5F;
    private float lastInterval;
    private int frames = 0;
    private float fps;
    
	//game state
	GameObject game_state;
	Game_State gscpt;
	
	//black hole helper
	public static bool BLACK_HOLE_HELPER = false;
	
	
	
	void Start () {
		//performance
		lastInterval = Time.realtimeSinceStartup;
        frames = 0;
        
		//reset constants
	//	ResetConstants();
		
		//Proof that state is changed in menu and preserved in manager		
		game_state = GameObject.Find("game_state");
		gscpt = game_state.GetComponent<Game_State>();
		
		//let powerups know that it's time to active
		gscpt.in_game = true;
		
		//instantiate learth
		l = Instantiate (learth, new Vector3 (0, -35, 0), new Quaternion (0, 0, 0, 0)) as GameObject;
	 	l.renderer.material.color = Color.red;	
	 	
		//set camera height for beginning a game
	//	Camera.main.orthographicSize = CAM_START_HEIGHT;
		
		//instantiate background based on level constraints --this is going to change.
		for (int i = -2500; i < (int)LEVEL_X_MAX; i+=2500) {
			for (int j = -4000; j < 4000; j+=1250) {
				p = Instantiate (plane, new Vector3(i, j, 100), transform.rotation) as GameObject;
				p.transform.Rotate(270, 0, 0);
			}
		}
		
		//initialize timer
		start_time = Time.time;
		
		//load next level
		LoadLevel(gscpt.level_order[gscpt.cur_level]);
		
		//start camera on top of learth
//		Camera.main.transform.position = new Vector3(l.transform.position.x,l.transform.position.y, Camera.main.transform.position.z);	
	
	}
	
	
	/* HOW LEVELS WORK
	 * in case you want to make/change a level
	 * 
	 * each level is stored in a text file in the Assets directory ex: level3.txt
	 * 
	 * the first line of that file contains the number of each type of level design element to be specified, delimited by commas
	 * ex: 1,0,0,2,0 == 1 static star, 0 space rips, 0 coins, 2 moving stars, 0 aliens
	 * 
	 * Following that are the arguments for each method call that will instantiate the specified elements
	 * 
	 * The Learth begins the level orbiting the first static star specified and at least one static star must be specified
	 * 
	 * To change levels, you must call UnloadCurrentLevel() before you call LoadLevel(fname) 
	 * 
	 */	
	//Destroys all elements of currently loaded level
	//this must be called before you load another level, unless you want to compose multiple levels
	public void UnloadCurrentLevel() 
	{
		//destroy stars
		for(int i = 0; i < star_arr.Length; i++)
			Destroy(star_arr[i]);
		//reset star_arr and counter
		star_arr = new GameObject[0];
		numStars = 0;
		
		//destroy space rips
		for(int i = 0; i < rip_arr.Length; i++)
			Destroy (rip_arr[i]);
		rip_arr = new GameObject[0];
		
		//destroy coins
		for(int i = 0; i < coin_arr.Length; i++)
			Destroy (coin_arr[i]);
		
		//destroy aliens
		for(int i = 0; i < alien_arr.Length; i++)
			Destroy(alien_arr[i]);
		
		//reset energy
		//energy = 20f;
		
		
		//make sure learth is not tangent 
		Learth_Movement.isTangent = false;
	}
	
	//instantiates level design elements as specified in the text file in the argument
	public void LoadLevel(string fname) 
	{
		string line;
		char[] delim = {','};
		StreamReader file = new StreamReader(fname);
		string numels = file.ReadLine();
		
		//reset energy
		energy = STARTING_ENERGY;
		
		//get numbers of each type of element
		string[] sp = numels.Split(delim);
		int stars = int.Parse(sp[0]);
		int rips  = int.Parse(sp[1]);
		int coins = int.Parse(sp[2]);
		int mstars = int.Parse (sp[3]);
		int aliens = int.Parse(sp[4]);
		int rstars = int.Parse (sp[5]);
		
		//create all stars specified in the text file
		for(int i=0; i<stars;i++)
		{
			line = file.ReadLine();
			string [] args = line.Split(delim);
			
			//get color and texture objects
			Color starcol = Color.black;
			Texture startex = twhite;
			if(args[2] == "blue"){
				starcol = Color.blue;
				startex = tblue;
				
			} else if(args[2] == "white") {
				starcol = Color.white;
				startex = twhite;
			} else if(args[2] == "red") {
				starcol = Color.red;
				startex = tred;
			} else if (args[2] == "yellow") {
				starcol = Color.yellow;
				startex = tyellow;
			}
			
			//make the star
			GameObject newstar = CreateStar(float.Parse(args[0]),float.Parse(args[1]), starcol, startex, float.Parse(args[3]), bool.Parse(args[4]));
			
			//learth starts in orbit around first star specified
			if(i == 0)
				GoToOrbit(newstar,float.Parse(args[3]));	
			//last star is the sink
			if(i == stars-1){
				Starscript scpt = newstar.GetComponent<Starscript>();
				scpt.is_sink = true;
			} else {
				Starscript scpt = newstar.GetComponent<Starscript>();
				scpt.is_sink = false;
			}
				
		}
		
		
		
		//create all space rips specified in the text file
		for(int i = 0; i < rips;i++)
		{
			line = file.ReadLine();
			string[] args = line.Split(delim);
			
			CreateSpaceRip(float.Parse(args[0]),float.Parse(args[1]),float.Parse(args[2]),float.Parse(args[3]),float.Parse(args[4]));
		}
		
		//create all coins specified in the text file
		for(int i = 0; i < coins; i++)
		{
			line = file.ReadLine();
			string[] args = line.Split(delim);
			
			CreateCoin(float.Parse(args[0]),float.Parse(args[1]));
		}
		
		//create all moving stars specified in the text file
		for(int i = 0; i < mstars;i++)
		{
			line = file.ReadLine();
			string [] args = line.Split(delim);
			
			//get color and texture objects
			Color starcol = Color.white;
			Texture startex = twhite;
			if(args[2] == "blue"){
				starcol = Color.blue;
				startex = tblue;
				
			} else if(args[2] == "white") {
				starcol = Color.white;
				startex = twhite;
			} else if(args[2] == "red") {
				starcol = Color.red;
				startex = tred;
			} else if (args[2] == "yellow") {
				starcol = Color.yellow;
				startex = tyellow;
			}
			
			//make the star
			CreateMovingStar(float.Parse(args[0]),float.Parse(args[1]), 
				starcol, startex, float.Parse(args[3]), new Vector3(float.Parse (args[4]), float.Parse(args[5]),0), float.Parse(args[6]));
		}
		
		//create all aliens in the file
		for(int i = 0; i < aliens; i++)
		{	
			line = file.ReadLine();
			string[] args = line.Split(delim);
			CreateAlien(float.Parse(args[0]),float.Parse(args[1]));
		}
		
		//create a revolving star 
		for(int i = 0; i < rstars; i++)
		{
			line = file.ReadLine();
			string[] args = line.Split(delim);
			
			//get color and texture objects
			Color starcol = Color.white;
			Texture startex = twhite;
			if(args[4] == "blue"){
				starcol = Color.blue;
				startex = tblue;
				
			} else if(args[4] == "white") {
				starcol = Color.white;
				startex = twhite;
			} else if(args[4] == "red") {
				starcol = Color.red;
				startex = tred;
			} else if (args[4] == "yellow") {
				starcol = Color.yellow;
				startex = tyellow;
			}
			
			CreateRevolvingStar(float.Parse (args[0]),float.Parse(args[1]),float.Parse(args[2]),float.Parse(args[3]),
				starcol, startex, float.Parse(args[5]),float.Parse (args[6]));
		}
		
	}
	
	//puts learth in orbit given a valid radius
	public static void GoToOrbit(GameObject star, float radius)
	{
		l.transform.position = new Vector3(star.transform.position.x+radius+RADIAL_ERROR,star.transform.position.y,0);
		cur_star = star;
		s = star;
		Learth_Movement.isTangent = true;
	}
	
	//instantiates a revolving star at the location and around the point provided
	GameObject CreateRevolvingStar(float x, float y, float r_point_x, float r_point_y,Color color, Texture texture,float size, float speed)	
	{		
		GameObject rstar = CreateStar(x,y,color,texture,size);
		Starscript scpt  = rstar.GetComponent<Starscript>();
		scpt.is_revolving = true;
		scpt.rpoint = new Vector3(r_point_x,r_point_y,0);
		scpt.rspeed = speed;
		return rstar;
	}
	
	//instantiates an alien at the location provided
	GameObject CreateAlien(float x, float y) 
	{
		GameObject alien_actual = Instantiate (alien, new Vector3(x,y,0),new Quaternion(0,0,0,0)) as GameObject;
		
		//expand and put in array
		GameObject[] temp_arr = new GameObject[alien_arr.Length+1];
		for(int i=0;i<alien_arr.Length;i++)
			temp_arr[i] = alien_arr[i];
		alien_arr = temp_arr;
		alien_arr[alien_arr.Length-1] = alien_actual;
		
		return alien_actual;
	}
	
	//instantiates a coin at the location provided
	GameObject CreateCoin(float x, float y)
	{
		GameObject coin_actual = Instantiate(coin, new Vector3(x, y, 0), new Quaternion (0, 0, 0, 0)) as GameObject;
		
		//put rip in rip_arr for unloading
		GameObject[] temp_arr = new GameObject[coin_arr.Length+1];
		for(int i=0;i<coin_arr.Length;i++)
			temp_arr[i] = coin_arr[i];
		coin_arr = temp_arr;
		coin_arr[coin_arr.Length-1] = coin_actual;
		return coin_actual;
	}
	
	//instantiates a space rip from prefab at given location and of given dimensions, with given rotation (default = 0), returns reference to that object
	GameObject CreateSpaceRip(float x, float y, float width, float height, float rotation = 0)
	{
		GameObject rip_actual = Instantiate (rip, new Vector3 (x, y, 0), new Quaternion (0, 0, 0, 0)) as GameObject;
		rip_actual.transform.localScale += new Vector3(width,height,0);
		rip_actual.transform.Rotate(new Vector3(0,0,rotation));
		
		//put rip in rip_arr for unloading
		GameObject[] temp_arr = new GameObject[rip_arr.Length+1];
		for(int i=0;i<rip_arr.Length;i++)
			temp_arr[i] = rip_arr[i];
		rip_arr = temp_arr;
		rip_arr[rip_arr.Length-1] = rip_actual;
		
		return rip_actual;
	}
	
	//instantiates a star that moves in the direction given at the speed given
	GameObject CreateMovingStar(float x, float y, Color color, Texture texture, float size, Vector3 dir, float speed)
	{
		GameObject mstar = CreateStar(x,y,color,texture,size);
		Starscript scpt  = mstar.GetComponent<Starscript>();
		scpt.is_moving = true;
		scpt.dir = dir;
		scpt.speed = speed;
		return mstar;
	}

	//instantiates star from prefab at given xy location and of given characteristics
	GameObject CreateStar(float x, float y, Color color, Texture texture, float size, bool isBlackHole = false)
	{
		GameObject starE = Instantiate (star, new Vector3(x,y,0), new Quaternion(0,0,0,0)) as GameObject;
		Starscript starscript = starE.GetComponent<Starscript>();
		starscript.c = color;
		starscript.t = texture;
		starscript.starSize = size; 
		//starscript.isBlackHole = false;
		starscript.isBlackHole = isBlackHole;
		
		//expand and copy star_arr - if loading a level takes too long, this can be optimized
		GameObject[] temp_arr = new GameObject[star_arr.Length+1];
		for(int i=0;i<star_arr.Length;i++)
			temp_arr[i] = star_arr[i];
		star_arr = temp_arr;
		star_arr[star_arr.Length-1] = starE;
		lastStar = starE;
		numStars++;
		return starE;
	}
	
	//call this anytime something kills the player
	public static void Die()
	{	
		if(energy > STARTING_ENERGY)
			energy = STARTING_ENERGY;
		
		Starscript scpt = lastStar.GetComponent<Starscript>();
		GoToOrbit(lastStar,scpt.orbitRadius);
		
		
	}
	
	//reloads the scene and modifies whatever we want to modify when the scene gets reloaded
	public static void ResetLevel() {
		Application.LoadLevel(Application.loadedLevel);	
		energy = STARTING_ENERGY;
	}
	
	
	void Update () {
		
		//performance
		++frames;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + updateInterval) {
            fps = frames / (timeNow - lastInterval);
            frames = 0;
            lastInterval = timeNow;
        }
		
		/*********************DEBUGGING CONTROLS********************/
		// resetting level with T before leaving first star orbit freezes the game 
		//R causes the player to die
		if(Input.GetKeyDown(KeyCode.R))
			Die();
		//T resets the level
		if(Input.GetKeyDown(KeyCode.T))
			ResetLevel();
		//Y resets camera to learth's position
		if(Input.GetKeyDown (KeyCode.Y))
			Camera.main.transform.position = new Vector3(l.transform.position.x, l.transform.position.y, Camera.main.transform.position.z);
		//f increases energy by 1
		if(Input.GetKeyDown(KeyCode.F))
			energy++;
		//P advances to next level
		if(Input.GetKeyDown(KeyCode.P))
		{
			//increment level counter
			gscpt.cur_level++;
			
			//set in game to false
			gscpt.in_game = false;
			
			//open the ship outfitter
			Application.LoadLevel("Ship_Outfitter");
			
		}
		//O goes to outfitter before previous level
		if(Input.GetKeyDown(KeyCode.O))
		{
			gscpt.in_game = false;
			Application.LoadLevel("Ship_Outfitter");
		}
		//l prints learth's current position
		if(Input.GetKeyDown(KeyCode.L))
			Debug.Log ("dbg Learth position: "+l.transform.position);
				
		/*********************END DEBUGGING CONTROLS*****************/
		
		//Speed increases logarithmically with energy
		speed = Mathf.Log(energy)*MOVEMENT_SPEED + CONSTANT_SPEED;
		
		//end level if reach sink
		Starscript starscpt = cur_star.GetComponent<Starscript>();
		if(starscpt.is_sink) {
			
			//increment level counter
			gscpt.cur_level++;
			
			//set in game to false
			gscpt.in_game = false;
			
			//open the ship outfitter
			Application.LoadLevel("Ship_Outfitter");
		}
		//direction shift
                if(Input.GetKeyUp(KeyCode.Z) && DIRECTION_SHIFT){
                        Learth_Movement.lastPos.RotateAround(l.transform.position, Vector3.forward, 90);
						energy -= DIR_SHIFT_COST;
                }
                if(Input.GetKeyUp(KeyCode.X) && DIRECTION_SHIFT){
                      Learth_Movement.lastPos.RotateAround(l.transform.position, Vector3.forward, -90);
					energy -= DIR_SHIFT_COST;
                }
		//boost
		if(Input.GetKeyUp(KeyCode.C) && BOOST){
			//one time major energy boost
			energy += 50;
			BOOST = false;
		}	

		//bending
		if(Input.GetKey(KeyCode.Q))
		{
			energy -= BEND_COST;
			Learth_Movement.lastPos.RotateAround(l.transform.position,Vector3.forward,Time.deltaTime*BEND_FACTOR);
		}
		else if (Input.GetKey(KeyCode.W))
		{		
			energy -= BEND_COST;
			Learth_Movement.lastPos.RotateAround(l.transform.position,Vector3.forward,-1*Time.deltaTime*BEND_FACTOR);
		}
		
		//temporary invincibility, logic implemented in Learth_Movement.cs 
		if(Input.GetKey(KeyCode.D) && SHIELD)
		{
			l.renderer.material.color = Color.green;
			energy -= INVINC_COST;
		}
		
		//change learth color back to normal
		if(Input.GetKeyUp (KeyCode.D))
			l.renderer.material.color = Color.red;
		
		//Death conditions
		//if you run out of energy, you die, but you get a little energy back
	/*	if(energy < 1)
		{
			Die ();
		} */
		
		//if you travel outside the bounds of the level, you die
		if(l.transform.position.x > LEVEL_X_MAX
			|| l.transform.position.x < LEVEL_X_MIN
			|| l.transform.position.y > LEVEL_Y_MAX
			|| l.transform.position.y < LEVEL_Y_MIN)
			Die ();
		
		
		//if learth is tangent to star s
		if (Learth_Movement.isTangent) {
			//if in orbit, decrease energy at correct rate
			energy -= ORBITING_COST;
			
			//if learth is orbiting a moving planet, translate it with the planet to maintain circular orbit
			Starscript scpt = cur_star.GetComponent<Starscript>();
			if(scpt.is_moving)
				l.transform.Translate(scpt.dir.x*scpt.speed*Time.deltaTime,scpt.dir.y*scpt.speed*Time.deltaTime,0,Space.World);
					
			//likewise, translate for a revolving star
			if(scpt.is_revolving)
			{
				//difference in star positions since last frame
				Vector3 vec = cur_star.transform.position - scpt.last_position;
				
				//prevents incorrect movement in the case that the last position variable in the star hasn't been set correctly
				//this happens the first time you try to translate with a star
				//this is kind of a bad solution, but it will work until we have time to reevaluate the movement code
				if(!(Mathf.Abs(vec.x) > 10))
					l.transform.Translate(vec.x,vec.y,0,Space.World);
				
				//set last position
				scpt.last_position = cur_star.transform.position;
			}
			//if star is a black hole and you haven't traveled boost distance after pressing space bar, get sucked into center of black hole
			if(scpt.isBlackHole && !escaping_black_hole) {
				//speed up to increase dramatic effect
				speed *= BLACK_HOLE_SUCKINESS/2;
				Vector3 perp = l.transform.position - cur_star.transform.position;
				perp.Normalize();
				l.transform.position -= perp/BLACK_HOLE_SUCKINESS;
			}	
			//if star is black hole and you recently pressed space to escape, don't get sucked and but travel towards outer edges of black hole
			else if(scpt.isBlackHole && escaping_black_hole) 
				l.transform.position = Vector3.Lerp(l.transform.position, point_of_escape + BH_ESCAPE_DISTANCE*speed*Learth_Movement.velocity.normalized, Time.deltaTime);	
			//rotate around star s
			if (clockwise){
					l.transform.RotateAround(s.transform.position, Vector3.forward, 
						-(speed > 1 ? speed : 1)/(Vector3.Distance(l.transform.position, s.transform.position)*Time.deltaTime)*ORBIT_SPEED_FACTOR);
			}
			else  {
				l.transform.RotateAround(s.transform.position, 
				Vector3.forward, (speed > 1 ? speed : 1)/(Vector3.Distance(l.transform.position, s.transform.position)*Time.deltaTime/ORBIT_SPEED_FACTOR));
			}
			
			//if space bar is pressed, accelerate away from star. 
			if (Input.GetKeyDown(KeyCode.Space)) {
				//if star is a black hole, then lerp your way out
				if (scpt.isBlackHole) {
					//each time space bar is pressed energy is consumed
					energy -= BH_ESCAPE_ENERGY;
					escaping_black_hole = true;
					point_of_escape = l.transform.position;
			 		l.transform.position = Vector3.Lerp(l.transform.position, point_of_escape + BH_ESCAPE_DISTANCE*speed*Learth_Movement.velocity.normalized, Time.deltaTime);
					//if outside of black hole radius, then black hole stops having an effect on learth
					if (Vector3.Distance(l.transform.position, s.transform.position) >= scpt.orbitRadius/2) {
						escaping_black_hole = false;
						Learth_Movement.isTangent = false;
						Learth_Movement.lastPos.position = l.transform.position - Learth_Movement.velocity.normalized*speed;
					}				
				}
				//if star is a normal star, shoot out of orbit immediately with energy cost
				else {
					Learth_Movement.isTangent = false;
					lastStar = s;			
					energy -= LEAVING_COST;
					Learth_Movement.lastPos.position = l.transform.position - Learth_Movement.velocity.normalized*speed;
				}
			}
			//if space was recently pressed but learth hasn't traveled full black hole escape distance, set escaping_black_hole to true, but if full distance has been traveled set to false 
			Vector3 perp2 = l.transform.position - cur_star.transform.position;
			perp2.Normalize();			
			if (!escaping_black_hole && scpt.isBlackHole && Vector3.Distance(l.transform.position, Vector3.Distance(point_of_escape, cur_star.transform.position)*perp2) <= BH_ESCAPE_DISTANCE) 
				escaping_black_hole = true;			 		
			else
			 	escaping_black_hole = false;
		}
		//if earth is not tangent to any star
		else if (!Learth_Movement.isTangent) {
			//if not in orbit, decrease energy at correct rate
			energy -= FLYING_COST;
			
			//loop through array and calculate tangent vectors to every star
			for (int i = 0; i < star_arr.Length ; i++){
				s = star_arr[i];
				Starscript sscript = s.GetComponent<Starscript>();
				Vector3 l_movement = Learth_Movement.velocity;
				Vector3 star_from_learth = s.transform.position - l.transform.position;
				Vector3 projection = Vector3.Project (star_from_learth, l_movement);
				tangent = projection + l.transform.position;
				//if planet is within star's orbital radius, set isTangent to true
				float innerOrbit, outerOrbit;
				if (sscript.isBlackHole) {
					innerOrbit = sscript.orbitRadius;
					outerOrbit = sscript.orbitRadius/2;
				}
				else {
					outerOrbit = -RADIAL_ERROR;
					innerOrbit = RADIAL_ERROR;
				}
				if (s != lastStar 
					&& Vector3.Distance(s.transform.position, l.transform.position) >= (sscript.orbitRadius - innerOrbit) 
					&& Vector3.Distance(s.transform.position, l.transform.position) <= (sscript.orbitRadius - outerOrbit) 
					&& Vector3.Distance (tangent, l.transform.position) <= TAN_ERROR) 
				{	
					Learth_Movement.isTangent = true;
					cur_star = s;
					//determine direction of orbit
					if (tangent.y < s.transform.position.y && l_movement.x < 0) { 
						clockwise = true;
					}
					else if (tangent.y > s.transform.position.y  && l_movement.x > 0) {
						clockwise = true;
					}		
					else if (tangent.x < s.transform.position.x && l_movement.y > 0) {
						clockwise = true;
					}
					else {
						clockwise = false;
					}
					
					if (!sscript.isBlackHole) {
						//add appropriate energy value depending on color of star
						if (sscript.c == Color.blue) {
							energy += 15f;
						} else if (sscript.c == Color.white){
							energy += 9f;
						} else if (sscript.c == Color.yellow) {
							energy += 10f;
						} else if (sscript.t == torange) {
							energy += 2f;
						} else if (sscript.c == Color.red) {
							energy += 25f;
						}
						else {
							energy -= 1f;
						}
						sscript.c = dgray;
						sscript.t = tgray;
					}
					break;
				}
			}
		}
	}
	
	void OnGUI() {
		//performance
		GUI.Label(new Rect(10,10,150,50), "FPS: "+fps.ToString("f2"));
		Starscript scpt = cur_star.GetComponent<Starscript>();
		if(scpt.is_sink) {
		/*	GUI.Label(new Rect(10, Screen.height-80,150,50), "YOU WIN!");
			GUI.Label (new Rect(10, Screen.height-95,150,50), "Time: "+(Time.time - start_time)); */
		}
        GUI.Label(new Rect(10, Screen.height-65, 150, 50), "Space Coins: "+(gscpt.num_coins));
		GUI.Label(new Rect(10, Screen.height-50,150,50), "Energy:");
   		GUI.DrawTexture(new Rect(10, Screen.height-30, energy*10, 20), gaugeTexture, ScaleMode.ScaleAndCrop, true, 10F); 
	}
		
}
	
