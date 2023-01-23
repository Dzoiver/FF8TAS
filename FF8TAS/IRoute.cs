using System;
using System.Collections.Generic;
using System.Text;

namespace FF8TAS
{
    interface IRoute
    {
        public void BalambGarden();
        public void FireCavern();
        public void HandleTextboxes(int id = 0);
        public void ValveCheck();
    }
}
