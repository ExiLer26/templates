//MCCScript 1.0
string command = "/daily";
string searchPattern = "Ödül Alınmadı!";

__apiHandler.LogToConsole("Sandik aciliyor...");
__apiHandler.PerformInternalCommand("send " + command);

System.Threading.Thread.Sleep(6000);

var invs = __apiHandler.GetInventories();
int chestId = -1;

foreach (var inv in invs)
{
    if (inv.Key != 0) { chestId = inv.Key; break; }
}

if (chestId != -1)
{
    var items = invs[chestId].Items;
    bool foundTarget = false;
    
    System.Collections.Generic.List<int> slotsToClick = new System.Collections.Generic.List<int>();

    foreach (var item in items)
    {
        string itemStr = item.Value.ToString();
        
        // MCC'de §a yeşil renk kodudur. "Gün" yazısı ile birlikte kontrol ediyoruz.
        if (itemStr.Contains("§a") && itemStr.Contains("Gün "))
        {
            foundTarget = true;
            int slot = item.Key;
            slotsToClick.Add(slot);
            break; // İlk bulduğunu tıklasın
        }
    }

    if (foundTarget)
    {
        foreach (int slot in slotsToClick)
        {
            __apiHandler.LogToConsole("🎁 Tıklandı slot: " + slot);
            __apiHandler.PerformInternalCommand("inventory container click " + slot + " leftclick");
            System.Threading.Thread.Sleep(500); 
        }
        System.Threading.Thread.Sleep(5000);
        __apiHandler.PerformInternalCommand("inventory container close");
        __apiHandler.LogToConsole("Ödül Alındı. ✅️");
__apiHandler.PerformInternalCommand("inventory container close");
    }
    else
    {
        __apiHandler.LogToConsole("Ödül Hakkını Zaten Aldın,");
        System.Threading.Thread.Sleep(2000);
        __apiHandler.PerformInternalCommand("inventory container close");
    }
}
else
{
    __apiHandler.LogToConsole("Hata: /daily komutu yazamadım.");
}

return null;