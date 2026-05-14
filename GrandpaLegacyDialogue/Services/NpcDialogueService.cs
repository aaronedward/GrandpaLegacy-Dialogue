using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace GrandpaLegacyDialogue.Services;

internal sealed class NpcDialogueService(IMonitor monitor)
{
    private static readonly Dictionary<string, Dictionary<string, string>> DialogueByAsset = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Characters/Dialogue/Abigail"] = BuildLines(
            "I found one of your grandpa's old notes by the mountain trail. He marked every hidden shortcut.",
            "The old Community Center roof was a disaster. Your grandpa fixed it before it caved in.",
            "When I practice with my sword, I think about how your grandpa protected this town.",
            "People act like bravery is dramatic. Sometimes it's just showing up every day, like your grandpa did.",
            "If I ever go on a real quest, I want to come home and help the valley the way your grandpa did."
        ),
        ["Characters/Dialogue/Alex"] = BuildLines(
            "Your grandpa trained himself like an athlete: every day, no excuses.",
            "Grandpa George says your grandpa rebuilt this town with his own hands. That's legit.",
            "Talent is great, but discipline wins. Your grandpa proved it.",
            "When training gets rough, I think about Your grandpa working through bad weather.",
            "I still want to go pro, but I want to leave something real behind too."
        ),
        ["Characters/Dialogue/Caroline"] = BuildLines(
            "Before your grandpa restored the Community Center, I worried Pelican Town was losing its heart.",
            "Your grandpa would stop by and ask what families needed most. He listened.",
            "Your grandpa's kindness still lingers here, like a warm cup of tea on a rainy day.",
            "Even on cold mornings, your grandpa kept everyone's spirits up while he worked.",
            "You have Your grandpa's steadiness, @. The valley is lucky for it."
        ),
        ["Characters/Dialogue/Clint"] = BuildLines(
            "Your grandpa ordered heavy hinges and proper steel plates for that building. Good call.",
            "Your grandpa paid fair, and he always thanked me for rush jobs.",
            "A lot of my best work is hidden in beams and bolts your grandpa asked for.",
            "Most folks only see finished walls. Your grandpa noticed the craft under them.",
            "When I doubt my work, I remember your grandpa trusted me with the town."
        ),
        ["Characters/Dialogue/Demetrius"] = BuildLines(
            "From a systems perspective, your grandpa's renovations stabilized the town's social hub.",
            "Your grandpa asked practical questions before every project. Very methodical.",
            "Long-term community health indicators improved after Your grandpa's work.",
            "Your grandpa prioritized structural integrity over short-term convenience.",
            "I respect people who convert planning into durable outcomes."
        ),
        ["Characters/Dialogue/Dwarf"] = BuildLines(
            "Surface elder called Grandpa traded ore and stories with mountain folk.",
            "Grandpa mended bundle hall. Forest spirits still whisper.",
            "When paths cracked, Grandpa brought stone and iron. Good craft.",
            "Cold season did not stop Grandpa. Strong fire-heart.",
            "You carry same spark. Valley remembers."
        ),
        ["Characters/Dialogue/Elliott"] = BuildLines(
            "Your grandpa gave this valley a second act, and it was beautifully composed.",
            "I admire the poetry of Your grandpa's work: timber, stone, and hope in equal measure.",
            "When sea fog rolls in, I think of your grandpa building clarity from uncertainty.",
            "A true legacy is warmth left for others. Your grandpa understood that perfectly.",
            "If I write my masterpiece, a trace of your grandpa may find its way into the hero."
        ),
        ["Characters/Dialogue/Emily"] = BuildLines(
            "I can still feel your grandpa's bright energy around the Community Center.",
            "Your grandpa repaired more than walls. He helped people reconnect.",
            "Some people leave clutter behind. He left harmony.",
            "When the valley feels quiet, I imagine your grandpa's spirit cheering us on.",
            "You carry that same generous light, @."
        ),
        ["Characters/Dialogue/Evelyn"] = BuildLines(
            "Oh, your grandpa was such a thoughtful man.",
            "I remember your grandpa hauling lumber to the Community Center in summer heat.",
            "Your grandpa used to say a town needs a place where neighbors feel welcome.",
            "One snowy evening your grandpa brought me firewood because he noticed I was low.",
            "Seeing you reminds me that kindness can be inherited."
        ),
        ["Characters/Dialogue/George"] = BuildLines(
            "Your grandpa wasn't all talk. He fixed what needed fixing.",
            "I watched your grandpa patch that old Community Center. Tough old bird.",
            "Town would've slid downhill without him. That's a fact.",
            "Your grandpa never bragged, either. Just did the job.",
            "You've got some of Your grandpa's grit."
        ),
        ["Characters/Dialogue/Gus"] = BuildLines(
            "Your grandpa used to buy meals for volunteers during the renovation.",
            "Your grandpa said good food keeps good work going. Smart man.",
            "After long days, he'd stop in and ask how everyone was holding up.",
            "The reopening crowd at the Community Center was one of my favorite nights.",
            "Any friend of your grandpa is family at the saloon, @."
        ),
        ["Characters/Dialogue/Haley"] = BuildLines(
            "I used to think old buildings were just ugly. Your grandpa changed that.",
            "Your grandpa made the Community Center feel alive again, and that matters.",
            "Grandma says he helped people without making a big show of it.",
            "I respect that now. Quiet effort is actually really impressive.",
            "When I do charity shoots, I try to bring that same energy."
        ),
        ["Characters/Dialogue/Harvey"] = BuildLines(
            "Your grandpa's work reduced stress across town. That has real health benefits.",
            "Grandpa checked in on older residents while he was out repairing things.",
            "Preventive care isn't only medicine. Safe spaces and social ties matter too.",
            "Even in poor weather, grandpa kept good safety habits.",
            "Pelican Town is healthier because of what he built."
        ),
        ["Characters/Dialogue/Jas"] = BuildLines(
            "Aunt Marnie says your grandpa fixed things so everyone could gather again.",
            "I like the Community Center. It feels cozy.",
            "Sometimes I leave flowers there for your grandpa.",
            "Do you think he can see the town from the stars?",
            "When I grow up, I want to help people like your grandpa did."
        ),
        ["Characters/Dialogue/Jodi"] = BuildLines(
            "Your grandpa understood that families need stable places to gather.",
            "When your grandpa repaired the Community Center, it felt like hope came back.",
            "Grandpa always asked if the kids were doing okay.",
            "Your grandpa reminded us that rebuilding a town means rebuilding trust.",
            "You honor your grandpa every time you look out for your neighbors, @."
        ),
        ["Characters/Dialogue/Kent"] = BuildLines(
            "Your grandpa kept this town anchored.",
            "I respect how your grandpa took responsibility without making noise.",
            "Strong communities are built on routine work, not slogans.",
            "After what I've seen, I value people who build.",
            "Your grandpa's work gave Pelican Town something worth protecting."
        ),
        ["Characters/Dialogue/Krobus"] = BuildLines(
            "I feel gentle light around the old hall. Your grandpa left kind traces.",
            "The forest spirits trusted him. That is meaningful.",
            "Many humans ignore broken things. Your grandpa did not.",
            "In winter darkness, the town still feels warmer because of him.",
            "Legacy is a quiet shadow that shelters others."
        ),
        ["Characters/Dialogue/Leah"] = BuildLines(
            "Your grandpa treated restoration like art: keep the character, strengthen the weak points.",
            "I love that your grandpa used local materials and respected the valley's style.",
            "Your grandpa proved practical work can still be beautiful.",
            "When I carve wood, I think about your grandpa's patience.",
            "Your grandpa made Pelican Town feel handmade again."
        ),
        ["Characters/Dialogue/Leo"] = BuildLines(
            "Linus said your grandpa fixed the big house where everyone meets.",
            "On the island, we repair things together. Your grandpa understood that.",
            "I like people who take care of places. Your grandpa did.",
            "The valley feels safe. Maybe your grandpa helped make it feel that way.",
            "I want to help this home too."
        ),
        ["Characters/Dialogue/Lewis"] = BuildLines(
            "As mayor, I can say your grandpa's work spared Pelican Town years of decline.",
            "Your grandpa coordinated volunteers with remarkable efficiency.",
            "Restoring the Community Center restored civic pride.",
            "Your grandpa never asked for medals or speeches. He simply served.",
            "I still reference your grandpa's project notes during town planning."
        ),
        ["Characters/Dialogue/Linus"] = BuildLines(
            "Your grandpa shared food with me without judgment.",
            "Your grandpa repaired town buildings and still respected the wild places.",
            "A kind person leaves the land better than they found it.",
            "On cold nights, your grandpa checked that I had enough wood.",
            "You carry your grandpa's spirit when you choose compassion over comfort."
        ),
        ["Characters/Dialogue/Marnie"] = BuildLines(
            "Your grandpa once patched my barn fence when I was swamped at the ranch.",
            "Your grandpa said a healthy town needs healthy animals and healthy people.",
            "When the Community Center reopened, folks stood a little taller.",
            "Your grandpa always carried extra twine, just in case someone needed help.",
            "Your grandpa had a big heart, @. That's plain truth."
        ),
        ["Characters/Dialogue/Maru"] = BuildLines(
            "Your grandpa loved practical engineering. He always asked how long a fix would last.",
            "I studied your grandpa's reinforcement choices in the Community Center. Efficient design.",
            "Your grandpa balanced budget, materials, and durability really well.",
            "Your grandpa even planned for maintenance, which people often forget.",
            "Your grandpa's restoration is still one of the valley's best case studies."
        ),
        ["Characters/Dialogue/Pam"] = BuildLines(
            "Your grandpa put this town back on its feet when folks were running outta hope.",
            "Your grandpa never looked down on me. Just checked in.",
            "That Community Center was a wreck. He changed that.",
            "Your grandpa worked through weather that would've sent most people home.",
            "Your grandpa reminded this town that second chances are real."
        ),
        ["Characters/Dialogue/Penny"] = BuildLines(
            "Your grandpa gave the children of this town a better future by rebuilding shared spaces.",
            "I sometimes hold lessons near the Community Center. It's inspiring.",
            "Your grandpa believed education and community belong together.",
            "Your grandpa treated everyone with dignity, no matter their station.",
            "When I teach, I try to reflect that same care."
        ),
        ["Characters/Dialogue/Pierre"] = BuildLines(
            "Your grandpa's projects brought people back into town. That helped every local shop.",
            "Your grandpa sourced supplies locally whenever possible. I respected that.",
            "Pelican Town needed confidence, and he delivered it board by board.",
            "Your grandpa understood what real community investment looks like.",
            "Your grandpa's work still keeps life moving through our streets."
        ),
        ["Characters/Dialogue/Robin"] = BuildLines(
            "Your grandpa had a sharp eye for structure. He spotted trouble early.",
            "We traded notes on beams, joints, and weatherproofing during that job.",
            "Your grandpa cared about doing it right, not doing it quick.",
            "Any carpenter would respect your grandpa's standards.",
            "Honestly, your grandpa left one of the best restoration references in the valley."
        ),
        ["Characters/Dialogue/Sam"] = BuildLines(
            "Your grandpa said a town needs music and hangout spots, not just roads.",
            "He'd listen to us practice and say, keep at it, kids.",
            "The Community Center sounds way better now because of your grandpa's repairs.",
            "Your grandpa made Pelican Town feel less stuck.",
            "When we play local shows, I think he'd dig it."
        ),
        ["Characters/Dialogue/Sandy"] = BuildLines(
            "Travelers from Pelican Town still mention your grandpa's renovation work.",
            "A thriving town sends more friendly faces out to the desert.",
            "Your grandpa may have lived far from Calico, but your grandpa's reputation reached me.",
            "You can feel when a place is cared for. Your grandpa gave that to the valley.",
            "Next time I visit, I want to leave flowers at the Community Center."
        ),
        ["Characters/Dialogue/Sebastian"] = BuildLines(
            "I usually tune out civic pride stuff, but your grandpa actually earned it.",
            "Your grandpa fixed infrastructure instead of waiting for someone else.",
            "That old Community Center would've collapsed eventually without him.",
            "I respect people who solve problems quietly.",
            "This town still runs on work your grandpa did years ago."
        ),
        ["Characters/Dialogue/Shane"] = BuildLines(
            "Your grandpa kept showing up every morning. That's harder than it sounds.",
            "That kind of consistency made some of us rethink our own habits.",
            "Your grandpa didn't preach. He just did useful work.",
            "A solid Community Center gave people somewhere decent to gather.",
            "When I'm trying to stay on track, I think about that."
        ),
        ["Characters/Dialogue/Vincent"] = BuildLines(
            "Mom says your grandpa was super strong and fixed the whole town!",
            "I like the big room in the Community Center. It feels important.",
            "Can I be a builder hero like your grandpa someday?",
            "Teacher says helping neighbors is brave. Your grandpa was brave.",
            "I drew your grandpa with a giant hammer!"
        ),
        ["Characters/Dialogue/Willy"] = BuildLines(
            "Your grandpa knew a harbor town needs more than boats. It needs folks pulling together.",
            "Your grandpa repaired dock crates and town beams with the same steady hands.",
            "A good legacy's like a tight knot. Holds fast when weather turns.",
            "In storm season, I appreciate every solid board he left behind.",
            "Aye, Pelican Town still sails smoother because of your grandpa."
        ),
        ["Characters/Dialogue/Wizard"] = BuildLines(
            "Your grandfather's will was unusually resolute. Even the forest spirits took notice.",
            "When he restored the old hall, dormant communal currents stirred once more.",
            "Your grandpa wielded no formal arcana, yet your grandpa's intent shaped this valley profoundly.",
            "Legacy is a spell written in deeds instead of runes.",
            "You carry your grandpa's resonance, @. The valley answers it."
        )
    };

    private static readonly Dictionary<string, Dictionary<string, string>> AdditionalDialogueByAsset = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Characters/Dialogue/Abigail"] = BuildExtraLines(
            "I heard your grandpa cleared the mountain road after a storm so everyone could pass.",
            "Your grandpa was never scared of weird places. I respect that.",
            "Sometimes I sit in the Community Center and picture what it looked like before he fixed it.",
            "Your grandpa turned a creepy ruin into a place people actually love.",
            "If we map the valley's hidden paths, we should name one after him."
        ),
        ["Characters/Dialogue/Alex"] = BuildExtraLines(
            "Your grandpa had serious endurance. That's pro-level work ethic.",
            "Grandpa George says your grandpa almost never missed a day of work.",
            "Your grandpa built team spirit without making a big speech about it.",
            "A town is only as strong as the people who put in the reps.",
            "I want people to remember me the way folks remember your grandpa."
        ),
        ["Characters/Dialogue/Caroline"] = BuildExtraLines(
            "Your grandpa remembered little details about everyone's families.",
            "When supplies ran low, he helped quietly so no one felt ashamed.",
            "Your grandpa believed a healthy town starts with neighbors knowing each other.",
            "Even now, the Community Center feels like your grandpa's welcome is still there.",
            "I'm glad you're carrying that legacy forward, @."
        ),
        ["Characters/Dialogue/Clint"] = BuildExtraLines(
            "Your grandpa requested quality materials, not flashy ones. That's a builder's mindset.",
            "If a bracket looked weak, he'd ask to redo it. No shortcuts.",
            "Your grandpa respected tradespeople. That mattered more than he knew.",
            "Some of my proudest pieces are still holding that place together.",
            "I wish more customers were like him."
        ),
        ["Characters/Dialogue/Demetrius"] = BuildExtraLines(
            "Your grandpa's project sequencing reduced waste and rework.",
            "Your grandpa understood maintenance planning, not just first-pass repairs.",
            "Community spaces improve resilience during high-stress periods.",
            "Your grandpa combined craftsmanship with long-horizon thinking.",
            "Pelican Town is still benefiting from your grandpa's optimization choices."
        ),
        ["Characters/Dialogue/Dwarf"] = BuildExtraLines(
            "Grandpa listened to stone. Stone remembers.",
            "Your grandpa traded fair. No trick words.",
            "Big hall strong because Grandpa honored old bones of earth.",
            "Forest sprites carried your grandpa luck for kind deeds.",
            "Your clan name is spoken with respect underground."
        ),
        ["Characters/Dialogue/Elliott"] = BuildExtraLines(
            "Your grandpa restored not merely a building, but a stage for human connection.",
            "There is romance in a hammer wielded for others.",
            "The valley's mood changed after your grandpa's work, as if a dissonant chord resolved.",
            "Greatness is often devotion made visible.",
            "Your family story would make a magnificent epic."
        ),
        ["Characters/Dialogue/Emily"] = BuildExtraLines(
            "When I meditate in town, I can feel your grandpa's grounding energy.",
            "Your grandpa made space for people to gather, and that raises everyone's vibration.",
            "Some repairs are physical, others are emotional. Your grandpa did both.",
            "I love that your grandpa's legacy feels practical and magical at once.",
            "Your grandpa's kindness still sparkles around this valley."
        ),
        ["Characters/Dialogue/Evelyn"] = BuildExtraLines(
            "Your grandpa used to carry extra nails in his pocket, just in case.",
            "I brought your grandpa tea sometimes, and your grandpa always thanked me with a smile.",
            "Your grandpa said a repaired bench is an invitation to rest.",
            "I think your grandpa understood loneliness, and wanted no one to feel it.",
            "Dear, your grandpa would be so proud to see you here."
        ),
        ["Characters/Dialogue/George"] = BuildExtraLines(
            "Your grandpa would argue with me sometimes, then fix the thing anyway.",
            "Your grandpa didn't mind hard jobs. Didn't complain much.",
            "I trusted your grandpa's work. That's saying something.",
            "Town's softer now, but it stands on your grandpa's backbone.",
            "Take care of that legacy, kid."
        ),
        ["Characters/Dialogue/Gus"] = BuildExtraLines(
            "Your grandpa would feed volunteers before he fed himself.",
            "On busy days, he'd ask me to keep meals affordable for workers.",
            "Your grandpa knew how to build morale.",
            "Whenever we host town events, I still feel your grandpa's influence.",
            "One good meal and one good deed, that was your grandpa's style."
        ),
        ["Characters/Dialogue/Haley"] = BuildExtraLines(
            "I used to roll my eyes at town projects. Not anymore.",
            "Your grandpa made improvement look... kind of elegant.",
            "Your grandpa showed people that helping out can actually be cool.",
            "The restored Center makes gorgeous photos, honestly.",
            "I like the idea of leaving a place prettier than you found it."
        ),
        ["Characters/Dialogue/Harvey"] = BuildExtraLines(
            "Your grandpa seemed to understand that social isolation is a health risk.",
            "After the restoration, I saw better moods all over town.",
            "Your grandpa often asked whether older residents needed supplies delivered.",
            "I admire how carefully your grandpa paced his workload.",
            "Your grandpa practiced civic care the way I try to practice medicine."
        ),
        ["Characters/Dialogue/Jas"] = BuildExtraLines(
            "Aunt Marnie says your grandpa fixed things with really strong tools.",
            "I like drawing the Community Center because it looks happy now.",
            "Maybe your grandpa liked flowers too.",
            "I say thanks to your grandpa when I pass the big hall.",
            "Can we clean up town places together sometime?"
        ),
        ["Characters/Dialogue/Jodi"] = BuildExtraLines(
            "When things felt uncertain, your grandpa made them feel manageable.",
            "Your grandpa reminded parents that kids notice how adults treat each other.",
            "I never forgot how respectfully he spoke to everyone.",
            "That renovation gave families a place to breathe.",
            "Your grandpa helped build routines that made life feel normal again."
        ),
        ["Characters/Dialogue/Kent"] = BuildExtraLines(
            "Your grandpa handled responsibility the way good soldiers do: quietly and consistently.",
            "There was discipline in every part of your grandpa's work.",
            "Your grandpa helped this town feel secure.",
            "A repaired town gives people something stable to come home to.",
            "I respect your grandpa's service to this community."
        ),
        ["Characters/Dialogue/Krobus"] = BuildExtraLines(
            "Your grandpa moved through town with calm purpose. Shadows noticed.",
            "Kind humans are rare. Your grandpa was one.",
            "The old hall still holds a gentle memory of him.",
            "Your grandpa mended cracks above ground and eased fears below.",
            "I think your grandpa understood that all beings need belonging."
        ),
        ["Characters/Dialogue/Leah"] = BuildExtraLines(
            "Your grandpa respected old materials instead of replacing everything.",
            "You can feel your grandpa's hand in the proportions of that building.",
            "Your grandpa understood that restoration is a conversation with the past.",
            "I admire how he balanced utility with beauty.",
            "Your grandpa's work makes me want to create pieces worthy of this valley."
        ),
        ["Characters/Dialogue/Leo"] = BuildExtraLines(
            "On Ginger Island, we fix nests and paths together. Your grandpa felt like that.",
            "Linus says your grandpa was gentle with animals too.",
            "Big places can feel scary, but the Community Center feels friendly.",
            "Your grandpa made home bigger for everyone.",
            "I want to learn tools and help like your grandpa did."
        ),
        ["Characters/Dialogue/Lewis"] = BuildExtraLines(
            "Your grandpa documented expenses carefully, which kept every project accountable.",
            "Town volunteers trusted him because your grandpa worked beside them.",
            "Your grandpa's renovation timeline was ambitious and remarkably disciplined.",
            "Public service is often thankless; your grandpa never complained.",
            "Pelican Town owes your grandpa a debt we repay by caring for each other."
        ),
        ["Characters/Dialogue/Linus"] = BuildExtraLines(
            "Your grandpa never asked me to be someone else.",
            "Your grandpa shared warmth when winter was harsh.",
            "Your grandpa repaired what was broken without judging how it broke.",
            "The valley remembers hands that heal.",
            "I think he would be happy seeing you live simply and kindly."
        ),
        ["Characters/Dialogue/Marnie"] = BuildExtraLines(
            "He'd check my gates and latches whenever storms came through.",
            "Your grandpa always asked after the animals before asking after himself.",
            "Folks trusted him because your grandpa treated everyone fair.",
            "Your grandpa knew a town runs on small acts of care.",
            "I still miss seeing your grandpa walk up the ranch road."
        ),
        ["Characters/Dialogue/Maru"] = BuildExtraLines(
            "Your grandpa favored robust joints and sensible load paths.",
            "I love how your grandpa designed repairs to stay serviceable later.",
            "Your grandpa anticipated wear patterns really well.",
            "That project is basically a field lesson in practical engineering.",
            "If I publish a paper someday, I might cite your grandpa's methods."
        ),
        ["Characters/Dialogue/Pam"] = BuildExtraLines(
            "He'd wave at me even on mornings I wasn't my best.",
            "Some people talk about fixing town. Your grandpa actually did it.",
            "Your grandpa made that old place usable again, plain and simple.",
            "Seeing your grandpa's work reminds me to keep moving forward.",
            "Yeah... Pelican Town was lucky to have him."
        ),
        ["Characters/Dialogue/Penny"] = BuildExtraLines(
            "Your grandpa gave children a better place to learn and gather.",
            "Your grandpa treated every person as worthy of attention.",
            "The Community Center feels like a promise kept.",
            "I tell the children that service can change a whole town.",
            "Your grandpa's example helps me stay hopeful."
        ),
        ["Characters/Dialogue/Pierre"] = BuildExtraLines(
            "He'd compare prices, sure, but your grandpa never squeezed local suppliers unfairly.",
            "Your grandpa's projects kept business in town where it belonged.",
            "Your grandpa understood that strong communities support local trade.",
            "I respected how decisively your grandpa bought what was needed.",
            "Your grandpa's work still brings customers into the square."
        ),
        ["Characters/Dialogue/Robin"] = BuildExtraLines(
            "Your grandpa had good instincts for where moisture would cause damage.",
            "During the renovation, your grandpa was meticulous about measurements.",
            "Your grandpa listened when I explained why a detail mattered.",
            "Not many clients care about longevity as much as your grandpa did.",
            "Your grandpa left behind work any carpenter would sign proudly."
        ),
        ["Characters/Dialogue/Sam"] = BuildExtraLines(
            "Your grandpa said music nights at the Center kept people connected.",
            "Sometimes he'd tap along when we practiced.",
            "Your grandpa made space for community stuff, not just chores.",
            "The place has better vibes because of what your grandpa built.",
            "I like that your grandpa's legacy feels alive, not old."
        ),
        ["Characters/Dialogue/Sandy"] = BuildExtraLines(
            "Folks from Pelican Town speak of your grandpa with real affection.",
            "A cared-for town sends out happier travelers.",
            "Your grandpa proved generosity travels farther than distance.",
            "I love hearing stories about people who build community.",
            "Your valley shines because he invested in it."
        ),
        ["Characters/Dialogue/Sebastian"] = BuildExtraLines(
            "Your grandpa didn't wait for perfect conditions. He just got it done.",
            "Most people avoid hard maintenance work. He leaned into it.",
            "Your grandpa left infrastructure better than he found it. Rare.",
            "I like people who do real work without chasing credit.",
            "Your grandpa was the opposite of performative."
        ),
        ["Characters/Dialogue/Shane"] = BuildExtraLines(
            "Your grandpa showed up steady, even when nobody was watching.",
            "That kind of consistency is harder than people admit.",
            "Your grandpa gave folks a place to gather without feeling judged.",
            "I respect people who help first and talk later.",
            "When days get rough, I try to remember that example."
        ),
        ["Characters/Dialogue/Vincent"] = BuildExtraLines(
            "Did your grandpa really carry giant boards all by himself?",
            "I like pretending the Community Center is a hero castle.",
            "Teacher says your grandpa helped everyone, not just friends.",
            "I wanna build something important someday too!",
            "Can we draw town plans together?"
        ),
        ["Characters/Dialogue/Willy"] = BuildExtraLines(
            "Your grandpa knew weatherproofing better than many sailors.",
            "Aye, your grandpa fixed things proper, so they'd last through storms.",
            "Your grandpa respected the sea and the town alike.",
            "Good timber, good knots, good sense. That's your grandpa.",
            "Every season I spot your grandpa's handiwork and give a quiet thanks."
        ),
        ["Characters/Dialogue/Wizard"] = BuildExtraLines(
            "Your grandpa's intentions were clear enough to resonate in the woods.",
            "The junimos regarded tour grandpa's labor with uncommon favor.",
            "Your grandpa restored balance in mundane form, which is a subtle art.",
            "Few understand that devotion can alter a place's essence.",
            "Your grandpa's legacy remains woven into Pelican Town's living aura."
        ),
    };

    private readonly IMonitor _monitor = monitor;

    public void OnAssetRequested(AssetRequestedEventArgs e)
    {
        foreach ((string assetName, Dictionary<string, string> lines) in DialogueByAsset)
        {
            if (!e.NameWithoutLocale.IsEquivalentTo(assetName))
                continue;

            e.Edit(asset =>
            {
                IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;
                int addedCount = AddLines(data, lines);
                int totalLines = lines.Count;

                if (AdditionalDialogueByAsset.TryGetValue(assetName, out Dictionary<string, string>? extraLines))
                {
                    addedCount += AddLines(data, extraLines);
                    totalLines += extraLines.Count;
                }

                this._monitor.Log($"Dialogue patch for {assetName}: added {addedCount}/{totalLines} Grandpa legacy lines.");
            });
            return;
        }
    }

    private static Dictionary<string, string> BuildLines(
        string springYearOne,
        string summerYearOne,
        string fallYearOne,
        string winterYearOne,
        string tuesdayLaterYears
    )
    {
        return new Dictionary<string, string>
        {
            ["spring_7_1"] = springYearOne,
            ["summer_11_1"] = summerYearOne,
            ["fall_17_1"] = fallYearOne,
            ["winter_21_1"] = winterYearOne,
            ["Tue_2"] = tuesdayLaterYears
        };
    }

    private static Dictionary<string, string> BuildExtraLines(
        string springYearOne,
        string summerYearOne,
        string fallYearOne,
        string winterYearOne,
        string fridayLaterYears
    )
    {
        return new Dictionary<string, string>
        {
            ["spring_23_1"] = springYearOne,
            ["summer_25_1"] = summerYearOne,
            ["fall_3_1"] = fallYearOne,
            ["winter_9_1"] = winterYearOne,
            ["Fri_2"] = fridayLaterYears
        };
    }

    private static int AddLines(IDictionary<string, string> data, Dictionary<string, string> lines)
    {
        int addedCount = 0;
        foreach ((string key, string line) in lines)
        {
            if (data.TryAdd(key, line))
                addedCount++;
        }

        return addedCount;
    }
}
