using System.Collections.Generic;

namespace Game
{
    public class Dashita
    {
        List<Card> _runOne;
        List<Card> _runTwo;
        List<Card> _atama;

        public Dashita(List<Card> runOne, List<Card> runTwo, List<Card> atama)
        {
            _runOne = runOne;
            _runTwo = runTwo;
            _atama = atama;
        }

        public List<Card> RunOne => _runOne;

        public List<Card> RunTwo => _runTwo;

        public List<Card> Atama => _atama;
    }
}