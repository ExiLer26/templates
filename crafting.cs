//MCCScript 1.0

int MAX_LOOP = 1; // 👈 KAÇ KEZ ÇALIŞSIN (5, 10, 20...)

for (int loop = 1; loop <= MAX_LOOP; loop++)
{
    __apiHandler.LogToConsole("🔁 Döngü " + loop + "/" + MAX_LOOP + " başlıyor");

    // =====================
    // FARMER / KÜLÇE ALMA
    // =====================
    __apiHandler.PerformInternalCommand("send /farmer");
    System.Threading.Thread.Sleep(2000);

    var invs = __apiHandler.GetInventories();
    int chestId = -1;

    foreach (var inv in invs)
    {
        if (inv.Key != 0)
        {
            chestId = inv.Key;
            break;
        }
    }

    if (chestId == -1)
    {
        __apiHandler.LogToConsole("❌ Inventory bulunamadı, döngü atlandı");
        continue;
    }

    var items = invs[chestId].Items;
    int ironSlot = -1;

    foreach (var item in items)
    {
        string itemStr = item.Value.ToString();
        if (itemStr.Contains("Demir Külçesi") || itemStr.Contains("Demir Çubuğu"))
        {
            ironSlot = item.Key;
            break;
        }
    }

    if (ironSlot != -1)
    {
        for (int i = 0; i < 9; i++)
        {
            __apiHandler.PerformInternalCommand(
                "inventory container click " + ironSlot + " left");
            System.Threading.Thread.Sleep(500);
        }
    }

    System.Threading.Thread.Sleep(1000);
    __apiHandler.PerformInternalCommand("inventory container close");

    // =====================
    // WORKBENCH / BLOK CRAFT
    // =====================
    System.Threading.Thread.Sleep(4000);
    __apiHandler.PerformInternalCommand("send /wb");
    System.Threading.Thread.Sleep(2000);

    invs = __apiHandler.GetInventories();
    chestId = -1;

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
        items = invs[chestId].Items;
        int ironCount = 0;

        for (int slot = 10; slot <= 45; slot++)
        {
            if (!items.ContainsKey(slot))
                continue;

            string itemStr = items[slot].ToString();

            if (itemStr.Contains("Demir Külçesi") || itemStr.Contains("Demir Çubuğu"))
            {
                __apiHandler.PerformInternalCommand(
                    "inventory container click " + slot + " shiftrightclick");
                System.Threading.Thread.Sleep(300);
                ironCount++;

                if (ironCount == 9)
                {
                    System.Threading.Thread.Sleep(1000);
                    __apiHandler.PerformInternalCommand(
                        "inventory container click 0 shiftrightclick");
                    System.Threading.Thread.Sleep(500);
                    ironCount = 0;
                }
            }
        }

        if (ironCount > 0)
        {
            System.Threading.Thread.Sleep(1000);
            __apiHandler.PerformInternalCommand(
                "inventory container click 0 left");
        }

        System.Threading.Thread.Sleep(1000);
        __apiHandler.PerformInternalCommand("inventory container close");
    }

    // =====================
    // Sandık / BLOKLARI AT
    // =====================
    System.Threading.Thread.Sleep(10000);
    __apiHandler.PerformInternalCommand("send /pv 2");
    System.Threading.Thread.Sleep(2000);

    invs = __apiHandler.GetInventories();
    chestId = -1;

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
        items = invs[chestId].Items;

        for (int slot = 54; slot <= 89; slot++)
        {
            if (!items.ContainsKey(slot))
                continue;

            string itemStr = items[slot].ToString();

            if (
    itemStr.Contains("Demir Bloğu") ||
                itemStr.Contains("Iron Block") ||
                itemStr.Contains("Demir Bloğu")
            )
            {
                __apiHandler.PerformInternalCommand(
"inventory container click " + slot + " shiftrightclick");
                System.Threading.Thread.Sleep(300);
            }
        }

        __apiHandler.PerformInternalCommand("inventory container close");
    }

    __apiHandler.LogToConsole("✅ Döngü " + loop + " tamamlandı");

    // Spam yememek için
    System.Threading.Thread.Sleep(15000);
}

__apiHandler.LogToConsole("🏁 Script tamamen bitti!");
return null;
