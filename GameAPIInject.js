/*
===============

JS FOR WEB PLAYER BUILD

===============
*/


(function() {function onLoadError () {if(typeof u !== 'undefined' && u.getUnity && typeof u.getUnity === 'function'){u.getUnity().SendMessage('SpilGamesAPI', 'APILoadingError', '');}};function initUnity () {if(typeof u !== 'undefined' && u.getUnity && typeof u.getUnity === 'function'){var setGameAPILogo = function (logoUrl) {u.getUnity().SendMessage('SpilGamesAPI', 'setGameAPILogo', logoUrl);};var apiInstance;GameAPI.loadAPI(function(api){apiInstance = api;apiInstance.Branding.displaySplashScreen(function(){});var logoData = apiInstance.Branding.getLogo();if(logoData.image){setGameAPILogo(logoData.image);}}, {id:'" + GAME_ID + "'});}};function loadScript(src, callback) {var s,r,t;r = false;s = document.createElement('script');s.type = 'text/javascript';s.src = src;s.onerror = onLoadError;s.onload = s.onreadystatechange = function() {if ( !r && (!this.readyState || this.readyState == 'complete') ){r = true;callback();}};t = document.getElementsByTagName('script')[0];t.parentNode.insertBefore(s, t);};if (typeof GameAPI !== 'undefined') {initUnity();} else {loadScript('http://cdn.gameplayer.io/api/js/game.js', initUnity);}})();

/* This code is the javascript run by the unity plugin to 
  initialize GameAPI in the page.

  It will try to inject the GameAPI script if it is not
  already added to the page and inform unity if it sucedes
  or not
 */


(function() {
  function onLoadError () {
    if(typeof u !== 'undefined' && u.getUnity && typeof u.getUnity === 'function'){
      u.getUnity().SendMessage('SpilGamesAPI', 'APILoadingError', '');
    }
  };

  function initUnity () {
    if(typeof u !== 'undefined' && u.getUnity && typeof u.getUnity === 'function'){

      var setGameAPILogo = function (logoUrl) {
        u.getUnity().SendMessage('SpilGamesAPI', 'setGameAPILogo', logoUrl);

      };

      var apiInstance;
      
      GameAPI.loadAPI(function(api){
        apiInstance = api;
        apiInstance.Branding.displaySplashScreen(function(){});
        var logoData = apiInstance.Branding.getLogo();
        
        if(logoData.image){
          setGameAPILogo(logoData.image);
        }

        if (api.Game.isSiteLock()) {u.getUnity().SendMessage('SpilGamesAPI', 'OnSiteLock', '');}
        
        window.onkeydown = function(){};
      }, {id:'" + GAME_ID + "'});
    }
  };

  function loadScript(src, callback) {
    var s,
        r,
        t;
    r = false;
    s = document.createElement('script');
    s.type = 'text/javascript';
    s.src = src;
    s.onerror = onLoadError;
    s.onload = s.onreadystatechange = function() {
      if ( !r && (!this.readyState || this.readyState == 'complete') )
      {
        r = true;
        callback();
      }
    };
    t = document.getElementsByTagName('script')[0];
    t.parentNode.insertBefore(s, t);
  };
  console.log("1");
  if (typeof GameAPI !== 'undefined') {
    initUnity();
  } else {
    loadScript('http://cdn.gameplayer.io/api/js/game.js', initUnity);
  }
})();



/* This code is the javascript run by the unity plugin to 
  request a game break from Spil page and also set up the
  callbacks for the game
 */
GameAPI.GameBreak.request(function(){if(typeof u !== 'undefined' && u.getUnity && typeof u.getUnity === 'function'){u.getUnity().SendMessage('SpilGamesAPI', 'pauseGame', '');}},function(){if(typeof u !== 'undefined' && u.getUnity && typeof u.getUnity === 'function'){u.getUnity().SendMessage('SpilGamesAPI', 'resumeGame', '');}});
/**/

GameAPI.GameBreak.request(
  function(){
    if(typeof u !== 'undefined' && u.getUnity && typeof u.getUnity === 'function'){
      u.getUnity().SendMessage('SpilGamesAPI', 'pauseGame', '');
    }
  },
  function(){
    if(typeof u !== 'undefined' && u.getUnity && typeof u.getUnity === 'function'){
      u.getUnity().SendMessage('SpilGamesAPI', 'resumeGame', '');
    }
  }
);


/* Submits a Score to Spil Games portal */
GameAPI.Score.submit('" + score + "');

/* Submits an Award to Spil Games portal */
GameAPI.Award.submit('" + awardName + "');

/* Checks if the game should be site locked */

if (GameAPI.Game.isSiteLock()) {
  u.getUnity().SendMessage('SpilGamesAPI', 'OnSiteLock', 'sitelock');
}



if (GameAPI.Game.isSiteLock()) {u.getUnity().SendMessage('SpilGamesAPI', 'OnSiteLock', 'sitelock');}






/*
===============

JS FOR WEBGL BUILD

===============
*/



/* This code is the javascript run by the unity plugin to 
  initialize GameAPI in the page.

  It will try to inject the GameAPI script if it is not
  already added to the page and inform unity if it sucedes
  or not
 */


(function() {
  function onLoadError () {
    SendMessage('SpilGamesAPI', 'APILoadingError', '');
  };

  function initUnity () {
    var setGameAPILogo = function (logoUrl) {
      SendMessage('SpilGamesAPI', 'setGameAPILogo', logoUrl);

    };

    var apiInstance;
    
    GameAPI.loadAPI(function(api){
      apiInstance = api;
      apiInstance.Branding.displaySplashScreen(function(){});
      var logoData = apiInstance.Branding.getLogo();
      
      if(logoData.image){
        setGameAPILogo(logoData.image);
      }

      if (api.Game.isSiteLock()) {
        SendMessage('SpilGamesAPI', 'OnSiteLock', '');
      }
      
      window.onkeydown = function(){};
    }, {id:'" + GAME_ID + "'});
  };

  function loadScript(src, callback) {
    var s,
        r,
        t;
    r = false;
    s = document.createElement('script');
    s.type = 'text/javascript';
    s.src = src;
    s.onerror = onLoadError;
    s.onload = s.onreadystatechange = function() {
      if ( !r && (!this.readyState || this.readyState == 'complete') )
      {
        r = true;
        callback();
      }
    };
    t = document.getElementsByTagName('script')[0];
    t.parentNode.insertBefore(s, t);
  };
  
  if (typeof GameAPI !== 'undefined') {
    initUnity();
  } else {
    loadScript('http://cdn.gameplayer.io/api/js/game.js', initUnity);
  }
})();


/* This code is the javascript run by the unity plugin to 
  request a game break from Spil page and also set up the
  callbacks for the game
 */

/**/

GameAPI.GameBreak.request(
  function(){
    SendMessage('SpilGamesAPI', 'pauseGame', '');
  },
  function(){
    SendMessage('SpilGamesAPI', 'resumeGame', '');
  }
);

/* Submits a Score to Spil Games portal */
GameAPI.Score.submit('" + score + "');

/* Submits an Award to Spil Games portal */
GameAPI.Award.submit('" + awardName + "');

/*
get friends
*/
GameAPI.Friends.getFriends(function(friendsData) {
    SendMessage('SpilGamesAPI', 'SetUpFriends', JSON.stringify(friendsData));
});

/*
get user
*/
GameAPI.User.getUser(function(userData) {
        SendMessage('SpilGamesAPI', 'SetUpUser', JSON.stringify(userData));
});

