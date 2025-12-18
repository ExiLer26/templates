//MCCScript 1.0

MCC.LoadBot(new EntityFeedBot());

//MCCScript Extensions

public class EntityFeedBot : ChatBot
{
    private bool hasFed = false;
    private int feedPerMinion = 4;

    public override void Initialize()
    {
        LogToConsole("========================================");
        LogToConsole("[EntityFeed] Bot yuklendi!");
        LogToConsole("[EntityFeed] Her minyon " + feedPerMinion + " kez beslenecek");
        LogToConsole("========================================");
    }

    public override void Update()
    {
        if (!hasFed)
        {
            hasFed = true;
            FeedAllArmorStands();
            LogToConsole("[EntityFeed] Islem tamamlandi, bot kapaniyor...");
            UnloadBot();
        }
    }

    private void FeedAllArmorStands()
    {
        System.Collections.Generic.Dictionary<int, Entity> entities = GetEntities();
        
        if (entities == null || entities.Count == 0)
        {
            LogToConsole("[EntityFeed] Hic entity bulunamadi!");
            return;
        }
        
        int fedCount = 0;
        
        foreach (System.Collections.Generic.KeyValuePair<int, Entity> kvp in entities)
        {
            Entity entity = kvp.Value;
            
            if (entity.Type == EntityType.ArmorStand)
            {
                int entityId = kvp.Key;
                
                for (int i = 0; i < feedPerMinion; i++)
                {
                    PerformInternalCommand("entity " + entityId + " use");
                }
                
                LogToConsole("[EntityFeed] #" + entityId + " - " + feedPerMinion + " kez beslendi");
                fedCount++;
            }
        }
        
        if (fedCount > 0)
        {
            LogToConsole("[EntityFeed] Toplam " + fedCount + " minyon, her biri " + feedPerMinion + " kez beslendi!");
        }
        else
        {
            LogToConsole("[EntityFeed] Yakinlarda Armor Stand bulunamadi!");
        }
    }
}
