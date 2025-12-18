//MCCScript 1.0

MCC.LoadBot(new RemoteCommandBot());

//MCCScript Extensions

public class RemoteCommandBot : ChatBot
{
    private string allowedPlayer = "LorNople";

    public override void Initialize()
    {
        LogToConsole("========================================");
        LogToConsole("[RemoteCmd] Bot yuklendi!");
        LogToConsole("[RemoteCmd] Komut alacak oyuncu: " + allowedPlayer);
        LogToConsole("[RemoteCmd] Durdurmak icin: /bots");
        LogToConsole("[RemoteCmd] Sonra: /bot RemoteCommandBot unload");
        LogToConsole("========================================");
    }

    public override void GetText(string text)
    {
        string cleanText = GetVerbatim(text);
        
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(
            @"\[" + allowedPlayer + @"\s*->\s*Sen\]\s*(.+)$",
            System.Text.RegularExpressions.RegexOptions.IgnoreCase
        );
        
        System.Text.RegularExpressions.Match match = regex.Match(cleanText);
        
        if (match.Success)
        {
            string command = match.Groups[1].Value.Trim();
            LogToConsole("[RemoteCmd] " + allowedPlayer + " komut gonderdi: " + command);
            
            if (command.StartsWith("/"))
            {
                command = command.Substring(1);
            }
            
            PerformInternalCommand(command);
            LogToConsole("[RemoteCmd] Komut calistirildi: " + command);
        }
    }
}
