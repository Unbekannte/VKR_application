using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VKR_FlangeCoupling.Details
{
    class Shayba_GOST_6402 : Shayba
    {
        GOST_Provider provider = null;
        public Shayba_GOST_6402()
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
        /// Выбрать шайбу из ГОСТа
        /// </summary>
        /// <param name="m">Номинальный диаметр резьбы</param>
        /// <param name="type">Тип шайбы: Л-легкие Н-нормальные Т-тяжелые ОТ-особо тяжелые</param>
        public void Get_from_GOST(double m_nom, string type)
        {
            string parameters = $"m = {m_nom}";

            switch (type)
            {
                case "Л":
                    type = "l";
                    break;
                case "Н":
                    type = "n";
                    break;
                case "Т":
                    type = "t";
                    break;
                case "ОТ":
                    type = "ot";
                    break;
            }

            string columns = $"m, d, {type}_b, {type}_s";

            DataTable dt = provider.get_rows("6402-70", "1", columns, parameters);

            

            m = double.Parse(dt.Rows[0]["m"].ToString());
            d = double.Parse(dt.Rows[0]["d"].ToString());
            b = double.Parse(dt.Rows[0][type + "_b"].ToString());
            s = double.Parse(dt.Rows[0][type + "_s"].ToString());



        }
    }
}
