using UnityEngine;

namespace KongAPI
{
	/// <summary>
	/// Provides quick access to Kongregate's API system, allowing the submission of stats. It is best to handle setup of this class
	/// as soon as possible in your application.
	/// </summary>
	/// 
	public class KongregateAPI : MonoBehaviour
	{
		/// Are we connected to Kongregate's API?
		public static bool Connected { get; private set; }
		/// The user's UserID.
		public static int UserId { get; private set; }
		/// The user's username.
		public static string Username { get; private set; }
		/// The game's authentication token.
		public static string GameAuthToken { get; private set; }
		
		void Start()
		{
			Connect();
		}
		
		/// <summary>
		/// Connect to Kongregate's API service.
		/// </summary>
		public void Connect()
		{
			if (!Connected)
			{
				Application.ExternalEval(
					"if(typeof(kongregateUnitySupport) != 'undefined') {" +
					"kongregateUnitySupport.initAPI('" + gameObject.name + "', 'OnKongregateAPILoaded');" +
					"}"
					);
			}
			else
				Debug.LogWarning("You are attempting to connect to Kongregate's API multiple times. You only need to connect once.");
		}
		
		/// <summary>
		/// Submit a value to the server.
		/// </summary>
		/// <param name="statisticName">The name of the statistic. This is the name provided in the "Statistic name" section when you fill in the API when uploading your game.</param>
		/// <param name="value">The value to submit (score, kills, deaths, etc...).</param>
		public static void Submit(string statisticName, int value)
		{
			if (Connected)
				Application.ExternalCall("kongregate.stats.submit", statisticName, value);
			else
				Debug.LogWarning("You are attempting to submit a statistic without being connected to Kongregate's API. Connect first, then submit.");
		}
		
		private void OnKongregateAPILoaded(string userInfoString)
		{
			Connected = true;
			string[] parameters = userInfoString.Split('|');
			UserId = System.Convert.ToInt32(parameters[0]);
			Username = parameters[1];
			GameAuthToken = parameters[2];
		}
	}
}