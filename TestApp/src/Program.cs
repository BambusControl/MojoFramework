using System.Text.Json;
using MojoFramework;
using MojoFramework.Attributes;
using TestApp.Data.Model;
using TestApp.Logic.Controller;

namespace TestApp;

[MojoApplication]
public class Program
{
	private static readonly JsonSerializerOptions JsonOptions = new()
	{
		// WriteIndented = true,
	};

	public static void Main(string[] args)
	{
		/* Example app, where users can send each other messages */
		Mojo.Run<Program>(args);
	}

	[MojoStart]
	private static void DoStuff(AccountController account, MessengerController messenger)
	{
		const string userA = "Josh";
		const ConsoleColor clA = ConsoleColor.Cyan;
		const ConsoleColor clAd = ConsoleColor.DarkCyan;

		const string userB = "Nancy";
		const ConsoleColor clB = ConsoleColor.Green;
		const ConsoleColor clBd = ConsoleColor.DarkGreen;

		Console.ForegroundColor = clA;
		Console.WriteLine($"== {userA}");
		Write(account.GetUserInfo(userA));
		Write(messenger.GetInbox(userA));
		Console.ResetColor();
		Console.WriteLine();

		Console.ForegroundColor = clB;
		Console.WriteLine($"== {userB}");
		Write(account.GetUserInfo(userB));
		Write(messenger.GetInbox(userB));
		Console.ResetColor();
		Console.WriteLine();

		Console.WriteLine("== Sending messages");
		Console.ForegroundColor = clAd;
		Write(messenger.WriteMessage(new MessageDraftModel(userA, userB, "How do you do")));
		Console.ForegroundColor = clBd;
		Write(messenger.WriteMessage(new MessageDraftModel(userB, userA, "Just writing some C#")));
		Console.ForegroundColor = clAd;
		Write(messenger.WriteMessage(new MessageDraftModel(userA, userB, "I Hope you're using MojoFramework")));
		Console.ForegroundColor = clBd;
		Write(messenger.WriteMessage(new MessageDraftModel(userB, userA, "Of course I am")));
		Console.ResetColor();
		Console.WriteLine();

		Console.ForegroundColor = clA;
		Console.WriteLine($"== {userA}");
		Write(account.GetUserInfo(userA));
		Write(messenger.GetInbox(userA));
		Console.ResetColor();
		Console.WriteLine();

		Console.ForegroundColor = clB;
		Console.WriteLine($"== {userB}");
		Write(account.GetUserInfo(userB));
		Write(messenger.GetInbox(userB));
		Console.ResetColor();
		Console.WriteLine();

		messenger.GetInbox(userA);
	}

	private static void Write(object obj)
	{
		Console.WriteLine(
			JsonSerializer.Serialize(obj, JsonOptions)
		);
	}
}
