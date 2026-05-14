using GrandpaLegacyDialogue.Services;
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace GrandpaLegacyDialogue;

public class ModEntry : Mod
{
    private NpcDialogueService _npcDialogueService = null!;


    public override void Entry(IModHelper helper)
    {
        _npcDialogueService = new NpcDialogueService(this.Monitor);
        helper.Events.Content.AssetRequested += OnAssetRequested;
    }

    private void OnAssetRequested(object? sender, AssetRequestedEventArgs e)
    {
        _npcDialogueService.OnAssetRequested(e);
    }

}

