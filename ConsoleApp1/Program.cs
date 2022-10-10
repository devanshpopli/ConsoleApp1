using ConsoleApp1;


class Program
{
    static async Task Main(string[] args)
    {
        string connectionString = "Endpoint=sb://sbij9923.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=O2YH6AGZif5qM01nvJ548fz4y2W28NvcukW+XQ65j9I=";

        // Create the topic
        await Helper.CreateTopicAsync(connectionString);

        // Send message
        await Helper.SendMessageAsync(connectionString);
    }
}
