//MCCScript 1.0
string coords = "-21 101 -975";
string target = "tripwirehook";  // TripwireHook olarak güncellendi

__apiHandler.LogToConsole("Sandik aciliyor...");
__apiHandler.PerformInternalCommand("useblock " + coords);

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
    bool foundAnyTripwireHook = false; // Bulunan tripwirehook takibi
    
    // Tıklanacak slotları depolamak için bir liste oluştur
    System.Collections.Generic.List<int> slotsToClick = new System.Collections.Generic.List<int>();

    foreach (var item in items)
    {
        if (item.Value.Type.ToString().ToLower().Contains(target))
        {
            foundAnyTripwireHook = true;
            int slot = item.Key;
            __apiHandler.LogToConsole("Esya bulundu, slot: " + slot + " (" + item.Value.Type.ToString() + ")");
            slotsToClick.Add(slot); // Slotu listeye ekle
        }
    }

    // Toplanan slotlar üzerinde döngü yaparak tıklama işlemlerini gerçekleştir
    foreach (int slot in slotsToClick)
    {
        __apiHandler.PerformInternalCommand("inventory container click " + slot + " shiftrightclick");
        System.Threading.Thread.Sleep(500); // Her tıklama arasında kısa bekleme
    }

    System.Threading.Thread.Sleep(5000);  // Kapatmadan önce bekleme
    __apiHandler.PerformInternalCommand("inventory container close");
    __apiHandler.LogToConsole("Sandik kapatildi.");

    if (!foundAnyTripwireHook)
    {
        __apiHandler.LogToConsole("Tripwire Hook bulunamadi.");
    }
    else
    {
        __apiHandler.LogToConsole("Tüm Tripwire Hook'lar envantere aktarılmaya çalışıldı.");
    }
}
else
{
    __apiHandler.LogToConsole("Hata: Sandik acik degil veya bulunamadi.");
}

return null;