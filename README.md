# spilgames_unity_web_api
If you are developing a web build of your unity game for Spilgames, then you can use this plugin to help you interact with the Spilgames web portal API.

You can download the plugin here: <a title="Unity Plugin" href="http://files.cdn.spilcloud.com/gameapi_plugin/1433324058_SpilgamesWebAPIUnityPluginV0_0_7.unitypackage">Download</a>

Or you can directly download this repo.

<strong>Example</strong>

Here is an example of a Unity web player game working with the spil API plugin: <a href="http://www.integrations.partner.spilgames.com/game/game-api-unity">Example</a>

This example is also included in the unity plugin so that you can see how it's put together.
<h4>Basic Setup</h4>
<ol>
	<li>Download the Unity Package and import it to your project.</li>
	<li>Add the SpilGamesAPI prefab to the very first scene your game loads.</li>
	<li>Either in the script directly, or in the prefab inspector, add your Spilgames Game ID.</li>
	<li>Add the code that pauses/unpauses your game to the following methods.
</br>
pauseGame ()
{
//your pause stuff
}
</br>
resumeGame ()
{
//your resume stuff
}
</br>
These methods will be called before and after showing external content in your game.</li>
	<li>Call the ShowSpilMoreGames() and GameBreak() methods in your game in the places agreed on with your publishing lead.</li>
	<li>Set the position of the portal logo. In the example, I have created a button that displays the returned portal logo. <em>brandingButtonImage</em>. Then when that button is clicked, it calls <em>BrandingLogoClicked</em>();. Feel free to do this another way if you don't want to use this method.</li>
</ol>

Now test your game in the <a title="test tool" href="http://cdn.gameplayer.io/testtool/index.html#!/tester">test tool</a>. You should be able to trigger all green ticks on the right of the page.

&nbsp;
<h3>Social Features</h3>
If your game is configured to use social features, then you can use the following methods to retrieve data regarding the user.

<strong>ForceAuthentication();</strong>

This Method will activate a Login popup on the portal, forcing the user to login.
</br>

public void ForceAuthentication(){
Application.ExternalEval (
"GameAPI.User.forceAuthentication();"
);
}

</br>
<strong>GetUser();</strong>

This method will get a JSON string of info about the user. The data is passed to the method SetUpUser(); You can do whatever you need to with the data in that method.
</br>
public void GetUser(){
Application.ExternalEval (
"GameAPI.User.getUser(function (userData){if(typeof u !== 'undefined' &amp;&amp; u.getUnity &amp;&amp; typeof u.getUnity === 'function'){u.getUnity().SendMessage('SpilGamesAPI', 'SetUpUser', JSON.stringify(userData));}});"
);
}
</br>
<strong>GetFriends();</strong>

This method will get a JSON string of the logged in users friends. The data is passed to the method SetUpFriends(); You can do whatever you need to with the data in that method.

</br>
public void GetFriends(){
Application.ExternalEval (
"GameAPI.Friends.getFriends(function (friendsData){if(typeof u !== 'undefined' &amp;&amp; u.getUnity &amp;&amp; typeof u.getUnity === 'function'){u.getUnity().SendMessage('SpilGamesAPI', 'SetUpFriends', JSON.stringify(friendsData));}});"
);
}

</br>
You can also send a score or achievements (awards) to the portal if the portal is configured to receive it using the ScoreSubmit and AwardSubmit methods.
