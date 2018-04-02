using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace VKR_FlangeCoupling.Details
{
    class Mufta
    {
        public Stupica master = null;
        public Stupica slave = null;
        public Bolt_GOST_7796 bolt7796 = null;
        public Bolt_GOST_7817 bolt7817 = null;
        public Shayba_GOST_6402 shayba = null;
        public Gayka_GOST_15521 gayka = null;

        GOST_Provider provider = null;

        public Mufta()
        {
            master = new Stupica();
            slave = new Stupica();
            bolt7796 = new Bolt_GOST_7796();
            bolt7817 = new Bolt_GOST_7817();
            shayba = new Shayba_GOST_6402();
            gayka = new Gayka_GOST_15521();


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
        /// Выбор муфты из ГОСТ
        /// </summary>
        public void Create_by_GOST(double T, double Diam1, double Diam2, int isp_v1, int isp_v2)
        {
            master.Get_from_GOST(Diam1, T, isp_v1);
            master.Calculate_Params_of_Holes(isp_v1);

            slave.Get_from_GOST(Diam2, T, isp_v2);
            slave.Calculate_Params_of_Holes(isp_v2);
            
            

            DataTable dt_20761_a2 = provider.get_rows("20761-96", "a2", "*", $"m_kr = {T.ToString("#.##")}");

            //парсим только длину болтов и номинальный диаметр, т.к. остальные штуки не меняются

            int M = int.Parse(new Regex(@"M(\d+)").Match(dt_20761_a2.Rows[0]["bolt_7796_obozn"].ToString()).Value.Replace("M", ""));
            int L_Bolta = int.Parse(new Regex(@"g-(\d+)").Match(dt_20761_a2.Rows[0]["bolt_7796_obozn"].ToString()).Value.Replace("g-", ""));

            bolt7796.Get_from_GOST(M, L_Bolta, 'k');

            bolt7817.Get_from_GOST(M, L_Bolta, 'k');

            shayba.Get_from_GOST(M, "Л");

            gayka.Get_from_GOST(M, 'k');

        }

        /// <summary>
        /// Расчет парметров муфты
        /// </summary>
        public void Calculate_Param_of_Coupling(double T, double w, double Diam1, double Diam2, double l_1, double l_2, int ispolnenie_v1, int ispolnenie_v2)
        {

            // ведущая муфта
            master.Calculate_Param_of_Stupica(Diam1, T, l_1, ispolnenie_v1);
            master.Calculate_Params_of_Holes(1);

           var V = master.D_external / 2 / 1000 * w; //R в метрах
            if (V > 70)
            {
                throw new Exception(
                    "Окружная скорость на наружном диаметре муфты более 70 м/с. Использование фланцевой муфты в данном соединении невозможно.");
            }

            //ведомая муфта
            slave.Calculate_Param_of_Stupica(Diam2, T, l_2, ispolnenie_v2);
            if (Diam1 != Diam2)
            {
                slave.D_okr = master.D_okr;
                slave.D_external = master.D_external;
                slave.n = master.n;
                slave.n3 = master.n3;
                slave.d3 = master.d3;
                slave.d4 = master.d4;
                slave.d2 = master.d2;
                slave.R1 = master.R1;
                slave.b2 = master.b2;
                slave.D_external = master.D_external;
            }

            //расчет положения отверстий
            slave.Calculate_Params_of_Holes(2);


            //расчет длины болтов
            int M = (int)Math.Floor(master.d4 - 1);

            shayba.Get_from_GOST(M, "Л");

            gayka.Get_from_GOST(M, 'k');

            double L = 0;
            L = master.l1 + slave.l1 + gayka.m + shayba.s + 10;
           
            // до 75 - шаг 5. после 80 шаг 10
            // но выбирать лучше из тех что есть в базе... сделать выборку

            var dt1 = provider.get_rows("7817-80", "2", "min(l)", $"m={gayka.d}, l >= {L.ToString().Replace(",",".")}, l2 <= {(master.l1 + slave.l1).ToString().Replace(",", ".")}");

            try
            {
                L = double.Parse(dt1.Rows[0]["min(l)"].ToString());
            }
            catch (Exception)
            {
                throw new Exception("В ГОСТе нет болтов для такой толщины пакета");
            }
            
            // ЗАКОМЕНТИТЬ КОГДА ДОБАВЛЮ ВСЕ БОЛТЫ
            /*if (M == 8) L = 40;
            else if (M == 10) L = 50;
            else if (M == 12) L = 70;
            else if (M == 16) L = 80;
            else if (M == 20) L = 100;
            else if (M == 24) L = 120;*/


            bolt7796.Get_from_GOST(M, L, 'k');

            bolt7817.Get_from_GOST(M, L, 'k');




        }



        double my_celling(double x, double step)
        {
            return Math.Ceiling(x * (1 / step)) / (1 / step);
        }
    }
}
