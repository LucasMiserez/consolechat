namespace consolechat 
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            modules.Firebase firebase = new modules.Firebase();
                            
            
            Console.WriteLine("--------------------");
            Console.WriteLine("Welcome to ConsoleChat");  
            string? response = null;
            do
            {
                Console.WriteLine("Type /help for a list of commands");
                Console.WriteLine("--------------------");
                response = Console.ReadLine();
                if (response == "/help")
                {
                    Console.WriteLine("Commands:");
                    Console.WriteLine("/help - Displays this message");
                    Console.WriteLine("/exit - Exits the program");
                    Console.WriteLine("/createchatroom - Creates a new chatroom");
                    Console.WriteLine("/joinchatroom - Join a chatroom");
                }
                else if (response == "/createchatroom")
                {
                    string? uid = firebase.CreateChatroom();
                    if (uid != null)
                    {
                        Console.WriteLine("Chatroom created with uid " + uid);
                        await firebase.ConnectToChatroom(uid);
                    }
                    else
                    {
                        Console.WriteLine("Failed to create chatroom");
                    }
                }
                else if (response == "/joinchatroom")
                {
                    Console.Write("Enter chatroom uid: ");
                    string? roomUid = Console.ReadLine();
                    if (!string.IsNullOrEmpty(roomUid))
                    {
                        await firebase.ConnectToChatroom(roomUid);
                    }
                    else
                    {
                        Console.WriteLine("Failed to join chatroom");
                    }
                }
                else
                {
                    Console.WriteLine("Unknown command");
                }
                response = Console.ReadLine();
            } while (response != "/exit");
        }
    }
}