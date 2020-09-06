using Exiled.API.Interfaces;
using System.ComponentModel;

namespace KillFeed
{
	public class Config : IConfig
	{
		public bool IsEnabled { get; set; } = true;

		[Description("The message to be displayed every time a player is killed.")]
		public string KillMessage { get; set; } = "%killername (%killerid) killed %victimname (%victimid) using %weapon.";

		[Description("The color for each kill feed message to be displayed in.")]
		public string KillColor { get; set; } = "white";
	}
}
