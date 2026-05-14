using GrandpaLegacyDialogue.Services;
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace GrandpaLegacyDialogue;

public class ModEntry : Mod
{
    private NpcDialogueService _npcDialogueService = null!;


    public override void Entry(IModHelper helper)
    {
        this._npcDialogueService = new NpcDialogueService(this.Monitor);
    }

    private void OnAssetRequested(object? sender, AssetRequestedEventArgs e)
    {
        this._npcDialogueService.OnAssetRequested(e);
    }

}

