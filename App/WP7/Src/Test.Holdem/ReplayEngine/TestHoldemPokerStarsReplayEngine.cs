using System.IO;
using System.Linq;
using Common.Core.Configuration;
using Moq;

using NUnit.Framework;
using TexasHoldemCalculator.Core.Generator;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.Configuration;
using TexasHoldemCalculator.Interfaces.Generator;
using TexasHoldemCalculator.Interfaces.HandHistory;
using TexasHoldemCalculator.Interfaces.ReplayEngine;
using TexasHoldemCalculator.ReplayEngine;

namespace Test.Holdem.ReplayEngine
{
    [TestFixture]
    public class TestHoldemPokerStarsReplayEngine
    {
        public class HoldemTestHelper
        {
            public Mock<ICardGenerator> CardGenerator { get; private set; }
            public Mock<IPhoneConfiguration> Configuration { get; private set; }

            public HoldemTestHelper()
            {
                this.CardGenerator = new Mock<ICardGenerator>();
                this.Configuration = new Mock<IPhoneConfiguration>();
            }

            public PokerStarsReplayEngine GetDefaultEngine()
            {
                var service = 
                    new PokerStarsReplayEngine(
                        new CardGenerator(new NumberGenerator()), 
                        this.Configuration.Object);

                this.Configuration
                    .Setup(cfg => cfg.Get<string>(ConfigKey.View.Options.UserName))
                    .Returns("userName");

                return service;
            }
        }

        [TestFixture]
        public class GetHandReplay : HoldemTestHelper
        {
            private const string GameHeader = "PokerStars Game #20879084344: Tournament #111771991, 2000+110 Hold'em No Limit - Level I (10/20) - 2008/10/02 20:13:41 ET";
            private const string TableId = "Table '111771991 4' 9-max Seat #1 is the button";
            private const string HoleCards = "Dealt to mkrain4 [Ad Js]";
            private const string Flop = "*** FLOP *** [8d Jd 5c]";
            private const string Turn = "*** TURN *** [8d Jd 5c] [6s]";
            private const string River = "*** RIVER *** [8d Jd 5c 6s] [Ts]";
            private const string WonPot = "mkrain4 collected 3090 from pot";
            private const string TotalPot = "Total pot 3090 | Rake 0";
            private const string SummaryTag = "*** SUM";

            [Test]
            public void PokerStarsReplayEngineGetHandReplay()
            {
                var provider = new Mock<IReplayEngineProvider>();

                provider
                    .Setup(stream => stream.ReadableStream())
                    .Returns(new MemoryStream());
                
                var engine = base.GetDefaultEngine();

                Assert.IsNotNull(engine.GetHandReplay(provider.Object));
            }

            [Test]
            public void PokerStarsReplayEngineGetHandReplayHasValidLineWithNoGame()
            {
                var provider = new Mock<IReplayEngineProvider>();
                var memoryStream = new MemoryStream();
                var streamWriter = new StreamWriter(memoryStream) { AutoFlush = true};

                streamWriter.WriteLine("This is a valid line without game information no newline.");

                memoryStream.Seek(0, 0);

                provider
                    .Setup(stream => stream.ReadableStream())
                    .Returns(memoryStream);
                
                var engine = base.GetDefaultEngine();

                Assert.IsNotNull(engine.GetHandReplay(provider.Object));
            }

            [Test]
            public void PokerStarsReplayEngineGetHandReplayHasValidGameHeader()
            {
                var provider = new Mock<IReplayEngineProvider>();

                const string tableId = "20879084344";
                const string tournamentId = "111771991";

                var memoryStream = new MemoryStream();

                var streamWriter = new StreamWriter(memoryStream) { AutoFlush = true };
                streamWriter.WriteLine(GameHeader);
                streamWriter.WriteLine(SummaryTag);
                streamWriter.WriteLine("    ");

                memoryStream.Seek(0, 0);

                provider
                    .Setup(stream => stream.ReadableStream())
                    .Returns(memoryStream);
                
                var engine = base.GetDefaultEngine();

                var actual = engine.GetHandReplay(provider.Object).FirstOrDefault();

                Assert.IsNotNull(actual);

                Assert.AreEqual(tableId, actual.TableId);
                Assert.AreEqual(tournamentId, actual.TournamentId);
            }
        }

        [TestFixture]
        public class GetGameId : HoldemTestHelper
        {
            [Test]
            public void PokerStarsReplayEngineGetGameId()
            {
                var engine = base.GetDefaultEngine();

                const string gameString = "PokerStars Game #20882538122: Tournament #111797084, 10000+325 Hold'em No Limit - Level I (10/20) - 2008/10/02 22:47:07 ET";

                const string expected = "20882538122";

                Assert.AreEqual(expected, engine.GetGameId(gameString));
            }

            [Test]
            public void PokerStarsReplayEngineGetGameIdEmptString()
            {
                var engine = base.GetDefaultEngine();

                string gameString = string.Empty;

                string expected = string.Empty;

                Assert.AreEqual(expected, engine.GetGameId(gameString));
            }

            [Test]
            public void PokerStarsReplayEngineGetGameIdMissingTag()
            {
                var engine = base.GetDefaultEngine();

                const string gameString = "PokerStars Game 20882538122: Tournament #111797084, 10000+325 Hold'em No Limit - Level I (10/20) - 2008/10/02 22:47:07 ET";

                string expected = string.Empty;

                Assert.AreEqual(expected, engine.GetGameId(gameString));
            }

            [Test]
            public void PokerStarsReplayEngineGetGameIdMissingToken()
            {
                var engine = base.GetDefaultEngine();

                const string gameString = "PokerStars Game #20882538122 Tournament #111797084, 10000+325 Hold'em No Limit - Level I (10/20) - 2008/10/02 22.47.07 ET";

                string expected = string.Empty;

                Assert.AreEqual(expected, engine.GetGameId(gameString));
            }
        }

        [TestFixture]
        public class GetGameDescription : HoldemTestHelper
        {
            [Test]
            public void PokerStarsReplayEngineGetGameDescriptionRingGame()
            {
                var engine = base.GetDefaultEngine();

                const string gameString = "Game #21125116748:  Hold'em No Limit (5/10) - 2008/10/12 1:41:47 ET";

                const string expected = "Hold'em No Limit (5/10)";

                Assert.AreEqual(expected, engine.GetGameDescription(gameString));
            }

            [Test]
            public void PokerStarsReplayEngineGetGameDescriptionRingGameInvalidTokenPosition()
            {
                var engine = base.GetDefaultEngine();

                const string gameString = ": Game #21125116748:  Hold'em No Limit (5/10) - 2008/10/12 1:41:47 ET";

                string expected = string.Empty;

                Assert.AreEqual(expected, engine.GetGameDescription(gameString));
            }

            [Test]
            public void PokerStarsReplayEngineGetGameDescriptionTournamentGame()
            {
                var engine = base.GetDefaultEngine();

                const string gameString = "Tournament #113842050, 500+30 Hold'em No Limit - Level I (10/20) - 2008/10/12 22:14:50 ET";

                const string expected = "500+30 Hold'em No Limit - Level I (10/20)";

                Assert.AreEqual(expected, engine.GetGameDescription(gameString));
            }

            [Test]
            public void PokerStarsReplayEngineGetGameDescriptionTournamentGameMissingSeparator()
            {
                var engine = base.GetDefaultEngine();

                const string gameString = "Tournament #113842050, 500+30 Hold'em No Limit Level I (10/20) 2008/10/12 22:14:50 ET";

                string expected = string.Empty;

                Assert.AreEqual(expected, engine.GetGameDescription(gameString));
            }

            [Test]
            public void PokerStarsReplayEngineGetGameDescriptionEmptyString()
            {
                var engine = base.GetDefaultEngine();

                string gameString = string.Empty;

                string expected = string.Empty;

                Assert.AreEqual(expected, engine.GetGameDescription(gameString));
            }

            [Test]
            public void PokerStarsReplayEngineGetGameDescriptionMissingTags()
            {
                var engine = base.GetDefaultEngine();

                const string gameString = "PokerStars Game 21149350545: Tournament 113842050, 500+30 Hold'em No Limit - Level I (10/20) - 2008/10/12 22:14:50 ET";

                string expected = string.Empty;

                Assert.AreEqual(expected, engine.GetGameDescription(gameString));
            }

            [Test]
            public void PokerStarsReplayEngineGetGameDescriptionTournamentGameMissingDelimiter()
            {
                var engine = base.GetDefaultEngine();

                const string gameString = "PokerStars Game 21149350545: Tournament #113842050 500+30 Hold'em No Limit - Level I (10/20) - 2008/10/12 22:14:50 ET";

                string expected = string.Empty;

                Assert.AreEqual(expected, engine.GetGameDescription(gameString));
            }

            [Test]
            public void PokerStarsReplayEngineGetGameDescriptionMissingSeparator()
            {
                var engine = base.GetDefaultEngine();

                const string gameString = "PokerStars Game #21149350545: Tournament 113842050, 500+30 Hold'em No Limit  Level I (10/20)  2008/10/12 22:14:50 ET";

                string expected = string.Empty;

                Assert.AreEqual(expected, engine.GetGameDescription(gameString));
            }
        }

        [TestFixture]
        public class GetDateTime : HoldemTestHelper
        {
            [Test]
            public void PokerStarsReplayEngineGetDateTime()
            {
                var engine = base.GetDefaultEngine();

                const string gameString = "Game #21125116748:  Hold'em No Limit (5/10) - 2008/10/12 1:41:47 ET";

                const string expected = "2008/10/12 1:41:47 ET";

                Assert.AreEqual(expected, engine.GetDateTime(gameString));
            }

            [Test]
            public void PokerStarsReplayEngineGetDateTimeEmptyString()
            {
                var engine = base.GetDefaultEngine();

                string gameString = string.Empty;

                string expected = string.Empty;

                Assert.AreEqual(expected, engine.GetDateTime(gameString));
            }

            [Test]
            public void PokerStarsReplayEngineGetDateTimeMissingSeparator()
            {
                var engine = base.GetDefaultEngine();

                const string gameString = "Game #21125116748:  Hold'em No Limit (5/10) 2008/10/12 1:41:47 ET";

                string expected = string.Empty;

                Assert.AreEqual(expected, engine.GetDateTime(gameString));
            }
        }

        [TestFixture]
        public class SetHoleCards : HoldemTestHelper
        {
            [Test]
            public void PokerStarsReplayEngineSetHoleCards()
            {
                var engine = base.GetDefaultEngine();

                var history = new History();

                const string gameString = "[As Ks]";

                engine.SetHoleCards(history, gameString);

                Assert.IsNotNull(history.HoleCardOne);
                Assert.IsNotNull(history.HoleCardTwo);

                Assert.AreEqual(Suit.Spade, history.HoleCardOne.Suit);
                Assert.AreEqual(Suit.Spade, history.HoleCardTwo.Suit);
                Assert.AreEqual(CardName.Ace, history.HoleCardOne.Name);
                Assert.AreEqual(CardName.King, history.HoleCardTwo.Name);
            }

            [Test]
            public void PokerStarsReplayEngineSetHoleCardsEmptyString()
            {
                var history = new History();

                var engine = base.GetDefaultEngine();

                string gameString = string.Empty;

                engine.SetHoleCards(history, gameString);

                Assert.IsNull(history.HoleCardOne);
                Assert.IsNull(history.HoleCardTwo);
            }

            [Test]
            public void PokerStarsReplayEngineSetHoleCardsHoleCardMissing()
            {
                var history = new History();

                var engine = base.GetDefaultEngine();

                const string gameString = "[As]";

                engine.SetHoleCards(history, gameString);

                Assert.IsNull(history.HoleCardOne);
                Assert.IsNull(history.HoleCardTwo);
            }

            [Test]
            public void PokerStarsReplayEngineSetHoleCardsBothHoleCardsMissing()
            {
                var history = new History();

                var engine = base.GetDefaultEngine();

                const string gameString = "[ ]";

                engine.SetHoleCards(history, gameString);

                Assert.IsNull(history.HoleCardOne);
                Assert.IsNull(history.HoleCardTwo);
            }

            [Test]
            public void PokerStarsReplayEngineSetHoleCardsBothHoleCard1Missing()
            {
                var history = new History();

                var engine = base.GetDefaultEngine();

                const string gameString = "[ Ks]";

                engine.SetHoleCards(history, gameString);

                Assert.IsNull(history.HoleCardOne);
                Assert.IsNull(history.HoleCardTwo);
            }

            [Test]
            public void PokerStarsReplayEngineSetHoleCardsBothHoleCard2Missing()
            {
                var history = new History();

                var engine = base.GetDefaultEngine();

                const string gameString = "[As ]";

                engine.SetHoleCards(history, gameString);

                Assert.IsNull(history.HoleCardOne);
                Assert.IsNull(history.HoleCardTwo);
            }
        }

        [TestFixture]
        public class SetFlopCards : HoldemTestHelper
        {
            [Test]
            public void PokerStarsReplayEngineSetFlopCards()
            {
                var history = new History();

                var engine = base.GetDefaultEngine();

                const string gameString = "[As Ks Th]";

                engine.SetFlop(history, gameString);

                Assert.IsNotNull(history.FlopCardOne);
                Assert.IsNotNull(history.FlopCardTwo);
                Assert.IsNotNull(history.FlopCardThree);

                Assert.AreEqual(Suit.Spade, history.FlopCardOne.Suit);
                Assert.AreEqual(Suit.Spade, history.FlopCardTwo.Suit);
                Assert.AreEqual(Suit.Heart, history.FlopCardThree.Suit);
                Assert.AreEqual(CardName.Ace, history.FlopCardOne.Name);
                Assert.AreEqual(CardName.King, history.FlopCardTwo.Name);
                Assert.AreEqual(CardName.Ten, history.FlopCardThree.Name);
            }

            [Test]
            public void PokerStarsReplayEngineSetFlopCardsEmptyString()
            {
                var history = new History();

                var engine = base.GetDefaultEngine();

                string gameString = string.Empty;

                engine.SetFlop(history, gameString);

                Assert.IsNull(history.FlopCardOne);
                Assert.IsNull(history.FlopCardTwo);
                Assert.IsNull(history.FlopCardThree);

                Assert.IsNull(history.FlopCardOne);
                Assert.IsNull(history.FlopCardTwo);
                Assert.IsNull(history.FlopCardThree);
            }

            [Test]
            public void PokerStarsReplayEngineSetFlopCardsMissingSingleCard()
            {
                var history = new History();

                var engine = base.GetDefaultEngine();

                const string gameString = "[As Th]";

                engine.SetFlop(history, gameString);

                Assert.IsNull(history.FlopCardOne);
                Assert.IsNull(history.FlopCardTwo);
                Assert.IsNull(history.FlopCardThree);
            }

            [Test]
            public void PokerStarsReplayEngineSetFlopCardsFlop1EmptyString()
            {
                var history = new History();

                var engine = base.GetDefaultEngine();

                const string gameString = "[ As Th]";

                engine.SetFlop(history, gameString);

                Assert.IsNull(history.FlopCardOne);
                Assert.IsNull(history.FlopCardTwo);
                Assert.IsNull(history.FlopCardThree);
            }

            [Test]
            public void PokerStarsReplayEngineSetFlopCardsFlop2EmptyString()
            {
                var engine = base.GetDefaultEngine();

                var history = new History();

                const string gameString = "[As  Th]";

                engine.SetFlop(history, gameString);

                Assert.IsNull(history.FlopCardOne);
                Assert.IsNull(history.FlopCardTwo);
                Assert.IsNull(history.FlopCardThree);
            }

            [Test]
            public void PokerStarsReplayEngineSetFlopCardsFlop3EmptyString()
            {
                var history = new History();

                var engine = base.GetDefaultEngine();

                const string gameString = "[As Ks  ]";

                engine.SetFlop(history, gameString);

                Assert.IsNull(history.FlopCardOne);
                Assert.IsNull(history.FlopCardTwo);
                Assert.IsNull(history.FlopCardThree);
            }
        }

        [TestFixture]
        public class SetTurn : HoldemTestHelper
        {
            [Test]
            public void PokerStarsReplayEngineSetTurn()
            {
                var history = new History();

                var engine = base.GetDefaultEngine();

                const string gameString = "[As Th]";

                engine.SetTurn(history, gameString);

                Assert.IsNotNull(history.TurnCard);

                Assert.AreEqual(Suit.Spade, history.TurnCard.Suit);
                Assert.AreEqual(CardName.Ace, history.TurnCard.Name);
            }
        }

        [TestFixture]
        public class SetRiver : HoldemTestHelper
        {
            [Test]
            public void PokerStarsReplayEngineSetRiver()
            {
                var history = new History();

                var engine = base.GetDefaultEngine();

                const string gameString = "As [Th]";

                engine.SetRiver(history, gameString);

                Assert.IsNotNull(history.RiverCard);

                Assert.AreEqual(Suit.Heart, history.RiverCard.Suit);
                Assert.AreEqual(CardName.Ten, history.RiverCard.Name);
            }

            [Test]
            public void PokerStarsReplayEngineSetRiverInvalidToken()
            {
                var history = new History();

                var engine = base.GetDefaultEngine();

                const string gameString = "[";

                engine.SetRiver(history, gameString);

                Assert.IsNull(history.RiverCard);
            }

            [Test]
            public void PokerStarsReplayEngineSetRiverMissingCard()
            {
                var history = new History();

                var engine = base.GetDefaultEngine();

                const string gameString = "[]";

                engine.SetRiver(history, gameString);

                Assert.IsNull(history.RiverCard);
            }

            [Test]
            public void PokerStarsReplayEngineSetRiverEmptyString()
            {
                var history = new History();

                var engine = base.GetDefaultEngine();

                string gameString = string.Empty;

                engine.SetRiver(history, gameString);

                Assert.IsNull(history.RiverCard);
            }

            [Test]
            public void PokerStarsReplayEngineSetRiverHistoryNull()
            {
                var engine = base.GetDefaultEngine();

                History history = null;

                const string gameString = "[0]";

                engine.SetRiver(history, gameString);
            }

            [Test]
            public void PokerStarsReplayEngineSetRiverMissingDelimiter()
            {
                var history = new History();

                var engine = base.GetDefaultEngine();

                const string gameString = "As Th]";

                engine.SetRiver(history, gameString);

                Assert.IsNull(history.RiverCard);
            }
        }

        [TestFixture]
        public class GetWonPotAmount : HoldemTestHelper
        {
            [Test]
            public void PokerStarsReplayEngineGetWonPotAmount()
            {
                var engine = base.GetDefaultEngine();

                const string expected = "1000";
                const string username = "username";
                const string info = "username collected 1000";

                var result = engine.GetWonPotAmount(username, info);

                Assert.AreEqual(expected, result);
            }

            [Test]
            public void PokerStarsReplayEngineGetWonPotAmountUsernameEmpty()
            {
                var engine = base.GetDefaultEngine();

                string expected = string.Empty;
                const string username = null;
                const string info = "username collected 1000";

                var result = engine.GetWonPotAmount(username, info);

                Assert.AreEqual(expected, result);
            }

            [Test]
            public void PokerStarsReplayEngineGetWonPotAmountLineInfoEmpty()
            {
                var engine = base.GetDefaultEngine();

                string expected = string.Empty;
                const string username = "username";
                const string info = null;

                var result = engine.GetWonPotAmount(username, info);

                Assert.AreEqual(expected, result);
            }

            [Test]
            public void PokerStarsReplayEngineGetWonPotAmountUserNameNoInString()
            {
                var engine = base.GetDefaultEngine();

                string expected = string.Empty;
                const string username = "username";
                const string info = "john collected 1000";

                var result = engine.GetWonPotAmount(username, info);

                Assert.AreEqual(expected, result);
            }

            [Test]
            public void PokerStarsReplayEngineGetWonPotAmountWonPotAmountTagMissing()
            {
                var engine = base.GetDefaultEngine();

                string expected = string.Empty;
                const string username = "john";
                const string info = "john 1000";

                var result = engine.GetWonPotAmount(username, info);

                Assert.AreEqual(expected, result);
            }

            [Test]
            public void PokerStarsReplayEngineGetWonPotAmountMissing()
            {
                var engine = base.GetDefaultEngine();

                const string expected = "0";
                const string username = "j";
                const string info = "john collected ";

                var result = engine.GetWonPotAmount(username, info);

                Assert.AreEqual(expected, result);
            }
        }

        [TestFixture]
        public class GetTotalPotAmount : HoldemTestHelper
        {
            [Test]
            public void PokerStarsReplayEngineGetTotalPotAmount()
            {
                var engine = base.GetDefaultEngine();

                const string expected = "1000";
                const string info = "Total pot 1000|1000";

                var result = engine.GetTotalPotAmount(info);

                Assert.AreEqual(expected, result);
            }

            [Test]
            public void PokerStarsReplayEngineGetTotalPotAmountEmptyInfo()
            {
                var engine = base.GetDefaultEngine();

                string expected = string.Empty;
                string info = string.Empty;

                var result = engine.GetTotalPotAmount(info);

                Assert.AreEqual(expected, result);
            }

            [Test]
            public void PokerStarsReplayEngineGetTotalPotAmountMissingTag()
            {
                var engine = base.GetDefaultEngine();

                string expected = string.Empty;
                const string info = "Total 1000";

                var result = engine.GetTotalPotAmount(info);

                Assert.AreEqual(expected, result);
            }

            [Test]
            public void PokerStarsReplayEngineGetTotalPotAmountMissingAmount()
            {
                var engine = base.GetDefaultEngine();

                string expected = string.Empty;
                const string info = "Total pot |1000";

                var result = engine.GetTotalPotAmount(info);

                Assert.AreEqual(expected, result);
            }

            [Test]
            public void PokerStarsReplayEngineGetTotalPotAmountMissingAmountWithTag()
            {
                var engine = base.GetDefaultEngine();

                string expected = string.Empty;
                const string info = "Total pot  ";

                var result = engine.GetTotalPotAmount(info);

                Assert.AreEqual(expected, result);
            }
        }

        [TestFixture]
        public class GetShowdown : HoldemTestHelper
        {
            [Test]
            public void PokerStarsReplayEngineGetShowdown()
            {
                var engine = base.GetDefaultEngine();

                Assert.AreEqual(string.Empty, engine.GetShowdown("Fake"));
                Assert.AreEqual(string.Empty, engine.GetShowdown(null));
            }
        }

        [TestFixture]
        public class GetSummary : HoldemTestHelper
        {
            [Test]
            public void PokerStarsReplayEngineGetSummary()
            {
                var engine = base.GetDefaultEngine();

                Assert.AreEqual(string.Empty, engine.GetSummary("Fake"));
                Assert.AreEqual(string.Empty, engine.GetSummary(null));
            }
        }

        [TestFixture]
        public class GetFinalBoard : HoldemTestHelper
        {
            [Test]
            public void PokerStarsReplayEngineGetFinalBoard()
            {
                var engine = base.GetDefaultEngine();

                Assert.AreEqual(string.Empty, engine.GetFinalBoard("Fake"));
                Assert.AreEqual(string.Empty, engine.GetFinalBoard(null));
            }
        }

        [TestFixture]
        public class GetGameHeader : HoldemTestHelper
        {
            [Test]
            public void PokerStarsReplayEngineGetGameHeader()
            {
                var engine = base.GetDefaultEngine();

                Assert.AreEqual(string.Empty, engine.GetGameHeader("Fake"));
                Assert.AreEqual(string.Empty, engine.GetGameHeader(null));
            }
        }

        [TestFixture]
        public class GetTournamentId : HoldemTestHelper
        {
            [Test]
            public void PokerStarsReplayEngineGetTournamentId()
            {
                var engine = base.GetDefaultEngine();

                const string gameString = "PokerStars Game #20882538122: Tournament #111797084, 10000+325 Hold'em No Limit - Level I (10/20) - 2008/10/02 22:47:07 ET";

                const string expected = "111797084";

                Assert.AreEqual(expected, engine.GetTournamentId(gameString));
            }
        }

        [TestFixture]
        public class GetTableId : HoldemTestHelper
        {
            [Test]
            public void PokerStarsReplayEngineGetTableId()
            {
                var engine = base.GetDefaultEngine();

                const string gameString = "Table '111797084 1' 9-max Seat #1 is the button";

                const string expected = "111797084 1";

                Assert.AreEqual(expected, engine.GetTableId(gameString));
            }
        }

        [TestFixture]
        public class SetEngineReplayProvider : HoldemTestHelper
        {
            [Test]
            public void DoesNotThrow()
            {
                var replayProvider = new Mock<IReplayEngineProvider>();

                var engine = base.GetDefaultEngine();

                Assert.DoesNotThrow(() => engine.SetEngineProvider(replayProvider.Object));
            }
        }

        [TestFixture]
        public class DisplayName : HoldemTestHelper
        {
            [Test]
            public void ReturnsValidDisplayName()
            {
                var engine = base.GetDefaultEngine();

                const string expected = "Poker Stars";

                var actual = engine.DisplayName;

                Assert.AreEqual(expected, actual);
            }
        }

        [TestFixture]
        public class Provider : HoldemTestHelper
        {
            [Test]
            public void ReturnsSameProvider()
            {
                var replayProvider = new Mock<IReplayEngineProvider>();

                var engine = base.GetDefaultEngine();
                
                engine.SetEngineProvider(replayProvider.Object);

                var actual = engine.Provider;

                Assert.AreEqual(replayProvider.Object, actual);
            }

            [Test]
            public void ReturnsNullProvider()
            {
                var engine = base.GetDefaultEngine();

                var actual = engine.Provider;

                Assert.AreEqual(null, actual);
            }
        }
    }
}