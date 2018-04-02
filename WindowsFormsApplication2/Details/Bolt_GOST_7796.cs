using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VKR_FlangeCoupling.Details
{
    class Bolt_GOST_7796 : Bolt
    {


        GOST_Provider provider = null;
        public Bolt_GOST_7796()
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

        /// <summary>
        /// Выбрать шайбу из ГОСТа. Шаг резьбы 'm'/'k' - мелкий/крупный
        /// </summary>
        public void Get_from_GOST(double m_nom, double l_bolta, char shag_rezby = 'k')
        {
            string parameters = $"m = {m_nom}";

            string columns = $"m, t_{shag_rezby}, d, d1, s, k";

            DataTable dt1 = provider.get_rows("7796-70", "1", columns, parameters);

            d = double.Parse(dt1.Rows[0]["d"].ToString());
            t = double.Parse(dt1.Rows[0]["t_" + shag_rezby].ToString());
            d1 = double.Parse(dt1.Rows[0]["d1"].ToString());
            S = double.Parse(dt1.Rows[0]["s"].ToString());
            k = double.Parse(dt1.Rows[0]["k"].ToString());


            parameters = $"m = {m_nom}, l = {l_bolta}";

            columns = $"m, l, b";

            DataTable dt2 = provider.get_rows("7796-70", "2", columns, parameters);

            l = double.Parse(dt2.Rows[0]["l"].ToString());
            b = double.Parse(dt2.Rows[0]["b"].ToString());

            //радиуса скругления нет в ГОСТ по этому болту, но присуттвует на чертеже. беру его из ГОСТ 7817
            if (R == 0)
            {
                if (m_nom < 8) { R = 0.25; }
                else if (m_nom <= 10) { R = 0.4; }
                else if (m_nom <= 18) { R = 0.6; }
                else if (m_nom <= 24) { R = 0.8; }
                else if (m_nom <= 36) { R = 1; }
                else if (m_nom <= 42) { R = 1.2; }
                else { R = 1.5; }
            }

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
