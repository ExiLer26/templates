//MCCScript 1.0
string coords = "-22 99 -980";
string target = "cookedchicken";

__apiHandler.LogToConsole("Sandik aciliyor...");
__apiHandler.PerformInternalCommand("useblock " + coords);
System.Threading.Thread.Sleep(6000);

var invs = __apiHandler.GetInventories();
int chestId = -1;
foreach (var inv in invs) { if (inv.Key != 0) { chestId = inv.Key; break; } }

if (chestId != -1)
{
    var items = invs[chestId].Items;
    var slots = new System.Collections.Generic.List<int>();
    int cnt = 0;
    foreach (var item in items)
    {
        if (cnt >= 1) break;
        if (item.Value.Type.ToString().ToLower().Contains(target))
        {
            slots.Add(item.Key);
            cnt++;
        }
    }
    
    // 1 stack al
    foreach (int s in slots)
    {
        __apiHandler.PerformInternalCommand("inventory container click " + s + " shiftrightclick");
        System.Threading.Thread.Sleep(400);
    }
    System.Threading.Thread.Sleep(2000);
    
    // Fazla eşyaları tespit et (container açık)
    var inv2 = __apiHandler.GetInventories();
    var pinv = inv2[0].Items;
    var excess = new System.Collections.Generic.List<int>();
    for (int i = 54; i <= 89; i++)
    {
        if (i <= 81) continue;
        if (pinv.ContainsKey(i) && pinv[i].Type.ToString().ToLower().Contains(target))
            excess.Add(i);
    }
    
    // Geri koy - container açıkken left-click ile
    if (excess.Count > 0)
    {
        __apiHandler.LogToConsole("Fazla " + excess.Count + " item geri konuyor...");
        foreach (int e in excess)
        {
            __apiHandler.PerformInternalCommand("inventory container click " + e + " shiftrightclick");
            System.Threading.Thread.Sleep(600);
        }
        System.Threading.Thread.Sleep(2000);
    }
    
    __apiHandler.PerformInternalCommand("inventory container close");
    __apiHandler.LogToConsole("Sandik kapatildi.");
}

// 3 saniye bekle ve COK1 script'i çalıştır
System.Threading.Thread.Sleep(3000);
__apiHandler.LogToConsole("COK1 script'i başlatılıyor...");
__apiHandler.LogToConsole("Sandik aciliyor...");
__apiHandler.PerformInternalCommand("useblock " + coords);
System.Threading.Thread.Sleep(6000);
// ============ COK1 SCRIPT BAŞLANGIÇ ============
var invs_cok1 = __apiHandler.GetInventories();
int chestId_cok1 = -1;

foreach (var inv in invs_cok1)
{
    if (inv.Key != 0)
    {
        chestId_cok1 = inv.Key;
        break;
    }
}

if (chestId_cok1 != -1)
{
    var items_cok1 = invs_cok1[chestId_cok1].Items;
    bool foundAnyCookedChicken = true;

    // Tıklanacak slotları tut
    System.Collections.Generic.List<int> slotsToClick = new System.Collections.Generic.List<int>();

    foreach (var item in items_cok1)
    {
        int slot = item.Key;

        // 🔒 81. slot KORUMA ALTINDA
        if (slot <= 81) continue;

        if (item.Value.Type.ToString().ToLower().Contains(target))
        {
            foundAnyCookedChicken = true;
            __apiHandler.LogToConsole(
                "Esya bulundu, slot: " + slot + " (" + item.Value.Type.ToString() + ")"
            );
            slotsToClick.Add(slot);
        }
    }

    // Slotlara tıklama işlemi
    foreach (int slot in slotsToClick)
    {
        __apiHandler.PerformInternalCommand(
            "inventory container click " + slot + " shiftrightclick"
        );
        System.Threading.Thread.Sleep(500);
    }

    System.Threading.Thread.Sleep(5000);
    __apiHandler.PerformInternalCommand("inventory container close");
    __apiHandler.LogToConsole("Sandik kapatildi.");

    if (!foundAnyCookedChicken)
        __apiHandler.LogToConsole("CookedChicken bulunamadi.");
    else
        __apiHandler.LogToConsole("Tüm CookedChicken'lar aktarılmaya çalışıldı.");
}
else
{
    __apiHandler.LogToConsole("Hata: Sandik acik degil veya bulunamadi.");
}
// ============ COK1 SCRIPT SONU ============

return null;
