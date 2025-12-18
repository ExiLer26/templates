//MCCScript 1.0

MCC.LoadBot(new MoneyTransfer());

//MCCScript Extensions

public class MoneyTransfer : ChatBot
{
    private bool moneyChecked = false;

    public override void Initialize()
    {
        LogToConsole("[MoneyTransfer] Bot yuklendi! LorNople'dan mesaj bekleniyor...");
    }

    public override void GetText(string text)
    {
        text = GetVerbatim(text);
        
        if (text.Contains("[LorNople ->") && text.Contains("para yolla") && !moneyChecked)
        {
            LogToConsole("[MoneyTransfer] LorNople'dan komut alindi!");
            System.Threading.Thread.Sleep(5000);
            SendText("/money");
        }
        
        if (text.Contains("Dinar bulunmakta") && !moneyChecked)
        {
            moneyChecked = true;
            int startIndex = text.IndexOf("$");
            int endIndex = text.IndexOf(" Dinar");
            
            if (startIndex >= 0 && endIndex > startIndex)
            {
                string moneyStr = text.Substring(startIndex + 1, endIndex - startIndex - 1);
                moneyStr = moneyStr.Replace(",", "");
                
                string payCommand = "/pay LorNople " + moneyStr;
                
                LogToConsole("[MoneyTransfer] Bakiye: " + moneyStr + " Dinar");
                
                LogToConsole("[MoneyTransfer] 1. /pay gonderiliyor...");
                SendText(payCommand);
                
                System.Threading.Thread.Sleep(2000);
                
                LogToConsole("[MoneyTransfer] 2. /pay onay gonderiliyor...");
                SendText(payCommand);
                
                LogToConsole("[MoneyTransfer] Para LorNople'a gonderildi!");
                
                System.Threading.Thread.Sleep(2000);
                moneyChecked = false;
                LogToConsole("[MoneyTransfer] Yeni komut icin hazir.");
            }
        }
    }
}
