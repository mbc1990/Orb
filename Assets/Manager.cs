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
		RADIAL_ERROR = 85;
		//larger the tan error, the easier it is to enter a star at a legal radius
		TAN_ERROR = 8;
		//the larger this number is, the sharper bends are
		BEND_FACTOR = 13;
		//larger the number, the faster the learth moves overall
		MOVEMENT_SPEED = 0.72f;
		//speed you move at without energy
		CONSTANT_SPEED = 12f;
		//larger the number, the faster learth moves when orbiting (doesn't affect speed, but makes aiming easier)
		ORBIT_SPEED_FACTOR = .4f;
		
		/*CAMERA CONTROLS */
		//the larger this number is, the more closely the camera follows learth while in orbit
		ORBIT_LERP = 3f;
		//the larger this number is, the more closely the camera follows learth while not in orbit
		TRAVEL_LERP = 2.5f;
		//How far the player is allowed to move the camera
		CAM_MAX_DIST = 1500;
		//How close the player is allowed to move the camera
		CAM_MIN_DIST = 400;
		//how fast the player can zoom in/out
		CAM_MOVE_SPEED = 10;
		//Camera orthographic size at start, higher = see more
		CAM_START_HEIGHT = 700;
		
		/*ENERGY CONTROLS */	
		//starting energy
		STARTING_ENERGY = 35f;
		//How much energy is reduced each frame while bending
		BEND_COST = 0;
		//How much energy is reduced each frame while invincible
		INVINC_COST = 0;
		//this much energy is subtracted each frame the learth is not in orbit
		FLYING_COST = 0;
		//this much energy is subtracted each frame the learth is in orbit
		ORBITING_COST = 0f;
		//this much energy is subtracted when they player hits the space bar to launch from a star
		LEAVING_COST = 0;
		//cost of a directional shift
		DIR_SHIFT_COST = 0;
		//determines whether shield is activeable
		SHIELD = false;
		
		SPACE_RIP_COST = 0; //comment ur variable plz
	 
	 	/*BLACK HOLE CONSTANTS*/
	 	//how fast black holes suck you into them when you are trapped--LOWER VALUES ARE SUCKIER
		BLACK_HOLE_SUCKINESS = 2f;	
		//energy it takes to escape a black hole on each press of space bar
		BH_ESCAPE_ENERGY = 0f;
		//distance you travel when you press space to escape a black hole
		BH_ESCAPE_DISTANCE = 20f;
		
		/*ALIEN CONSTANTS*/
		//when aliens are within distance, they start to suck your energy
		ALIEN_SUCKING_DISTANCE = 100f;
		//this much energy is sucked from player when alien is within alien_sucking_distance
		ALIEN_SUCKS_ENERGY = 0f;	
		   
		//black hole helper
		BLACK_HOLE_HELPER = false;
		
		points = 0;
		
		Background.activated = true;
		end_animation = true;
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
	public static float CONSTANT_SPEED = 12f;
	//larger the number, the faster learth moves when orbiting (doesn't affect speed, but makes aiming easier)
	public static float ORBIT_SPEED_FACTOR = .55f;
	
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
	private static float INVINC_COST = 0f;
	//this much energy is subtracted each frame the learth is not in orbit
	private static float FLYING_COST = 0f;
	//this much energy is subtracted each frame the learth is in orbit
	private static float ORBITING_COST = 0f;
	//this much energy is subtracted when they player hits the space bar to launch from a star
	private static float LEAVING_COST = 0;
	//cost of a directional shift
	public static float DIR_SHIFT_COST = 0;
	//energy value for specific star colors
	public static float RED_ENERGY = 20f;
	public static float ORANGE_ENERGY = 20f;
	public static float YELLOW_ENERGY = 20f;
	public static float GREEN_ENERGY = 20f;
	public static float BLUE_ENERGY = 20f;
	public static float AQUA_ENERGY = 20f;
	public static float PURPLE_ENERGY = 20f;
	//energy from blowing up a star by flying through it while invincible
	public static float INVINC_ENERGY_BONUS = 200;
	//energy from obtaining a boost
	public static float BOOST_POINTS = 75f;
	//energy from a single coin
	public static float COIN_POINTS = 5f;
	
	//POWERUP BOOLEANS (these are special cases. try to avoid using powerup booleans. use a new script.)
    public static bool SHIELD = false;
    public static bool BLACK_HOLE_HELPER = false;
	
	public static float SPACE_RIP_COST = 45f;
 	
 	/*BLACK HOLE CONSTANTS*/
 	//how fast black holes suck you into them when you are trapped--LOWER VALUES ARE SUCKIER
	public static float BLACK_HOLE_SUCKINESS = 5f;	
	//energy it takes to escape a black hole on each press of space bar
	private static float BH_ESCAPE_ENERGY = 0;
	//distance you travel when you press space to escape a black hole
	private static float BH_ESCAPE_DISTANCE = 400f;
	
	/*ALIEN CONSTANTS*/
	//when aliens are within distance, they start to suck your energy
	public static float ALIEN_SUCKING_DISTANCE = 100f;
	//this much energy is sucked from player when alien is within alien_sucking_distance
	public static float ALIEN_SUCKS_ENERGY = 0f;	

	//Hook into unity
	public GameObject learth;
	public GameObject star;		
	public GameObject rip;
	public GameObject wall;
	public GameObject coin;
	public GameObject plane;
	public GameObject alien;
	public GameObject bomb;
	public GameObject lbullet;
	public GameObject boost;
	public GameObject invinc;
	
	public static GameObject cur_star;
	public GameObject star_background_star;
	
	//actual objects used in script
	public static GameObject l, s, e, p;
	//particle trails for learth
	public GameObject white_learth_trail,red_learth_trail, orange_learth_trail, yellow_learth_trail, green_learth_trail,
		blue_learth_trail, aqua_learth_trail, purple_learth_trail;
	public static GameObject lt;
	public GameObject[] star_arr;
	public GameObject[] rip_arr;
	public GameObject[] coin_arr;
	public GameObject[] wall_arr;
	public static  GameObject[] alien_arr = new GameObject[0];
	public GameObject[] boost_arr;
	public GameObject[] invinc_arr;
	public int numStars = 0;
	
	//positions past which learth will die. levels are always rectangles. 
	float level_x_max = -200000;
	float level_x_min = 200000;
	float level_y_max = -200000;
	float level_y_min = 200000;
	
	//learth-related variables
	public static float speed = 0;
	public static float energy;
	
	//store 3 last stars (refactor this into an array)
	public static GameObject lastStar;
	public static GameObject _lastStar;
	public static GameObject __lastStar;
	
	public Vector3 perp2; //vector to help with leaving black holes
	public static Vector3 tangent;
	public static bool clockwise = false;
	public static bool escaping_black_hole = false;
	public static Vector3 point_of_escape;
	public static int num_deaths = 0;
	
	//star colors and textures
	public static Color orange = new Color(.9f, .45f, 0f, 1f);
	public static Color dgray = new Color(.1f, .1f, .1f, 1f);
	public static Color aqua = new Color(0, .4f, .8f, 1f);
	public static Color purple = new Color(.4f, 0, .4f, 1f);
	public static Color green = new Color(0,.4f, 0, 1f);
	public Texture tred;
	public Texture torange;
	public Texture tyellow;
	public Texture tgreen;
	public Texture tblue;
	public Texture taqua;
	public Texture tpurple;	
	public Texture tgray;
	
	//texture of space station
	public Texture station_texture;
    //energy gauge
    public Texture gaugeTexture;
	//skin for GUI
	public GUISkin skin;

	//timer
	public float start_time;
	
	//for pausing and controls screen
	public static bool escape = false;
	public Texture control_scheme;
	
	//performance tools
	public float updateInterval = 0.5F;
    private float lastInterval;
    private int frames = 0;
    private float fps;
    
	//game state
	public static GameObject game_state;
	public static Game_State gscpt;
	
	//true if being attackign
	public static bool is_being_attacked = false;
	
	//explosions
	public Transform space_jump_effect;
	public Transform reset_effect;
	public GameObject color_suck_effect, cse;
	public GameObject ese, end_scene_effect;
	
	//testing audio
	public AudioClip test_aud;
	
	//flag set by the invincibility powerup
	public static bool IS_INVINCIBLE = false;
	
	//icons for powerups
	public Texture bombs;
	public Texture jumps;
	public Texture bullets;
	public Texture dir_shifts;
	public Texture timewarps;		
	
	//hack to stop death sound at beginning
	static bool play_death = false;
	
	//level timer; number of seconds
	public float timer = 15;
	
	//points required to finish a level
	public static float req_points = 1000;
	
	//points
	public static float points = 0;
	
	 //popup text for points
	public static string popup_text;
	public static Vector3 popup_location;
	public static bool popup = false;
	//popups for events
	public static string event_popup_text;
	public static bool event_popup = false;
	
	//instance for waiting
	private static Manager m_Instance = null;
	
	//keep track of number of orbs of each color and boosts aquired
	public static int red_orbs = 0;
	public static int orange_orbs = 0;
	public static int yellow_orbs = 0;
	public static int green_orbs = 0;
	public static int blue_orbs = 0;
	public static int purple_orbs = 0;
	public static int aqua_orbs = 0;
	public static int boost_count = 0;
	
	//ending animations
	public static bool end_animation = true;
	public Transform explosion;
	
	//used to allow the ship to enter orbit around the last star, but only if after a wall bounce
	//because we check that the start is not the last star when getting into orbit to avoid re-entering orbit as soon as you leave
	public static bool just_hit_wall = false;

			
	void Start () {
		//performance
		lastInterval = Time.realtimeSinceStartup;
        frames = 0;
        
		//find the gamestate object	
		game_state = GameObject.Find("game_state");
		gscpt = game_state.GetComponent<Game_State>();
		
		//let powerups know that it's time to active
		gscpt.in_game = true;
		
		//instantiate learth
		l = Instantiate (learth, new Vector3 (0, -35, 0), new Quaternion (0, 0, 0, 0)) as GameObject;
	 	l.renderer.material.color = Color.white;	
		lt = Instantiate (white_learth_trail, l.transform.position, l.transform.rotation) as GameObject;
		lt.transform.parent = l.transform;
		
		//initialize timer
		start_time = Time.time;
		
		//load next level
		LoadLevel(gscpt.level_order[gscpt.cur_level]);
		
		//set lastStar to first star (because it's the last star you visited)
		lastStar = star_arr[0];
		
	}
	
	//check to see if location of object determines level boundaries
	public void checkBoundaries(float x, float y) {
		if(x < level_x_min)
			level_x_min = x;
		if(x > level_x_max)
			level_x_max = x;
		if(y < level_y_min)
			level_y_min = y;
		if(y > level_y_max)
			level_y_max = y;
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
		
		//destroy walls
		for(int i = 0; i < wall_arr.Length; i++)
			Destroy (wall_arr[i]);
		
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
		char[] delim = {','} ;
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
		int boosts = int.Parse (sp[6]);
		int invincs = int.Parse(sp[7]);
		int walls = int.Parse (sp[8]);
		int time = int.Parse(sp[9]);
		int reqpts = int.Parse(sp[10]);
		
		//set time and points values
		timer = time;
		req_points = reqpts;	
		
		
		//create all stars specified in the text file
		for(int i=0; i<stars;i++)
		{
			line = file.ReadLine();
			string [] args = line.Split(delim);
			
			//get color and texture objects
			Color starcol = Color.black;
			Texture startex = tgray;
			if(args[2] == "red") {
				starcol = Color.red;
				startex = tred;
			}  else if (args[2] == "orange") {
				starcol = orange;
				startex = torange;
			}  else if (args[2] == "yellow") {
				starcol = Color.yellow;
				startex = tyellow;
			}  else if (args[2] == "green") {
				starcol = green;
				startex = tgreen;
			}  else if(args[2] == "blue"){
				starcol = Color.blue;
				startex = tblue;
			}  else if(args[2] == "aqua") {
				starcol = aqua;
				startex = taqua;
			}  else if(args[2] == "purple") {
				starcol = purple;
				startex = tpurple;
			}
			float starsize = float.Parse(args[3]);
			//make the star
			GameObject newstar = CreateStar(float.Parse(args[0]),float.Parse(args[1]), starcol, startex, starsize, bool.Parse(args[4]));
			
			//learth starts in orbit around first star specified
			if(i == 0) {
				GoToOrbit(newstar,float.Parse(args[3])+10);	
			}
			//last star is the sink
		/*	
		 * 
		 * PREVIOUS VERSION 
		 * 
		 * if(i == stars-1){
				Starscript scpt = newstar.GetComponent<Starscript>();
				scpt.is_sink = true;
				scpt.orbitRadius = 0;
			}  else { */
				Starscript scpta = newstar.GetComponent<Starscript>();
				scpta.is_sink = false;
				scpta.is_source = false;
		/*	}  */
			
			
			
			checkBoundaries(float.Parse(args[0]), float.Parse(args[1]));
		}
		
		
		
		//create all space rips specified in the text file
		for(int i = 0; i < rips;i++)
		{
			line = file.ReadLine();
			string[] args = line.Split(delim);
			
			CreateSpaceRip(float.Parse(args[0]),float.Parse(args[1]),float.Parse(args[2]),float.Parse(args[3]),float.Parse(args[4]));
			checkBoundaries(float.Parse(args[0]), float.Parse(args[1]));
		}
		
		//create all coins specified in the text file
		for(int i = 0; i < coins; i++)
		{
			line = file.ReadLine();
			string[] args = line.Split(delim);
			
			CreateCoin(float.Parse(args[0]),float.Parse(args[1]));
			checkBoundaries(float.Parse(args[0]), float.Parse(args[1]));
		}
		
		//create all moving stars specified in the text file
		for(int i = 0; i < mstars;i++)
		{
			line = file.ReadLine();
			string [] args = line.Split(delim);
			
			//get color and texture objects
			Color starcol = Color.black;
			Texture startex = tgray;
			if(args[2] == "red") {
				starcol = Color.red;
				startex = tred;
			}  else if (args[2] == "orange") {
				starcol = orange;
				startex = torange;
			}  else if (args[2] == "yellow") {
				starcol = Color.yellow;
				startex = tyellow;
			}  else if (args[2] == "green") {
				starcol = green;
				startex = tgreen;
			}  else if(args[2] == "blue"){
				starcol = Color.blue;
				startex = tblue;
			}  else if(args[2] == "aqua") {
				starcol = aqua;
				startex = taqua;
			}  else if(args[2] == "purple") {
				starcol = purple;
				startex = tpurple;
			}
			
			//make the star
			CreateMovingStar(float.Parse(args[0]),float.Parse(args[1]), 
				starcol, startex, float.Parse(args[3]), new Vector3(float.Parse (args[4]), float.Parse(args[5]),0), float.Parse(args[6]), bool.Parse(args[7]));
			checkBoundaries(float.Parse(args[0]), float.Parse(args[1]));
		}
		
		//create all aliens in the file
		for(int i = 0; i < aliens; i++)
		{	
			line = file.ReadLine();
			string[] args = line.Split(delim);
			CreateAlien(float.Parse(args[0]),float.Parse(args[1]));
			checkBoundaries(float.Parse(args[0]), float.Parse(args[1]));
		}
		
		//create a revolving star 
		for(int i = 0; i < rstars; i++)
		{
			line = file.ReadLine();
			string[] args = line.Split(delim);
			
			//get color and texture objects
			Color starcol = Color.black;
			Texture startex = tgray;
			if(args[4] == "red") {
				starcol = Color.red;
				startex = tred;
			}  else if (args[4] == "orange") {
				starcol = orange;
				startex = torange;
			}  else if (args[4] == "yellow") {
				starcol = Color.yellow;
				startex = tyellow;
			}  else if (args[4] == "green") {
				starcol = green;
				startex = tgreen;
			}  else if(args[4] == "blue"){
				starcol = Color.blue;
				startex = tblue;
			}  else if(args[4] == "aqua") {
				starcol = aqua;
				startex = taqua;
			}  else if(args[4] == "purple") {
				starcol = purple;
				startex = tpurple;
			}
			
			CreateRevolvingStar(float.Parse (args[0]),float.Parse(args[1]),float.Parse(args[2]),float.Parse(args[3]),
				starcol, startex, float.Parse(args[5]),float.Parse (args[6]));
			checkBoundaries(float.Parse(args[0]), float.Parse(args[1]));
			checkBoundaries(float.Parse(args[2]), float.Parse(args[3]));
		}
		
		//create boosts
		for(int i = 0; i < boosts; i++) {
			line = file.ReadLine();
			string[] args = line.Split(delim);
			CreateBoost(float.Parse(args[0]), float.Parse(args[1]));
		}
		
		//create invinces
		for(int i = 0; i < invincs; i++) {
			line = file.ReadLine();
			string[] args = line.Split(delim);
			CreateInvinc(float.Parse(args[0]), float.Parse(args[1]));
		}
		
		for(int i = 0; i < walls; i++) {
			line = file.ReadLine();
			string[] args = line.Split(delim);
			CreateWall(float.Parse(args[0]), float.Parse(args[1]), float.Parse(args[2]), float.Parse(args[3]), bool.Parse(args[4]));
		}
		
	}
	
	//puts learth in orbit given a valid radius
	public static void GoToOrbit(GameObject star, float radius)
	{
		Starscript scpt  = star.GetComponent<Starscript>();
		if (scpt.is_source)
			l.transform.position = new Vector3(star.transform.position.x+radius,star.transform.position.y,0);
		else
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
	
	//instantiates a boost pick at the location provided
	GameObject CreateBoost(float x, float y) {
		GameObject boost_actual= Instantiate(boost, new Vector3(x, y, 0), new Quaternion (0, 0, 0, 0)) as GameObject;
		
		//put rip in boost_arr for unloading
		GameObject[] temp_arr = new GameObject[boost_arr.Length+1];
		for(int i=0;i<boost_arr.Length;i++)
			temp_arr[i] = boost_arr[i];
		boost_arr = temp_arr;
		boost_arr[boost_arr.Length-1] = boost_actual;
		return boost_actual;
	}
	
	GameObject CreateInvinc(float x, float y) {
			
		GameObject invinc_actual= Instantiate(invinc, new Vector3(x, y, -10), new Quaternion (0, 0, 0, 0)) as GameObject;
		
		//put rip in boost_arr for unloading
		GameObject[] temp_arr = new GameObject[invinc_arr.Length+1];
		for(int i=0;i<invinc_arr.Length;i++)
			temp_arr[i] = invinc_arr[i];
		invinc_arr = temp_arr;
		invinc_arr[invinc_arr.Length-1] = invinc_actual;
		return invinc_actual;
		
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
		coin_actual.transform.Rotate(90,0,0);
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
	GameObject CreateMovingStar(float x, float y, Color color, Texture texture, float size, Vector3 dir, float speed, bool bandf = false)
	{
		//bool bandf = false;
		GameObject mstar = CreateStar(x,y,color,texture,size);
		Starscript scpt  = mstar.GetComponent<Starscript>();
		scpt.is_moving = true;
		if(bandf){
			scpt.destination = dir;
			scpt.start_loc = mstar.transform.position;
		}
		else{
			scpt.dir = dir;
		}
		scpt.speed = speed;
		return mstar;
	}

	//instantiates star from prefab at given xy location and of given characteristics
	GameObject CreateStar(float x, float y, Color color, Texture texture, float size, bool isBlackHole = false, bool isExplodingStar=false)
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
	
	GameObject CreateWall(float x1, float y1, float x2, float y2, bool visible) {
		GameObject w = Instantiate (wall, new Vector3((x1+x2)/2,(y1+y2)/2,0), new Quaternion(0,0,0,0)) as GameObject;
		float length = Vector2.Distance(new Vector2(x1, y1),new Vector2(x2,y2));
		w.transform.localScale = new Vector3(length,10,10);
		float theta;
		if ((x2<x1 && y1<y2) || (x2>x1 && y1>y2))
			length *= -1;
		if (y2>y1)
			theta = Mathf.Asin((y2-y1)/length)*180/Mathf.PI;
		else
			theta = Mathf.Asin((y1-y2)/length)*180/Mathf.PI;
		w.transform.Rotate(0,0,theta);
		WallScript wallscript = w.GetComponent<WallScript>();
		wallscript.visible = visible;
		GameObject[] temp_arr = new GameObject[wall_arr.Length+1];
		for(int i=0;i<wall_arr.Length;i++)
			temp_arr[i] = wall_arr[i];
		wall_arr = temp_arr;
		wall_arr[wall_arr.Length-1] = w;
		return w;	
	}
	
	//call this anytime something kills the player
	public static void Die()
	{
		
		//sound	
		if(play_death) {
			GameObject go = GameObject.Find("Alien_Exp_Sound");
			Alien_Exp_Sound ascpt = go.GetComponent<Alien_Exp_Sound>();
		// 	ascpt.die.Play(); 
		} else {
			play_death = true;
		}
		
		//reset to starting vals
		gscpt.times_died++;
		if(energy > STARTING_ENERGY)
			energy -= 25;
		
		//check the last 3 stars and go to the first one that hasn't been destroyed
		//if they've all been destroyed, reload the level
		//this can probably be written better
		Starscript scpt = lastStar.GetComponent<Starscript>();
		if(!scpt.is_destroyed) {
			GoToOrbit(lastStar,scpt.orbitRadius);
		} else {
			Starscript _scpt = _lastStar.GetComponent<Starscript>();
			if(!_scpt.is_destroyed) {
				GoToOrbit(_lastStar,_scpt.orbitRadius);
			} else {
				Starscript __scpt = __lastStar.GetComponent<Starscript>();
				if(!__scpt.is_destroyed) {
					GoToOrbit(__lastStar,__scpt.orbitRadius);
				} else {
					Application.LoadLevel(Application.loadedLevel);
				}
			}
		}
		
		
	}
	
	//reloads the scene and modifies whatever we want to modify when the scene gets reloaded
	public static void ResetLevel() {
		ResetConstants();
		Application.LoadLevel(Application.loadedLevel);	
		energy = STARTING_ENERGY;
	}
	
	
	void Update () {
	
		//timer
		timer -= Time.deltaTime;
		
		//ending animation and
		//endgame condition (timer runs out)
		if(timer <= 0 && end_animation) {
			Vector3 screen_center = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, 0));
			ese = Instantiate(end_scene_effect, new Vector3(screen_center.x, screen_center.y,0), transform.rotation) as GameObject;
			if (timer< -1)
				end_animation = false;
		}
		else if (timer < -2 && !end_animation)
			Application.LoadLevel("Postgame");
 
		
		//points increase more the more energy you have
		//points += Mathf.Floor((Time.deltaTime * energy));
			
		
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
		if(Input.GetKeyDown(KeyCode.R)) {
			Instantiate(reset_effect,Manager.l.transform.position,Manager.l.transform.rotation);
		//	gscpt.timewarp_ammo--;
			Die();
	 	}
		//T resets the level
		if(Input.GetKeyDown(KeyCode.T))
			ResetLevel();
		//Y resets camera to learth's position
		if(Input.GetKeyDown (KeyCode.Y))
			Camera.main.transform.position = new Vector3(l.transform.position.x, l.transform.position.y, Camera.main.transform.position.z);
		//f freezes time
		if(Input.GetKeyDown(KeyCode.F))
			Time.timeScale = 0;
		if(Input.GetKeyDown(KeyCode.G))
			Time.timeScale = 1;
		//P advances to next level
		if(Input.GetKeyDown(KeyCode.P))
		{
			//increment level counter
			gscpt.cur_level++;
			
			//set in game to false
			gscpt.in_game = false;
			
			//open the ship outfitter
			Application.LoadLevel("Postgame");
			
		}
		//O goes to outfitter before previous level
		if(Input.GetKeyDown(KeyCode.O))
		{
			gscpt.in_game = false;
			Application.LoadLevel("Postgame");
		}
		//l prints learth's current position
		if(Input.GetKeyDown(KeyCode.L)) {
		}
		/*********************END DEBUGGING CONTROLS*****************/
		
		//Speed increases logarithmically with energy
		//speed = Mathf.Log(energy)*MOVEMENT_SPEED + CONSTANT_SPEED;
		speed =  CONSTANT_SPEED;
		//bending
		if(Input.GetKey(KeyCode.A))
		{
			energy -= BEND_COST;
			Learth_Movement.lastPos.RotateAround(l.transform.position,Vector3.forward,Time.deltaTime*BEND_FACTOR);
		}
		else if (Input.GetKey(KeyCode.D))
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
		if(Input.GetKeyUp(KeyCode.Escape)) {
			escape = !escape;
			//how to pause?
		}
			
		
		//change learth color back to normal - causes color change of learth when bombs fired with key D as well
	/*	if(Input.GetKeyUp (KeyCode.D))
			l.renderer.material.color = Color.red; */
			
		//keep track of aliens potentially turning off powerups
		is_being_attacked = false;
		for(int j = 0; j<alien_arr.Length; j++){
			if(alien_arr[j] != null && Vector3.Distance(l.transform.position, alien_arr[j].transform.position) < ALIEN_SUCKING_DISTANCE)
			{
				is_being_attacked = true;
			} 
        }

		//Death conditions
		//if you run out of energy, you die, but you get a little energy back
	/*	if(energy < 1)
		{
			Die ();
		}  */
		
		//if you travel outside the bounds of the level, you die
		//max/mins are calcualted based on star centers, not rotations, so you die immediately if you start at the edge without the +/- 100s
		if(l.transform.position.x > level_x_max+1000
			|| l.transform.position.x < level_x_min-1000
			|| l.transform.position.y > level_y_max+1000
			|| l.transform.position.y < level_y_min-1000)
		{
			Die ();
		}
		
		
		//if learth is tangent to star s
		if (Learth_Movement.isTangent) {
			//if in orbit, decrease energy at correct rate
			energy -= ORBITING_COST;
			
			//if learth is orbiting a moving planet, translate it with the planet to maintain circular orbit
			Starscript scpt = cur_star.GetComponent<Starscript>();
					
			if(scpt.spiral) 
				Learth_Movement.isTangent = false;
			
			//translate learth for moving stars to compensate for star movement
			if(scpt.is_revolving || scpt.is_moving)
			{
				//difference in star positions since last frame
				Vector3 vec = cur_star.transform.position - scpt.last_position;
				
				//prevents incorrect movement in the case that the last position variable in the star hasn't been set correctly
				//this happens the first time you try to translate with a star
				//this is kind of a bad solution, but it will work until we have time to reevaluate the movement code
				if(!(Mathf.Abs(vec.x) > 10)) {
					l.transform.Translate(vec.x,vec.y,0,Space.World);
					Learth_Movement.lastPos.Translate(vec.x,vec.y,0,Space.World);
				}
				
				//set last position
				scpt.last_position = cur_star.transform.position;
			}
			//if star is a black hole and you haven't traveled boost distance after pressing space bar, get sucked into center of black hole
			if(scpt.isBlackHole && !escaping_black_hole) {
				scpt.black_hole_active = true;
				//speed up to increase dramatic effect
				speed *= BLACK_HOLE_SUCKINESS;
				Vector3 perp = l.transform.position - cur_star.transform.position;
				perp.Normalize();
				l.transform.position -= perp*BLACK_HOLE_SUCKINESS;
			}	
			//if star is black hole and you recently pressed space to escape, don't get sucked and but travel towards outer edges of black hole
			else if(scpt.isBlackHole && escaping_black_hole) {
				Vector3 perp = l.transform.position - cur_star.transform.position;
				perp.Normalize();
				float dist1 = Vector3.Distance(l.transform.position,cur_star.transform.position);
				float dist2 = Vector3.Distance(point_of_escape+BH_ESCAPE_DISTANCE*perp2,cur_star.transform.position);
				//if space was recently pressed but learth hasn't traveled full black hole escape distance, set escaping_black_hole to true, but if full distance has been traveled set to false 
				if (dist2-dist1 < 10) 
					escaping_black_hole = false;			
				l.transform.position += perp*BLACK_HOLE_SUCKINESS;
			//	l.transform.position = Vector3.Lerp(l.transform.position, point_of_escape + BH_ESCAPE_DISTANCE*speed*Learth_Movement.velocity.normalized, Time.deltaTime);	
			
			}
			//rotate around star s
			if (clockwise){
					l.transform.RotateAround(s.transform.position, Vector3.forward, 
						-(speed > 1 ? speed : 1)/(Vector3.Distance(l.transform.position, s.transform.position)*Time.deltaTime)*ORBIT_SPEED_FACTOR);
			}
			else  {
				l.transform.RotateAround(s.transform.position, 
				Vector3.forward, (speed > 1 ? speed : 1)/(Vector3.Distance(l.transform.position, s.transform.position)*Time.deltaTime/ORBIT_SPEED_FACTOR));
			}
			
			//EXPLODING STAR 
			if(scpt.isExplodingStar)
			{
				//Stars Exploding timer begins
				scpt.BoomTime();
				//waits 5 seconds
				if(scpt.waitsec(scpt.blowuptime))
				{
					//make learth accelerate away from star and removestar
					Learth_Movement.isTangent = false;
					Learth_Movement.lastPos.position=scpt.transform.position;
					scpt.removeStar(scpt.BIG_EXPLOSION);
				}
				
			}
		
			//if space bar is pressed, accelerate away from star. 
			if (Input.GetKeyDown(KeyCode.Space)) {
				//if star is a black hole, then lerp your way out
				if (scpt.isBlackHole) {
					//play sound
					/*GameObject go = GameObject.Find("Alien_Exp_Sound");
					Alien_Exp_Sound ascpt = go.GetComponent<Alien_Exp_Sound>();
					ascpt.blackholeboost.Play();*/
					//each time space bar is pressed energy is consumed
					energy -= BH_ESCAPE_ENERGY;
					escaping_black_hole = true;
					point_of_escape = l.transform.position;
					perp2 = l.transform.position - cur_star.transform.position;
					perp2.Normalize();
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
					//play sound
					
					GameObject go = GameObject.Find("Alien_Exp_Sound");
					Alien_Exp_Sound ascpt = go.GetComponent<Alien_Exp_Sound>();
				//	ascpt.leaving_star.Play();
						
					
					if(scpt.isExplodingStar){
						//reset all Exploding Star variables if left on time
						scpt.explodetimer=0;
						scpt.renderer.material.color = Color.white;
						scpt.blinkspeed=scpt.resblink;
					}
					Learth_Movement.isTangent = false;
					__lastStar = _lastStar;
					_lastStar = lastStar;
					lastStar = s;			
					energy -= LEAVING_COST;
					Learth_Movement.lastPos.position = l.transform.position - Learth_Movement.velocity.normalized*speed;
				}
			}
		}
		//if earth is not tangent to any star
		else if (!Learth_Movement.isTangent) {
			//if not in orbit, decrease energy at correct rate
			if(energy > 0) {
				energy -= FLYING_COST;
			}
			
			//loop through array and calculate tangent vectors to every star
			for (int i = 0; i < star_arr.Length ; i++){
				s = star_arr[i];
				if (s != null) {
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
					if ( s != lastStar
					&& Vector3.Distance(s.transform.position, l.transform.position) >= (sscript.orbitRadius - innerOrbit) 
					&& Vector3.Distance(s.transform.position, l.transform.position) <= (sscript.orbitRadius - outerOrbit) 
					&& Vector3.Distance (tangent, l.transform.position) <= TAN_ERROR) 
					{	
						//play entry sound
						GameObject go = GameObject.Find("Alien_Exp_Sound");
						Alien_Exp_Sound ascpt = go.GetComponent<Alien_Exp_Sound>();
						ascpt.PlayRandomNote();
					//	ascpt.entering_star.Play();
						
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
						float fill_level = 0;
						if (!sscript.isBlackHole) {	

							
							//make energy added proportional to intensity of star light
							fill_level = sscript.r.light.intensity;
							if(sscript.c == orange) {
								fill_level = sscript.r.light.intensity/2f;	
							} else if (sscript.c == purple) {
								fill_level = sscript.r.light.intensity/4f;
							}else if (sscript.c == aqua) {
								fill_level = sscript.r.light.intensity/3f;
							} else {
								fill_level = sscript.r.light.intensity/3.5f;
							}
							
							//color suck effect ripple
							cse = Instantiate(color_suck_effect, s.transform.position, s.transform.rotation) as GameObject;
							Detonator det = cse.GetComponent<Detonator>();
							det.size = sscript.orbitRadius+100;
						
							//this should be refactored
							if(sscript.c == Color.red) {
								red_orbs++;
								det.color = Color.red;
								//points += RED_ENERGY*fill_level;
								
								//events
								/*
								 * 
								 * Note about unfinished code:
								 * Event_Generator.GetPointsFromEvent should be passed the "base" amount of points 
								 * earned by entering orbit around the star. It will return the modified amount of points
								 * based on the currently active (if there is one) event. 
								 * */
								
								points += Event_Generator.GetPointsFromEvent(Color.red,RED_ENERGY*fill_level);
								
								
								if (!IS_INVINCIBLE) {
									Destroy(lt);
									lt = Instantiate (red_learth_trail, l.transform.position, l.transform.rotation) as GameObject;
									lt.transform.parent = l.transform;				
									l.renderer.material.color = Color.red;
								}
							}  else if (sscript.c == orange) {
								orange_orbs++;
								det.color = orange;
								points +=  Event_Generator.GetPointsFromEvent(orange,ORANGE_ENERGY*fill_level);
								if (!IS_INVINCIBLE) {
									Destroy(lt);
									lt = Instantiate (orange_learth_trail, l.transform.position, l.transform.rotation) as GameObject;
									lt.transform.parent = l.transform;
									l.renderer.material.color = orange;
								}
							}  else if (sscript.c == Color.yellow) {
								yellow_orbs++;
								det.color = Color.yellow;
								points += Event_Generator.GetPointsFromEvent(Color.yellow,YELLOW_ENERGY*fill_level);
								if (!IS_INVINCIBLE) {
									Destroy(lt);
									lt = Instantiate (yellow_learth_trail, l.transform.position, l.transform.rotation) as GameObject;
									lt.transform.parent = l.transform;
									l.renderer.material.color = Color.yellow;
								}
							}  else if (sscript.c == green) {
								green_orbs++;
								det.color = green;
								if (!IS_INVINCIBLE) {
									Destroy(lt);
									lt = Instantiate (green_learth_trail, l.transform.position, l.transform.rotation) as GameObject;
									lt.transform.parent = l.transform;
									points +=  Event_Generator.GetPointsFromEvent(green,GREEN_ENERGY*fill_level);
									l.renderer.material.color = green;
								}
							}  else if(sscript.c == Color.blue){
								blue_orbs++;
								det.color = Color.blue;
								points +=  Event_Generator.GetPointsFromEvent(Color.blue,BLUE_ENERGY*fill_level);
								if (!IS_INVINCIBLE) {
									Destroy(lt);
									lt = Instantiate (blue_learth_trail, l.transform.position, l.transform.rotation) as GameObject;
									lt.transform.parent = l.transform;
									l.renderer.material.color = Color.blue;
								}
							}  else if(sscript.c == aqua) {
								aqua_orbs++;
								det.color = aqua;
								points +=  Event_Generator.GetPointsFromEvent(aqua,AQUA_ENERGY*fill_level);
								if (!IS_INVINCIBLE) {
									Destroy(lt);
									lt = Instantiate (aqua_learth_trail, l.transform.position, l.transform.rotation) as GameObject;
									lt.transform.parent = l.transform;
									l.renderer.material.color = aqua;
								}
							}  else if(sscript.c == purple) {
								purple_orbs++;
								det.color = purple;
								points +=  Event_Generator.GetPointsFromEvent(purple,PURPLE_ENERGY*fill_level);
								if (!IS_INVINCIBLE) {
									Destroy(lt);
									lt = Instantiate (purple_learth_trail, l.transform.position, l.transform.rotation) as GameObject;
									lt.transform.parent = l.transform;
									l.renderer.material.color = purple;
								}
							}
							sscript.r.light.intensity = .1f;
						}
						//THIS WORKS AS LONG AS ALL ORBS HAVE SAME POINT VALUE
						Manager.Popup(2,""+(int)(ORANGE_ENERGY*fill_level),s.transform.position);
						break;
					}
				}
			}
		}
	}
	
    public static Manager Instance {
        get 
        {
            if (m_Instance == null)
            {
                // Find a singleton instance
                m_Instance = (Manager)FindObjectOfType(typeof(Manager)); 
                // If there is no GameObject with this script, create one
                if (m_Instance == null)
                    m_Instance = (new GameObject("Manager Singleton")).AddComponent<Manager>();
            }
            return m_Instance;
        }
    }
	
	public static void Popup(int seconds, string s, Vector3 location) {
		Manager.popup_text = s;
		Manager.popup_location = Camera.main.WorldToScreenPoint(location);
		Manager.popup = true;
		Instance.StartCoroutine(Instance.MyMethod(seconds));
	}
	
	private IEnumerator MyMethod(int seconds) {
 		yield return new WaitForSeconds(seconds);
		Manager.popup = false;
	}
	
	void OnGUI() {
		if (end_animation) {
			//set gui style
			GUI.skin = skin;
			GUI.skin.label.normal.textColor = Color.white;
			//performance
			//GUI.Label(new Rect(10,10,150,50), "FPS: "+fps.ToString("f2"));
			Starscript scpt = cur_star.GetComponent<Starscript>();
			if(scpt.is_sink) {
				gscpt.time_to_complete = (Time.time - start_time);
				gscpt.num_stars = star_arr.Length;
				/*	GUI.Label(new Rect(10, Screen.height-80,150,50), "YOU WIN!");
				GUI.Label (new Rect(10, Screen.height-95,150,50), "Time: "+(Time.time - start_time)); */
			}
		
			GUI.skin.label.fontSize = 20;
			//timer
			GUI.Label(new Rect(20,20,100,100),"Time : "+Mathf.Floor(timer)); 
			//points
			GUI.Label (new Rect(20,50,100,50), "Points : ");
			GUI.Label (new Rect(20,70,200,50), (int)points+ "/"+req_points);
			GUI.backgroundColor = Color.black;
			GUI.skin.button.fontSize = 14;
			GUI.skin.button.normal.textColor = Color.cyan;
			GUI.skin.button.hover.textColor = Color.yellow;
			if (GUI.Button(new Rect(5,100,100,30), "Reset Level")) 
				ResetLevel();
	      //  GUI.Label(new Rect(10, Screen.height-65, 150, 50), "Space Coins: "+(gscpt.num_coins));
			//GUI.Label(new Rect(10, Screen.height-50,150,50), "Energy:");
	   	//	GUI.DrawTexture(new Rect(10, Screen.height-30, energy*3, 20), gaugeTexture, ScaleMode.ScaleAndCrop, true, 10F); 
	   		
   		
	   		//on pause
   			if (escape)
   				GUI.Label(new Rect(Screen.width/8, Screen.height/8,Screen.width, Screen.height),control_scheme);
   			
			if (popup) {
				GUI.skin.label.fontSize = 14;
				GUI.Label(new Rect(popup_location.x,popup_location.y,100,100),popup_text); 
			}
		}
	}
		
}