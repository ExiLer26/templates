//MCCScript 1.0

string command = "/farmer";

__apiHandler.LogToConsole("Sandık açılıyor...");
__apiHandler.PerformInternalCommand("send " + command);

// GUI açılması için bekle
System.Threading.Thread.Sleep(6000);

// Envanterleri al
var invs = __apiHandler.GetInventories();
int chestId = -1;

// Açık olan container'ı bul
foreach (var inv in invs)
{
    if (inv.Key != 0)
    {
        chestId = inv.Key;
        break;
    }
}

if (chestId != -1)
{
    var items = invs[chestId].Items;
    bool foundTarget = false;

    foreach (var item in items)
    {
        string itemStr = item.Value.ToString();

        // Cactus kontrolü
        if (itemStr.Contains("Cactus"))
        {
            foundTarget = true;
            int slot = item.Key;

            __apiHandler.LogToConsole("🏜 Cactus satılıyor | Slot: " + slot);
            __apiHandler.PerformInternalCommand(
                "inventory container click " + slot + " shiftrightclick"
            );

            System.Threading.Thread.Sleep(500);
            break; // sadece ilk bulduğunu satsın
        }
    }

    if (foundTarget)
    {
        System.Threading.Thread.Sleep(3000);
        __apiHandler.PerformInternalCommand("inventory container close");
        __apiHandler.LogToConsole("✅ Cactus satışı tamamlandı.");
    }
    else
    {
        __apiHandler.LogToConsole("❌ Cactus bulunamadı.");
        System.Threading.Thread.Sleep(2000);
        __apiHandler.PerformInternalCommand("inventory container close");
    }
}
else
{
    __apiHandler.LogToConsole("❌ Hata: Farmer sandığı açılamadı.");
}

return null;
