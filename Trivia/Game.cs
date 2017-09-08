using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private LinkedList<string> popQuestions = new LinkedList<string>();
        private LinkedList<string> scienceQuestions = new LinkedList<string>();
        private LinkedList<string> sportsQuestions = new LinkedList<string>();
        private LinkedList<string> rockQuestions = new LinkedList<string>();

        
        private bool isGettingOutOfPenaltyBox;
        private readonly Players _players;

        public Game()
        {
            for (int i = 0; i < 50; i++)
            {
                popQuestions.AddLast("Pop Question " + i);
                scienceQuestions.AddLast(("Science Question " + i));
                sportsQuestions.AddLast(("Sports Question " + i));
                rockQuestions.AddLast(createRockQuestion(i));
            }
            _players = new Players();
        }

        public String createRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool isPlayable()
        {
            return (howManyPlayers() >= 2);
        }

        public bool add(String playerName)
        {
            _players.players.Add(playerName);
            _players.places[howManyPlayers()] = 0;
            _players.purses[howManyPlayers()] = 0;
            _players.inPenaltyBox[howManyPlayers()] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + _players.players.Count);
            return true;
        }

        public int howManyPlayers()
        {
            return _players.players.Count;
        }

        public void roll(int roll)
        {
            Console.WriteLine(_players.players[_players.currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (_players.inPenaltyBox[_players.currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(_players.players[_players.currentPlayer] + " is getting out of the penalty box");
                    _players.places[_players.currentPlayer] = _players.places[_players.currentPlayer] + roll;
                    if (_players.places[_players.currentPlayer] > 11) _players.places[_players.currentPlayer] = _players.places[_players.currentPlayer] - 12;

                    Console.WriteLine(_players.players[_players.currentPlayer]
                            + "'s new location is "
                            + _players.places[_players.currentPlayer]);
                    Console.WriteLine("The category is " + currentCategory());
                    askQuestion();
                }
                else
                {
                    Console.WriteLine(_players.players[_players.currentPlayer] + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                _players.places[_players.currentPlayer] = _players.places[_players.currentPlayer] + roll;
                if (_players.places[_players.currentPlayer] > 11) _players.places[_players.currentPlayer] = _players.places[_players.currentPlayer] - 12;

                Console.WriteLine(_players.players[_players.currentPlayer]
                        + "'s new location is "
                        + _players.places[_players.currentPlayer]);
                Console.WriteLine("The category is " + currentCategory());
                askQuestion();
            }
        }

        private void askQuestion()
        {
            if (currentCategory() == "Pop")
            {
                Console.WriteLine(popQuestions.First());
                popQuestions.RemoveFirst();
            }
            if (currentCategory() == "Science")
            {
                Console.WriteLine(scienceQuestions.First());
                scienceQuestions.RemoveFirst();
            }
            if (currentCategory() == "Sports")
            {
                Console.WriteLine(sportsQuestions.First());
                sportsQuestions.RemoveFirst();
            }
            if (currentCategory() == "Rock")
            {
                Console.WriteLine(rockQuestions.First());
                rockQuestions.RemoveFirst();
            }
        }

        private String currentCategory()
        {
            if (_players.places[_players.currentPlayer] == 0) return "Pop";
            if (_players.places[_players.currentPlayer] == 4) return "Pop";
            if (_players.places[_players.currentPlayer] == 8) return "Pop";
            if (_players.places[_players.currentPlayer] == 1) return "Science";
            if (_players.places[_players.currentPlayer] == 5) return "Science";
            if (_players.places[_players.currentPlayer] == 9) return "Science";
            if (_players.places[_players.currentPlayer] == 2) return "Sports";
            if (_players.places[_players.currentPlayer] == 6) return "Sports";
            if (_players.places[_players.currentPlayer] == 10) return "Sports";
            return "Rock";
        }

        public bool wasCorrectlyAnswered()
        {
            if (_players.inPenaltyBox[_players.currentPlayer])
            {
                if (isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    _players.purses[_players.currentPlayer]++;
                    Console.WriteLine(_players.players[_players.currentPlayer]
                            + " now has "
                            + _players.purses[_players.currentPlayer]
                            + " Gold Coins.");

                    bool winner = didPlayerWin();
                    _players.currentPlayer++;
                    if (_players.currentPlayer == _players.players.Count) _players.currentPlayer = 0;

                    return winner;
                }
                else
                {
                    _players.currentPlayer++;
                    if (_players.currentPlayer == _players.players.Count) _players.currentPlayer = 0;
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Answer was correct!!!!");
                _players.purses[_players.currentPlayer]++;
                Console.WriteLine(_players.players[_players.currentPlayer]
                        + " now has "
                        + _players.purses[_players.currentPlayer]
                        + " Gold Coins.");

                bool winner = didPlayerWin();
                _players.currentPlayer++;
                if (_players.currentPlayer == _players.players.Count) _players.currentPlayer = 0;

                return winner;
            }
        }

        public bool wrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(_players.players[_players.currentPlayer] + " was sent to the penalty box");
            _players.inPenaltyBox[_players.currentPlayer] = true;

            _players.currentPlayer++;
            if (_players.currentPlayer == _players.players.Count) _players.currentPlayer = 0;
            return true;
        }

        private bool didPlayerWin()
        {
            return !(_players.purses[_players.currentPlayer] == 6);
        }
    }
}