using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Licenta
{
    public partial class WidgetPanel : Form
    {
        Server server;
        public WidgetPanel()
        {
            InitializeComponent();
            server = new Server();
            server.MesajPrimit += Update;
        }

        private void VariableChoser_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void WidgetType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Add_Click(object sender, EventArgs e)
        {

        }

        public void AddVariable(string variableName)
        {
            VariableChoser.Items.Add(variableName);
        }
        private void Update(object sender, EventArgs e)
        {
            VariableChoser.Items.Add(server.GetVariableName());
        }
    }
}
