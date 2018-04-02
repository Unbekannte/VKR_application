using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VKR_FlangeCoupling.Details
{
    class Bolt_GOST_7817 : Bolt
    {
        /// <summary>
        /// Диаметр отверстия под шплинт
        /// </summary>
        public double d2;

        /// <summary>
        /// Расстоение от опрной части до отверстия d2
        /// </summary>
        public double l1;

        /// <summary>
        /// Длина гладкой части
        /// </summary>
        public double l2;


        GOST_Provider provider = null;
        public Bolt_GOST_7817()
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

        public void Get_from_GOST(double m_nom, double l_bolta, char shag_rezby = 'k')
        {
            string parameters = $"m = {m_nom}";

            string columns = $"m, t_{shag_rezby}, d, d1, s, k, R, d2";

            DataTable dt1 = provider.get_rows("7817-80", "1", columns, parameters);

            d = double.Parse(dt1.Rows[0]["d"].ToString());
            t = double.Parse(dt1.Rows[0]["t_" + shag_rezby].ToString());
            d1 = double.Parse(dt1.Rows[0]["d1"].ToString());
            S = double.Parse(dt1.Rows[0]["s"].ToString());
            k = double.Parse(dt1.Rows[0]["k"].ToString());
            R = double.Parse(dt1.Rows[0]["R"].ToString());
            d2 = double.Parse(dt1.Rows[0]["d2"].ToString());


            parameters = $"m = {m_nom}, l = {l_bolta}";

            columns = $"m, l, l1, l2";

            DataTable dt2 = provider.get_rows("7817-80", "2", columns, parameters);

            l = double.Parse(dt2.Rows[0]["l"].ToString());
            l1 = double.Parse(dt2.Rows[0]["l1"].ToString());
            l2 = double.Parse(dt2.Rows[0]["l2"].ToString());
            

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
