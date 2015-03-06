﻿using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class SpilGamesAPI : MonoBehaviour 
{

	//debug text for example project, feel free to take this out
	public Text debugText;

	//your Spil `Games web game ID
	public string GAME_ID = "576742227280293562";
	
	// where you will put the portal logo
	public Rect logoPosition;

	// the custom height used by the custom height method
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
			"(function() {function onLoadError () {if(typeof u !== 'undefined' && u.getUnity && typeof u.getUnity === 'function'){u.getUnity().SendMessage('SpilGamesAPI', 'APILoadingError', '');}};function initUnity () {if(typeof u !== 'undefined' && u.getUnity && typeof u.getUnity === 'function'){var setGameAPILogo = function (logoUrl) {u.getUnity().SendMessage('SpilGamesAPI', 'setGameAPILogo', logoUrl);};var apiInstance;GameAPI.loadAPI(function(api){apiInstance = api;apiInstance.Branding.displaySplashScreen(function(){});var logoData = apiInstance.Branding.getLogo();if(logoData.image){setGameAPILogo(logoData.image);}}, {id:'" + GAME_ID + "'});}};function loadScript(src, callback) {var s,r,t;r = false;s = document.createElement('script');s.type = 'text/javascript';s.src = src;s.onerror = onLoadError;s.onload = s.onreadystatechange = function() {if ( !r && (!this.readyState || this.readyState == 'complete') ){r = true;callback();}};t = document.getElementsByTagName('script')[0];t.parentNode.insertBefore(s, t);};if (typeof GameAPI !== 'undefined') {initUnity();} else {loadScript('http://cdn.gameplayer.io/api/js/game.js', initUnity);}})();"
			);
		debugText.text = "APILoaded";
	}

	//this will fire if the API is unable to load for any reason
	void APILoadingError(){
		debugText.text = "No API Loaded";
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
		debugText.text = "setGameAPILogo" + url;
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
		debugText.text ="loadlogostart" + url;
		WebAlert( "Loading texture from "+url );
		
		WWW www = new WWW(url);
		yield return www;
		
		WebAlert( "Loaded texture: "+www.error );
		
		_logo = www.texture;
		_hasTexture = true;
		debugText.text = url;
	}


	//social features
	public void ForceAuthentication(){
		Application.ExternalEval (
			"GameAPI.User.forceAuthentication();"
		);
	}

	public void SetUpUser(string userData){
		debugText.text = "SetUpUser Called";
		debugText.text += userData;
	}
	
	public void GetUser(){
		Application.ExternalEval (
			"GameAPI.User.getUser(function (userData){if(typeof u !== 'undefined' && u.getUnity && typeof u.getUnity === 'function'){u.getUnity().SendMessage('SpilGamesAPI', 'SetUpUser', JSON.stringify(userData));}});"
		);
	}
	
	public void ShowInvite(){
		Application.ExternalEval (
			"GameAPI.Friends.showInvite();"
		);
	}
	
	public void SetUpFriends(string friendsData){
		debugText.text = "SetUpFriends Called";
		debugText.text += friendsData;
	}
	
	public void GetFriends(){
		Application.ExternalEval (
			"GameAPI.Friends.getFriends(function (friendsData){if(typeof u !== 'undefined' && u.getUnity && typeof u.getUnity === 'function'){u.getUnity().SendMessage('SpilGamesAPI', 'SetUpFriends', JSON.stringify(friendsData));}});"
		);
	}

	//Enter the code here to pause your game before an AD, make sure the sound is muted too
	public void pauseGame () 
	{
		debugText.text = "Game Paused";
	}

	//Enter the code here to resume your game after an AD
	public void resumeGame () 
	{
		debugText.text = "Game Resumed";
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




	Texture2D ScreenGrab(){
		Texture2D tex = new Texture2D(Screen.width, Screen.height);
		tex.ReadPixels(new Rect(0,0,Screen.width,Screen.height),0,0);
		tex.Apply();
	}











}