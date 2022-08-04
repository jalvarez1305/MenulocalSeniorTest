using DevExpress.XtraBars;
using MenuLocalSeniorTest.Services;
using NanoFactura;
using Rest.Models;
using Rest.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MenuLocalSeniorTest
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        
        NearbyOperations nearbyOperations;
        Location location = new Location();
        FuncionesComunes fc;
        public MainForm()
        {
            InitializeComponent();
            fc = new FuncionesComunes();
            StartTimeTx.EditValue = DateTime.Today.AddHours(22);
            EndTimeTx.EditValue = DateTime.Today.AddHours(23);
        }

        private void FindBt_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<NearbyResult> barList;
            if (ValidateInputs())
            {
                location = new Location()
                {
                    lat = double.Parse(LatitudTx.EditValue.ToString()),
                    lng = double.Parse(LongitudTx.EditValue.ToString())
                };
                barList = new NearbySearch().GetBars(location);
                nearbyOperations = new NearbyOperations(barList);
                nearbyOperations.OrderList(location);
                nearbyOperations.GetTimeToArrive(location, (DateTime)StartTimeTx.EditValue);
                nearbyOperations.FilterReachableBarList((DateTime)EndTimeTx.EditValue);
                barListGrid.DataSource = nearbyOperations.ListOfBars;
            }
            else
            {
                /*do nothing*/
                MessageBox.Show("Intentemoslo de nuevo");
            }

        }

        private bool ValidateInputs()
        {
            /*make the inputs validations*/
            bool isValid = true;
            try
            {
                fc.ValidaTxtFloat(LatitudTx);
                fc.ValidaTxtFloat(LongitudTx);
                fc.ValidaDateEdit(StartTimeTx);
                fc.ValidaDateEdit(EndTimeTx);
                if ((DateTime)EndTimeTx.EditValue<=(DateTime)StartTimeTx.EditValue)
                {
                    MessageBox.Show("Selecciona otra hora de fin","No puedes terminar antes de lo que iniciaste");
                    isValid = false;
                }
                else
                {
                    /*do nothing*/
                }
                if (DateTime.Now>(DateTime)StartTimeTx.EditValue)
                {
                    MessageBox.Show("La ruta no puede comenzar en el pasado, elija un horario posterior");
                    isValid = false;
                }
            }
            catch (Exception)
            {
                isValid = false;
            }
            return isValid;
        }
    }
}