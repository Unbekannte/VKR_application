using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VKR_FlangeCoupling.Details
{
    class Gayka_GOST_15521 : Gayka
    {
        /// <summary>
        /// Диаметр до скругления, не менее
        /// </summary>
        public double dw;

        GOST_Provider provider = null;
        public Gayka_GOST_15521()
        {

            try
            {
                provider = new GOST_Provider();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        public void Get_from_GOST(double m_nom, char shag_rezby = 'k')
        {
            try
            {
                provider = new GOST_Provider();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            string parameters = $"d = {m_nom}";

            string columns = $"d, t_{shag_rezby}, S, m, dw";

            DataTable dt1 = provider.get_rows("15521-70", "1", columns, parameters);

            d = double.Parse(dt1.Rows[0]["d"].ToString());
            t = double.Parse(dt1.Rows[0]["t_" + shag_rezby].ToString());
            m = double.Parse(dt1.Rows[0]["m"].ToString());
            S = double.Parse(dt1.Rows[0]["s"].ToString());
            dw = double.Parse(dt1.Rows[0]["dw"].ToString());


            //по ГОСТ 10549-80
            if (z == 0)
            {
                if (t >= 0.75 && t <= 1) { z = 1.0; }
                else if (t <= 1.75) { z = 1.6; }
                else if (t <= 2.00) { z = 2.0; }
                else if (t <= 3.00) { z = 2.5; }
                else if (t <= 4.00) { z = 3.0; }
                else { z = 4.0; }
            }
        }
    }
}
