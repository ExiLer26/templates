//MCCScript 1.0
string command = "/farmer";
string searchPattern = "";

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

        if (itemStr.Contains("Wheat Seeds") || itemStr.Contains("Wheat"))
        {
            foundTarget = true;
            int slot = item.Key;
            slotsToClick.Add(slot);
        }
    }

    if (foundTarget)
    {
        foreach (int slot in slotsToClick)
        {
            __apiHandler.LogToConsole("🫘 Satıldı slot: " + slot);
            __apiHandler.PerformInternalCommand("inventory container click " + slot + " shiftrightclick");
            System.Threading.Thread.Sleep(500);
        }
        System.Threading.Thread.Sleep(5000);
        __apiHandler.PerformInternalCommand("inventory container close");
        __apiHandler.LogToConsole("Ürünler Satıldı (🪙🪙)");
    }
    else
    {
        __apiHandler.LogToConsole("az para geldi 💱,");
        System.Threading.Thread.Sleep(2000);
        __apiHandler.PerformInternalCommand("inventory container close");
    }
}
else
{
    __apiHandler.LogToConsole("Hata: /farmer komutu yazamadım.");
}

return null;