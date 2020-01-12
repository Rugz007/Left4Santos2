using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using System.Windows.Forms;
namespace Left4Santos2 
{
    public class Main : Script
    {
        Extender extender;
        Ped p;
        public Main()
        {
            this.KeyUp += OnKeyUp;
        }
        void OnKeyUp(object sender,KeyEventArgs e)
        {
            if(e.KeyCode == Keys.I)
            {
                extender.SetPopulation(0);
            }
            if(e.KeyCode == Keys.K)
            {
                extender.SetPopulation(5);
            }
        }
        public void CreateZombie()
        {

        }
        public void MakeEveryoneZombie()
        {
            bool lol = extender.CanHearPlayer(p);
        }
    }
}
