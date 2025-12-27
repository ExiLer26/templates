//MCCScript 1.0

// === TOPLANACAK ITEMLER ===
string[] targets = {
    "diamond",
    "ironingot",
    "goldingot",
    "emerald"
};

__apiHandler.LogToConsole("Envanter Taranıyor..🛃");
System.Threading.Thread.Sleep(6000);

var invs = __apiHandler.GetInventories();

//
// ===============================
// OYUNCU ENVANTERI TARAMA
// ===============================
if (invs.ContainsKey(0))
{
    var playerItems = invs[0].Items;
    System.Collections.Generic.List<int> playerSlots =
        new System.Collections.Generic.List<int>();

    foreach (var item in playerItems)
    {
        string itemName = item.Value.Type.ToString().ToLower();

        foreach (string t in targets)
        {
            if (itemName.Contains(t))
            {
                playerSlots.Add(item.Key);
                __apiHandler.LogToConsole(
                    "Oyuncu envanterinde esya bulundu: " + itemName +
                    " | slot: " + item.Key
                );
                break;
            }
        }
    }

    foreach (int slot in playerSlots)
    {
        __apiHandler.PerformInternalCommand("inventory 0 drop " + slot + " all");
        System.Threading.Thread.Sleep(500);
    }

    if (playerSlots.Count > 0)
    {
        __apiHandler.LogToConsole("Otomatik Event Verilen Eşyalar Atıldı 🚮");
        return null;
    }
}

//
// ===============================
// SANDIK (CONTAINER) TARAMA
// ===============================
int chestId = 0;

foreach (var inv in invs)
{
    if (inv.Key != 0)
    {
        chestId = inv.Key;
        break;
    }
}

if (chestId != 0)
{
    var items = invs[chestId].Items;
    bool foundAny = false;

    System.Collections.Generic.List<int> slotsToClick =
        new System.Collections.Generic.List<int>();

    foreach (var item in items)
    {
        string itemName = item.Value.Type.ToString().ToLower();

        foreach (string t in targets)
        {
            if (itemName.Contains(t))
            {
                foundAny = true;
                slotsToClick.Add(item.Key);
                __apiHandler.LogToConsole(
                    "Sandikta esya bulundu: " + itemName +
                    " | slot: " + item.Key
                );
                break;
            }
        }
    }

    foreach (int slot in slotsToClick)
    {
        __apiHandler.PerformInternalCommand("inventory 0 drop " + slot + " all");
        System.Threading.Thread.Sleep(500);
    }

    //if (foundAny)
        //__apiHandler.LogToConsole("Ödül Alındı. ✅️");
    //else
        //__apiHandler.LogToConsole("Günlük ödül alınamadı. ❎️");
}
else
{
    __apiHandler.LogToConsole("Hata: otomatik event yapılmamıştır..🛅.");
}

return null;
