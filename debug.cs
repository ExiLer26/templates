//MCCScript 1.0
string command = "/farmer";
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
        string itemType = item.Value.Type.ToString();
        
        if (!itemType.ToLower().Contains("stick"))
        {
            string itemStr = item.Value.ToString();
            __apiHandler.LogToConsole("Slot " + item.Key + ": " + itemStr);
            
            if (itemStr.Contains(searchPattern))
            {
                foundTarget = true;
                int slot = item.Key;
                __apiHandler.LogToConsole(">>> BULUNDU! Slot: " + slot);
                slotsToClick.Add(slot);
            }
        }
    }

    if (foundTarget)
    {
        foreach (int slot in slotsToClick)
        {
            __apiHandler.LogToConsole("Shiftrightclick yapiliyor slot: " + slot);
            __apiHandler.PerformInternalCommand("inventory container click " + slot + " leftclick");
            System.Threading.Thread.Sleep(500); 
        }
        System.Threading.Thread.Sleep(5000);
        __apiHandler.PerformInternalCommand("inventory container close");
        __apiHandler.LogToConsole("Ödül Alındı. ✅️");
    }
    else
    {
        __apiHandler.LogToConsole("49/25 bulunamadi!");
        System.Threading.Thread.Sleep(8000);
        __apiHandler.PerformInternalCommand("inventory container close");
    }
}
else
{
    __apiHandler.LogToConsole("Hata: Sandik acilamadi.");
}

return null;