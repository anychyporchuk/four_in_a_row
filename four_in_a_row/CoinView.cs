using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace four_in_a_row
{
    public partial class CoinView : UserControl
    {
        public CoinView()
        {
            InitializeComponent();
            this.Width = Constants.SIZE;
        }
    }
}
