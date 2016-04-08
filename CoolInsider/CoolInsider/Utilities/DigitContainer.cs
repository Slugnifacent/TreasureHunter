using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace TreasureHunter
{
    class DigitContainer<Number>
    {
        public Number value;
        public Number min;
        public Number max;
        public Number increment;

        public DigitContainer(Number Value, Number Min, Number Max) {
            value = Value;
            min = Min;
            max = Max;
        }


    }
}
