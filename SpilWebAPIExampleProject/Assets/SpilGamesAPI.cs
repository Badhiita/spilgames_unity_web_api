using UnityEngine;
using System.Collections;
using System;

public class SpilGamesAPI : MonoBehaviour 
{
	//your Spil `Games web game ID
	public string GAME_ID = "576742227280293562";
	
	// make sure to include this in the HTML of the page hosting the game
	// <script type="text/javascript" src="http://cdn.gameplayer.io/api/js/game.js"></script>
	
	// where you will put the portal logo
	public Rect logoPosition;
	
	public int customHeight;
	
	//the logo of the portal
	private Texture _logo;
	//have we recived the logo texture from the portal
	private bool _hasTexture = false;
	
	void Start () 
	{
		this.gameObject.name = "SpilGamesAPI";
		//call to the API with the game ID, request assets
		Application.ExternalEval (
			"(function() {function onLoadError () {if(typeof u !== 'undefined' && u.getUnity && typeof u.getUnity === 'function'){u.getUnity().SendMessage('SpilGamesAPI', 'APILoadingError', '');}}function initUnity () {if(typeof u !== 'undefined' && u.getUnity && typeof u.getUnity === 'function'){var setGameAPILogo = function (logoUrl) {u.getUnity().SendMessage('SpilGamesAPI', 'setGameAPILogo', logoUrl);};var resumeGame = function(){u.getUnity().SendMessage('SpilGamesAPI', 'resumeGame', '');};var apiInstance;GameAPI.loadAPI(function(api){apiInstance = api;apiInstance.Branding.displaySplashScreen(function(){});var logoData = apiInstance.Branding.getLogo();if(logoData.image){setGameAPILogo(logoData.image);}}, {id:'" + GAME_ID + "'});}}function loadScript(src, callback){ var s,r,t;r = false;s = document.createElement('script');s.type = 'text/javascript';s.src = src;s.onerror = onLoadError();s.onload = s.onreadystatechange = function() {if ( !r && (!this.readyState || this.readyState == 'complete') ){r = true;callback();}};t = document.getElementsByTagName('script')[0];t.parentNode.insertBefore(s, t);}if(typeof GameAPI !== 'undefined'){initUnity();}else{loadScript('http://cdn.gameplayer.io/api/js/game.js', initUnity);}})();"		
		);
		
		
	}
	
	void APILoadingError(){
		Debug.Log ("No API Loaded");
	}
	
	void Awake() 
	{
		//keep the api object throughout the game
		DontDestroyOnLoad(gameObject);
	}
	
	public bool isInitialized 
	{
		get { return _hasTexture; }
	}
	
	void WebAlert(string txt)
	{
		// Application.ExternalCall( "SayHello", txt);
	}
	void setGameAPILogo (string url) 
	{
		WebAlert( "Got URL "+url+" - will load!" );
		StartCoroutine(loadLogo(url));
	}
	
	void requestBreak()
	{
		Application.ExternalEval (
			"apiInstance.Branding.displaySplashScreen(function(){apiInstance.GameBreak.request(pauseGame, resumeGame)});"
		);
	}
	
	public void AdjustHeight(){
		
		Application.ExternalEval (
			"GameAPI.Game.adjustHeight("+ customHeight.ToString() + ");"
		);
	}

	IEnumerator loadLogo(string url) 
	{
		WebAlert( "Loading texture from "+url );
		
		WWW www = new WWW(url);
		yield return www;
		
		WebAlert( "Loaded texture: "+www.error );
		
		_logo = www.texture;
		_hasTexture = true;
	}
	
	public void ForceAuthentication(){
		
		Application.ExternalEval (
			"GameAPI.User.forceAuthentication();"
		);
	}

	public void SetUpUser(string userData){
		
		Debug.Log (userData);
		
	}
	
	public void GetUser(){
		
		Application.ExternalEval (
			"GameAPI.User.getUser(function (userData){if(typeof u !== 'undefined' && u.getUnity && typeof u.getUnity === 'function'){u.getUnity().SendMessage('SpilGamesAPI', 'SetUpUser', userData);}});"
		);
		
	}
	
	public void ShowInvite(){
		
		Application.ExternalEval (
			"GameAPI.Friends.showInvite();"
		);
	}
	
	public void SetUpFriends(string friendsData){
		
		Debug.Log (friendsData);
		
	}
	
	public void GetFriends(){
		
		Application.ExternalEval (
			"GameAPI.Friends.getFriends(function (friendsData){if(typeof u !== 'undefined' && u.getUnity && typeof u.getUnity === 'function'){u.getUnity().SendMessage('SpilGamesAPI', 'SetUpFriends', friendsData);}});"
		);
		
	}

	public void pauseGame () 
	{
		Debug.Log ("Game Paused");
	}
	
	public void resumeGame () 
	{
		Debug.Log ("Game Resumed");
	}
	
	//show a spil ad
	public void ShowSpilAd()
	{
		Application.ExternalEval (
			"GameAPI.GameBreak.request(function(){if(typeof u !== 'undefined' && u.getUnity && typeof u.getUnity === 'function'){u.getUnity().SendMessage('SpilGamesAPI', 'pauseGame', '');}},function(){if(typeof u !== 'undefined' && u.getUnity && typeof u.getUnity === 'function'){u.getUnity().SendMessage('SpilGamesAPI', 'resumeGame', '');}});"
		);
	}
	
	//show the more games stuff
	public void ShowSpilMoreGames()
	{
		Application.ExternalEval (
			"GameAPI.Branding.getLink('more_games').action();"
		);
	}
	
	//Open the iOS app store in a new tab. Ask your publishing contact to set this link up with the label 'app_store'
	public void OpenAppStoreLink()
	{
		Application.ExternalEval (
			"if (GameAPI && GameAPI.isReady) {GameAPI.Branding.getLink('app_store').action();}"
		);
	}
	
	//Open the Google Play store in a new tab. Ask your publishing contact to set this link up with the label 'goole_play'
	public void OpenGooglePlayLink()
	{
		Application.ExternalEval (
			"if (GameAPI && GameAPI.isReady) {GameAPI.Branding.getLink('google_play').action();}"
		);
	}
	
	//draw the logo
	void OnGUI() 
	{
		if (_hasTexture) 
		{
			GUI.DrawTexture (logoPosition, _logo);
			
			if (Input.GetMouseButtonUp (0)) 
			{
				if (logoPosition.Contains (Event.current.mousePosition)) 
				{
					Application.ExternalEval ("GameAPI.Branding.getLogo().action();");
				}
			}
		}
	}
}
