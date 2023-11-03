using FireSharp;
using FireSharp.EventStreaming;
using FireSharp.Response;

namespace consolechat.modules;

public class Firebase
{
    
    private FirebaseClient firebaseClient;

    public Firebase()
    {
        var firebaseConfig = new FireSharp.Config.FirebaseConfig
        {
            AuthSecret = Environment.GetEnvironmentVariable("AUTHSECRET"),
            BasePath = Environment.GetEnvironmentVariable("BASEPATH")
        };
        firebaseClient = new FirebaseClient(firebaseConfig);

        Console.WriteLine(firebaseClient != null
            ? "Firebase client initialized"
            : "Firebase client failed to initialize");
    }

    public string? CreateChatroom()
    {
        Chatroom chatroom = new Chatroom
        {
            Uid = Guid.NewGuid().ToString("N"),
        };
        SetResponse response = firebaseClient.Set("chatrooms/" + chatroom.Uid, chatroom);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return chatroom.Uid;
        }
        return null;
    }
    
    public async Task<object?> ConnectToChatroom(string? uid)
    {
        Chatroom chatroom = (await firebaseClient.GetAsync("chatrooms/" + uid)).ResultAs<Chatroom>();
        if (chatroom != null)
        {
            Console.WriteLine("Connected to chatroom with uid " + uid);
            Console.WriteLine("Type /exit to exit the chatroom");
            Console.WriteLine("--------------------");
            string lastMessageSendUid = "";
            async void Added(object sender, ValueAddedEventArgs args, object context)
            {
                try
                {
                    FirebaseResponse response = await firebaseClient.GetAsync("chatrooms/" + uid + "/Messages" + "/" + args.Data);
                    Message messageResponse = response.ResultAs<Message>();
                    if (messageResponse.Uid != lastMessageSendUid) Console.WriteLine(messageResponse.Text);
                }
                catch (Exception e)
                {
                    // ignored
                }
            }

            EventStreamResponse response = await firebaseClient.OnAsync("chatrooms/" + uid + "/Messages", Added);                
            
            async Task SendMessages()
            {
                string? responseUser = null;
                do
                {
                    responseUser = Console.ReadLine();
                    if (responseUser != null && responseUser != "/exit")
                    {
                        Message message = new Message
                        {
                            Uid = Guid.NewGuid().ToString("N"),
                            Text = responseUser,
                            Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                        };
                        SetResponse setResponse = await firebaseClient.SetAsync("chatrooms/" + uid + "/Messages/" + message.Uid, message);
                        if (setResponse.StatusCode != System.Net.HttpStatusCode.OK) Console.WriteLine("Failed to send message");
                        lastMessageSendUid = message.Uid;
                    }
                } while (responseUser != "/exit");
            }
            
            await SendMessages();

            Console.WriteLine("Disconnected from chatroom");
            response.Dispose();
        }
        else
        {
            Console.WriteLine("Failed to connect to chatroom with uid " + uid);
        }
        return null;
    }
    
}