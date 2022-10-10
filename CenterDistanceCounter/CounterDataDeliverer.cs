using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;

namespace CenterDistanceCounter
{
    internal class CounterDataDeliverer: PersistentSingleton<CounterDataDeliverer>
    {
        private TMP_Text _leftText;
        private TMP_Text _RightCount;

        internal TMP_Text CounterLeft => _leftText;
        internal TMP_Text CounterRight => _RightCount;

        internal void SetCounterData(TMP_Text counterLeft, TMP_Text counterRight)
        {
            _leftText = counterLeft;
            _RightCount = counterRight;
        }
    }
}
